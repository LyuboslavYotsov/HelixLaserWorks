using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Order;
using HelixLaserWorks.Core.Services;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Configuration;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HelixLaserWorks.Tests.ServicesTests
{
    [TestFixture]
    public class OrderService_Should
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;

        private ApplicationDbContext _dbContext;

        private IOrderService _orderService;

        private IdentityUser TestUser;

        private IdentityUser NewUser = null!;

        private Part ElevatorPart;

        private Part CarPart;

        private Part CustomPart;

        private Part OriginalPart;

        private Order TestOrder;

        private Order NewUserCompletedOrder = null!;

        private Order NewUserInReviewOrder = null!;

        [SetUp]
        public async Task SetUp()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HelixLaserWorksInMemoryDbForOrderTesting")
            .Options;
            _dbContext = new ApplicationDbContext(_contextOptions);
            var seedData = new SeedData();

            await _dbContext.Thicknesses.AddRangeAsync(seedData.Thicknesses);
            await _dbContext.MaterialTypes.AddRangeAsync(seedData.MetalType, seedData.PlasticType, seedData.WoodType);
            await _dbContext.Materials.AddRangeAsync(seedData.MildSteel, seedData.StainlessSteel, seedData.Copper, seedData.Aluminum, seedData.ChipWood);
            await _dbContext.MaterialsThicknesses.AddRangeAsync(seedData.MildSteelThichnesses);
            await _dbContext.MaterialsThicknesses.AddRangeAsync(seedData.StainlessSteelThicknesses);
            await _dbContext.MaterialsThicknesses.AddRangeAsync(seedData.AluminumThicknesses);
            await _dbContext.MaterialsThicknesses.AddRangeAsync(seedData.CopperThicknesses);
            await _dbContext.MaterialsThicknesses.AddRangeAsync(seedData.ChipwoodThicknesses);

            TestUser = new IdentityUser()
            {
                Id = "2c2e7178-6349-4801-78fs-426de93ab2c7",
                UserName = "test@mail.com",
                NormalizedUserName = "TEST@MAIL.COM",
                Email = "test@mail.com",
                NormalizedEmail = "TEST.COM",
            };

            await _dbContext.Users.AddAsync(TestUser);

            ElevatorPart = new Part()
            {
                Id = 1,
                Name = "EvevatorPart",
                Description = "PartForElevatorsDescription",
                MaterialId = 1,
                Quantity = 20,
                SchemeURL = "TestPartName.pdf",
                Thickness = 1,
                CreatorId = TestUser.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                OrderId = 1,
            };

            CarPart = new Part()
            {
                Id = 2,
                Name = "CarPart",
                Description = "PartForCarsDescription",
                MaterialId = 2,
                Quantity = 50,
                SchemeURL = "TestPartName.pdf",
                Thickness = 5,
                CreatorId = TestUser.Id,
                CreatedOn = DateTime.Now.AddDays(1),
                UpdatedOn = DateTime.Now.AddDays(1),
                OrderId = 1,
            };

            CustomPart = new Part()
            {
                Id = 3,
                Name = "CustomPart",
                Description = "CustomPartDescription",
                MaterialId = 2,
                Quantity = 50,
                SchemeURL = "TestPartName.pdf",
                Thickness = 5,
                CreatorId = TestUser.Id,
                CreatedOn = DateTime.Now.AddDays(2),
                UpdatedOn = DateTime.Now.AddDays(2)
            };

            OriginalPart = new Part()
            {
                Id = 4,
                Name = "OriginalPart",
                Description = "OriginalPartDescription",
                MaterialId = 4,
                Quantity = 100,
                SchemeURL = "TestPartName.pdf",
                Thickness = 2,
                CreatorId = TestUser.Id,
                CreatedOn = DateTime.Now.AddDays(3),
                UpdatedOn = DateTime.Now.AddDays(3)
            };

            await _dbContext.Parts.AddRangeAsync(ElevatorPart, CarPart, CustomPart, OriginalPart);

            TestOrder = new Order()
            {
                Id = 1,
                CustomerId = TestUser.Id,
                CreatedOn = DateTime.Now,
                Description = "TestOrderDescription",
                OfferId = null,
                CustomerPhoneNumber = "0874569854",
                Status = OrderStatus.Pending,
                Title = "TestOrderTitle",
                Parts = new List<Part>() { CarPart, ElevatorPart }
            };

            await _dbContext.Orders.AddAsync(TestOrder);

            await _dbContext.SaveChangesAsync();

            _orderService = new OrderService(_dbContext);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.DisposeAsync();
        }

        [Test]//.CancelOrderAsync()
        public async Task RemoveOrderAndSetPartsOrderIdToNullWhenOrderIsCanceled()
        {
            int orderId = 1;

            var order = await _dbContext.Orders.FindAsync(orderId);
            var orderParts = await _dbContext.Parts.Where(p => p.OrderId == orderId).ToListAsync();

            Assert.IsNotNull(order);

            await _orderService.CancelOrderAsync(orderId);

            Assert.IsFalse(await _dbContext.Orders.AnyAsync());

            foreach (var part in orderParts)
            {
                Assert.IsNull(part.OrderId);
            }
        }

        [Test] //.CreateOrderAsync()
        public async Task CreateNewOrderSuccesfullyWithValidDataAndAttachSelectedPartsToNewOrder()
        {
            string testUserId = TestUser.Id;
            List<int> orderSelectedPartsIds = new List<int>() { CustomPart.Id, OriginalPart.Id };

            OrderFormModel orderModel = new OrderFormModel()
            {
                Title = "newOrderTitle",
                Description = "newOrderDescription",
                CustomerPhoneNumber = "0871233214",
                SelectedParts = orderSelectedPartsIds
            };

            Assert.IsNull(CustomPart.OrderId);
            Assert.IsNull(OriginalPart.OrderId);

            await _orderService.CreateOrderAsync(testUserId, orderModel);

            var newOrder = await _dbContext.Orders.Where(o => o.Title == orderModel.Title).FirstOrDefaultAsync();

            Assert.IsNotNull(newOrder);
            Assert.IsNotNull(newOrder.Parts);
            Assert.IsTrue(newOrder.Status == OrderStatus.Pending);
            Assert.IsTrue(await _dbContext.Orders.CountAsync() == 2);

            foreach (var part in newOrder.Parts)
            {
                Assert.IsTrue(part.OrderId == newOrder.Id);
            }
        }

        [Test] //.DeclindeOrder()
        public async Task ChangeOrderStatusAndAttachAdminFeedbackWhenOrderIsDeclined()
        {
            var orderToDecline = await _dbContext.Orders.FindAsync(TestOrder.Id);

            OrderDeclineViewModel declineModel = new OrderDeclineViewModel()
            {
                Id = TestOrder.Id,
                UserEmail = TestUser.Email,
                Title = TestOrder.Title,
                Feedback = "BadOrder"
            };

            Assert.IsNotNull(orderToDecline);
            Assert.IsNull(orderToDecline.AdminFeedback);
            Assert.IsTrue(orderToDecline.Status == OrderStatus.Pending);

            await _orderService.DeclineOrder(TestOrder.Id, declineModel);

            Assert.IsNotNull(orderToDecline);
            Assert.NotNull(orderToDecline.AdminFeedback);
            Assert.IsTrue(orderToDecline.AdminFeedback == declineModel.Feedback);
            Assert.IsTrue(orderToDecline.Status == OrderStatus.DeclinedByAdmin);
        }

        [Test] //.GetOrderForDeclineAsync()
        public async Task ReturnCorrectModelForDeclineForGivenOrderId()
        {
            var order = await _dbContext.Orders.FindAsync(TestOrder.Id);

            var declineModel = await _orderService.GetOrderForDeclineAsync(TestOrder.Id);

            Assert.IsNotNull(order);
            Assert.IsNotNull(declineModel);
            Assert.That(order.Id, Is.EqualTo(declineModel.Id));
            Assert.That(order.Title, Is.EqualTo(declineModel.Title));
            Assert.That(order.Customer.Email, Is.EqualTo(declineModel.UserEmail));
        }

        [Test] //.GetOrderModelForOfferAsync()
        public async Task ReturnCorrectOrderModelWithPartsInformationForGivenOrderId()
        {
            var orderFromDb = await _dbContext.Orders.FindAsync(TestOrder.Id);

            var orderModelFromMethod = await _orderService.GetOrderModelForOfferAsync(TestOrder.Id);

            Assert.IsNotNull(orderFromDb);
            Assert.IsNotNull(orderModelFromMethod);

            Assert.That(orderModelFromMethod.Id == orderFromDb.Id);
            Assert.That(orderModelFromMethod.Title == orderFromDb.Title);
            Assert.That(orderModelFromMethod.Description == orderFromDb.Description);
            Assert.That(orderModelFromMethod.AdminFeedback == orderFromDb.AdminFeedback);
            Assert.That(orderModelFromMethod.CreatedOn == orderFromDb.CreatedOn.ToString("MM/dd/yy HH:mm", CultureInfo.InvariantCulture));
            Assert.That(orderModelFromMethod.Status == orderFromDb.Status.ToString());
            Assert.That(orderModelFromMethod.CustomerPhoneNumber == orderFromDb.CustomerPhoneNumber);
            Assert.That(orderModelFromMethod.Parts.Count() == orderFromDb.Parts.Count());

            foreach (var modelPart in orderModelFromMethod.Parts)
            {
                Assert.IsTrue(orderFromDb.Parts.Any(p => p.Id == modelPart.Id));
            }
        }

        [Test] //.GetOrderStatusAsync()
        public async Task ReturnCorrectOrderStatusForGivenOrderId()
        {
            var expectedStatus = OrderStatus.Pending;

            var statusFromMethod = await _orderService.GetOrderStatusAsync(TestOrder.Id);

            Assert.That(statusFromMethod, Is.EqualTo(expectedStatus));

            var order = await _dbContext.Orders.FindAsync(TestOrder.Id);

            Assert.IsNotNull(order);

            order.Status = OrderStatus.Completed;
            await _dbContext.SaveChangesAsync();

            expectedStatus = OrderStatus.Completed;

            statusFromMethod = await _orderService.GetOrderStatusAsync(TestOrder.Id);

            Assert.That(statusFromMethod, Is.EqualTo(expectedStatus));
        }

        [Test] //.HasAnOfferAsync()
        public async Task ReturnTrueIfGivenOrderHasAnOffer()
        {
            var order = await _dbContext.Orders.FindAsync(TestOrder.Id);

            Assert.IsNotNull(order);

            order.OfferId = 1;
            await _dbContext.SaveChangesAsync();

            Assert.IsTrue(await _orderService.HasAnOfferAsync(TestOrder.Id));
        }

        [Test] //.HasAnOfferAsync()
        public async Task ReturnFalseIfGivenOrderOfferIdIsNull()
        {
            var order = await _dbContext.Orders.FindAsync(TestOrder.Id);

            Assert.IsNotNull(order);

            Assert.False(await _orderService.HasAnOfferAsync(TestOrder.Id));
        }

        [Test]//.MarkAsReviewdAsync()
        public async Task ChangeOrderStatusToInReviewForGivenOrder()
        {
            var order = await _dbContext.Orders.FindAsync(TestOrder.Id);

            Assert.IsNotNull(order);

            Assert.That(order.Status != OrderStatus.InReview);

            await _orderService.MarkAsReviewdAsync(order.Id);

            Assert.That(order.Status == OrderStatus.InReview);
        }

        [Test]//.OrderExistsAsync()
        public async Task ReturnTrueOnlyIfGivenOrderExists()
        {
            int existingOrderId = TestOrder.Id;
            int nonExistingOrderId = 123;

            Assert.IsTrue(await _orderService.OrderExistAsync(existingOrderId));
            Assert.IsFalse(await _orderService.OrderExistAsync(nonExistingOrderId));
        }

        [Test]//.UserIsCreatorAsync()
        public async Task ReturnTrueIfGivenUserIsCreatorOfGivenOrder()
        {
            string correctUserId = TestUser.Id;
            string anotherUserId = "fakeUserId";
            int orderId = TestOrder.Id;

            Assert.IsTrue(await _orderService.UserIsCreatorAsync(correctUserId, orderId));
            Assert.IsFalse(await _orderService.UserIsCreatorAsync(anotherUserId, orderId));
        }

        [Test]//.AllAsync()
        public async Task ReturnAllOrdersWhenUserIdIsNullAndNoFilterIsAdded()
        {
            await AddAnotherUserWithOrderInDb();

            int allOrdersCount = await _dbContext.Orders.CountAsync();

            var ordersFromMethod = await _orderService.GetAllAsync(
                null,
                null,
                null,
                1,
                4);

            Assert.IsNotNull(ordersFromMethod);
            Assert.That(ordersFromMethod.Orders.Count(), Is.EqualTo(allOrdersCount));
        }

        [Test]//.AllAsync() with UserId
        public async Task ReturnAllOrdersForGivenUserWhenUserIdIsNotNullAndNoFilterIsAdded()
        {
            await AddAnotherUserWithOrderInDb();

            int newUserOrdersCount = await _dbContext.Orders.Where(o => o.CustomerId == NewUser.Id).CountAsync();

            var ordersFromMethod = await _orderService.GetAllAsync(
                NewUser.Id,
                null,
                null,
                1,
                4);

            Assert.IsNotNull(ordersFromMethod);
            Assert.That(ordersFromMethod.Orders.Count(), Is.EqualTo(newUserOrdersCount));

            foreach (var order in ordersFromMethod.Orders)
            {
                Assert.That(order.CustomerEmail == NewUser.Email);
            }
        }

        [Test]//.AllAsync() with Status Filter
        public async Task ReturnOnlyOrdersWithSelectedStatus()
        {
            await AddAnotherUserWithOrderInDb();

            var status = OrderStatus.Completed;

            int completedOrdersCount = await _dbContext.Orders.Where(o => o.Status == OrderStatus.Completed).CountAsync();

            var ordersFromMethod = await _orderService.GetAllAsync(
                null,
                null,
                status,
                1,
                4);

            Assert.IsNotNull(ordersFromMethod);
            Assert.That(ordersFromMethod.Orders.Count(), Is.EqualTo(completedOrdersCount));
            foreach (var order in ordersFromMethod.Orders)
            {
                Assert.That(order.Status == status.ToString());
            }
        }

        [Test]//.AllAsync() with Search Term
        public async Task ReturnOnlyOrdersMatchingGivenSearchTerm()
        {
            await AddAnotherUserWithOrderInDb();

            string searchTerm = "inreview";

            int matchingOrdersCount = 1;

            var ordersFromMethod = await _orderService.GetAllAsync(
                null,
                searchTerm,
                null,
                1,
                4);

            Assert.IsNotNull(ordersFromMethod);
            Assert.That(ordersFromMethod.Orders.Count(), Is.EqualTo(matchingOrdersCount));
        }

        private async Task AddAnotherUserWithOrderInDb()
        {
            NewUser = new IdentityUser()
            {
                Id = "2d3e7178-6349-4801-78fs-426de93ab2c7",
                UserName = "newUser@mail.com",
                NormalizedUserName = "NEWUSER@MAIL.COM",
                Email = "newUser@mail.com",
                NormalizedEmail = "NEWUSER@MAIL.COM",
            };

            await _dbContext.Users.AddAsync(NewUser);
            await _dbContext.SaveChangesAsync();

            NewUserCompletedOrder = new Order()
            {
                Id = 2,
                CustomerId = NewUser.Id,
                CreatedOn = DateTime.Now.AddDays(1),
                Description = "NewOrderDescription",
                OfferId = 2,
                CustomerPhoneNumber = "0874569854",
                Status = OrderStatus.Completed,
                Title = "NewCompletedOrderTitle",
                Parts = new List<Part>() { OriginalPart }
            };

            NewUserInReviewOrder = new Order()
            {
                Id = 3,
                CustomerId = NewUser.Id,
                CreatedOn = DateTime.Now.AddDays(2),
                Description = "NewOrderDescription",
                OfferId = null,
                CustomerPhoneNumber = "0874564554",
                Status = OrderStatus.InReview,
                Title = "NewInReviewOrderTitle",
                Parts = new List<Part>() { CustomPart }
            };

            await _dbContext.AddRangeAsync(NewUserCompletedOrder, NewUserInReviewOrder);
            await _dbContext.SaveChangesAsync();
        }
    }
}

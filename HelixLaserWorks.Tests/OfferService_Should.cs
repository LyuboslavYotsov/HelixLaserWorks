using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Services;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using HelixLaserWorks.Core.Models.Offer;

namespace HelixLaserWorks.Tests
{
    [TestFixture]
    public class OfferService_Should
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;

        private ApplicationDbContext _dbContext;

        private IOfferService _offerService;

        private IdentityUser TestUser;

        private IdentityUser NewUser;

        private Part TestPart;

        private Order TestOrder;

        private Offer TestOffer;

        [SetUp]
        public async Task SetUp()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HelixLaserWorksInMemoryOfferServiceServiceTestDb")
            .Options;
            _dbContext = new ApplicationDbContext(_contextOptions);

            var seedData = new SeedData();

            await _dbContext.Thicknesses.AddRangeAsync(seedData.Thicknesses);
            await _dbContext.Materials.AddAsync(seedData.MildSteel);
            await _dbContext.MaterialsThicknesses.AddRangeAsync(seedData.MildSteelThichnesses);

            TestUser = new IdentityUser()
            {
                Id = "2c2e7178-6349-4801-78fs-426de93ab2c7",
                UserName = "test@mail.com",
                NormalizedUserName = "TEST@MAIL.COM",
                Email = "test@mail.com",
                NormalizedEmail = "TEST.COM",
            };


            NewUser = new IdentityUser()
            {
                Id = "2c2e1254-6349-4801-78fs-426de93ab2c7",
                UserName = "newUser@mail.com",
                NormalizedUserName = "NEWUSER@MAIL.COM",
                Email = "newUser@mail.com",
                NormalizedEmail = "NEWUSER@MAIL.COM",
            };

            TestPart = new Part()
            {
                Id = 1,
                Name = "TestPartName",
                Description = "TestPartDescription",
                MaterialId = 1,
                Quantity = 20,
                SchemeURL = "TestPartName.pdf",
                Thickness = 1,
                CreatorId = TestUser.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                OrderId = 1,
            };

            TestOrder = new Order()
            {
                Id = 1,
                CustomerId = TestUser.Id,
                CreatedOn = DateTime.Now,
                CustomerPhoneNumber = "0874562314",
                Description = "TestDescription",
                OfferId = 1,
                Status = OrderStatus.ReadyWithOffer,
                Title = "TestOrderTitle"
            };

            TestOffer = new Offer()
            {
                Id = 1,
                CreatedOn = DateTime.Now,
                IsAccepted = false,
                IsCustomerContacted = false,
                Notes = "NotContactedNotAccepted",
                OrderId = TestOrder.Id,
                Price = 1200m,
                ProductionDays = 7,
            };

            await _dbContext.Users.AddRangeAsync(TestUser, NewUser);
            await _dbContext.Orders.AddAsync(TestOrder);
            await _dbContext.Parts.AddAsync(TestPart);
            await _dbContext.Offers.AddAsync(TestOffer);
            await _dbContext.SaveChangesAsync();

            _offerService = new OfferService(_dbContext);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.DisposeAsync();
        }

        [Test]//.AcceptOfferAsync()
        public async Task SuccesfullyMarkOfferAsAcceptedAndChangeAttachedOrderStatusToCompleted()
        {
            var offerFromDb = await _dbContext.Offers.FindAsync(1);

            Assert.IsNotNull(offerFromDb);
            Assert.IsFalse(offerFromDb.IsAccepted);
            Assert.That(offerFromDb.Order.Status, Is.EqualTo(OrderStatus.ReadyWithOffer));

            await _offerService.AcceptOfferAsync(TestOffer.Id);

            Assert.IsTrue(offerFromDb.IsAccepted);
            Assert.That(offerFromDb.Order.Status, Is.EqualTo(OrderStatus.Completed));
        }

        [Test]//.CreateAsync()
        public async Task SuccesfullyCreateNewOfferWithValidData()
        {
            var newOrder = new Order()
            {
                Id = 2,
                CustomerId = TestUser.Id,
                CreatedOn = DateTime.Now,
                CustomerPhoneNumber = "0874562314",
                Description = "TestDescription",
                OfferId = null,
                Status = OrderStatus.Pending,
                Title = "TestOrderTitle",
            };

            await _dbContext.Orders.AddAsync(newOrder);
            await _dbContext.SaveChangesAsync();

            var offerModel = new OfferFormModel()
            {
                Notes = "SomeNotes",
                OrderId = 2,
                Price = 130m,
                ProductionDays = 10,
            };

            int countBeforeCreation = await _dbContext.Offers.CountAsync();

            await _offerService.CreateAsync(offerModel);

            Assert.IsTrue(await _dbContext.Offers.CountAsync() > countBeforeCreation);

            var corespondingOrder = await _dbContext.Orders.FindAsync(newOrder.Id);

            Assert.IsNotNull(corespondingOrder);

            Assert.IsTrue(corespondingOrder.Status == OrderStatus.ReadyWithOffer);
        }

        [Test]
        public async Task ReturnCorrectDetailsModelForGivenOfferId()
        {
            var offerDetailedModel = await _offerService.GetOfferDetailsAsync(TestOffer.Id);

            var offerFromDb = await _dbContext.Offers.FindAsync(TestOffer.Id);

            Assert.IsNotNull(offerDetailedModel);

            Assert.That(TestOffer.Id, Is.EqualTo(offerDetailedModel.Id));
            Assert.That(TestOffer.Price, Is.EqualTo(offerDetailedModel.Price));

            Assert.That(TestOrder.Id, Is.EqualTo(offerDetailedModel.Order.Id));
            Assert.That(TestOrder.Title, Is.EqualTo(offerDetailedModel.Order.Title));
            Assert.That(TestOrder.Status.ToString(), Is.EqualTo(offerDetailedModel.Order.Status));

            Assert.IsTrue(offerDetailedModel.Order.Parts.Count() == 1);
            Assert.IsTrue(offerDetailedModel.Order.Parts.First().Id == TestPart.Id);
            Assert.IsTrue(offerDetailedModel.Order.Parts.First().Name == TestPart.Name);
        }

        [Test]
        public async Task ReturnOnlyOffersForGivenUserId()
        {
            var testUserSecondOrder = new Order()
            {
                Id = 2,
                CustomerId = TestUser.Id,
                CreatedOn = DateTime.Now,
                Description = "TestDescription",
                Title = "TestUserSecondOrderTitle",
                OfferId = 2,
                Status = OrderStatus.InReview
            };

            var testUserSecondOffer = new Offer()
            {
                Id = 2,
                CreatedOn = DateTime.Now,
                OrderId = 2,
                Price = 130m,
                ProductionDays = 6,
                Notes = "SomeNotes"
            };

            var newUserOrder = new Order()
            {
                Id = 3,
                CustomerId = NewUser.Id,
                CreatedOn = DateTime.Now,
                Description = "TestDescription",
                Title = "TestUserSecondOrderTitle",
                OfferId = 3,
                Status = OrderStatus.InReview
            };

            var newUserOffer = new Offer()
            {
                Id = 3,
                CreatedOn = DateTime.Now,
                OrderId = 3,
                Price = 1300m,
                ProductionDays = 16,
                Notes = "SomeNotes"
            };

            await _dbContext.Orders.AddRangeAsync(testUserSecondOrder,newUserOrder);
            await _dbContext.Offers.AddRangeAsync(testUserSecondOffer,newUserOffer);
            await _dbContext.SaveChangesAsync();

            var testUserOffersFromMethod = await _offerService.GetUserOffersAsync(TestUser.Id);
            var newUserOffersFromMethod = await _offerService.GetUserOffersAsync(NewUser.Id);


            Assert.NotNull(testUserOffersFromMethod);
            Assert.NotNull(newUserOffersFromMethod);
            Assert.IsTrue(testUserOffersFromMethod.Count() == 2);
            Assert.IsTrue(newUserOffersFromMethod.Count() == 1);
        }

        [Test]
        public async Task ReturnTrueForExistingOffer()
        {
            Assert.IsTrue(await _offerService.OfferExistAsync(TestOffer.Id));
        }

        [Test]
        public async Task ReturnFalseForNONExistingOffer()
        {
            int nonExistingOfferId = 111;

            Assert.IsFalse(await _offerService.OfferExistAsync(nonExistingOfferId));
        }

        [Test]
        public async Task ReturnTrueIfGivenOfferIsAccepted()
        {
            var offer = await _dbContext.Offers.FindAsync(TestOffer.Id);

            Assert.NotNull(offer);
            offer.IsAccepted = true;

            Assert.IsTrue(await _offerService.OfferIsAcceptedAsync(TestOffer.Id));
        }

        [Test]
        public async Task ReturnFalseIfGivenOfferIsNOTAccepted()
        {
            Assert.IsFalse(await _offerService.OfferIsAcceptedAsync(TestOffer.Id));
        }

        [Test]
        public async Task ReturnTrueIfGivenUserIsCreatorOfOrderAttachedToGivenOffer()
        {
            Assert.IsTrue(await _offerService.UserIsOrderCreatorAsync(TestOffer.Id, TestUser.Id));
        }

        [Test]
        public async Task ReturnFalseIfGivenUserIsNOTCreatorOfOrderAttachedToGivenOffer()
        {
            Assert.IsFalse(await _offerService.UserIsOrderCreatorAsync(TestOffer.Id, NewUser.Id));
        }

        [Test]
        public async Task ReturnOnlyAcceptedOffersWhereUserIsContactedIsFalse()
        {
            var testUserSecondOrder = new Order()
            {
                Id = 2,
                CustomerId = TestUser.Id,
                CreatedOn = DateTime.Now,
                Description = "TestDescription",
                Title = "TestUserSecondOrderTitle",
                OfferId = 2,
                Status = OrderStatus.InReview
            };

            var testUserSecondOffer = new Offer()
            {
                Id = 2,
                CreatedOn = DateTime.Now,
                OrderId = 2,
                Price = 130m,
                ProductionDays = 6,
                Notes = "OnlyAccepted",
                IsAccepted = true,
            };

            var testUserThirdOrder = new Order()
            {
                Id = 3,
                CustomerId = NewUser.Id,
                CreatedOn = DateTime.Now,
                Description = "TestDescription",
                Title = "TestUserSecondOrderTitle",
                OfferId = 3,
                Status = OrderStatus.InReview
            };

            var testUserThirdOffer = new Offer()
            {
                Id = 3,
                CreatedOn = DateTime.Now,
                OrderId = 3,
                Price = 1300m,
                ProductionDays = 16,
                Notes = "OnlyAccepted",
                IsAccepted = true,
            };

            await _dbContext.Orders.AddRangeAsync(testUserSecondOrder, testUserThirdOrder);
            await _dbContext.Offers.AddRangeAsync(testUserSecondOffer, testUserThirdOffer);
            await _dbContext.SaveChangesAsync();

            var offerWaitingForContact = await _offerService.GetOffersWaitingForContactAsync();

            Assert.NotNull(offerWaitingForContact);
            Assert.IsTrue(offerWaitingForContact.Count() == 2);
            Assert.IsTrue(offerWaitingForContact.Count() < await _dbContext.Offers.CountAsync());
        }

        [Test]
        public async Task SuccesfullyChangeOfferIsCustomerContactedToTrue()
        {
            var testUserSecondOrder = new Order()
            {
                Id = 2,
                CustomerId = TestUser.Id,
                CreatedOn = DateTime.Now,
                Description = "TestDescription",
                Title = "TestUserSecondOrderTitle",
                OfferId = 2,
                Status = OrderStatus.InReview
            };

            var testUserSecondOffer = new Offer()
            {
                Id = 2,
                CreatedOn = DateTime.Now,
                OrderId = 2,
                Price = 130m,
                ProductionDays = 6,
                Notes = "OnlyAccepted",
                IsAccepted = true,
            };

            await _dbContext.Orders.AddAsync(testUserSecondOrder);
            await _dbContext.Offers.AddAsync(testUserSecondOffer);
            await _dbContext.SaveChangesAsync();

            var offerForTest = await _dbContext.Offers.FindAsync(testUserSecondOffer.Id);

            Assert.IsNotNull(offerForTest);
            Assert.IsFalse(offerForTest.IsCustomerContacted);

            await _offerService.ContactAchievedAsync(offerForTest.Id);

            Assert.IsTrue(offerForTest.IsCustomerContacted);
        }
    }
}

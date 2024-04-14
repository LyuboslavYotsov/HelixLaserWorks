using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Review;
using HelixLaserWorks.Core.Services;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using HelixLaserWorks.Infrastructure.Data.Models.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HelixLaserWorks.Tests.ServicesTests
{
    [TestFixture]
    public class ReviewService_Should
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;

        private ApplicationDbContext _dbContext;

        private IReviewService _reviewService;

        private IdentityUser TestUser;

        private IdentityUser NewUser;

        private Review TestUserReview;

        private Review NewUserReview;

        [SetUp]
        public async Task SetUp()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HelixLaserWorksInMemoryReviewServiceTestDb")
            .Options;
            _dbContext = new ApplicationDbContext(_contextOptions);

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

            NewUserReview = new Review()
            {
                Id = 1,
                Comment = "newUser",
                CreatedOn = DateTime.Now.AddDays(1),
                CustomerId = NewUser.Id,
                Rating = 1
            };

            TestUserReview = new Review()
            {
                Id = 2,
                Comment = "testUser",
                CreatedOn = DateTime.Now,
                CustomerId = TestUser.Id,
                Rating = 4
            };

            await _dbContext.Users.AddRangeAsync(TestUser, NewUser);
            await _dbContext.Reviews.AddRangeAsync(TestUserReview, NewUserReview);
            await _dbContext.SaveChangesAsync();

            _reviewService = new ReviewService(_dbContext);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.DisposeAsync();
        }

        [Test]//.CreateReviewAsync()
        public async Task SuccesfullyCreateNewReviewWithValidData()
        {
            int countBeforeCreating = await _dbContext.Reviews.CountAsync();

            var reviewModel = new ReviewFormModel()
            {
                Comment = "TestReviewComment",
                Rating = 3
            };

            await _reviewService.CreateReviewAsync(reviewModel, TestUser.Id);

            int countAfterCreating = await _dbContext.Reviews.CountAsync();

            Assert.That(countAfterCreating > countBeforeCreating);
            Assert.IsTrue(await _dbContext.Reviews.AnyAsync(r => r.Comment == reviewModel.Comment && r.Rating == reviewModel.Rating));
        }

        [Test]//.GetReviewsAsync()
        public async Task ReturnAllReviewsFromDbCorrect()
        {
            int expectedCount = await _dbContext.Reviews.CountAsync();

            var reviewsFromMethod = await _reviewService.GetReviewsAsync();

            Assert.IsNotNull(reviewsFromMethod);
            Assert.That(reviewsFromMethod.Count(), Is.EqualTo(expectedCount));

            foreach (var review in reviewsFromMethod)
            {
                Assert.IsNotNull(review.Id);
                Assert.IsNotNull(review.Comment);
                Assert.IsNotNull(review.Rating);
                Assert.IsNotNull(review.UserEmail);
                Assert.IsNotNull(review.CreatedOn);
            }
        }

        [Test]//.UserCanWriteReviewAsync()
        public async Task ReturnTrueIfCustomerHaveAtleasOneCompletedOrder()
        {
            var userWithCompletedOrder = new IdentityUser()
            {
                Id = "2c2e1254-6349-2222-78fs-426de93ab2c7",
                UserName = "validUser@mail.com",
                NormalizedUserName = "VALIDUSER@MAIL.COM",
                Email = "validUser@mail.com",
                NormalizedEmail = "VALIDUSER@MAIL.COM"
            };

            await _dbContext.Users.AddAsync(userWithCompletedOrder);
            await _dbContext.SaveChangesAsync();

            var completedOrder = new Order()
            {
                Id = 1,
                CustomerId = userWithCompletedOrder.Id,
                CreatedOn = DateTime.Now,
                Description = "TestOrderDescription",
                OfferId = null,
                CustomerPhoneNumber = "0874569854",
                Status = OrderStatus.Completed,
                Title = "TestOrderTitle"
            };

            await _dbContext.Orders.AddAsync(completedOrder);
            await _dbContext.SaveChangesAsync();

            Assert.IsTrue(await _reviewService.UserCanWriteReviewAsync(userWithCompletedOrder.Id));
        }

        [Test]//.UserCanWriteReviewAsync()
        public async Task ReturnFalseIfCustomerDoesNOTHaveAtleasOneCompletedOrder()
        {
            var userWithCompletedOrder = new IdentityUser()
            {
                Id = "2c2e1254-6349-2222-78fs-426de93ab2c7",
                UserName = "validUser@mail.com",
                NormalizedUserName = "VALIDUSER@MAIL.COM",
                Email = "validUser@mail.com",
                NormalizedEmail = "VALIDUSER@MAIL.COM"
            };

            await _dbContext.Users.AddAsync(userWithCompletedOrder);
            await _dbContext.SaveChangesAsync();

            var pendingOrder = new Order()
            {
                Id = 1,
                CustomerId = userWithCompletedOrder.Id,
                CreatedOn = DateTime.Now,
                Description = "TestOrderDescription",
                OfferId = null,
                CustomerPhoneNumber = "0874569854",
                Status = OrderStatus.Pending,
                Title = "TestOrderTitle"
            };

            await _dbContext.Orders.AddAsync(pendingOrder);
            await _dbContext.SaveChangesAsync();

            Assert.IsFalse(await _reviewService.UserCanWriteReviewAsync(userWithCompletedOrder.Id));
        }

        [Test]//.UserHasReviewAsync()
        public async Task ReturnTrueIfUserHasAnReview()
        {
            Assert.IsTrue(await _reviewService.UserHasReviewAsync(TestUser.Id));
            Assert.IsTrue(await _reviewService.UserHasReviewAsync(NewUser.Id));
        }

        [Test]//.UserHasReviewAsync()
        public async Task ReturnFalseIfUserDoesNOTHaveAnReview()
        {
            var userWithoutReview = new IdentityUser()
            {
                Id = "2c2e1254-6349-2222-78fs-426de93ab2c7",
                UserName = "validUser@mail.com",
                NormalizedUserName = "VALIDUSER@MAIL.COM",
                Email = "validUser@mail.com",
                NormalizedEmail = "VALIDUSER@MAIL.COM"
            };

            await _dbContext.Users.AddAsync(userWithoutReview);
            await _dbContext.SaveChangesAsync();

            Assert.IsFalse(await _reviewService.UserHasReviewAsync(userWithoutReview.Id));
        }
    }
}

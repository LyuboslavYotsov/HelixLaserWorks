using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Enumerations;
using HelixLaserWorks.Core.Models.Part;
using HelixLaserWorks.Core.Services;
using HelixLaserWorks.Infrastructure.Data;
using HelixLaserWorks.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HelixLaserWorks.Tests
{
    [TestFixture]
    public class PartService_Should
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;

        private ApplicationDbContext _dbContext;

        private IPartService _partService;

        private IdentityUser PartTestUser;

        private Part TestPart;

        private IFormFile fileMock;

        [SetUp]
        public async Task SetUp()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HelixLaserWorksInMemoryDbPartsTest")
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

            PartTestUser = new IdentityUser()
            {
                Id = "2c2e7178-6349-4801-78fs-426de93ab2c7",
                UserName = "test@mail.com",
                NormalizedUserName = "TEST@MAIL.COM",
                Email = "test@mail.com",
                NormalizedEmail = "TEST.COM",
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
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            await _dbContext.Users.AddAsync(PartTestUser);
            await _dbContext.Parts.AddAsync(TestPart);

            await _dbContext.SaveChangesAsync();

            var mockFileManageService = new Mock<IFileManageService>();

            fileMock = new Mock<IFormFile>().Object;


            mockFileManageService
                .Setup(m => m.UploadFile(fileMock, PartTestUser.Email))
                .ReturnsAsync("pathString.pdf");

            _partService = new PartService(_dbContext, mockFileManageService.Object);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.DisposeAsync();
        }

        [Test] //.CreateAsync()
        public async Task CreateNewPartWithValidData()
        {
            string userId = PartTestUser.Id;
            string userEmail = PartTestUser.Email;

            PartFormModel newPartModel = new PartFormModel()
            {
                MaterialId = 1,
                PartThickness = 1,
                Description = "NewTestPartDescription",
                Name = "NewTestPartName",
                Quantity = 100,
                SchemeFile = fileMock,
            };

            int partsCountBeforeCreate = await _dbContext.Parts.CountAsync();

            await _partService.CreateAsync(newPartModel, userId, userEmail);

            int partsCountAfterCreate = await _dbContext.Parts.CountAsync();

            Assert.IsTrue(partsCountAfterCreate > partsCountBeforeCreate);
            Assert.IsTrue(await _dbContext.Parts.AnyAsync(p => p.Name == newPartModel.Name));
            Assert.IsTrue(await _dbContext.Parts.AnyAsync(p => p.Description == newPartModel.Description));
            Assert.IsTrue(await _dbContext.Parts.AnyAsync(p => p.CreatorId == userId));
        }

        [Test] //.DeleteAsync()
        public async Task DeleteExistingPartSuccesfully()
        {
            int existingPartId = TestPart.Id;

            var countBeforeDelete = await _dbContext.Parts.CountAsync();

            await _partService.DeleteAsync(existingPartId);

            var countAfterDelete = await _dbContext.Parts.CountAsync();

            Assert.IsTrue(countBeforeDelete > countAfterDelete);
        }

        [Test] //.EditAsync()
        public async Task EditExistingPartSuccesfully()
        {
            int existingPartId = TestPart.Id;

            var editPartModel = new PartFormModel()
            {
                Name = "EditedPart",
                Description = "EditedDescription",
                SchemeFile = null,
                MaterialId = 2,
                Quantity = 999,
                PartThickness = 3
            };

            await _partService.EditAsync(editPartModel, existingPartId, PartTestUser.Email);

            var partAfterEdit = await _dbContext.Parts.FindAsync(existingPartId);

            Assert.IsNotNull(partAfterEdit);
            Assert.IsTrue(partAfterEdit.Name == editPartModel.Name);
            Assert.IsTrue(partAfterEdit.Description == editPartModel.Description);
            Assert.IsTrue(partAfterEdit.Quantity == editPartModel.Quantity);
            Assert.IsTrue(partAfterEdit.Thickness == editPartModel.PartThickness);
        }

        [Test] //.GetPartForEditAsync()
        public async Task ReturnCorrectPartModelForEdit()
        {
            var existingPart = await _dbContext.Parts.FindAsync(TestPart.Id);
            Assert.IsNotNull(existingPart);

            var partModelForEdit = await _partService.GetPartForEditAsync(existingPart.Id);

            Assert.IsNotNull(partModelForEdit);
            Assert.That(partModelForEdit.Name, Is.EqualTo(existingPart.Name));
            Assert.That(partModelForEdit.Description, Is.EqualTo(existingPart.Description));
            Assert.That(partModelForEdit.Quantity, Is.EqualTo(existingPart.Quantity));
            Assert.That(partModelForEdit.PartThickness, Is.EqualTo(existingPart.Thickness));
        }

        [Test] //.GetUserPartsForDropdownAsync()
        public async Task ReturnOnlyPartCreatedByGivenUser()
        {
            var newUser = new IdentityUser()
            {
                Id = "2c2e7178-6349-4584-78fs-426de93ab2c7",
                UserName = "newuser@mail.com",
                NormalizedUserName = "NEWUSER@MAIL.COM",
                Email = "newuser@mail.com",
                NormalizedEmail = "NEWUSER@MAIL.COM",
            };

            var newUserPartOne = new Part()
            {
                Id = 2,
                Name = "NeUserPartOne",
                Description = "NeUserPartOneDescription",
                MaterialId = 1,
                Quantity = 20,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 1,
                CreatorId = newUser.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            var newUserPartTwo = new Part()
            {
                Id = 3,
                Name = "NeUserPartTwo",
                Description = "NeUserPartTwoDescription",
                MaterialId = 1,
                Quantity = 20,
                SchemeURL = "NeUserPartTwoPartName.pdf",
                Thickness = 1,
                CreatorId = newUser.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.Parts.AddAsync(newUserPartOne);
            await _dbContext.Parts.AddAsync(newUserPartTwo);
            await _dbContext.SaveChangesAsync();

            var totalPartCount = await _dbContext.Parts.CountAsync();
            int newUserPartsCount = 2;

            var partsFromMethod = await _partService.GetUserPartsForDropdownAsync(newUser.Id);

            Assert.NotNull(partsFromMethod);
            Assert.That(partsFromMethod.Count < totalPartCount);
            Assert.That(partsFromMethod.Count(), Is.EqualTo(newUserPartsCount));
            foreach (var part in partsFromMethod)
            {
                Assert.NotNull(part.Name);
                Assert.NotNull(part.Id);
                Assert.NotNull(part.PartMaterial);
                Assert.NotNull(part.PartThickness);
            }
        }

        [Test] //.IsOrdered()
        public async Task ReturnFalseIfPartIsNotOrdered()
        {
            int notOrderedPartId = TestPart.Id;

            Assert.IsFalse(await _partService.IsOrdered(notOrderedPartId));
        }

        [Test] //.IsOrdered()
        public async Task ReturnTrueIfPartIsOrdered()
        {
            var orderedPart = new Part()
            {
                Id = 2,
                Name = "OrderedPart",
                Description = "OrderedPartDescription",
                MaterialId = 1,
                Quantity = 20,
                SchemeURL = "OrderedPartName.pdf",
                Thickness = 1,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                OrderId = 1
            };

            await _dbContext.Parts.AddAsync(orderedPart);
            await _dbContext.SaveChangesAsync();

            Assert.True(await _partService.IsOrdered(orderedPart.Id));
        }

        [Test] //.PartExistsAsync()
        public async Task ReturnTrueForExistingPart()
        {
            int existingPartid = TestPart.Id;

            Assert.IsTrue(await _partService.PartExistsAsync(existingPartid));
        }

        [Test] //.PartExistsAsync()
        public async Task ReturnFalseForNonExistingPart()
        {
            int nonExistingPartid = 500;

            Assert.IsFalse(await _partService.PartExistsAsync(nonExistingPartid));
        }

        [Test] //.UserIsCreatorAsync()
        public async Task ReturnTrueIfExistingUserIsCreatorOfExistingPart()
        {
            string existingUserId = PartTestUser.Id;
            int existingPartid = TestPart.Id;

            Assert.IsTrue(await _partService.UserIsCreatorAsync(existingPartid, existingUserId));
        }

        [Test] //.UserIsCreatorAsync()
        public async Task ReturnFalseForNonExistingUserOrPart()
        {
            string existingUserId = PartTestUser.Id;
            string nonExistingUserId = "nonExistingUserId";
            int existingPartid = TestPart.Id;
            int nonExistingPartid = 800;

            Assert.False(await _partService.UserIsCreatorAsync(existingPartid, nonExistingUserId));
            Assert.False(await _partService.UserIsCreatorAsync(nonExistingPartid, existingUserId));
            Assert.False(await _partService.UserIsCreatorAsync(nonExistingPartid, nonExistingUserId));
        }

        [Test] //.GetUserPartsAsync()
        public async Task ReturnAllUserPartsWhenNoFilterIsAdded()
        {
            var testPartOne = new Part()
            {
                Id = 2,
                Name = "NeUserPartOneOldest",
                Description = "NeUserPartOneDescription",
                MaterialId = 2,
                Quantity = 50,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 3,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(-1),
                UpdatedOn = DateTime.Now.AddDays(-1)
            };

            var testPartTwo = new Part()
            {
                Id = 3,
                Name = "NeUserPartOneNewest",
                Description = "NeUserPartOneDescription",
                MaterialId = 4,
                Quantity = 100,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 2,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(2),
                UpdatedOn = DateTime.Now.AddDays(2)
            };

            await _dbContext.AddRangeAsync(testPartOne, testPartTwo);
            await _dbContext.SaveChangesAsync();

            int expectedUserPartsCount = 3;

            var userPartsQueryModel = await _partService.GetUserPartsAsync(PartTestUser.Id,
                null,
                null,
                PartSorting.Newest,
                1,
                1);

            Assert.IsNotNull(userPartsQueryModel);

            Assert.That(userPartsQueryModel.TotalPartsCount, Is.EqualTo(expectedUserPartsCount));
        }

        [Test] //.GetUserPartsAsync()
        public async Task ReturnOnlyPartsForSelectedMaterialFilter()
        {
            var testPartOne = new Part()
            {
                Id = 2,
                Name = "NeUserPartOneOldest",
                Description = "NeUserPartOneDescription",
                MaterialId = 2,
                Quantity = 50,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 3,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(-1),
                UpdatedOn = DateTime.Now.AddDays(-1)
            };

            var testPartTwo = new Part()
            {
                Id = 3,
                Name = "NeUserPartOneNewest",
                Description = "NeUserPartOneDescription",
                MaterialId = 4,
                Quantity = 100,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 2,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(2),
                UpdatedOn = DateTime.Now.AddDays(2)
            };

            await _dbContext.AddRangeAsync(testPartOne, testPartTwo);
            await _dbContext.SaveChangesAsync();

            int expectedUserPartsCount = 1;

            var userPartsQueryModel = await _partService.GetUserPartsAsync(PartTestUser.Id,
                4,
                null,
                PartSorting.Newest,
                1,
                1);

            Assert.IsNotNull(userPartsQueryModel);

            Assert.That(userPartsQueryModel.TotalPartsCount, Is.EqualTo(expectedUserPartsCount));
            Assert.That(userPartsQueryModel.Parts.First().Name, Is.EqualTo(testPartTwo.Name));
        }

        [Test] //.GetUserPartsAsync()
        public async Task ReturnOnlyPartsThatContainGivenSearchTermInTheirNameOrDescription()
        {
            var testPartOne = new Part()
            {
                Id = 2,
                Name = "NeUserPartOneOldest",
                Description = "NeUserPartOneDescription",
                MaterialId = 2,
                Quantity = 50,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 3,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(-1),
                UpdatedOn = DateTime.Now.AddDays(-1)
            };

            var testPartTwo = new Part()
            {
                Id = 3,
                Name = "NeUserPartOneNewest",
                Description = "NeUserPartOneDescription",
                MaterialId = 4,
                Quantity = 100,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 2,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(2),
                UpdatedOn = DateTime.Now.AddDays(2)
            };

            await _dbContext.AddRangeAsync(testPartOne, testPartTwo);
            await _dbContext.SaveChangesAsync();

            int expectedUserPartsCount = 1;

            var userPartsQueryModel = await _partService.GetUserPartsAsync(PartTestUser.Id,
                null,
                "newest",
                PartSorting.Newest,
                1,
                1);

            Assert.IsNotNull(userPartsQueryModel);

            Assert.That(userPartsQueryModel.TotalPartsCount, Is.EqualTo(expectedUserPartsCount));
            Assert.That(userPartsQueryModel.Parts.First().Name, Is.EqualTo(testPartTwo.Name));
        }

        [Test] //.GetUserPartsAsync()
        public async Task ReturnOnlyPartsInCorrectOrderDependingOnSorting()
        {
            var testPartOne = new Part()
            {
                Id = 2,
                Name = "NeUserPartOneOldest",
                Description = "NeUserPartOneDescription",
                MaterialId = 2,
                Quantity = 50,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 3,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(-1),
                UpdatedOn = DateTime.Now.AddDays(-1)
            };

            var testPartTwo = new Part()
            {
                Id = 3,
                Name = "NeUserPartOneNewest",
                Description = "NeUserPartOneDescription",
                MaterialId = 4,
                Quantity = 100,
                SchemeURL = "NeUserPartOnePartName.pdf",
                Thickness = 2,
                CreatorId = PartTestUser.Id,
                CreatedOn = DateTime.Now.AddDays(2),
                UpdatedOn = DateTime.Now.AddDays(2)
            };

            await _dbContext.AddRangeAsync(testPartOne, testPartTwo);
            await _dbContext.SaveChangesAsync();

            int expectedUserPartsCount = 3;

            var userPartsQueryModel = await _partService.GetUserPartsAsync(PartTestUser.Id,
                null,
                null,
                PartSorting.Oldest,
                1,
                1);

            Assert.IsNotNull(userPartsQueryModel);

            Assert.That(userPartsQueryModel.TotalPartsCount, Is.EqualTo(expectedUserPartsCount));
            Assert.That(userPartsQueryModel.Parts.First().Name, Is.EqualTo(testPartOne.Name));
        }
    }
}

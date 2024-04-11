using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Services;
using HelixLaserWorks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HelixLaserWorks.Tests
{
    [TestFixture]
    public class ThicknessService_Should
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;

        private ApplicationDbContext _dbContext;

        private IThicknessService _thicknessService;

        [SetUp]
        public async Task SetUp()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HelixLaserWorksInMemoryThicknessServiceTestDb")
            .Options;

            _dbContext = new ApplicationDbContext(_contextOptions);

            var seedData = new SeedData();

            await _dbContext.Thicknesses.AddRangeAsync(seedData.Thicknesses);

            await _dbContext.SaveChangesAsync();

            _thicknessService = new ThicknessService(_dbContext);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.DisposeAsync();
        }

        [Test]//.GetAllThicknessesAsync()
        public async Task ReturnAllAvailableThicknessValuesFromDb()
        {
            var allThicknessesFromDb = await _dbContext.Thicknesses.Select(x => x.Value).ToListAsync();

            var thicknessesFromMethod = await _thicknessService.GetAllThicknessesAsync();

            Assert.That(thicknessesFromMethod.Count, Is.EqualTo(allThicknessesFromDb.Count));

            foreach (var thicknessFromDb in allThicknessesFromDb)
            {
                Assert.IsTrue(thicknessesFromMethod.Contains(thicknessFromDb));
            }
        }

        [Test]
        public async Task ReturnTrueIfAllGivenThicknessValuesAreValid()
        {
            List<double> validThicknessess = new List<double>(){ 1, 2, 3, 4, 5, 6, 10, 20 };

            Assert.IsTrue(await _thicknessService.ThicknessesAreValidAsync(validThicknessess));
        }

        [Test]
        public async Task ReturnFalseIfAnyGivenThicknessValueIsInvalid()
        {
            List<double> invalidThicknesses = new List<double>() { 1, 2, 3, 450, 5, 6.4, 1.5, 20 };

            Assert.IsFalse(await _thicknessService.ThicknessesAreValidAsync(invalidThicknesses));
        }
    }
}

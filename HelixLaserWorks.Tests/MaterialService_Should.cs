using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Material;
using HelixLaserWorks.Core.Services;
using HelixLaserWorks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HelixLaserWorks.Tests
{
    [TestFixture]
    public class MaterialService_Should
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;

        private ApplicationDbContext _dbContext;

        private IMaterialService _materialService;

        [SetUp]
        public async Task SetUp()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "HelixLaserWorksInMemoryDb")
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

            await _dbContext.SaveChangesAsync();

            _materialService = new MaterialService(_dbContext);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.DisposeAsync();
        }

        [Test] //.AllAsync()
        public async Task ReturnAllMaterials()
        {
            int validMaterialsCount = 5;
            int[] validMaterialsIds = { 1, 2, 3, 4, 5 };

            var materials = await _materialService.AllAsync();

            Assert.IsNotNull(materials);
            Assert.That(materials.Count().Equals(validMaterialsCount));

            foreach (var material in materials)
            {
                Assert.That(validMaterialsIds.Contains(material.Id));
            }
        }

        [Test] //.AllAvailableAsync()
        public async Task ReturnOnlyAvailableMaterials()
        {
            int validMaterialsCount = 3;
            int[] validMaterialsIds = { 1, 2, 3, };

            var materials = await _materialService.AllAvailableAsync();

            Assert.IsNotNull(materials);
            Assert.That(materials.Count().Equals(validMaterialsCount));

            foreach (var material in materials)
            {
                Assert.That(validMaterialsIds.Contains(material.Id));
            }
        }

        [Test] //.DisableAsync()
        public async Task DisableAnAvailableMaterial()
        {
            int availableMaterialId = 1;

            var material = await _dbContext.Materials.FindAsync(availableMaterialId);

            Assert.IsTrue(material.IsAvailable);

            await _materialService.DisableAsync(material.Id);

            Assert.IsFalse(material.IsAvailable);
        }

        [Test] //.EnableAsync()
        public async Task EnableAnDisabledMaterial()
        {
            int unAvailableMaterialId = 5;

            var material = await _dbContext.Materials.FindAsync(unAvailableMaterialId);

            Assert.IsFalse(material.IsAvailable);

            await _materialService.EnableAsync(material.Id);

            Assert.IsTrue(material.IsAvailable);
        }

        [Test] //.AddAsync()
        public async Task AddNewMaterialWithValidData()
        {
            var newMaterial = new MaterialFormModel()
            {
                MaterialTypeId = 1,
                PricePerSquareMeter = 10,
                CorrosionResistance = true,
                Name = "TestMaterial",
                Density = 5,
                Description = "TestMaterialDescription",
                ImageUrl = "TestImageURL",
                Specification = null,
                SelectedThicknesses = new List<double>() { 1, 2, 3, 4, }
            };

            int expectedCount = await _dbContext.Materials.CountAsync() + 1;

            await _materialService.AddAsync(newMaterial);

            int countAfterAdd = await _dbContext.Materials.CountAsync();


            Assert.That(countAfterAdd, Is.EqualTo(expectedCount));
            Assert.IsTrue(await _dbContext.Materials.AsNoTracking().AnyAsync(m => m.Name == newMaterial.Name));

            var newMaterialFromDb = await _dbContext.Materials
                .AsNoTracking()
                .Include(m => m.MaterialThicknesses)
                .ThenInclude(mt => mt.Thickness)
                .FirstAsync(m => m.Name == newMaterial.Name);
            foreach (var thickness in newMaterial.SelectedThicknesses)
            {
                Assert.IsTrue(newMaterialFromDb.MaterialThicknesses.Any(mt => mt.Thickness.Value == thickness));
            }
        }

        [Test] //.EditAsync()
        public async Task EditExistingMaterial()
        {
            var editModel = new MaterialFormModel()
            {
                MaterialTypeId = 1,
                PricePerSquareMeter = 10,
                CorrosionResistance = true,
                Name = "TestMaterial",
                Density = 5,
                Description = "TestMaterialDescription",
                ImageUrl = "TestImageURL",
                Specification = null,
                SelectedThicknesses = new List<double>() { 1 }
            };

            int materialToEditId = 4;

            var materialBeforeEdit = await _dbContext.Materials
                .AsNoTracking()
                .Where(m => m.Id == materialToEditId)
                .Include(m => m.MaterialThicknesses)
                .ThenInclude(mt => mt.Thickness)
                .FirstAsync();

            string nameBeforeEdit = materialBeforeEdit.Name;
            string descriptionBeforeEdit = materialBeforeEdit.Description;
            int thicknessesCountBeforeEdit = materialBeforeEdit.MaterialThicknesses.Count();

            await _materialService.EditAsync(materialToEditId, editModel);

            var materialAfterEdit = await _dbContext.Materials
                .AsNoTracking()
                .Where(m => m.Id == materialToEditId)
                .Include(m => m.MaterialThicknesses)
                .ThenInclude(mt => mt.Thickness)
                .FirstAsync();

            int thicknessesCountAfterEdit = materialAfterEdit.MaterialThicknesses.Count();

            Assert.That(materialAfterEdit.Name, Is.Not.SameAs(nameBeforeEdit));
            Assert.That(materialAfterEdit.Description, Is.Not.SameAs(descriptionBeforeEdit));
            Assert.That(thicknessesCountAfterEdit, Is.Not.EqualTo(thicknessesCountBeforeEdit));
        }

        [Test] //.GetAllATypesAsync()
        public async Task ReturnAllAvailableMaterialTypes()
        {
            var expectedTypesCount = await _dbContext.MaterialTypes.CountAsync();

            var typesFromService = await _materialService.GetAllATypesAsync();

            Assert.That(typesFromService.Count, Is.EqualTo(expectedTypesCount));
        }

        [Test] //.GetAllForDropdownAsync()
        public async Task ReturnOnlyAvailableMaterialsNameAndId()
        {
            var availableMaterialsCount = await _dbContext.Materials.CountAsync(m => m.IsAvailable);

            var materialsFromService = await _materialService.GetAllForDropdownAsync();

            Assert.That(materialsFromService.Count, Is.EqualTo(availableMaterialsCount));

            foreach (var material in materialsFromService)
            {
                Assert.IsNotNull(material.Id);
                Assert.IsNotNull(material.Name);
            }
        }

        [Test] //.GetAvailableThicknessesForMaterialAsync()
        public async Task ReturnCorrectAvailableThicknessesForGivenMaterial()
        {
            int materialId = 4;

            var expectedThicknesses = await _dbContext
                .MaterialsThicknesses
                .Where(mt => mt.MaterialId == materialId)
                .Select(mt => mt.Thickness.Value)
                .ToListAsync();

            var thicnessesFromService = await _materialService.GetAvailableThicknessesForMaterialAsync(materialId);

            Assert.That(thicnessesFromService.Count, Is.EqualTo(expectedThicknesses.Count));

            foreach (var thickness in thicnessesFromService)
            {
                Assert.That(expectedThicknesses.Contains(thickness));
            }
        }

        [Test] //.GetDetailsByIdAsync()
        public async Task ReturnCorrectDetailsForGivenMaterial()
        {
            int materialId = 4;

            var materialFromDb = await _dbContext.Materials.FindAsync(materialId);

            var materialFromService = await _materialService.GetDetailsByIdAsync(materialId);

            Assert.That(materialFromService?.Name, Is.EqualTo(materialFromDb?.Name));
            Assert.That(materialFromService?.Description, Is.EqualTo(materialFromDb?.Description));
            Assert.That(materialFromService?.Price, Is.EqualTo(materialFromDb?.PricePerSquareMeter.ToString()));
            Assert.That(materialFromService?.Type, Is.EqualTo(materialFromDb?.MaterialType.Name));
        }

        [Test] //.GetMaterialForEditAsync()
        public async Task ReturnCorrectMaterialModelForEdit()
        {
            int materialId = 4;

            var materialFromDb = await _dbContext.Materials.FindAsync(materialId);

            var materialFromService = await _materialService.GetMaterialForEditAsync(materialId);

            Assert.That(materialFromService?.Name, Is.EqualTo(materialFromDb?.Name));
            Assert.That(materialFromService?.Description, Is.EqualTo(materialFromDb?.Description));
            Assert.That(materialFromService?.PricePerSquareMeter, Is.EqualTo(materialFromDb?.PricePerSquareMeter));
            Assert.That(materialFromService?.MaterialTypeId, Is.EqualTo(materialFromDb?.MaterialTypeId));
            Assert.That(materialFromService?.CorrosionResistance, Is.EqualTo(materialFromDb?.CorrosionResistance));
        }

        [Test]//.MaterialExistsAsync()
        public async Task ReturnTrueForExistingMaterial()
        {
            int existingMaterialId = 4;

            Assert.IsTrue(await _materialService.MaterialExistsAsync(existingMaterialId));
        }

        [Test]//.MaterialExistsAsync()
        public async Task ReturnFalseForNonExistingMaterial()
        {
            int nonExistingMaterialId = 104;

            Assert.IsFalse(await _materialService.MaterialExistsAsync(nonExistingMaterialId));
        }

        [Test]//.MaterialThicknessExistsAsync()
        public async Task ReturnTrueForExistingThicknessForGivenMaterial()
        {
            int existingMaterialId = 4;
            double existingThicknessValue = 1;

            Assert.IsTrue(await _materialService.MaterialThicknessExistsAsync(existingMaterialId, existingThicknessValue));
        }

        [Test]//.MaterialThicknessExistsAsync()
        public async Task ReturnFalseForNonExistingThicknessForGivenMaterial()
        {
            int existingMaterialId = 4;
            double nonExistingThicknessValue = 54;

            Assert.IsFalse(await _materialService.MaterialThicknessExistsAsync(existingMaterialId, nonExistingThicknessValue));
        }

        [Test]//.MaterialTypeExistsAsync()
        public async Task ReturnTrueForExistingMaterialType()
        {
            int existingMaterialTypeId = 1;

            Assert.IsTrue(await _materialService.MaterialTypeExistsAsync(existingMaterialTypeId));
        }

        [Test]//.MaterialTypeExistsAsync()
        public async Task ReturnFalseForNonExistingMaterialType()
        {
            int nonExistingMaterialTypeId = 10;

            Assert.IsFalse(await _materialService.MaterialTypeExistsAsync(nonExistingMaterialTypeId));
        }
    }
}

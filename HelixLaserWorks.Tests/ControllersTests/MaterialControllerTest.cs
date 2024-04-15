using HelixLaserWorks.Controllers;
using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Material;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HelixLaserWorks.Tests.ControllersTests
{
    [TestFixture]
    public class MaterialControllerTest
    {
        private Mock<IMaterialService> _materialServiceMock;
        private MaterialController _materialController;

        [SetUp]
        public void SetUp()
        {
            _materialServiceMock = new Mock<IMaterialService>();
            _materialController = new MaterialController(_materialServiceMock.Object);
        }

        [Test]
        public async Task ActionAllReturnsViewResultWithCorrectModel()
        {
            IEnumerable<MaterialViewModel> materials = new List<MaterialViewModel>() {
                new MaterialViewModel()
                {
                    Id = 1,
                    Name = "Name",
                    Description = "Description",
                    ImageUrl = "imageURl",
                    IsAvailable = true,
                    Type = "Type"
                }
            };

            _materialServiceMock = new Mock<IMaterialService>();

            _materialServiceMock.Setup(ms => ms.AllAvailableAsync())
                .ReturnsAsync(materials);

            _materialController = new MaterialController(_materialServiceMock.Object);

            var result = await _materialController.All();

            Assert.IsInstanceOf(typeof(ViewResult), result);

            var viewModel = result as ViewResult;

            Assert.IsNotNull(viewModel);
            var resultModels = viewModel.ViewData.Model as IEnumerable<MaterialViewModel>;

            Assert.IsNotNull(resultModels);
            Assert.IsTrue(resultModels.Count() == 1);
            Assert.IsTrue(resultModels.First().Name == "Name");
            Assert.IsTrue(resultModels.First().Id == 1);
        }

        [Test]
        public async Task ActionDetailsReturnsViewForValidModel()
        {
            var validMaterialModel = new MaterialDetailViewModel()
            {
                Id = 1,
                Name = "Name",
                Description = "Description"
            };

            _materialServiceMock = new Mock<IMaterialService>();

            _materialServiceMock.Setup(ms => ms.GetDetailsByIdAsync(validMaterialModel.Id))
                .ReturnsAsync(validMaterialModel);

            _materialController = new MaterialController(_materialServiceMock.Object);

            var result = await _materialController.Details(validMaterialModel.Id);

            Assert.IsInstanceOf(typeof(ViewResult), result);

            var viewModel = result as ViewResult;

            Assert.IsNotNull(viewModel);
            var resultModel = viewModel.ViewData.Model as MaterialDetailViewModel;

            Assert.IsNotNull(resultModel);
            Assert.IsTrue(resultModel.Name == validMaterialModel.Name);
            Assert.IsTrue(resultModel.Id == validMaterialModel.Id);
            Assert.IsTrue(resultModel.Description == validMaterialModel.Description);
        }

        [Test]
        public async Task ActionDetailsReturnsBadRequestWhenMaterialDoesNotExist()
        {
            var validMaterialModel = new MaterialDetailViewModel()
            {
                Id = 1,
                Name = "Name",
                Description = "Description"
            };

            _materialServiceMock = new Mock<IMaterialService>();

            _materialServiceMock.Setup(ms => ms.GetDetailsByIdAsync(validMaterialModel.Id))
                .ReturnsAsync(validMaterialModel);

            _materialController = new MaterialController(_materialServiceMock.Object);

            int nonExistingId = 50;

            var result = await _materialController.Details(nonExistingId);

            Assert.IsInstanceOf(typeof(BadRequestResult), result);
        }

        [Test]
        public async Task ActionGetAvailableThicknessesJSONShouldReturnJSONObjectWithAllAvailableThicknessesForGivenMaterialId()
        {
            var thicknesses = new List<double>() { 1, 2, 3, 12 };

            int materialId = 1;

            _materialServiceMock = new Mock<IMaterialService>();

            _materialServiceMock.Setup(ms => ms.GetAvailableThicknessesForMaterialAsync(materialId))
                .ReturnsAsync(thicknesses);

            _materialController = new MaterialController(_materialServiceMock.Object);

            var result = await _materialController.GetAvailableThicknessesJSON(materialId);

            Assert.IsInstanceOf(typeof(JsonResult), result);
        }
    }
}

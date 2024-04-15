using HelixLaserWorks.Areas.Admin.Controllers;
using HelixLaserWorks.Core.Contracts;
using HelixLaserWorks.Core.Models.Material;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HelixLaserWorks.Tests.ControllersTests
{
    [TestFixture]
    public class AdminMaterialControllerTest
    {
        private Mock<IMaterialService> _materialServiceMock;
        private MaterialController _materialController;
        private Mock<IThicknessService> _thicknessServiceMock;

        [SetUp]
        public void SetUp()
        {
            _thicknessServiceMock = new Mock<IThicknessService>();
            _materialServiceMock = new Mock<IMaterialService>();
            _materialController = new MaterialController(_materialServiceMock.Object, _thicknessServiceMock.Object);
        }

        [Test] //GET
        public async Task ActionAddShouldReturnViewResultWithCorrectModel()
        {
            var availableThicknesses = new List<double>() { 1, 2, 3 };
            var types = new List<MaterialTypeViewModel>()
            {
                new MaterialTypeViewModel()
                {
                    Id = 1,
                    Name = "Metal"
                }
            };

            _thicknessServiceMock.Setup(ts => ts.GetAllThicknessesAsync()).ReturnsAsync(availableThicknesses);
            _materialServiceMock.Setup(ms => ms.GetAllATypesAsync()).ReturnsAsync(types);

            _materialController = new MaterialController(_materialServiceMock.Object, _thicknessServiceMock.Object);

            var result = await _materialController.Add();

            Assert.IsInstanceOf(typeof(ViewResult), result);

            var viewModel = result as ViewResult;

            Assert.IsNotNull(viewModel);

            var model = viewModel.ViewData.Model as MaterialFormModel;
            Assert.IsNotNull(model);
            Assert.That(availableThicknesses.Count, Is.EqualTo(model.AvailableThicknesses.Count));
            Assert.IsTrue(model.MaterialTypes.Any(mt => mt.Name == "Metal"));
        }

        [Test] //POST
        public async Task ActionAddShouldReturnViewWithModelIfModelStateIsInvalid()
        {
            var availableThicknesses = new List<double>() { 1, 2, 3 };
            var types = new List<MaterialTypeViewModel>()
            {
                new MaterialTypeViewModel()
                {
                    Id = 1,
                    Name = "Metal"
                }
            };

            var materialFormModel = new MaterialFormModel()
            {
                SelectedThicknesses = availableThicknesses,
                MaterialTypeId = 1,
                CorrosionResistance = false,
                Density = 3,
                Description = "Description",
                ImageUrl = "ImageURL",
                Name = "Name",
                PricePerSquareMeter = 120m,
            };

            _thicknessServiceMock.Setup(ts => ts.ThicknessesAreValidAsync(availableThicknesses)).ReturnsAsync(false);
            _materialServiceMock.Setup(ms => ms.MaterialTypeExistsAsync(1)).ReturnsAsync(false);

            _materialController = new MaterialController(_materialServiceMock.Object, _thicknessServiceMock.Object);

            var result = await _materialController.Add(materialFormModel);

            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test] //POST
        public async Task ActionAddShouldRedirectToActionWhenModelStateIsValid()
        {
            var availableThicknesses = new List<double>() { 1, 2, 3 };
            var types = new List<MaterialTypeViewModel>()
            {
                new MaterialTypeViewModel()
                {
                    Id = 1,
                    Name = "Metal"
                }
            };

            var materialFormModel = new MaterialFormModel()
            {
                SelectedThicknesses = availableThicknesses,
                MaterialTypeId = 1,
                CorrosionResistance = false,
                Density = 3,
                Description = "Description",
                ImageUrl = "ImageURL",
                Name = "Name",
                PricePerSquareMeter = 120m,
            };

            _thicknessServiceMock.Setup(ts => ts.ThicknessesAreValidAsync(availableThicknesses)).ReturnsAsync(true);
            _materialServiceMock.Setup(ms => ms.MaterialTypeExistsAsync(1)).ReturnsAsync(true);

            _materialController = new MaterialController(_materialServiceMock.Object, _thicknessServiceMock.Object);

            var result = await _materialController.Add(materialFormModel);

            Assert.IsInstanceOf(typeof(RedirectToActionResult), result);

            var resultRedirect = result as RedirectToActionResult;

            string expectedActionName = "All";
            string expectedControllerName = "Material";
            string expectedAreaName = "Admin";

            Assert.IsNotNull(resultRedirect);
            Assert.That(expectedActionName, Is.EqualTo(resultRedirect.ActionName));
            Assert.That(expectedControllerName, Is.EqualTo(resultRedirect.ControllerName));

            Assert.IsNotNull(resultRedirect.RouteValues);
            Assert.IsTrue(resultRedirect.RouteValues.Any(v => v.Value?.ToString() == expectedAreaName));
        }

        [Test] //GET
        public async Task ActionEditShouldReturnBadRequestForNonExistingMaterial()
        {
            var result = await _materialController.Edit(1);

            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Test]
        public async Task ActionEditShouldReturnViewResultWithModelWhenModelStateIsInvalid()
        {
            var materialModel = new MaterialFormModel()
            {
                MaterialTypeId = 1,
                CorrosionResistance = false,
                PricePerSquareMeter = 12,
                Density = 4,
                Description = "DEscription",
                Name = "Name",
                ImageUrl = "ImageURL",
            };

            _materialServiceMock.Setup(ms => ms.MaterialTypeExistsAsync(materialModel.MaterialTypeId)).ReturnsAsync(false);
            _thicknessServiceMock.Setup(ts => ts.ThicknessesAreValidAsync(new List<double>() { 1, 2 })).ReturnsAsync(false);

            _materialController = new MaterialController(_materialServiceMock.Object, _thicknessServiceMock.Object);

            var result = await _materialController.Edit(1, materialModel);

            Assert.IsInstanceOf(typeof(ViewResult), result);

            var resultView = result as ViewResult;
            Assert.IsNotNull(resultView);

            var resultModel = resultView.ViewData.Model as MaterialFormModel;
            Assert.IsNotNull(resultModel);

            Assert.IsTrue(resultModel.Name == materialModel.Name);
            Assert.IsTrue(resultModel.Description == materialModel.Description);
            Assert.IsTrue(resultModel.PricePerSquareMeter == materialModel.PricePerSquareMeter);
        }
    }
}

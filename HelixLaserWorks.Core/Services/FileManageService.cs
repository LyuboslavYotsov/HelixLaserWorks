using HelixLaserWorks.Core.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HelixLaserWorks.Core.Services
{
    public class FileManageService : IFileManageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileManageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data", "PartsSchemeFiles", file.FileName);

            using FileStream fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return filePath;
        }
    }
}

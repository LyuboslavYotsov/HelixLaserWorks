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

        public Task DeleteFile(string filePath)
        {
            File.Delete(filePath);

            return Task.CompletedTask;
        }

        public async Task<byte[]> DownloadFile(string filePath)
        {
            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<string> UploadFile(IFormFile file, string userEmail)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data", "PartsSchemeFiles", $"{userEmail}-{file.FileName}");

            using FileStream fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return filePath;
        }
    }
}

using HelixLaserWorks.Core.Contracts;
using Microsoft.AspNetCore.Http;

namespace HelixLaserWorks.Core.Services
{
    public class FileManageService : IFileManageService
    {

        public async Task<string> UploadFile(IFormFile file)
        {
            var filePath = Path.Combine(@"C:\Users\Luboslav\Desktop\ASP.NET FinalProject\HelixLaserWorks\HelixLaserWorks\wwwroot\Data\PartsSchemeFiles\", file.FileName);

            using FileStream fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return filePath;
        }
    }
}

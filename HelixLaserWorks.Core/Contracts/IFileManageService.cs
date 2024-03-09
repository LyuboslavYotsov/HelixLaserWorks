﻿using Microsoft.AspNetCore.Http;

namespace HelixLaserWorks.Core.Contracts
{
    public interface IFileManageService
    {
        Task<string> UploadFile(IFormFile file);
    }
}

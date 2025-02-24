using Common.ConfigurationSettings;
using Common.Models;
using Core.Results;
using Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Business.Repositories.Storage
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public LocalStorage(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task<Core.Results.IResult> DeleteAsync(string path, string fileName)
        {
            string fullPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, path, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return new SuccessResult("File deleted successfully.");
            }
            return new ErrorResult("File not found.");
        }

        public IDataResult<List<string>> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            if (directory.Exists)
            {
                var fileNames = directory.GetFiles().Select(f => f.Name).ToList();
                return new SuccessDataResult<List<string>>(fileNames);
            }
            return new ErrorDataResult<List<string>>(null, StatusMessage_DirectoryNotFound, StatusCode_DirectoryNotFound);
        }

        public IDataResult<string> HasFile(string path, string fileName)
        {
            string fullPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, path, fileName);
            bool exists = File.Exists(fullPath);
            return new SuccessDataResult<string>(fullPath);
        }

        public IDataResult<string> HasFileFromDirectlyFullPath(string fullPath)
        {
            bool exists = File.Exists(fullPath);
            string content = exists ? fullPath : null;
            return new SuccessDataResult<string>(fullPath);
        }

        //public async Task<IDataResult<(string fileName, string pathOrContainerName)>> SingleUploadAsync(string path, IFormFile file)
        //{
        //    string uploadPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, path);

        //    if (!Directory.Exists(uploadPath))
        //        Directory.CreateDirectory(uploadPath);

        //    string fileNewName = file.FileName;
        //    await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

        //    //string pathNewName = $"Uploads/{path}/{fileNewName}";

        //    return new SuccessDataResult<(string fileName, string pathOrContainerName)>((file.FileName, uploadPath));
        //}
        public async Task<IDataResult<(string fileName, string pathOrContainerName)>> SingleUploadAsync(string path, IFormFile file, HttpContext httpContext)
        {
           //string uploadPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, path);
            string uploadPath = Path.Combine("wwwroot", path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string fileNewName = file.FileName;
            string fullPath = Path.Combine(uploadPath, fileNewName);
            await CopyFileAsyncLocal(fullPath, file);

            // Construct the URL of the uploaded file
            string baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";
            string fileUrl = $"{baseUrl}/{path}/{fileNewName}".Replace("\\", "/");

            return new SuccessDataResult<(string fileName, string pathOrContainerName)>((fileNewName, fileUrl));
        }

        private async Task CopyFileAsyncLocal(string destinationPath, IFormFile file)
        {
            using (var stream = new FileStream(destinationPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public async Task<IDataResult<List<(string fileName, string pathOrContainerName)>>> MultipleUploadAsync(List<string> paths, List<IFormFile> files)
        {
            if (paths.Count != files.Count)
            {
                return new ErrorDataResult<List<(string fileName, string pathOrContainerName)>>(null, StatusCode_Success, StatusMessage_FilesCountMismatch);
            }

            List<(string fileName, string pathOrContainerName)> uploadedFiles = new List<(string fileName, string pathOrContainerName)>();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var path = paths[i];



                string uploadPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, path);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                //string fileNewName = await FileRenameAsync(file.FileName);
                string fileNewName = file.FileName;
                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

                string pathNewName = $"Uploads/{path}/{fileNewName}";

                uploadedFiles.Add((file.FileName, pathNewName));
            }

            return new SuccessDataResult<List<(string fileName, string pathOrContainerName)>>(uploadedFiles);
        }

        public async Task<IDataResult<List<(string fileName, string pathOrContainerName)>>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                //string fileNewName = await FileRenameAsync(file.FileName);
                string fileNewName = file.FileName;
                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

                string pathNewName = $"{path}/{fileNewName}";

                datas.Add((file.FileName, pathNewName));
            }

            return new SuccessDataResult<List<(string fileName, string pathOrContainerName)>>(datas);
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: true);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("File upload failed.", ex);
            }
        }

        public async Task<Core.Results.IResult> DeleteFromDirectlyFullPaths(List<string> fullPaths)
        {
            foreach (var fullPath in fullPaths)
            {
                var fileFullPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, fullPath);
                if (File.Exists(fileFullPath))
                {
                    File.Delete(fileFullPath);
                }
            }

            return new SuccessResult("Files deleted successfully.");

        }

        public async Task<Core.Results.IResult> DeleteFromDirectlyFullPath(string fullPath)
        {
            string fileFullPath = Path.Combine(ConfigSettings.ApplicationSetting.BaseLocalStorageDomain, fullPath);
            if (File.Exists(fileFullPath))
            {
                File.Delete(fileFullPath);
                return new SuccessResult("File deleted successfully.");
            }
            return new ErrorResult("File not found.");
        }
    }
}

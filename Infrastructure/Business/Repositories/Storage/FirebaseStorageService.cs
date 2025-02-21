using Common.ConfigurationSettings;
using Common.Models;
using Core.Results;
using Domain.Interfaces;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Literals.StringLiterals;

namespace Infrastructure.Business.Repositories.Storage
{
    public class FirebaseStorageService : Storage, IFirebaseStorage
    {
        private readonly string _bucketName = ConfigSettings.ApplicationSetting.FireBaseStorage.BucketName;
        private readonly string _baseUrl = ConfigSettings.ApplicationSetting.FireBaseStorage.BaseUrl;
        private readonly StorageClient _storageClient;

        public FirebaseStorageService()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("serviceAccountKey.json")
                });
            }

            _storageClient = StorageClient.Create();
        }

        public async Task<Core.Results.IResult> DeleteAsync(string path, string fileName)
        {
            try
            {
                await _storageClient.DeleteObjectAsync(_bucketName, $"{path}/{fileName}");
                return new SuccessResult("File deleted successfully.");
            }
            catch (Exception)
            {
                return new ErrorResult("File not found or deletion failed.");
            }
        }

        public IDataResult<List<string>> GetFiles(string path)
        {
            var files = _storageClient.ListObjects(_bucketName, path)
                                      .Select(obj => obj.Name)
                                      .ToList();

            return files.Any()
                ? new SuccessDataResult<List<string>>(files)
                : new ErrorDataResult<List<string>>(null, StatusCode_FilesNotFound, StatusMessage_FilesNotFound);
        }

        public IDataResult<string> HasFile(string path, string fileName)
        {
            var filePath = $"{path}/{fileName}";

            var file = _storageClient.ListObjects(_bucketName, filePath).FirstOrDefault();
            return file != null
                ? new SuccessDataResult<string>(filePath)
                : new ErrorDataResult<string>(null, StatusCode_FilesNotFound, StatusMessage_FilesNotFound);
        }

        public async Task<IDataResult<(string fileName, string pathOrContainerName)>> SingleUploadAsync(string path, IFormFile file, HttpContext httpContext)
        {
            string fileName = file.FileName;
            string filePath = $"{path}/{fileName}";

            using var stream = file.OpenReadStream();
            await _storageClient.UploadObjectAsync(_bucketName, filePath, file.ContentType, stream);

            string downloadUrl = $"{_baseUrl}{_bucketName}/o/{Uri.EscapeDataString(filePath)}?alt=media";

            return new SuccessDataResult<(string fileName, string pathOrContainerName)>((fileName, downloadUrl));
        }

        public async Task<IDataResult<List<(string fileName, string pathOrContainerName)>>> MultipleUploadAsync(List<string> paths, List<IFormFile> files)
        {
            if (paths.Count != files.Count)
                return new ErrorDataResult<List<(string fileName, string pathOrContainerName)>>(null, StatusCode_FilesCountMismatch, StatusMessage_FilesCountMismatch);

            var uploadedFiles = new List<(string fileName, string pathOrContainerName)>();

            for (int i = 0; i < files.Count; i++)
            {
                string fileName = files[i].FileName;
                string filePath = $"{paths[i]}/{fileName}";

                using var stream = files[i].OpenReadStream();
                await _storageClient.UploadObjectAsync(_bucketName, filePath, files[i].ContentType, stream);

                string downloadUrl = $"{_baseUrl}{_bucketName}/o/{Uri.EscapeDataString(filePath)}?alt=media";
                uploadedFiles.Add((fileName, downloadUrl));
            }

            return new SuccessDataResult<List<(string fileName, string pathOrContainerName)>>(uploadedFiles);
        }

        public async Task<Core.Results.IResult> DeleteFromDirectlyFullPaths(List<string> fullPaths)
        {
            foreach (var filePath in fullPaths)
            {
                try
                {
                    await _storageClient.DeleteObjectAsync(_bucketName, filePath);
                }
                catch (Exception)
                {
                    return new ErrorResult($"Failed to delete {filePath}.");
                }
            }
            return new SuccessResult("Files deleted successfully.");
        }

        public async Task<Core.Results.IResult> DeleteFromDirectlyFullPath(string fullPath)
        {
            try
            {
                await _storageClient.DeleteObjectAsync(_bucketName, fullPath);
                return new SuccessResult("File deleted successfully.");
            }
            catch (Exception)
            {
                return new ErrorResult("File not found.");
            }
        }

        public Task<IDataResult<List<(string fileName, string pathOrContainerName)>>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            throw new NotImplementedException();
        }

        public IDataResult<string> HasFileFromDirectlyFullPath(string fullPath)
        {
            throw new NotImplementedException();
        }
    }
}

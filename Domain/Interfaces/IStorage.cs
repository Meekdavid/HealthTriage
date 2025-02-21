using Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStorage
    {
        Task<IDataResult<List<(string fileName, string pathOrContainerName)>>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
        Task<IDataResult<(string fileName, string pathOrContainerName)>> SingleUploadAsync(string pathOrContainerName, IFormFile file, HttpContext httpContext);
        Task<IDataResult<List<(string fileName, string pathOrContainerName)>>> MultipleUploadAsync(List<string> paths, List<IFormFile> files);
        Task<Core.Results.IResult> DeleteAsync(string pathOrContainerName, string fileName);
        Task<Core.Results.IResult> DeleteFromDirectlyFullPath(string fullPath);
        Task<Core.Results.IResult> DeleteFromDirectlyFullPaths(List<string> fullPaths);
        IDataResult<List<string>> GetFiles(string pathOrContainerName);
        IDataResult<string> HasFile(string pathOrContainerName, string fileName);
        IDataResult<string> HasFileFromDirectlyFullPath(string fullPath);
    }
}

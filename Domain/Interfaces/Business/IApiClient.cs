using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Business
{
    public interface IApiClient
    {
        Task<string> GetAsync(string url, Dictionary<string, string> queryParams = null, Dictionary<string, string> headers = null);
        Task<string> PostAsync(string url, HttpContent content, Dictionary<string, string> headers = null);
    }
}

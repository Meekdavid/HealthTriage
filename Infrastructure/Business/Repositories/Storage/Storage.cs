using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business.Repositories.Storage
{
    public class Storage
    {
        protected async Task<string> FileRenameAsync(string fileName)
        {
            return $"{fileName}{Path.GetExtension(fileName)}";
        }
    }
}

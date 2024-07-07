using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InsurTech.Core.Service
{
    public interface IUploadService
    {
        Task<string> UploadImageAsync(Stream stream, string fileName);
    }
}

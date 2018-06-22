
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using BINF.Models;
using Microsoft.AspNetCore.Http;

namespace BINF.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadAsync(IEnumerable<IFormFile> files, string name, string containername);
        Task<bool> DeleteFile(string name, string containername);

        List<FileModel> ListFiles(List<string> name, string searchterm);
        List<FileModel> ListFiles(string name, string searchterm, List<string> containernames);

        bool CheckIfFileExists(string filename, string containername);
        IFormFile GetFile(string name, string containername);

    }
}

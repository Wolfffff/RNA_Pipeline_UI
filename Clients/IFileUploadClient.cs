//using Inuvo.SearchLinks.Core.Models.FileUpload;
//using Inuvo.SearchLinks.Core.Pagination;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Web;
//using BINF.Models;
//using Microsoft.AspNetCore.Http;

//namespace BINF.Clients
//{
//    public interface IFileUploadClient
//    {

//        CloudBlobContainer CreateIfContainerNotExists(string name);

//        Task<string> Upload(IEnumerable<IFormFile> file, string name, string containername);

//        List<FileModel> GetFileList(List<string> name, string searchterm);
//        List<FileModel> GetFileList(string name, string searchterm, List<string> containername);

//        Task<bool> DeleteBlob(string blobFileName, string containername);
//        bool CheckIfBlobExists(string blobName, string containername);
//        CloudBlob GetBlob(string name, string containername);
//    }
//}

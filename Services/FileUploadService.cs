//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Web;
//using Microsoft.WindowsAzure.Storage.Blob;
//using BINF.Services;
//using Microsoft.AspNetCore.Http;
//using BINF.Models;

//namespace BINF.Services
//{
//    public class FileUploadService : IFileUploadService
//    {
//        FileUploadService() => _uploadClient = new FileUploadClient();
//        ProcessingController() => _fileUploadService = new FileUploadService();
//        public async Task<string> UploadAsync(IEnumerable<IFormFile> files, string name, string containername) //return type?
//        {

//                var stored = await _uploadClient.Upload(files, name, containername);

//                return stored;
//        }

//        public List<FileModel> ListFiles(List<string> name, string searchterm)
//        {

//                var blobs = _uploadClient.GetFileList(name, searchterm, options);
//                return blobs;

//        }

//        public List<FileModel> ListFiles(string name, string searchterm, List<string> containernames)
//        {

//                var blobs = _uploadClient.GetFileList(name, searchterm, containernames);
//                return blobs;

//        }

//        public async Task<bool> DeleteFile(string name, string containername) //return type?
//        {

//                await _uploadClient.DeleteBlob(name, containername);
//                return true;

//        }
//        public bool CheckIfFileExists(string file, string containername)
//        {
//            return _uploadClient.CheckIfBlobExists(file, containername);

//        }
//        public CloudBlob GetFile(string name, string containername)
//        {
//            return _uploadClient.GetBlob(name, containername);

//        }
//    }
//}

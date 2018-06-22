
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Web;
//using System.Linq;
//using BINF.Clients;
//using Microsoft.AspNetCore.Http;
//using BINF.Models;
//using System.IO;

//namespace BINF.Clients
//{
//    public class FileUploadClient : IFileUploadClient
//    {

        
//        //{
//        //    return 
//        //}

//        //public async Task<string> Upload(IEnumerable<IFormFile> file, string name, string containername)
//        //{

//        //        if (file != null)
//        //        {
//        //            foreach (var f in file)
//        //            {
//        //                if (f != null)
//        //                {
//        //                    return "test'"

//        //                }
//        //            }
//        //        }
//        //        return "Success";
            
//        //}

//        public List<FileModel> GetFileList(string name, string searchterm)
//        {
//            var listOfItems = new List<FileModel>();
//            var tempName = name;
//            foreach (var n in containernames)
//            {
//                if (n == "searchlinks-clients-public" || n == "searchlinks-public")
//                {
//                    name = "public";
//                }
//                else
//                {
//                    name = tempName;
//                }

//                CloudBlobContainer container = CreateIfContainerNotExists(n);
//                options = options.DefaultSort<CloudBlockBlob>(m => m.Name, Sort.Ascending).DefaultPageSize(50);
//                var blobItems = container.ListBlobs(string.Format("{0}/", name), false); //search in their virtual dir
//                string tempname;
//                foreach (var item in blobItems)
//                {
//                    if (item.GetType() == typeof(CloudBlockBlob))
//                    {
//                        CloudBlockBlob blob = (CloudBlockBlob)item;

//                        switch (blob.Container.Name)
//                        {
//                            case "searchlinks-clients-private":
//                                tempname = _clientService.Find(Convert.ToInt32(blob.Name.Split('/')[0])).Name;
//                                break;
//                            case "searchlinks-clients-public":
//                                tempname = "Public Client Files";
//                                break;
//                            case "searchlinks-private":
//                                tempname = "SearchLinks Private";
//                                break;
//                            default:
//                                tempname = "SearchLinks Public";
//                                break;
//                        }
//                        listOfItems.Add(FileModel.From(blob, tempname));
//                    }
//                }
//            }
//            if (!string.IsNullOrEmpty(searchterm))
//            {
//                var searchStrings = searchterm.Split(new[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
//                foreach (var searchString in searchStrings)
//                {
//                    listOfItems = listOfItems.Where(f => f.CloudBlob.Name.Contains(searchString)).ToList();
//                }
//            }
//            var paged = listOfItems.AsQueryable()
//                    .OrderIfMatches(options.Sort, m => m.CloudBlob.Name)
//                    .OrderIfMatches(options.Sort, m => m.ContainerName)
//                    .TakePage(options.CurrentPage)
//                    .ToList();


//            return new List<FileModel> { Items = paged, Sort = options.Sort, TotalCount = listOfItems.Count, Page = options.CurrentPage };

//        }

//        public PagedResult<FileModel> GetFileList(List<string> name, string searchterm, PageOptions options)
//        {
//            var tempName = name;
//            var containername = new List<string> { };
//            var listOfItems = new List<FileModel>();
//            options = options.DefaultSort<CloudBlockBlob>(m => m.Name, Sort.Ascending).DefaultPageSize(50);
//            for (int i = 0; i < name.Count; i++)
//            {
//                if (name[i] == "public")
//                {
//                    containername.Add("searchlinks-clients-public");
//                }
//                else
//                {
//                    containername.Add("searchlinks-clients-private");
//                }
//                CloudBlobContainer container = CreateIfContainerNotExists(containername[i]);
//                options = options.DefaultSort<CloudBlockBlob>(m => m.Name, Sort.Ascending).DefaultPageSize(50);
//                var blobItems = container.ListBlobs(string.Format("{0}/", name[i]), false); //search in their virtual dir
//                string tempname;
//                foreach (var item in blobItems)
//                {
//                    if (item.GetType() == typeof(CloudBlockBlob))
//                    {
//                        CloudBlockBlob blob = (CloudBlockBlob)item;
//                        switch (blob.Container.Name)
//                        {
//                            case "searchlinks-public":
//                                tempname = "SearchLinks Public";
//                                break;

//                            case "searchlinks-clients-public":
//                                tempname = "Public Client Files";
//                                break;
//                            case "searchlinks-private":
//                                tempname = "SearchLinks Private";
//                                break;
//                            default:
//                                int id;
//                                Int32.TryParse(blob.Name.Split('/')[0], out id);
//                                tempname = _clientService.FindByValidClickClientId(id).Name;
//                                break;
//                        }

//                        listOfItems.Add(FileModel.From(blob, tempname));
//                    }
//                }
//            }




//            if (!string.IsNullOrEmpty(searchterm))
//            {
//                var searchStrings = searchterm.Split(new[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
//                foreach (var searchString in searchStrings)
//                {
//                    listOfItems = listOfItems.Where(f => f.CloudBlob.Name.Contains(searchString)).ToList();
//                }
//            }

//            var paged = listOfItems.AsQueryable()
//                    .OrderIfMatches(options.Sort, m => m.CloudBlob.Name)
//                    .OrderIfMatches(options.Sort, m => m.ContainerName)
//                    .TakePage(options.CurrentPage)
//                    .ToList();


//            return new PagedResult<FileModel> { Items = paged, Sort = options.Sort, TotalCount = listOfItems.Count, Page = options.CurrentPage };
//        }

//        public async Task<bool> DeleteBlob(string blobFileName, string containername)
//        {
//            try
//            {
//                CloudBlobContainer container = CreateIfContainerNotExists(containername);
//                CloudBlockBlob blob = container.GetBlockBlobReference(blobFileName);
//                blob.Delete();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                var wrappedEx = new InuvoException("Unexpected error deleting file.", ex);
//                _loggingService.Log(wrappedEx);
//                throw wrappedEx;
//            }

//        }
//        public bool CheckIfBlobExists(string name, string containername)
//        {
//            CloudBlobContainer container = CreateIfContainerNotExists(containername);
//            var blobref = container.GetBlobReference(name);

//            var exists = blobref.Exists();
//            return exists;

//        }

//        public CloudBlob GetBlob(string name, string containername)
//        {
//            var container = CreateIfContainerNotExists(containername);
//            CloudBlob blob = container.GetBlobReference(name);
//            return blob;
//        }
//    }
//}

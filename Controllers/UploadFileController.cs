using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using NGS;
using NGS.Models;
using NGS.Other;
using FileResult = DataAccess.Model.FileResult;

namespace NGS.Controllers
                                    

    

{
    [Route("api")]
    public class FileUploadController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public FileUploadController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [Route("files")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> UploadFiles(FileDescriptionShort fileDescriptionShort)
        {
          
                foreach (var item in Path.GetInvalidPathChars())
                {
                    fileDescriptionShort.Description.Replace(item, '_');
                }
            Directory.CreateDirectory("input/" + fileDescriptionShort.Description);
           
            var names = new List<string>();
            var contentTypes = new List<string>();
            if (ModelState.IsValid)
            {
  
                foreach (var file in fileDescriptionShort.File)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        contentTypes.Add(file.ContentType);
                        names.Add(fileName);

                        await file.SaveAsAsync(Path.Combine("input/" + fileDescriptionShort.Description, fileName));

                    }
                }
            }

            var files = new DataAccess.Model.FileResult
            {
                FileNames = names,
                ContentTypes = contentTypes,
                Description = fileDescriptionShort.Description,
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
            };

            _fileRepository.AddFileDescriptions(files);


            return RedirectToAction("ViewAllFiles", "FileClient");
        }

        [Route("download/{id}")]
        [HttpGet]
        public FileStreamResult Download(int id)
        {
            var fileDescription = _fileRepository.GetFileDescription(id);

            var path = fileDescription.Description + fileDescription.FileName;
            var stream = new FileStream(path, FileMode.Open);
            return File(stream, fileDescription.ContentType, fileDescription.FileName);
        }

        [Route("downloadResult")]
        [HttpGet]
        public FileStreamResult DownloadResult(string fileName)
        {
            var filename = "";
            var stream = new FileStream(fileName, FileMode.Open);
            var split = fileName.LastIndexOf("/");

            if (split != -1)
            {
                filename = fileName.Substring(split+ 1);
            }
            string contentType = "application/octet-stream";



            return File(stream, contentType, filename);
        }

        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var fileDescription = _fileRepository.GetFileDescription(id);
            var path = fileDescription.FileName;
            System.IO.File.Delete(path);
            _fileRepository.DeleteFileDescription(id);
            return RedirectToAction("ViewAllFiles", "FileClient");
        }

        [Route("quality")]
        [HttpGet]
        public IActionResult Quality()
        {
            var pwd = Directory.GetCurrentDirectory();
            Directory.Delete(pwd + "/results", true);
            Directory.CreateDirectory(pwd + "/results");
            var bash = "docker run --rm -v " + pwd + "/input:/input -v " + pwd + "/results:/working ualrngs/rna-seq-pipeline:2.0 quality";
            var result = bash.Bash();


            return RedirectToAction("ViewResultFiles", "FileClient", new { result = result });
        }
        [Route("assemble")]
        [HttpGet]
        public IActionResult Assemble()
        {
            var pwd = Directory.GetCurrentDirectory();
            Directory.Delete(pwd + "/results", true);
            Directory.CreateDirectory(pwd + "/results");
            var bash = "docker run --rm -v " + pwd + "/input:/input -v " + pwd + "/refGenome/refFile:/indexDir -v " + pwd + "/results:/working ualrngs/rna-seq-pipeline:2.0 assemble full -k -r";

            var result = bash.Bash();


            return RedirectToAction("ViewResultFiles", "FileClient", new { result = result });
        }
    }
}
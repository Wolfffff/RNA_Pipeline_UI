using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Model;
using System.IO;

namespace NGS.Controllers
{
    public class FileClientController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public FileClientController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Index Page";

            return View();
        }


        public ActionResult MultiClient()
        {
            ViewBag.Title = "Fasta Input Client";

            return View();
        }


        public ActionResult ViewAllFiles()
        {
            var model = new NGS.Models.AllUploadedFiles { FileShortDescriptions = _fileRepository.GetAllFiles().ToList() };
            return View(model);
        }

        public ActionResult ViewResultFiles(string result)
        {
            if (result != "")
            {
                ViewBag.Response = result;
            }
            string[] files = Directory.GetFiles("results",
    "*.*",
    SearchOption.AllDirectories);
            return View(files);
        }


    }
}
using System.Collections.Generic;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using NGS.Models;

namespace DataAccess
{
    public interface IFileRepository
    {
        IEnumerable<FileDescriptionShort> AddFileDescriptions(DataAccess.Model.FileResult fileResult);

        IEnumerable<FileDescriptionShort> GetAllFiles();

        FileDescription GetFileDescription(int id);
        void DeleteFileDescription(int id);
    }
}
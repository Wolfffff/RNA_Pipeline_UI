using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BINF.Models
{
    public class FileModel
    {
        public string ContainerName { get; set; }
        public CloudBlockBlob CloudBlob { get; set; }
        public static FileModel From(CloudBlockBlob cb, string containerName)
        {
            return new FileModel() { CloudBlob = cb, ContainerName = containerName };
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class FileListService : IFileListService
    {

        public IEnumerable<FileItem> GetFileList(string folderPath)
        {
            var di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*.*", SearchOption.AllDirectories).Select(i => new FileItem(i));
        }


    }
}

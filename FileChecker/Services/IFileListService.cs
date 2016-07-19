using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileListService
    {
        IEnumerable<FileItem> GetFileList(string folderPath);
    }
}
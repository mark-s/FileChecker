using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileListService
    {
        void PopulateFileList(string leftFolderPath, string rightFolderPath);

        IEnumerable<FilePair> GetFilesInBothSides();

        IEnumerable<FileItem> GetFilesOnlyInLeftSide();

        IEnumerable<FileItem> GetFilesOnlyInRightSide();
    }
}
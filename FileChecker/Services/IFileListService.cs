using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileListService
    {
        void PopulateFileList(ComparisonSettings settings);

        IEnumerable<FilePair> GetFilesInBothSides();

        IEnumerable<FileItem> GetFilesOnlyInLeftSide();

        IEnumerable<FileItem> GetFilesOnlyInRightSide();
    }
}
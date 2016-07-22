using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileListService
    {
        void PopulateFileList(ComparisonSettings settings);

        IList<FilePair> GetFilesInBothSides();

        IList<FileItem> GetFilesOnlyInLeftSide();

        IList<FileItem> GetFilesOnlyInRightSide();
    }
}
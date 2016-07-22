using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class FileListService : IFileListService
    {
        private ReadOnlyCollection<FileItem> _leftSideFiles;
        private ReadOnlyCollection<FileItem> _rightSideFiles;
        private readonly IEqualityComparer<FileItem> _comparer;

        public FileListService(IEqualityComparer<FileItem> comparer)
        {
            _comparer = comparer;
        }

        public void PopulateFileList(ComparisonSettings settings)
        {
            // get left side list of files
            _leftSideFiles = GetFileList(settings.PathToCheckLeft).OrderBy(f => f.FullName).ToList().AsReadOnly();

            // get right side list of files
            _rightSideFiles = GetFileList(settings.PathToCheckRight).OrderBy(f => f.FullName).ToList().AsReadOnly();
        }


        public IList<FilePair> GetFilesInBothSides()
        {
            Validate();

            var query =  from leftFile in _leftSideFiles
                   join rightFile in _rightSideFiles on leftFile.FileNamePartForComparison equals rightFile.FileNamePartForComparison
                   select new FilePair(leftFile, rightFile);
            return query.AsParallel().ToList();
        }

        public IList<FileItem> GetFilesOnlyInLeftSide()
        {
            Validate();
            return _leftSideFiles.Except(_rightSideFiles, _comparer).AsParallel().ToList();
        }

        public IList<FileItem> GetFilesOnlyInRightSide()
        {
            Validate();
            return _rightSideFiles.Except(_leftSideFiles, _comparer).AsParallel().ToList();
        }


        private IEnumerable<FileItem> GetFileList(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            return directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories).AsParallel().Select(i => new FileItem(i, folderPath));
        }

        private void Validate()
        {
            if (_leftSideFiles == null && _rightSideFiles == null)
                throw new InvalidOperationException("Please call PopulateFileList!!");
        }

    }
}

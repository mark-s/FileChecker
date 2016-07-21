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
        private readonly ISession _session;
        private ReadOnlyCollection<FileItem> _leftSideFiles;
        private ReadOnlyCollection<FileItem> _rightSideFiles;
        private readonly IEqualityComparer<FileItem> _comparer;

        public FileListService(ISession session, IEqualityComparer<FileItem> comparer)
        {
            _session = session;
            _comparer = comparer;
        }

        public void PopulateFileList(string leftFolderPath, string rightFolderPath)
        {
            // get left side list of files
            _leftSideFiles = GetFileList(leftFolderPath, _session.Settings.PathToCheckLeft).OrderBy(f => f.FullName).ToList().AsReadOnly();

            // get right side list of files
            _rightSideFiles = GetFileList(rightFolderPath, _session.Settings.PathToCheckRight).OrderBy(f => f.FullName).ToList().AsReadOnly();
        }


        public IEnumerable<FilePair> GetFilesInBothSides()
        {
            Validate();

            return from leftFile in _leftSideFiles
                   join rightFile in _rightSideFiles on leftFile.FileNamePartForComparison equals rightFile.FileNamePartForComparison
                   select new FilePair(leftFile, rightFile);

        }

        public IEnumerable<FileItem> GetFilesOnlyInLeftSide()
        {
            Validate();
            return _leftSideFiles.Except(_rightSideFiles, _comparer);
        }

        public IEnumerable<FileItem> GetFilesOnlyInRightSide()
        {
            Validate();
            return _rightSideFiles.Except(_leftSideFiles, _comparer);
        }


        private IEnumerable<FileItem> GetFileList(string folderPath, string baseFolderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            return directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories).AsParallel().Select(i => new FileItem(i, baseFolderPath));
        }

        private void Validate()
        {
            if (_leftSideFiles == null && _rightSideFiles == null)
                throw new InvalidOperationException("Please call PopulateFileList!!");
        }

    }
}

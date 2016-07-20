using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class FileListService : IFileListService
    {
        private readonly IFileItemNameComparer _nameComparer;
        private ReadOnlyCollection<FileItem> _leftSideFiles;
        private ReadOnlyCollection<FileItem> _rightSideFiles;



        public FileListService(IFileItemNameComparer nameComparer)
        {
            _nameComparer = nameComparer;
        }

        public void PopulateFileList(string leftFolderPath, string rightFolderPath)
        {
            // get left side list of files
            _leftSideFiles = GetFileList(leftFolderPath).OrderBy(f => f.FullName).ToList().AsReadOnly();

            // get right side list of files
            _rightSideFiles = GetFileList(rightFolderPath).OrderBy(f => f.FullName).ToList().AsReadOnly();
        }



        public IEnumerable<FilePair> GetFilesInBothSides()
        {
            Validate();

            foreach (var leftSideFile in _leftSideFiles)
            {
                var rightSideFile = _rightSideFiles.FirstOrDefault(rightFile => _nameComparer.IsNameAndPathTheSame(leftSideFile, rightFile));

                if (rightSideFile == null)
                    continue;

                yield return new FilePair(leftSideFile, rightSideFile);
              
            }
        }


        public IEnumerable<FileItem> GetFilesOnlyInLeftSide()
        {
            Validate();
            return _leftSideFiles.Where(a => !_rightSideFiles.Select(b => b.FileInfo.Name).Contains(a.FileInfo.Name));
        }

        public IEnumerable<FileItem> GetFilesOnlyInRightSide()
        {
            Validate();
            return _rightSideFiles.Where(a => !_leftSideFiles.Select(b => b.FileInfo.Name).Contains(a.FileInfo.Name));
        }


        private IEnumerable<FileItem> GetFileList(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);
            return directoryInfo.EnumerateFiles("*.*", SearchOption.AllDirectories).AsParallel().Select(i => new FileItem(i));
        }

        private void Validate()
        {
            if (_leftSideFiles == null && _rightSideFiles == null)
                throw new InvalidOperationException("Please call PopulateFileList!!");
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using FileChecker.Entities;
using FileChecker.Services;

namespace FileChecker
{
    public class FileCheckerMain : IFileCheckerMain
    {
        private readonly ISession _session;
        private readonly IFileListService _fileListService;
        private readonly IFileHashService _fileHashService;

        private readonly List<FilePair> _filePairs = new List<FilePair>();

        public FileCheckerMain(ISession session, IFileListService fileListService, IFileHashService fileHashService)
        {
            _session = session;
            _fileListService = fileListService;
            _fileHashService = fileHashService;
        }

        public void Go()
        {

            PopulateFilePairList();

            PopulateFileHashValues();

        }

        private void PopulateFilePairList()
        {
            // get left side list of files
            var leftSideFiles = _fileListService.GetFileList(_session.UserArgs.PathToCheckLeft).ToList();

            // get right side list of files
            var rightSideFiles = _fileListService.GetFileList(_session.UserArgs.PathToCheckRight).ToList();


            foreach (var leftSideFile in leftSideFiles)
            {
                var pair = new FilePair
                {
                    LeftFile = leftSideFile,
                    RightFile = rightSideFiles.FirstOrDefault(rightFile => rightFile.Equals(leftSideFile)),
                };
 
                _filePairs.Add(pair);
            }
        }

        private void PopulateFileHashValues()
        {
            foreach (var filePair in _filePairs)
            {
                filePair.LeftFileHash = _fileHashService.GetFileHash(filePair.LeftFile);
                filePair.RightFileHash = _fileHashService.GetFileHash(filePair.RightFile);
            }

        }
    }
}

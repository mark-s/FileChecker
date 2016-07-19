using System.Collections.Generic;
using System.Linq;
using FileChecker.Entities;
using FileChecker.Services;
using FileChecker.Services.ResultOutputters;

namespace FileChecker
{
    public class FileCheckerMain : IFileCheckerMain
    {
        private readonly ISession _session;
        private readonly IFileListService _fileListService;
        private readonly IFileHashService _fileHashService;
        private readonly IFileItemNameComparer _nameComparer;
        private readonly IOutputResults _outputResultService;

        private readonly List<FilePair> _filePairs = new List<FilePair>();

        public FileCheckerMain(ISession session, 
                                         IFileListService fileListService, 
                                         IFileHashService fileHashService,
                                         IFileItemNameComparer nameComparer,
                                         IOutputResults outputResultService)
        {
            _session = session;
            _fileListService = fileListService;
            _fileHashService = fileHashService;
            _nameComparer = nameComparer;
            _outputResultService = outputResultService;
        }

        public void Go()
        {

            PopulateFilePairList();

            PopulateFileHashValues();

            _outputResultService.ProduceResults(_filePairs);
        }

        private void PopulateFilePairList()
        {
            // get left side list of files
            var leftSideFiles = _fileListService.GetFileList(_session.Settings.PathToCheckLeft).ToList();

            // get right side list of files
            var rightSideFiles = _fileListService.GetFileList(_session.Settings.PathToCheckRight).ToList();


            foreach (var leftSideFile in leftSideFiles)
            {
                
                var pair = new FilePair
                {
                    LeftFile = leftSideFile,
                    RightFile = rightSideFiles.FirstOrDefault(rightFile => _nameComparer.Compare(leftSideFile, rightFile)),
                };
 
                _filePairs.Add(pair);
            }
        }

        private void PopulateFileHashValues()
        {
            foreach (var filePair in _filePairs)
            {
                filePair.LeftFile.FileHash = _fileHashService.GetFileHash(filePair.LeftFile);
                filePair.RightFile.FileHash = _fileHashService.GetFileHash(filePair.RightFile);
            }

        }
    }
}

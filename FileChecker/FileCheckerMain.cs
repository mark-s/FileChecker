using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileChecker.Entities;
using FileChecker.Services;


namespace FileChecker
{
    public class FileCheckerMain : IFileCheckerMain
    {
        private readonly IFileListService _fileListService;
        private readonly IFileHashService _fileHashService;
        private readonly IResultsOutputService _resultsOutputService;


        public FileCheckerMain(IFileListService fileListService,
                                         IFileHashService fileHashService,
                                         IResultsOutputService resultsOutputService)
        {
            _fileListService = fileListService;
            _fileHashService = fileHashService;
            _resultsOutputService = resultsOutputService;
        }

        public void Go(ComparisonSettings settings)
        {
            _fileListService.PopulateFileList(settings);

            // this is 
            var filePairs = _fileListService.GetFilesInBothSides().ToList();
            PopulateFileHashValues(filePairs);

            var filesOnlyInLeft = _fileListService.GetFilesOnlyInLeftSide();
            var filesOnlyInRight = _fileListService.GetFilesOnlyInRightSide();

            var results = new ComparisonResults(filePairs, filesOnlyInLeft, filesOnlyInRight);

            _resultsOutputService.OutputResults(results, settings);
        }

        private void PopulateFileHashValues(IEnumerable<FilePair> filePairs)
        {
            Parallel.ForEach(filePairs, pair =>
            {
                pair.LeftFile.FileContentHash = _fileHashService.GetFileHash(pair.LeftFile);
                pair.RightFile.FileContentHash = _fileHashService.GetFileHash(pair.RightFile);
            });

        }
    }
}

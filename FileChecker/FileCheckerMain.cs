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



        public FileCheckerMain(IFileListService fileListService,
                                         IFileHashService fileHashService)
        {
            _fileListService = fileListService;
            _fileHashService = fileHashService;
        }

        public void RunFileCheck(ComparisonSettings settings, IResultsOutputService outputService)
        {
            _fileListService.PopulateFileList(settings);

            // this is not quite the right place for this... 
            var filePairs = _fileListService.GetFilesInBothSides();
            PopulateFileHashValues(filePairs);

            var filesOnlyInLeft = _fileListService.GetFilesOnlyInLeftSide();
            var filesOnlyInRight = _fileListService.GetFilesOnlyInRightSide();

            var results = new ComparisonResults(filePairs, filesOnlyInLeft, filesOnlyInRight);

            outputService.OutputResults(results, settings);
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileChecker.Entities;
using FileChecker.Services;


namespace FileChecker
{
    public class FileCheckerMain : IFileCheckerMain
    {
        private readonly ISession _session;
        private readonly IFileListService _fileListService;
        private readonly IFileHashService _fileHashService;
        private readonly IResultsOutputService _resultsOutputService;


        public FileCheckerMain(ISession session,
                                         IFileListService fileListService,
                                         IFileHashService fileHashService,
                                         IResultsOutputService resultsOutputService)
        {
            _session = session;
            _fileListService = fileListService;
            _fileHashService = fileHashService;
            _resultsOutputService = resultsOutputService;
        }

        public void Go()
        {
            _fileListService.PopulateFileList(_session.Settings.PathToCheckLeft, _session.Settings.PathToCheckRight);

            var filePairs = _fileListService.GetFilesInBothSides().ToList();
            PopulateFileHashValues(filePairs);

            var filesOnlyInLeft = _fileListService.GetFilesOnlyInLeftSide().ToList();
            var filesOnlyInRight = _fileListService.GetFilesOnlyInRightSide().ToList();

            var results = new ComparisonResults(filePairs, filesOnlyInLeft, filesOnlyInRight);

            _resultsOutputService.OutputResults(results, _session.Settings);
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

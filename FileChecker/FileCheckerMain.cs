using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IOutputResults _outputResultService;


        public FileCheckerMain(ISession session,
                                         IFileListService fileListService,
                                         IFileHashService fileHashService,
                                         IOutputResults outputResultService)
        {
            _session = session;
            _fileListService = fileListService;
            _fileHashService = fileHashService;
            _outputResultService = outputResultService;
        }

        public void Go()
        {
            _fileListService.PopulateFileList(_session.Settings.PathToCheckLeft, _session.Settings.PathToCheckRight);

            var filePairs = _fileListService.GetFilesInBothSides().ToList();

            PopulateFileHashValues(filePairs);

            _outputResultService.DeleteExistingResults();

            _outputResultService.ProduceDiff(filePairs, _session.Settings.OnlyShowDiffs, "File Comparsion");

            var filesOnlyInLeft = _fileListService.GetFilesOnlyInLeftSide();
            _outputResultService.ProduceListOfFiles(filesOnlyInLeft.ToList(), "Files Missing From Right");

            var filesOnlyInRight = _fileListService.GetFilesOnlyInRightSide();
            _outputResultService.ProduceListOfFiles(filesOnlyInRight.ToList(), "Files Missing From Left");
        }


        private void PopulateFileHashValues(IEnumerable<FilePair> filePairs)
        {

            Parallel.ForEach(filePairs, pair =>
            {
                pair.LeftFile.FileHash = _fileHashService.GetFileHash(pair.LeftFile);
                pair.RightFile.FileHash = _fileHashService.GetFileHash(pair.RightFile);
            });

        }
    }
}

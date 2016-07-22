using System.Collections.Generic;
using Anotar.Log4Net;
using FileChecker.Entities;
using FileChecker.Services;
using FileChecker.Services.ResultOutputters;
using SimpleInjector;

namespace FileChecker
{
    public class Bootstrapper
    {
        private readonly Container _container;
        private ComparisonSettings _settings;

        public Bootstrapper(Container container)
        {
            _container = container;
        }

        public void Run(ComparisonSettings settings)
        {
            LogTo.Info("Running Bootstrapper");

            _settings = settings;

            RegisterServices();

            // register the main program so the contain will build it up for us
            _container.Register<IFileCheckerMain, FileCheckerMain>(Lifestyle.Singleton);
        }

        private void RegisterServices()
        {
            LogTo.Info("Registering Services - START");
            
             _container.Register<IProgramArgumentsValidator, ProgramArgumentsValidator>(Lifestyle.Singleton);
             _container.Register<IProgramArgumentsParser, ProgramArgumentsParser>(Lifestyle.Singleton);
             _container.Register<IFileHashService, FileHashService>(Lifestyle.Singleton);
             _container.Register<IEqualityComparer<FileItem>, FileItemComparer>(Lifestyle.Singleton);
             _container.Register<IFileListService, FileListService>(Lifestyle.Singleton);
             _container.Register<IResultsOutputService, ResultsOutputService>(Lifestyle.Singleton);
            
            LogTo.Info("Registering Services - END");
        }



        public void SetupOutput()
        {
            var outputService =_container.GetInstance<IResultsOutputService>();
            outputService.AddOutputter(new ResultsConsoleWriter());
            outputService.AddOutputter(new ResultsFileWriter(_settings));

            if (_settings.SendEmailWhenDone)
                outputService.AddOutputter(new EmailSender(_settings));
        }

        public IFileCheckerMain GetMainRunner()
        {
            return _container.GetInstance<IFileCheckerMain>();
        }




    }
}

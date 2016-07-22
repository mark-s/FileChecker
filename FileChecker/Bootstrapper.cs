using System.Collections.Generic;
using Anotar.Log4Net;
using FileChecker.Entities;
using FileChecker.Services;
using SimpleInjector;

namespace FileChecker
{
    public class Bootstrapper
    {
        private readonly Container _container;

        public Bootstrapper(Container container)
        {
            _container = container;
        }

        public void Run(ComparisonSettings settings)
        {
            LogTo.Info("Running Bootstrapper");

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

            LogTo.Info("Registering Services - END");
        }

        public IFileCheckerMain GetMainRunner()
        {
            return _container.GetInstance<IFileCheckerMain>();
        }

        
    }
}

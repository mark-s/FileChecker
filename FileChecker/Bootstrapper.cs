using Anotar.Log4Net;
using FileChecker.Services;
using FileChecker.Services.ResultOutputters;
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

        public void Run()
        {
            LogTo.Info("Running Bootstrapper");

            RegisterServices();

            // register the main program so the contain will build it up for us
            _container.Register<IFileCheckerMain, FileCheckerMain>(Lifestyle.Singleton);
        }

        private void RegisterServices()
        {
            LogTo.Info("Registering Services - START");
            
             _container.Register<IProgramArgumentsParser, ProgramArgumentsParser>(Lifestyle.Singleton);
             _container.Register<ISession, Session>(Lifestyle.Singleton);
             _container.Register<IFileHashService, FileHashService>(Lifestyle.Singleton);
             _container.Register<IFileListService, FileListService>(Lifestyle.Singleton);
             _container.Register<IFileItemNameComparer, FileItemNameComparer>(Lifestyle.Singleton);

            // Console output
            //_container.Register<IOutputResults, ResultsConsoleWriter>(Lifestyle.Singleton);

            // Disk output
            _container.Register<IOutputResults, ResultsFileWriter>(Lifestyle.Singleton);

            LogTo.Info("Registering Services - END");
        }

        public void SetProgramConfig(string[] args)
        {
            // If the args are good then actually parse the args
            var parser =  _container.GetInstance<IProgramArgumentsParser>();
            var session = _container.GetInstance<ISession>();

            session.Settings = parser.ParseArgs(args);
        }

        public IFileCheckerMain GetMainRunner()
        {
            return _container.GetInstance<IFileCheckerMain>();
        }
    }
}

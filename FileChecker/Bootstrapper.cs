﻿using System.Collections.Generic;
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
            
             _container.Register<IProgramArgumentsValidator, ProgramArgumentsValidator>(Lifestyle.Singleton);
             _container.Register<IProgramArgumentsParser, ProgramArgumentsParser>(Lifestyle.Singleton);
             _container.Register<ISession, Session>(Lifestyle.Singleton);
             _container.Register<IFileHashService, FileHashService>(Lifestyle.Singleton);
             _container.Register<IEqualityComparer<FileItem>, FileItemComparer>(Lifestyle.Singleton);
             _container.Register<IFileListService, FileListService>(Lifestyle.Singleton);
             _container.Register<IResultsOutputService, ResultsOutputService>(Lifestyle.Singleton);
            
            LogTo.Info("Registering Services - END");
        }

        public ComparisonSettings SetProgramConfig(string[] args)
        {
            // parse the args to get the settings file location 
            var parser =  _container.GetInstance<IProgramArgumentsParser>();
            var settingsFile = parser.GetSettingsFileLocation(args);

            // and then load the settings from the file and put them in the session
            var settingsLoader = new JsonSettingsLoader();
            var session = _container.GetInstance<ISession>();
            session.Settings = settingsLoader.GetStoredSettings(settingsFile);

            return session.Settings;
        }

        public void SetupOutput(ComparisonSettings settings)
        {
            var outputService =_container.GetInstance<IResultsOutputService>();
            outputService.AddOutputter(new ResultsConsoleWriter());
            outputService.AddOutputter(new ResultsFileWriter(settings));

            if (settings.SendEmailWhenDone)
                outputService.AddOutputter(new EmailSender(settings));
        }

        public IFileCheckerMain GetMainRunner()
        {
            return _container.GetInstance<IFileCheckerMain>();
        }




    }
}

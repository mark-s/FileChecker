using System;
using System.Diagnostics;
using Anotar.Log4Net;
using FileChecker.Entities;
using FileChecker.Services;
using FileChecker.Services.ResultOutputters;
using log4net.Config;
using SimpleInjector;

namespace FileChecker
{
    public class Program
    {


        public static void Main(string[] args)
        {
            InitLogging();

            // Check the arguments, before we do anything else - bail out with error message if they're bad
            var validator = new ProgramArgumentsValidator();
            var validationResult = validator.ValidateArgs(args);
            if (validationResult.IsValid == false)
            {
                Console.WriteLine(validationResult.Message);
                PromptForClose();
                return;
            }

            // load the config file we got from the command line and validate it's good
            var settings = LoadSettingsFromSettingsFile(args, new ProgramArgumentsParser(validator), new JsonSettingsLoader());

            // setup the output(s) as required by the args so we can output the results of the validation
            var outputService = SetupOutputService(settings);

            var settingsValidationResult = ComparisonSettings.Validate(settings);
            if (settingsValidationResult.IsValid == false)
            {
                Console.WriteLine(settingsValidationResult.Message);
                outputService.OutputError(settingsValidationResult.Message);
                PromptForClose();
                return;
            }
            

            // Create a new Simple Injector container
            var container = new Container();

            // pass it into the boostrapper for population and populate it!
            var bootstrapper = new Bootstrapper(container);
            bootstrapper.Run(settings);

            // Let's get to work
            var mainRunner = bootstrapper.GetMainRunner();

            try
            {
                mainRunner.RunFileCheck(settings, outputService);
            }
            catch (Exception ex)
            {
                LogTo.FatalException("Exception!", ex);
                outputService.OutputError(ex.Message);
            }

            PromptForClose();
        }


        public static ComparisonSettings LoadSettingsFromSettingsFile(string[] args, IProgramArgumentsParser argumentsParser, ISettingsProvider<ComparisonSettings> settingsFileLoader)
        {
            // parse the args to get the settings file location 
            var settingsFileLocation = argumentsParser.GetSettingsFileLocation(args);

            // and then load the settings from the file
            return settingsFileLoader.GetStoredSettings(settingsFileLocation);
        }

        public static IResultsOutputService SetupOutputService(ComparisonSettings settings)
        {
            var outputService = new ResultsOutputService(new TimeStampService());

            outputService.AddOutputter(new ResultsConsoleWriter());
            outputService.AddOutputter(new ResultsFileWriter(settings));

            if (settings.SendEmailWhenDone)
                outputService.AddOutputter(new EmailSender(settings));

            return outputService;
        }


        private static void InitLogging()
        {
            // Init the logging provider
            XmlConfigurator.Configure();
            LogTo.Info("START!");
        }


        // this is DEBUG only because this is destined to run on a headless server
        [Conditional("DEBUG")]
        private static void PromptForClose()
        {
            Console.Write("Press enter to close");
            Console.ReadLine();
        }

    }
}

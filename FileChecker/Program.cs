using System;
using System.Diagnostics;
using Anotar.Log4Net;
using FileChecker.Services;
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

            // Create a new Simple Injector container
            var container = new Container();

            // pass it into the boostrapper for population and populate it!
            var bootstrapper = new Bootstrapper(container);
            bootstrapper.Run();
            bootstrapper.SetProgramConfig(args);

            // Let's get to work
            var mainRunner = bootstrapper.GetMainRunner();

            try
            {
                mainRunner.Go();
            }
            catch (Exception ex)
            {
                LogTo.FatalException("Exception!", ex);
                // TODO: maybe send an email here if configured ...
            }

            PromptForClose();
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

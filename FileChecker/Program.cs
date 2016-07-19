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

            // Check the arguments - bail out with error message if they're bad
            var validator = new ProgramArgumentsValidator();
            var validationResult = validator.ValidateArgs(args);
            if (validationResult.IsValid == false)
            {
                Console.WriteLine(validationResult.Message);
                Console.ReadLine();
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
            mainRunner.Go();

            Console.ReadLine();
        }

        private static void InitLogging()
        {
            // Init the logging provider
            XmlConfigurator.Configure();
           LogTo.Info("START!");
        }


    }
}

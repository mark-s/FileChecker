using System.Collections.Generic;
using System.IO;
using System.Linq;
using Anotar.Log4Net;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class ProgramArgumentsValidator : IProgramArgumentsValidator
    {

        public ValidationResult ValidateArgs(IList<string> args)
        {
            var isValid = true;
            var message = "";

            // if there's no argument providede - just bail out!
            if (args.Count != 1) return new ValidationResult(false, GetUsageText());
        

            var settingsFileFileName = args[0];

            if (File.Exists(settingsFileFileName) == false)
            {
                isValid = false;
                message = "Please provide a valid settings file location";
            }

            if (isValid)
                LogTo.Info("Arguments provided to program: " + args.Aggregate((current, next) => current + " | " + next));
            else
                LogTo.Fatal("Arguments provided are invalid. Error message: " + message);

            return new ValidationResult(isValid, message);
        }



        private string GetUsageText()
        {
            return "USAGE: FileChecker \"c:\\some folder\\settings.json\"";
        }


    }
}

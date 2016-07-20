using System;
using System.Collections.Generic;
using System.IO;
using Anotar.Log4Net;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class ProgramArgumentsValidator : IProgramArgumentsValidator
    {



        public ArgsValidationResult ValidateArgs(IList<string> args)
        {
            var isValid = true;
            var message = "";

            if (args.Count != 4)
            {
                isValid = false;
                message = GetUsageText();
                LogTo.Error("Invalid number arguments passed in");
            }
            else if (Directory.Exists(args[0]) == false)
            {
                isValid = false;
                message = String.Format("Left Side Path to check: [{0}] does not exist!", args[0]);
                LogTo.Error(message);
            }
            else if (Directory.Exists(args[1]) == false)
            {
                isValid = false;
                message = String.Format("Right Side Path to check: [{0}] does not exist!", args[1]);
                LogTo.Error(message);
            }

            if (isValid)
                LogTo.Info("Arguments validated ok: [{0}]\t[{1}]", args[0], args[1]);

            return new ArgsValidationResult(isValid, message);
        }

        private  string GetUsageText()
        {
            return "USAGE: FileChecker \"LEFT SIDE PATHTOCHECK\" \"RIGHT SIDE PATHTOCHECK\" \"OUTPUT.TXT\" TRUE\\FALSE (diffs Only)";
        }
    }
}

using System;
using System.IO;
using Anotar.Log4Net;

namespace FileChecker.Entities
{
    public class ComparisonSettings
    {
        public string PathToCheckLeft { get; private set; }
        public string PathToCheckRight { get; private set; }

        public string ResultsOutputFile { get; private set; }

        public bool OnlyShowDiffs { get; private set; }

        public bool SendEmailWhenDone { get; private set; }
        public string EmailTo { get; private set; }
        public string EmailServer { get; private set; }


        public ComparisonSettings(string pathToCheckLeft, string pathToCheckRight, string resultsOutputFile,
                                                bool onlyShowDiffs, bool sendEmailWhenDone, string emailTo, string emailServer)
        {
            PathToCheckLeft = pathToCheckLeft;

            PathToCheckRight = pathToCheckRight;

            ResultsOutputFile = resultsOutputFile;

            OnlyShowDiffs = onlyShowDiffs;

            SendEmailWhenDone = sendEmailWhenDone;

            EmailTo = emailTo;

            EmailServer = emailServer;

            LogTo.Info("Using Settings: ");
            LogTo.Info(" PathToCheckLeft\t" + PathToCheckLeft);
            LogTo.Info(" PathToCheckRight\t" + PathToCheckRight);
            LogTo.Info(" ResultsOutputFile\t" + ResultsOutputFile);
            LogTo.Info(" OnlyShowDiffs\t" + OnlyShowDiffs);
            LogTo.Info(" SendEmailWhenDone\t" + SendEmailWhenDone);
            LogTo.Info(" EmailTo\t" + EmailTo);
            LogTo.Info(" EmailServer\t" + EmailServer);
        }


        public static ValidationResult Validate(ComparisonSettings settings)
        {
            var isValid = true;
            var message = "";


            if (String.IsNullOrEmpty(settings.PathToCheckLeft) || Directory.Exists(settings.PathToCheckLeft) == false)
            {
                isValid = false;
                message += String.Format("Left Side Path to check: [{0}] is not valid / does not exist!{1}", settings.PathToCheckLeft, Environment.NewLine);
            }

            if (String.IsNullOrEmpty(settings.PathToCheckRight) || Directory.Exists(settings.PathToCheckRight) == false)
            {
                isValid = false;
                message += String.Format("Right Side Path to check: [{0}] is not valid / does not exist!{1}", settings.PathToCheckRight, Environment.NewLine);
            }

            var outputFilePath = Path.GetDirectoryName(settings.ResultsOutputFile);
            if (String.IsNullOrEmpty(outputFilePath) || Directory.Exists(outputFilePath) == false)
            {
                isValid = false;
                message += String.Format("Output File Folder: [{0}] is not valid / does not exist!{1}", outputFilePath, Environment.NewLine);
            }

            if (settings.SendEmailWhenDone == true && (String.IsNullOrEmpty(settings.EmailTo) || String.IsNullOrEmpty(settings.EmailServer)))
            {
                isValid = false;
                message += String.Format("When SendEmailWhenDone is set to true, EmailTo and EmailServer must be set too.{0}", Environment.NewLine);
            }

            LogTo.Error(message);


            return new ValidationResult(isValid, message);
        }


    }
}
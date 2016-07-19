using Anotar.Log4Net;

namespace FileChecker.Entities
{
    public class FileCheckerUserArgs
    {
        public string PathToCheckLeft { get; }
        public string PathToCheckRight { get; }
        public string ResultsOutputFilePath { get; }

        public FileCheckerUserArgs(string pathToCheckLeft, string pathToCheckRight, string resultsOutputFilePath)
        {
            PathToCheckLeft = pathToCheckLeft;

            PathToCheckRight = pathToCheckRight;

            ResultsOutputFilePath = resultsOutputFilePath;

            LogTo.Info("Set User Args to: ");
            LogTo.Info(" PathToCheckLeft\t" + PathToCheckLeft);
            LogTo.Info(" PathToCheckRight\t" + pathToCheckRight);
            LogTo.Info(" ResultsOutputFilePath\t" + resultsOutputFilePath);
        }

    }
}
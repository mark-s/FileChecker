using Anotar.Log4Net;

namespace FileChecker.Entities
{
    public class ComparisonSettings
    {
        public string PathToCheckLeft { get; private set; }
        public string PathToCheckRight { get; private set; }
        public string ResultsOutputFilePath { get; private set; }

        public ComparisonSettings(string pathToCheckLeft, string pathToCheckRight, string resultsOutputFilePath)
        {
            PathToCheckLeft = pathToCheckLeft;

            PathToCheckRight = pathToCheckRight;

            ResultsOutputFilePath = resultsOutputFilePath;

            LogTo.Info("Set User Args to: ");
            LogTo.Info(" PathToCheckLeft\t" + PathToCheckLeft);
            LogTo.Info(" PathToCheckRight\t" + PathToCheckRight);
            LogTo.Info(" ResultsOutputFilePath\t" + ResultsOutputFilePath);
        }

    }
}
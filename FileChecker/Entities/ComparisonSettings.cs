using Anotar.Log4Net;

namespace FileChecker.Entities
{
    public class ComparisonSettings
    {
        public string PathToCheckLeft { get; private set; }
        public string PathToCheckRight { get; private set; }
        public string ResultsOutputFilePath { get; private set; }
        public bool OnlyShowDiffs { get; private set; }

        public ComparisonSettings(string pathToCheckLeft, string pathToCheckRight, string resultsOutputFilePath, bool onlyShowDiffs)
        {
            PathToCheckLeft = pathToCheckLeft;

            PathToCheckRight = pathToCheckRight;

            ResultsOutputFilePath = resultsOutputFilePath;

            OnlyShowDiffs = onlyShowDiffs;

            LogTo.Info("Set User Args to: ");
            LogTo.Info(" PathToCheckLeft\t" + PathToCheckLeft);
            LogTo.Info(" PathToCheckRight\t" + PathToCheckRight);
            LogTo.Info(" ResultsOutputFilePath\t" + ResultsOutputFilePath);
            LogTo.Info(" OnlyShowDiffs\t" + OnlyShowDiffs);
        }

    }
}
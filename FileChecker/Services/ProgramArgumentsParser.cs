using Anotar.Log4Net;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class ProgramArgumentsParser : IProgramArgumentsParser
    {
        public ComparisonSettings ParseArgs(string[] args)
        {
            LogTo.Info("Parsing arguments");

            return new ComparisonSettings(args[0], args[1], args[2]);
        }

    }
}

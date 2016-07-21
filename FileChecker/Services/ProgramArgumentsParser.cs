using System;

namespace FileChecker.Services
{
    public class ProgramArgumentsParser : IProgramArgumentsParser
    {
        private readonly IProgramArgumentsValidator _validator;

        public ProgramArgumentsParser(IProgramArgumentsValidator validator)
        {
            _validator = validator;
        }

        public string GetSettingsFileLocation(string[] args)
        {
            // this should have already been validated, but just to make sure let's do it again
            if (_validator.ValidateArgs(args).IsValid == false)
                throw new ArgumentException("passed in arguments are invalid!", "args");

            return args[0];

        }
    }
}

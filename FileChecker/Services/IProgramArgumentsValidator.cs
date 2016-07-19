using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IProgramArgumentsValidator
    {
        ArgsValidationResult ValidateArgs(IList<string> args);
    }
}
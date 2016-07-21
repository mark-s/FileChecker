using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IProgramArgumentsValidator
    {
        ValidationResult ValidateArgs(IList<string> args);
    }
}
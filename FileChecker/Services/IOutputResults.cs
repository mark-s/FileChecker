using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IOutputResults
    {
        void ProduceResults(IList<FilePair> filePairs);
    }
}
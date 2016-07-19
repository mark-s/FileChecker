using System.Collections.Generic;
using FileChecker.Entities;

namespace FileChecker.Services.ResultOutputters
{
    public interface IOutputResults
    {
        void ProduceResults(IList<FilePair> filePairs);
    }
}
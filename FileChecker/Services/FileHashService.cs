using System.Security.Cryptography;
using Anotar.Log4Net;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class FileHashService : IFileHashService
    {

        public byte[] GetFileHash(FileItem file)
        {
            LogTo.Info("Calculating hash for file\t" + file.FileInfo.FullName);

            var sha1Managed = new SHA1Managed();
            return sha1Managed.ComputeHash(file.FileInfo.OpenRead());
            
        }

    }
}
using System.Security.Cryptography;
using Anotar.Log4Net;
using FileChecker.Entities;

namespace FileChecker.Services
{
    public class FileHashService : IFileHashService
    {

        public byte[] GetFileHash(FileItem file)
        {
            if (file == null)
            {
                LogTo.Warn("file passed in is NULL - returning empty hash");
                return new byte[0];
            }

            LogTo.Info("Calculating hash for file\t" + file.FullName);

            using (var sha1Managed = new SHA1Managed())
            {
                return sha1Managed.ComputeHash(file.FileInfo.OpenRead());
            }
            
        }

    }
}
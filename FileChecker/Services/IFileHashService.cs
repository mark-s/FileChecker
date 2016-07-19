using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileHashService
    {
        byte[] GetFileHash(FileItem file);
    }
}
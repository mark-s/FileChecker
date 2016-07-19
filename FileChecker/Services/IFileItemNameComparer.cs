using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileItemNameComparer
    {
        bool Compare(FileItem leftFile, FileItem rightFile);
    }
}
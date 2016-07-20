using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileItemNameComparer 
    {
        bool IsNameAndPathTheSame(FileItem leftFile, FileItem rightFile);
    }
}
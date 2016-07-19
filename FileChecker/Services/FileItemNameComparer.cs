using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface IFileItemNameComparer
    {
        bool Compare(FileItem leftFile, FileItem rightFile);
    }

    public class FileItemNameComparer : IFileItemNameComparer
    {
        private readonly ISession _session;

        public FileItemNameComparer(ISession session)
        {
            _session = session;
        }

        public bool Compare(FileItem leftFile, FileItem rightFile)
        {

            var filePartToCompareLeft = leftFile.FileInfo.FullName.Replace(_session.UserArgs.PathToCheckLeft, "");
            var filePartToCompareRight = rightFile.FileInfo.FullName.Replace(_session.UserArgs.PathToCheckRight, "");

            return filePartToCompareLeft == filePartToCompareRight;
        }

    }
}

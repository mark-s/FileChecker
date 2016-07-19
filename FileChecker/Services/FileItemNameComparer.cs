using FileChecker.Entities;

namespace FileChecker.Services
{
    public class FileItemNameComparer : IFileItemNameComparer
    {
        private readonly ISession _session;

        public FileItemNameComparer(ISession session)
        {
            _session = session;
        }

        public bool Compare(FileItem leftFile, FileItem rightFile)
        {

            var filePartToCompareLeft = leftFile.FileInfo.FullName.Replace(_session.Settings.PathToCheckLeft, "");
            var filePartToCompareRight = rightFile.FileInfo.FullName.Replace(_session.Settings.PathToCheckRight, "");

            return filePartToCompareLeft == filePartToCompareRight;
        }

    }
}

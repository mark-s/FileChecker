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

        public bool IsNameAndPathTheSame(FileItem leftFile, FileItem rightFile)
        {
            var filePartToCompareLeft = GetPartOfPathToCompare(leftFile.FileInfo.FullName, _session.Settings.PathToCheckLeft);
            var filePartToCompareRight = GetPartOfPathToCompare(rightFile.FileInfo.FullName, _session.Settings.PathToCheckRight);

            return filePartToCompareLeft == filePartToCompareRight;
        }

        private string GetPartOfPathToCompare(string fullPathOfFile, string fullPathFromSettings)
        {
            return fullPathOfFile.Replace(fullPathFromSettings, "");
        }

    }
}

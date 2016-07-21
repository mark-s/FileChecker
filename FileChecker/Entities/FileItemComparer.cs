namespace FileChecker.Entities
{
    // custom comparer to make the LINQ easy
    public class FileItemComparer : System.Collections.Generic.IEqualityComparer<FileItem>
    {
        public bool Equals(FileItem f1, FileItem f2)
        {
            return (f1.FileNamePartForComparison == f2.FileNamePartForComparison);
        }

        public int GetHashCode(FileItem obj)
        {
            return obj.FileNamePartForComparison.GetHashCode();
        }
    }
}
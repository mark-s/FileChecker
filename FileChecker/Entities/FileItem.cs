using System;
using System.IO;

namespace FileChecker.Entities
{
    public class FileItem : IEquatable<FileItem>
    {
        public FileInfo FileInfo { get; private set; }
        public string PathToCompare { get;  set; }
        public string FileName { get; private set; }

        public FileItem(FileInfo fileInfo)
        {
            FileInfo = fileInfo;

            FileName = FileInfo.Name;
        }

        public bool Equals(FileItem other)
        {
            bool isPathSame = other.PathToCompare == this.PathToCompare;
            bool isFileNameSame = other.FileName == this.FileName;

            return isFileNameSame && isPathSame;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashPath = PathToCompare == null ? 0 : PathToCompare.GetHashCode();

            //Get hash code for the Code field.
            int hashFileName = FileName == null ? 0 : FileName.GetHashCode();

            //Calculate the hash code for the product.
            return hashPath ^ hashFileName;
        }

    }
}
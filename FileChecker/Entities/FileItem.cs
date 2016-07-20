using System;
using System.IO;

namespace FileChecker.Entities
{
    public class FileItem 
    {
        public FileInfo FileInfo { get; private set; }
        public string FullName { get; private set; }

        public string FileNamePartForComparison { get; private set; }

        public byte[] FileHash { get; set; }

        public bool IsInLeftSide { get; set; }
        public bool IsInRightSide { get; set; }

        public string FileHashString
        {
            get { return BitConverter.ToString(FileHash); }
        }

        public FileItem(FileInfo fileInfo, string basePathToExclude)
        {
            FileInfo = fileInfo;

            FullName = FileInfo.FullName;

            FileNamePartForComparison = FileInfo.FullName.Replace(basePathToExclude , "");
        }

    }
}
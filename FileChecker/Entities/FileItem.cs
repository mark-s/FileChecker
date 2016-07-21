using System;
using System.IO;

namespace FileChecker.Entities
{
    public class FileItem 
    {
        public FileInfo FileInfo { get; private set; }
        public string FullName { get; private set; }

        public string FileNamePartForComparison { get; private set; }

        public byte[] FileContentHash { get; set; }
        public string FileContentHashString
        {
            // strip out the default BitConverter's '-'s
            get { return BitConverter.ToString(FileContentHash).Replace("-",""); }
        }


        public FileItem(FileInfo fileInfo, string basePathToExclude)
        {
            FileInfo = fileInfo;

            FullName = FileInfo.FullName;

            FileNamePartForComparison = FileInfo.FullName.Replace(basePathToExclude , "");
        }

    }
}
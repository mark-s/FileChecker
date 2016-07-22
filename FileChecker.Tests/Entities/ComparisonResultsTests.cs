using System.Collections.Generic;
using System.IO;
using FileChecker.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace FileChecker.Tests.Entities
{
    [TestFixture]
    public class ComparisonResultsTests
    {
        private List<FilePair> _testFilePairs;
        private FileInfo _fileInfo1;
        private string _basePathToExclude;
        private FileInfo _fileInfo2;
        private string _basePathToExclude2;
        private FilePair _filePair;
        private FileItem _fileItem1;
        private FileItem _fileItem2;

        [SetUp]
        public virtual void SetUp()
        {
            _fileInfo1 = new FileInfo("some file path");
            _basePathToExclude = @"c:\BASEPATH\HERE";
             _fileItem1 = new FileItem(_fileInfo1, _basePathToExclude);

            _fileInfo2 = new FileInfo("some file path");
            _basePathToExclude2 = @"c:\BASEPATH\HERE";
            _fileItem2 = new FileItem(_fileInfo2, _basePathToExclude2);

            _filePair = new FilePair(_fileItem1, _fileItem2);

            _testFilePairs = new List<FilePair> { _filePair };
        }


        [Test]
        public void Constructor_GoodValues_FilePairsPropertySet()
        {
            // Arrange

            // Act
            var comparisonResults = new ComparisonResults(_testFilePairs, 
                                                                                    new List<FileItem> {_fileItem1},
                                                                                    new List<FileItem> {_fileItem2});

            // Assert
            comparisonResults.FilePairs.ShouldBeEquivalentTo(_testFilePairs);

        }

        [Test]
        public void Constructor_GoodValues_FilesOnlyInLeftPropertySet()
        {
            // Arrange
            var filesOnlyInLeft = new List<FileItem> {_fileItem1};

            // Act
            var comparisonResults = new ComparisonResults(_testFilePairs,
                                                                                    filesOnlyInLeft,
                                                                                    new List<FileItem> { _fileItem2 });

            // Assert
            comparisonResults.FilesOnlyInLeft.ShouldBeEquivalentTo(filesOnlyInLeft);
        }

        [Test]
        public void Constructor_GoodValues_FilesOnlyInRightPropertySet()
        {
            // Arrange
            var filesOnlyInRight = new List<FileItem> { _fileItem2 };

            // Act
            var comparisonResults = new ComparisonResults(_testFilePairs,
                                                                                    new List<FileItem> { _fileItem1 },
                                                                                    filesOnlyInRight);

            // Assert
            comparisonResults.FilesOnlyInRight.ShouldBeEquivalentTo(filesOnlyInRight);
        }

    }
}

using Xunit;
using System;
using DotNetFileSystemWrapper;

namespace DotNetFileSystemWrapper.Tests.File
{
    public class ExistsTests : IDisposable {
        private IFileSystem _fs;

        public ExistsTests() {
            AssureRootPathExists();
            _fs = new FileSystemFactory().PhysicalFileSystem();
        }

        public void Dispose() {
            ClearRootPath();
        }

        private static string RootPath() {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "TestRoot");
        }

        private static void AssureRootPathExists() {
            System.IO.Directory.CreateDirectory(RootPath());            
        }

        private static void ClearRootPath() {
            System.IO.Directory.Delete(RootPath(), recursive: true);
        }

        private string PathRelativeToRoot(string filename) {
            return System.IO.Path.Combine(RootPath(), filename);
        }

        [Fact]
        public void NonExistingFileShouldReturnFalse() {
            var exists = _fs.File.Exists("filename_of_non_existing_file.txt");

            Assert.False(exists);
        }

        [Fact]
        public void ExistingFileShouldReturnTrue() {
            var path = PathRelativeToRoot("file_name.txt");
            _fs.File.WriteAllText(path, "content");
            var exists = _fs.File.Exists(path);

            Assert.True(exists);
        }
    }
}
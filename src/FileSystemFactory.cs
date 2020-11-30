﻿using System;

namespace src
{
    public interface IFileSystem {
        IFile File {get;}
    }

    public interface IFile {
        bool Exists(string path);
        void WriteAllText(string path, string contents);
    }

    public class FileSystemFactory
    {
        public IFileSystem PhysicalFileSystem() =>
            new PhysicalFileSystem();
    }

    internal class PhysicalFileSystem : IFileSystem
    {
        private class PhysicalFile : IFile
        {
            public bool Exists(string path)
            {
                return System.IO.File.Exists(path);
            }

            public void WriteAllText(string path, string contents) {
                System.IO.File.WriteAllText(path, contents);
            }
        }
        public IFile File => new PhysicalFile();
    }
}

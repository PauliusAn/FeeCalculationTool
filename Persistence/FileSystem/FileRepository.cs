﻿using System.IO;

namespace Persistence.FileSystem
{
    public class FileRepository
    {
        private const string FilePath = "transactions.txt";
        private StreamReader _fileStreamReader;

        public string ReadNextLine()
        {
            if (_fileStreamReader == null)
            {
                _fileStreamReader = new StreamReader(FilePath);
            }

            if (_fileStreamReader.EndOfStream)
            {
                _fileStreamReader.Dispose();
                return null;
            }

            return _fileStreamReader.ReadLine();
        }
    }
}

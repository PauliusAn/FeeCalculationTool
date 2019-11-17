using System.IO;

namespace Persistence.FileSystem
{
    public class FileReader : IFileReader
    {
        private readonly string _filePath;
        private StreamReader _fileStreamReader;

        public FileReader(string filePath)
        {
            _filePath = filePath;
        }

        public string ReadNextLine()
        {
            if (_fileStreamReader == null)
            {
                _fileStreamReader = new StreamReader(_filePath);
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

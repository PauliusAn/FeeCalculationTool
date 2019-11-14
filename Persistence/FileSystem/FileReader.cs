using System.IO;

namespace Persistence.FileSystem
{
    public class FileReader : IFileReader
    {
        private const string FilePath = "../../../../transactions.txt";
        private StreamReader _fileStreamReader;

        // Todo: Validate the read line to be sure it's a valid transaction format

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

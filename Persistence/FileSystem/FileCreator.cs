using System.IO;

namespace Persistence.FileSystem
{
    public class FileCreator: IFileCreator
    {
        public bool CreateFileIfNotExists(string path)
        {
            if (File.Exists(path))
            {
                return false;
            }

            File.Create(path).Close();
            return true;
        }
    }
}

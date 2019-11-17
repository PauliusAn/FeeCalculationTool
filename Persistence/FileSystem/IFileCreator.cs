namespace Persistence.FileSystem
{
    public interface IFileCreator
    {
        bool CreateFileIfNotExists(string path);
    }
}

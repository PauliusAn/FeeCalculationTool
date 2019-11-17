using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.FileSystem
{
    public interface IFileCreator
    {
        bool CreateFileIfNotExists(string path);
    }
}

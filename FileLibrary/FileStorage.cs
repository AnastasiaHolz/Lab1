using System.IO;
using FileLibrary.Interfaces;

namespace FileLibrary
{
    public class FileStorage : IFileStorage
    {
        public string[] ReadAllLines(string path)
        {
            if (!File.Exists(path)) return new string[0];
            return File.ReadAllLines(path);
        }

        public void SaveAllLines(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }
    }
}

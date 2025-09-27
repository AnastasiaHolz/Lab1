namespace FileLibrary.Interfaces
{
    public interface IFileStorage
    {
        string[] ReadAllLines(string path);
        void SaveAllLines(string path, string[] lines);
    }
}

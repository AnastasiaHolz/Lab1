using ConsoleApp.Models;
using FileLibrary.Interfaces;
using ConsoleApp.Utils;

namespace ConsoleApp.Business
{
    public class Repository : IRepository
    {
        private readonly IFileStorage _storage;
        private readonly string _filePath;

        public Repository(IFileStorage storage, string filePath)
        {
            _storage = storage;
            _filePath = filePath;
        }

        public Person[] LoadAll()
        {
            var lines = _storage.ReadAllLines(_filePath);
            return Parser.ParseEntities(lines);
        }

        public void SaveAll(Person[] arr)
        {
            var lines = Parser.EntitiesToLines(arr);
            _storage.SaveAllLines(_filePath, lines);
        }

        public void Add(Person p)
        {
            var cur = LoadAll();
            int n = cur.Length;
            Person[] next = new Person[n + 1];
            for (int i = 0; i < n; i++) next[i] = cur[i];
            next[n] = p;
            SaveAll(next);
        }

        public void DeleteAt(int index)
        {
            var cur = LoadAll();
            if (index < 0 || index >= cur.Length) return;
            int n = cur.Length;
            Person[] next = new Person[n - 1];
            int j = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == index) continue;
                next[j++] = cur[i];
            }
            SaveAll(next);
        }

        public Person[] FindByLastName(string lastName)
        {
            var cur = LoadAll();
            Person[] res = new Person[0];
            for (int i = 0; i < cur.Length; i++)
            {
                if (string.Equals(cur[i].LastName, lastName, StringComparison.OrdinalIgnoreCase))
                    Append(ref res, cur[i]);
            }
            return res;
        }

        public Student FindStudentById(string studentId)
        {
            var cur = LoadAll();
            for (int i = 0; i < cur.Length; i++)
            {
                if (cur[i] is Student s && string.Equals(s.StudentId, studentId, StringComparison.OrdinalIgnoreCase))
                    return s;
            }
            return null;
        }

        private void Append(ref Person[] arr, Person p)
        {
            int old = arr.Length;
            Array.Resize(ref arr, old + 1);
            arr[old] = p;
        }
    }
}

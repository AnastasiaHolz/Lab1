using ConsoleApp.Models;

namespace ConsoleApp.Business
{
    public interface IRepository
    {
        Person[] LoadAll();
        void SaveAll(Person[] arr);
        void Add(Person p);
        void DeleteAt(int index);
        Person[] FindByLastName(string lastName);
        Student FindStudentById(string studentId);
    }
}

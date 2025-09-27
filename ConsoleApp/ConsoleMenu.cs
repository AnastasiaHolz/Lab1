using System;
using FileLibrary;
using ConsoleApp.Business;
using ConsoleApp.Models;
using ConsoleApp.Utils;

namespace ConsoleApp
{
    public class ConsoleMenu
    {
        private readonly IRepository _repo;

        public ConsoleMenu(IRepository repo)
        {
            _repo = repo;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("=== Меню ===");
                Console.WriteLine("1. Показати всі записи");
                Console.WriteLine("2. Додати студента");
                Console.WriteLine("3. Додати музиканта");
                Console.WriteLine("4. Додати пілота");
                Console.WriteLine("5. Видалити запис за індексом");
                Console.WriteLine("6. Пошук за прізвищем");
                Console.WriteLine("7. Пошук студента за ID");
                Console.WriteLine("8. Обчислити кількість студенток 1-го курсу у гуртожитку");
                Console.WriteLine("0. Вихід");
                Console.Write("Вибір: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": ShowAll(); break;
                    case "2": AddStudent(); break;
                    case "3": AddMusician(); break;
                    case "4": AddPilot(); break;
                    case "5": DeleteAt(); break;
                    case "6": SearchByLastName(); break;
                    case "7": SearchByStudentId(); break;
                    case "8": CountFirstCourseGirlsInDorm(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір"); break;
                }
                Console.WriteLine();
            }
        }

        private void ShowAll()
        {
            var arr = _repo.LoadAll();
            if (arr.Length == 0) { Console.WriteLine("База порожня."); return; }
            for (int i = 0; i < arr.Length; i++)
            {
                var p = arr[i];
                Console.WriteLine($"[{i}] {p.GetType().Name}: {p.FirstName} {p.LastName}");
                if (p is Student s)
                    Console.WriteLine($"    ID={s.StudentId}, Course={s.Course}, Gender={s.Gender}, Residence={s.Residence}");
                if (p is Musician m) Console.WriteLine($"    Instrument={m.Instrument}");
                if (p is Pilot pl) Console.WriteLine($"    License={pl.License}");
            }
        }

        private void AddStudent()
        {
            Console.Write("FirstName: ");
            string fn = Console.ReadLine() ?? "";
            Console.Write("LastName: ");
            string ln = Console.ReadLine() ?? "";
            Console.Write("StudentId (AA123456): ");
            string sid = Console.ReadLine() ?? "";
            Console.Write("Course (1-6): ");
            string cstr = Console.ReadLine() ?? "1";
            int.TryParse(cstr, out int course);
            Console.Write("Gender (ч/ж): ");
            string gender = Console.ReadLine() ?? "";
            Console.Write("Residence (e.g. 2.305 or address): ");
            string res = Console.ReadLine() ?? "";

            try
            {
                var s = new Student(fn, ln, sid, course, gender, res);
                _repo.Add(s);
                Console.WriteLine("Студента додано.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка при додаванні: " + ex.Message);
            }
        }

        private void AddMusician()
        {
            Console.Write("FirstName: ");
            string fn = Console.ReadLine() ?? "";
            Console.Write("LastName: ");
            string ln = Console.ReadLine() ?? "";
            Console.Write("Instrument: ");
            string instr = Console.ReadLine() ?? "";
            var m = new Musician(fn, ln, instr);
            _repo.Add(m);
            Console.WriteLine("Музиканта додано.");
        }

        private void AddPilot()
        {
            Console.Write("FirstName: ");
            string fn = Console.ReadLine() ?? "";
            Console.Write("LastName: ");
            string ln = Console.ReadLine() ?? "";
            Console.Write("License: ");
            string lic = Console.ReadLine() ?? "";
            var p = new Pilot(fn, ln, lic);
            _repo.Add(p);
            Console.WriteLine("Пілота додано.");
        }

        private void DeleteAt()
        {
            Console.Write("Індекс для видалення: ");
            string s = Console.ReadLine() ?? "";
            if (int.TryParse(s, out int idx))
            {
                _repo.DeleteAt(idx);
                Console.WriteLine("Видалено (якщо індекс був валідний).");
            }
            else Console.WriteLine("Невірний індекс");
        }

        private void SearchByLastName()
        {
            Console.Write("Прізвище для пошуку: ");
            string ln = Console.ReadLine() ?? "";
            var res = _repo.FindByLastName(ln);
            if (res.Length == 0) Console.WriteLine("Не знайдено");
            else
            {
                for (int i = 0; i < res.Length; i++)
                    Console.WriteLine($"{res[i].GetType().Name}: {res[i].FirstName} {res[i].LastName}");
            }
        }

        private void SearchByStudentId()
        {
            Console.Write("StudentId: ");
            string id = Console.ReadLine() ?? "";
            var s = _repo.FindStudentById(id);
            if (s == null) Console.WriteLine("Не знайдено");
            else Console.WriteLine($"{s.FirstName} {s.LastName}, course={s.Course}, residence={s.Residence}");
        }

        private void CountFirstCourseGirlsInDorm()
        {
            var all = _repo.LoadAll();
            int count = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i] is Student st)
                {
                    if (st.Gender?.ToLower() == "ж" && st.Course == 1 && !string.IsNullOrWhiteSpace(st.Residence) && st.Residence.Contains('.'))
                        count++;
                }
            }
            Console.WriteLine($"Кількість студенток 1-го курсу у гуртожитку: {count}");
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i] is Student st)
                {
                    if (st.Gender?.ToLower() == "ж" && st.Course == 1 && !string.IsNullOrWhiteSpace(st.Residence) && st.Residence.Contains('.'))
                        Console.WriteLine($"{st.FirstName} {st.LastName}, ID={st.StudentId}, Residence={st.Residence}");
                }
            }
        }
    }
}

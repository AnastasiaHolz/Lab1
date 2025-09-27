using System;
using System.Text.RegularExpressions;
using ConsoleApp.Models;

namespace ConsoleApp.Utils
{
    public static class Parser
    {
        public static Person[] ParseEntities(string[] lines)
        {
            if (lines == null || lines.Length == 0) return new Person[0];

            Person[] persons = new Person[0];

            foreach (var line in lines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;

                string type = parts[0];

                try
                {
                    if (type == "Student" && parts.Length >= 7)
                    {
                        string fn = parts[1];
                        string ln = parts[2];
                        int course = int.Parse(parts[3]);
                        string sid = parts[4];
                        string gender = parts[5];
                        string residence = parts[6];
                        var s = new Student(fn, ln, sid, course, gender, residence);
                        AddPerson(ref persons, s);
                    }
                    else if (type == "Musician" && parts.Length >= 4)
                    {
                        string fn = parts[1];
                        string ln = parts[2];
                        string instr = parts[3];
                        var m = new Musician(fn, ln, instr);
                        AddPerson(ref persons, m);
                    }
                    else if (type == "Pilot" && parts.Length >= 4)
                    {
                        string fn = parts[1];
                        string ln = parts[2];
                        string lic = parts[3];
                        var p = new Pilot(fn, ln, lic);
                        AddPerson(ref persons, p);
                    }
                }
                catch { }
            }

            return persons;
        }


        public static string[] EntitiesToLines(Person[] arr)
        {
            if (arr == null || arr.Length == 0) return new string[0];
            string[] outLines = new string[0];
            foreach (var p in arr)
            {
                if (p is Student s)
                    Append(ref outLines, $"Student {s.FirstName} {s.LastName} {s.Course} {s.StudentId} {s.Gender} {s.Residence}");
                else if (p is Musician m)
                    Append(ref outLines, $"Musician {m.FirstName} {m.LastName} {m.Instrument}");
                else if (p is Pilot pl)
                    Append(ref outLines, $"Pilot {pl.FirstName} {pl.LastName} {pl.License}");
            }
            return outLines;
        }


       

        private static void Append<T>(ref T[] arr, T item)
        {
            int old = arr.Length;
            Array.Resize(ref arr, old + 1);
            arr[old] = item;
        }

        private static void AddPerson(ref Person[] arr, Person p)
        {
            int old = arr.Length;
            Array.Resize(ref arr, old + 1);
            arr[old] = p;
        }

        private static bool StringEquals(string a, string b)
        {
            return string.Equals(a?.Trim(), b?.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}

using System;
using FileLibrary;
using FileLibrary.Interfaces;
using ConsoleApp.Business;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbFile = "database.txt";
            IFileStorage storage = new FileStorage();
            IRepository repo = new Repository(storage, dbFile);
            var menu = new ConsoleMenu(repo);
            menu.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFacade
{
    public class DisplayService : IDisplayService
    {
        private ILibraryService libraryService;


        public DisplayService()
        {
            this.libraryService = new LibraryService();
        }

        public DisplayService(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public void HandleMenu()
        {
            Menu();
            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.Q)
            {
                Console.Clear();
                Menu();
                switch (key)
                {
                    case ConsoleKey.A:
                        Console.Write("id, title, type(Books = 0, CDs= 1, DVDs= 2): ");
                        var line = Console.ReadLine();
                        string message = string.Empty;
                        libraryService.Add(line, out message);
                        Console.Write(message);
                        break;

                    case ConsoleKey.L:
                        Console.Write("Id: ");
                        line = Console.ReadLine();
                        Console.WriteLine(libraryService.Lent(line));
                        Console.WriteLine("Enter any key to continue...");
                        Console.ReadKey();

                        break;

                    case ConsoleKey.C:
                        Console.Write("Id: ");
                        line = Console.ReadLine();
                        Console.WriteLine(libraryService.CheckIn(line));
                        Console.WriteLine("Enter any key to continue...");
                        Console.ReadKey();
                        break;

                    case ConsoleKey.R:
                        GenerateReport();
                        break;

                    case ConsoleKey.I:
                        GenerateItemsList();
                        break;

                    default:
                        break;
                }

                Console.Clear();
                Menu();
                key = Console.ReadKey().Key;
            }
        }

        private void GenerateItemsList()
        {
            var allItems = libraryService.AllItems();
            if (allItems.Count() > 0)
            {
                Console.WriteLine("id, title, type");
                foreach (var l in allItems)
                {
                    Console.Write(l.Id);
                    Console.Write(", ");
                    Console.Write(l.Title);
                    Console.Write(", ");
                    Console.WriteLine(l.Type);
                }
            }
            else
            {
                Console.WriteLine("No Items");
            }
            Console.WriteLine("Enter any key to continue...");
            Console.ReadKey();
        }

        private void GenerateReport()
        {
            var lentOutItems = libraryService.LentOutItems();
            if (lentOutItems.Count() > 0)
            {
                Console.WriteLine("id, title, type, Lent Out Time(Days)");
                foreach (var l in lentOutItems)
                {
                    Console.Write(l.Id);
                    Console.Write(", ");
                    Console.Write(l.Title);
                    Console.Write(", ");
                    Console.Write(l.Type);
                    Console.Write(", ");
                    Console.WriteLine(DateTime.Now.Day - l.LentDate.Value.Day);
                }
            }
            else
            {
                Console.WriteLine("No Lent Out Items");
            }
            Console.WriteLine("Enter any key to continue...");
            Console.ReadKey();
        }

        private static void Menu()
        {
            Console.WriteLine("A: Add");
            Console.WriteLine("L: Lend");
            Console.WriteLine("C: Check in");
            Console.WriteLine("R: Report of all the items lent out");
            Console.WriteLine("I: List of all the items sorted by type and title");
        }
    }
}

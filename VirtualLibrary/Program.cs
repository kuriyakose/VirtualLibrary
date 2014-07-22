using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryFacade;

namespace VirtualLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var displayService = new DisplayService();
                displayService.HandleMenu();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}

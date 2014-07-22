using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFacade
{
    public interface ILibraryService
    {
        bool Add(string inputString, out string message);

        string Lent(string id);

        string CheckIn(string id);

        IList<LibraryItem> LentOutItems();

        IList<LibraryItem> AllItems();     
    }
}

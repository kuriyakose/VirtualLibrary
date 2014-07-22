using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFacade
{
    public class LibraryItem
    {
        public string Id;
        public string Title;
        public ItemConstants.Type Type;
        public DateTime? LentDate = null;

        public LibraryItem() 
        { }

        public LibraryItem(string id, string title, ItemConstants.Type type)
        {
            this.Id = id;
            this.Title = title;
            this.Type = type;
        }

    }
}

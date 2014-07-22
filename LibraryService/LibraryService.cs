using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFacade
{
    public class LibraryService : ILibraryService
    {
        IList<LibraryItem> libraryItemList;

        public LibraryService()
        {
            libraryItemList = new List<LibraryItem>();
        }

        public bool Add(string inputString, out string message)
        {
            if (inputString == string.Empty)
                throw new ArgumentException();

            string[] inputArray = inputString.Split(new string[] { "," }, StringSplitOptions.None);
            if (inputArray.Count() < 3)
                throw new ArgumentException();

            var id = inputArray[0].Trim();
            var items = libraryItemList.Where(x => x.Id == id);
            if (items.Count() == 0)
            {
                var title = inputArray[1];
                ItemConstants.Type type;
                if (Enum.TryParse<ItemConstants.Type>(inputArray[2], out type))
                {
                    if (Enum.IsDefined(typeof(ItemConstants.Type), type))
                    {
                        libraryItemList.Add(new LibraryItem(id, title, type));
                        message = "Added";
                        return true;
                    }
                    else
                    {
                        message = "Invalid Type";
                        return false;
                    }
                } else {
                    message = "Invalid Type";
                    return false;
                }
            }
            else
            {
                message = "Id already exist, start again with different ID";
                return false;
            }
        }

        public string Lent(string inputString)
        {
            if (inputString == string.Empty)
                throw new ArgumentException();

            inputString = inputString.Trim();
            var id = inputString.Trim();
            var items = libraryItemList.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                if (items.FirstOrDefault().LentDate == null)
                {
                    items.FirstOrDefault().LentDate = DateTime.Now;
                    return "Lent out";
                }
                else
                {
                    return "Id already lent out, start again with different ID";
                }
            } else {
                return "Id not exists, start again with different ID";
            }
        }

        public string CheckIn(string inputString)
        {
            if (inputString == string.Empty)
                throw new ArgumentException();

            inputString = inputString.Trim();
            var id = inputString.Trim();
            var items = libraryItemList.Where(x => x.Id == id);
            if (items.Count() > 0)
            {
                if (items.FirstOrDefault().LentDate != null)
                {
                    items.FirstOrDefault().LentDate = null;
                    return "Checked In";
                }
                else
                {
                    return "Id already checked in, start again with different ID";
                }
            }
            else
            {
                return "Id not exists, start again with different ID";
            }
        }

        public IList<LibraryItem> LentOutItems()
        {
            return libraryItemList.Where(x => x.LentDate != null).ToList();
        }

        public IList<LibraryItem> AllItems()
        {
            return libraryItemList.OrderBy(x => x.Type).ThenBy(y => y.Title).ToList();
        }

        public long Count()
        {
            return libraryItemList.Count();
        }
    }
}

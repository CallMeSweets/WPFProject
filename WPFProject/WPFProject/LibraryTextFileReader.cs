using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLibrary
{
    public class LibraryTextFileReader
    {
        private static readonly string INPUT_FILE_PATH = "library.txt";

        public ObservableCollection<Book> ReadFromFile()
        {
            string[] lines = File.ReadAllLines(INPUT_FILE_PATH);

            ObservableCollection<Book> retv = new ObservableCollection<Book>();

            for(int i = 0; i < lines.Length; i += 3)
            {
                Book book = new Book(lines[i], lines[i + 1], int.Parse(lines[i + 2]));
                retv.Add(book);
            }

            return retv;
        }
    }
}

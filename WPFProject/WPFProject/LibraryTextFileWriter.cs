using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLibrary
{
    public class LibraryTextFileWriter
    {
        private static readonly string OUTPUT_FILE_PATH = "library.txt";

        public void WriteToFile(ICollection<Book> books)
        {
            using (StreamWriter file = new StreamWriter(OUTPUT_FILE_PATH))
            {
                foreach (var book in books)
                {
                    file.WriteLine(book.Title);
                    file.WriteLine(book.Author);
                    file.WriteLine(book.Year);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFLibrary
{
    /// <summary>
    /// Interaction logic for BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public Book Book { get; set; }
        public Tuple<string, string, int> initialBookVal;

        private bool okFlag = false;
        public bool OKFlag
        {
            get
            {
                return okFlag;
            }
            set
            {
                okFlag = value;
            }
        }

        public BookWindow()
        {
            InitializeComponent();

            Book = new Book("", "", 0);
            DataContext = Book;

            initialBookVal = Tuple.Create<string, string, int>(Book.Title, Book.Author, Book.Year);
        }

        public BookWindow(Book book)
        {
            InitializeComponent();

            Book = book;
            DataContext = Book;

            initialBookVal = Tuple.Create<string, string, int>(Book.Title, Book.Author, Book.Year);
        }

        private void PreviewYearInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if(!OKFlag)
            {
                Book.Title = initialBookVal.Item1;
                Book.Author = initialBookVal.Item2;
                Book.Year = initialBookVal.Item3;
            }
            base.OnClosing(e);
        }
    }
}

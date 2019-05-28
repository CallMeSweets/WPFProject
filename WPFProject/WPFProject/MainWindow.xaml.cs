using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Book> books;
        private CollectionViewSource collectionViewSource;

        public MainWindow()
        {
            InitializeComponent();
            LibraryTextFileReader reader = new LibraryTextFileReader();
            books = reader.ReadFromFile();
            
            collectionViewSource = new CollectionViewSource();
            collectionViewSource.Source = books;
            booksListView.ItemsSource = collectionViewSource.View;
        }

        private void FilterBtn_Click(object sender, RoutedEventArgs e)
        {
            collectionViewSource.View.Filter = Filter;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            authorTextBox.Clear();
            titleTextBox.Clear();
            yearTextBox.Clear();
            collectionViewSource.View.Filter = Filter;
        }

        private void PreviewYearInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int selectedCnt = booksListView.SelectedItems.Count;

            for(int i = selectedCnt - 1; i >= 0; i--)
            {
                books.Remove(booksListView.SelectedItems[i] as Book);
            }
        }

        private void BooksListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            // take into account vertical scrollbar
            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;

            var colWidth = 0.328;
            gView.Columns[0].Width = workingWidth * colWidth;
            gView.Columns[1].Width = workingWidth * colWidth;
            gView.Columns[2].Width = workingWidth * colWidth;
        }

        private bool Filter(object o)
        {
            Book book = o as Book;
            if (book == null)
            {
                return false;
            }

            bool retv = true;
            if (!String.IsNullOrEmpty(yearTextBox.Text))
            {
                int searchedYear = int.Parse(yearTextBox.Text);
                retv = (book.Year == searchedYear);
            }

            if(!String.IsNullOrEmpty(titleTextBox.Text))
            {
                retv = retv && book.Title.Contains(titleTextBox.Text);
            }

            if (!String.IsNullOrEmpty(authorTextBox.Text))
            {
                retv = retv && book.Author.Contains(authorTextBox.Text);
            }

            return retv;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            BookWindow window = new BookWindow();
            window.Title = "Insert New Book";
            window.ShowDialog();

            if(window.OKFlag)
            {
                Book newBook = window.Book;
                books.Add(new Book(newBook.Author, newBook.Title, newBook.Year));

                //refilter
                collectionViewSource.View.Filter = Filter;
            }
        }

        private void BooksListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            Book selected = listView.SelectedItem as Book;

            BookWindow window = new BookWindow(selected);
            window.Title = "Edit Book";
            window.ShowDialog();

            //refilter
            collectionViewSource.View.Filter = Filter;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            LibraryTextFileWriter writer = new LibraryTextFileWriter();
            writer.WriteToFile(books);
            base.OnClosing(e);
        }
    }
}

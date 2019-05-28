using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLibrary
{
    public class Book : INotifyPropertyChanged
    {
        private OKCommand okCommand = new OKCommand();
        public OKCommand OKCommand
        {
            get
            {
                return okCommand;
            }
        }

        private CancelCommand cancelCommand = new CancelCommand();
        public CancelCommand CancelCommand
        {
            get
            {
                return cancelCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        private string author;
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Author"));
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Title"));
            }
        }

        private int year;
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Year"));
            }
        }
    }
}

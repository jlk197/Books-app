using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfApp1.ViewModel 
{
    class BooksListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ListCollectionView view;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<BookViewModel> books;
        public ObservableCollection<BookViewModel> Books { 
            get { return books; }
            set { books = value; RaisePropertyChanged(nameof(Books)); }
        }
        private IDaoMock daoMock;
        public BooksListViewModel(ObservableCollection<ProducerViewModel> producers) {

            string libraryName = ConfigurationManager.AppSettings["library_name"];
            daoMock = new BLC.BLC(libraryName).DAO;
            books = new ObservableCollection<BookViewModel>();
            foreach (var car in daoMock.GetAllBooks()) {
                ProducerViewModel p = producers.First(x => x.Id == car.Producer.Id);  
                books.Add(new BookViewModel(car, p));
            }
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(books);
            addNewBookCommand = new RelayCommand(_ => AddNewBook(), _ => CanAddBook());
            saveBookCommand = new RelayCommand(_ => SaveBook(), _ => CanSaveChanges());
            filterDataCommand = new RelayCommand(_ => FilterData());
        }

        private void AddNewBook() {
            BookViewModel cvm = new BookViewModel(daoMock.CreateNewBook(), null);
            cvm.IsChanged = true;
            EditedBook = cvm;
        }

        private RelayCommand addNewBookCommand;
        public ICommand AddNewBookCommand
        {
            get => addNewBookCommand;
        }

        private BookViewModel selectedBook;
        public BookViewModel SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                if (CanAddBook()) {
                    EditedBook = selectedBook;
                }
                RaisePropertyChanged(nameof(SelectedBook));
            }
        }

        private BookViewModel editedBook;
        public BookViewModel EditedBook
        {
            get { return editedBook; }
            set
            {
                editedBook = value;
                RaisePropertyChanged(nameof(EditedBook));
            }
        }

        private RelayCommand saveBookCommand;
        public ICommand SaveBookCommand { get => saveBookCommand; }

        private void SaveBook() 
        {
            if (!EditedBook.HasErrors) {
                if (EditedBook.Id == 0)
                {
                    books.Add(EditedBook);
                    daoMock.AddBook(EditedBook.Book);
                }
                daoMock.SaveChanges();
                EditedBook.IsChanged = false;
                EditedBook = null;
            }
        }

        private bool CanAddBook() 
        {
            if (EditedBook == null || !EditedBook.IsChanged ) {
                return true;
            }
            return false;
        }

        private bool CanSaveChanges()
        {
            if (EditedBook == null || !EditedBook.IsChanged)
            {
                return false;
            }
            else {
                return !EditedBook.HasErrors;
            }
        }
        private String filter;
        public String Filter {
            get { return filter; } 
            set { 
                filter = value;
                RaisePropertyChanged(nameof(Filter));
            }
        }

        private RelayCommand filterDataCommand;
        public RelayCommand FilterDataCommand { get => filterDataCommand; }
        public void FilterData() {
            if (string.IsNullOrEmpty(filter)){
                view.Filter = null;
            }
            else
            {
                view.Filter = c => ((BookViewModel) c).Title.Contains(filter, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}

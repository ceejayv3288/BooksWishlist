using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using static BooksWishlist.Models.GoogleBooksAPI;

namespace BooksWishlist.ViewModels
{
    public class BooksPageViewModel
    {
        INavigationService _navigationService;
        public ICommand NewBookCommand { get; set; }
        public ICommand PageAppearingCommand { get; set; }
        public ICommand ItemSelectedCommand { get; set; }
        public ObservableCollection<Book> SavedBooks { get; set; }
        public BooksPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NewBookCommand = new DelegateCommand(NewBookAction);
            PageAppearingCommand = new DelegateCommand(PageAppearingAction);
            ItemSelectedCommand = new DelegateCommand<object>(ItemSelectedAction);
            SavedBooks = new ObservableCollection<Book>();
            ReadSavedBook();
        }

        private void ReadSavedBook()
        {
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            //{
            //    conn.CreateTable<Book>();
            //    var books = conn.Table<Book>().ToList();

            //    SavedBooks.Clear();
            //    foreach (var book in books)
            //    {
            //        SavedBooks.Add(book);
            //    }
            //}

            App.Connection.CreateTable<Book>();
            var books = App.Connection.Table<Book>().ToList();
            SavedBooks.Clear();
            foreach (var book in books)
            {
                SavedBooks.Add(book);
            }
        }

        private async void NewBookAction()
        {
            await _navigationService.NavigateAsync("NewBookPage", null, false, true);
        }

        private void PageAppearingAction()
        {
            ReadSavedBook();
        }

        private async void ItemSelectedAction(object obj)
        {
            var selectedBook = (obj as ListView).SelectedItem as Book;
            var parameters = new NavigationParameters();
            parameters.Add("selected_book", selectedBook);
            await _navigationService.NavigateAsync("BookDetailsPage", parameters);
        }
    }
}

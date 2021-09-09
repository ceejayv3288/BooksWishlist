using Prism.Commands;
using Prism.Navigation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using static BooksWishlist.Models.GoogleBooksAPI;

namespace BooksWishlist.ViewModels
{
    public class BooksPageViewModel
    {
        INavigationService _navigationService;
        public ICommand NewBookCommand { get; set; }
        public ICommand PageAppearingCommand { get; set; }
        public ObservableCollection<Book> SavedBooks { get; set; }
        public BooksPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NewBookCommand = new DelegateCommand(NewBookAction);
            PageAppearingCommand = new DelegateCommand(PageAppearingAction);
            SavedBooks = new ObservableCollection<Book>();
            ReadSavedBook();
        }

        private void ReadSavedBook()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Book>();
                var books = conn.Table<Book>().ToList();

                SavedBooks.Clear();
                foreach (var book in books)
                {
                    SavedBooks.Add(book);
                }
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
    }
}

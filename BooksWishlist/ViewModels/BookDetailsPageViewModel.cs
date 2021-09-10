using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static BooksWishlist.Models.GoogleBooksAPI;

namespace BooksWishlist.ViewModels
{
    public class BookDetailsPageViewModel : BaseViewModel, INavigatedAware
    {
        public ICommand DeleteBookCommand { get; set; }

        public Book selectedBook { get; set; }
        public string title { get; set; }
        public string Title 
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public string author { get; set; }
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }
        public string thumbnail { get; set; }
        public string Thumbnail
        {
            get { return thumbnail; }
            set
            {
                thumbnail = value;
                OnPropertyChanged("Thumbnail");
            }
        }

        public BookDetailsPageViewModel()
        {
            DeleteBookCommand = new DelegateCommand(DeleteBookAction);
        }

        private void DeleteBookAction()
        {
            App.Connection.CreateTable<Book>();
            int bookDeleted = App.Connection.Delete(selectedBook);
            if (bookDeleted >= 1)
            {
                App.Current.MainPage.DisplayAlert("Success!", "The book is successfully deleted.", "Ok");
                App.Current.MainPage.Navigation.PopAsync();
            }
            else
                App.Current.MainPage.DisplayAlert("Failed!", "An error has occured.", "Ok");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            selectedBook = parameters["selected_book"] as Book;

            Thumbnail = selectedBook.thumbnail;
            Author = selectedBook.authors;
            Title = selectedBook.title;
        }
    }
}

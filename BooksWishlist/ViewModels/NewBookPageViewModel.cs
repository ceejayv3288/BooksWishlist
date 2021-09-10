using BooksWishlist.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using static BooksWishlist.Models.GoogleBooksAPI;

namespace BooksWishlist.ViewModels
{
    public class NewBookPageViewModel
    {
        private JsonSerializer _serializer = new JsonSerializer();

        public ICommand SearchCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ObservableCollection<Book> SearchResults { get; set; }

        public NewBookPageViewModel()
        {
            SearchCommand = new DelegateCommand<string>(GetSearchResults);
            SaveCommand = new DelegateCommand<Book>(SaveBooks, CanSaveBooks);
            SearchResults = new ObservableCollection<Book>();
        }

        private async void GetSearchResults(string query)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //BooksAPI books;
                    //HttpResponseMessage response = await client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q={query}&key={Constants.GOOGLE_BOOKS_API_KEY}");
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    using (var stream = await response.Content.ReadAsStreamAsync())
                    //    {
                    //        using (var reader = new StreamReader(stream))
                    //        {
                    //            using (var json = new JsonTextReader(reader))
                    //            {
                    //                books = _serializer.Deserialize<BooksAPI>(json);
                    //            }
                    //        }
                    //    }
                    //}


                    var data = await App.GoogleBooksAPIManager.GetSearchListAsync(query);

                    //var result = await client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={query}&key={Constants.GOOGLE_BOOKS_API_KEY}");
                    //var data = JsonConvert.DeserializeObject<BooksAPI>(result);

                    SearchResults.Clear();
                    foreach (var book in data.items)
                    {
                        Book currentBook = new Book
                        {
                            thumbnail = book.volumeInfo.imageLinks?.thumbnail != null ? book.volumeInfo.imageLinks?.thumbnail : string.Empty,
                            title = book.volumeInfo?.title != null ? book.volumeInfo?.title : string.Empty
                        };

                        string authors = string.Empty;
                        if (book.volumeInfo.authors != null)
                        {
                            foreach (var author in book.volumeInfo.authors)
                            {
                                authors += author + ", ";
                            }
                            authors = authors.Substring(0, authors.Length - 2);
                            currentBook.authors = authors;
                        }

                        SearchResults.Add(currentBook);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void SaveBooks(Book book)
        {
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            //{
            //    var recordCount = conn.Table<Book>().Where(bookStored => bookStored.authors == book.authors && bookStored.thumbnail == book.thumbnail && bookStored.title == book.title).Count();
            //    if (recordCount == 0)
            //    {
            //        conn.CreateTable<Book>();
            //        int bookInserted = conn.Insert(book);
            //        if (bookInserted >= 1)
            //            App.Current.MainPage.DisplayAlert("Success!", "Book saved", "Ok");
            //        else
            //            App.Current.MainPage.DisplayAlert("Failure", "An error occured while saving the book", "Ok");
            //    }
            //    else
            //    {
            //        App.Current.MainPage.DisplayAlert("Failed", "Book already saved!", "Ok");
            //    }
            //}

            var recordCount = App.Connection.Table<Book>().Where(bookStored => bookStored.authors == book.authors && bookStored.thumbnail == book.thumbnail && bookStored.title == book.title).Count();
            if (recordCount == 0)
            {
                App.Connection.CreateTable<Book>();
                int bookInserted = App.Connection.Insert(book);
                if (bookInserted >= 1)
                    App.Current.MainPage.DisplayAlert("Success!", "Book saved", "Ok");
                else
                    App.Current.MainPage.DisplayAlert("Failure", "An error occured while saving the book", "Ok");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Failed", "Book already saved!", "Ok");
            }
        }
        private bool CanSaveBooks(Book book)
        {
            return book != null;
        }
    }
}

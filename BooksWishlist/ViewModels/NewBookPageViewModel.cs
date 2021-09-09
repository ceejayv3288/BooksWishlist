using BooksWishlist.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
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
        public ObservableCollection<Books> SearchResults { get; set; }

        public NewBookPageViewModel()
        {
            SearchCommand = new DelegateCommand<string>(GetSearchResults);
            SearchResults = new ObservableCollection<Books>();
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

                    var result = await client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={query}&key={Constants.GOOGLE_BOOKS_API_KEY}");
                    var data = JsonConvert.DeserializeObject<BooksAPI>(result);

                    SearchResults.Clear();
                    foreach (var book in data.items)
                    {
                        Books currentBook = new Books
                        {
                            thumbnail = book.volumeInfo.imageLinks?.thumbnail != null ? book.volumeInfo.imageLinks?.thumbnail : string.Empty,
                            title = book.volumeInfo?.title != null ? book.volumeInfo?.title : string.Empty
                        };

                        string authors = string.Empty;
                        if (book.volumeInfo.authors != null)
                        {
                            foreach (var author in book.volumeInfo.authors)
                            {
                                authors += author + " ,";
                            }
                            authors = authors.Replace(" ,", string.Empty);
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
    }
}

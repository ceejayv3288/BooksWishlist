using BooksWishlist.Helpers;
using BooksWishlist.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static BooksWishlist.Models.GoogleBooksAPI;

namespace BooksWishlist.Services
{
    public class GoogleBooksAPIServices : IGoogleBooksAPIServices
    {
        readonly HttpClient _client = new HttpClient();

        public GoogleBooksAPIServices()
        {

        }

        public async Task<BooksAPI> GetSearchListAsync(string query)
        {
            try
            {
                var result = await _client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={query}&key={Constants.GOOGLE_BOOKS_API_KEY}");
                return JsonConvert.DeserializeObject<BooksAPI>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

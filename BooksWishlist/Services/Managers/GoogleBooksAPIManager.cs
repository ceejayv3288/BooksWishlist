using BooksWishlist.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BooksWishlist.Models.GoogleBooksAPI;

namespace BooksWishlist.Services.Managers
{
    public class GoogleBooksAPIManager
    {
        IGoogleBooksAPIServices _googleBooksAPIServices;

        public GoogleBooksAPIManager(IGoogleBooksAPIServices googleBooksAPIServices)
        {
            _googleBooksAPIServices = googleBooksAPIServices;
        }

        public Task<BooksAPI> GetSearchListAsync(string query)
        {
            return _googleBooksAPIServices.GetSearchListAsync(query);
        }
    }
}

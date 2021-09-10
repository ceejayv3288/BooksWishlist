using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BooksWishlist.Models.GoogleBooksAPI;

namespace BooksWishlist.Services.Interfaces
{
    public interface IGoogleBooksAPIServices
    {
        Task<BooksAPI> GetSearchListAsync(string query);
    }
}

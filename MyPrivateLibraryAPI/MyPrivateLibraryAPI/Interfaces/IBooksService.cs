using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Interfaces
{
    public interface IBooksService
    {
        Task<List<Book>> GetAll(string userId);
        Task<List<Book>> GetFilteredBooks(string userId, BookFilters filters);
        Task<Book> GetBookWithId(int id);
        Task AddBook(Book book);
        Task<bool> RemoveBook(int id);
        Task<bool> UpdateBook(Book book);
    }
}

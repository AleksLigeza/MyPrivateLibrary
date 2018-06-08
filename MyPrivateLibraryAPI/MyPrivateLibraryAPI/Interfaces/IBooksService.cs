using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPrivateLibraryAPI.DbModels;

namespace MyPrivateLibraryAPI.Interfaces
{
    public interface IBooksService
    {
        Task<List<Book>> GetBooks(string userId);
        Task<List<Book>> GetBooksWithTitle(string userId, string title);
        Task<List<Book>> GetBooksWithYear(string userId, int year);
        Task<Book> GetBookById(int id);
        Task AddBook(Book book);
        Task RemoveBook(int id);
        Task UpdateBook(Book book);
    }
}

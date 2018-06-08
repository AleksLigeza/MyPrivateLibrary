using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPrivateLibraryAPI.DbModels;

namespace MyPrivateLibraryAPI.Interfaces
{
    public interface IBooksService
    {
        Task<List<Book>> GetAll(string userId);
        Task<List<Book>> GetBooksWithTitle(string userId, string title);
        Task<List<Book>> GetBooksWithYearBetween(string userId, int start, int end);
        Task<Book> GetBookWithId(int id);
        Task AddBook(Book book);
        Task<bool> RemoveBook(int id);
        Task<bool> UpdateBook(Book book);
    }
}

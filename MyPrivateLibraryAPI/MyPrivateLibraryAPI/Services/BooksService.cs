using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Interfaces;

namespace MyPrivateLibraryAPI.Services
{
    public class BooksService : IBooksService
    {
        private ApplicationDbContext _context;
        public BooksService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Book>> GetBooks(string userId)
        {
            return _context.Books.Where(x => x.UserId == userId).ToListAsync();
        }

        public Task<List<Book>> GetBooksWithTitle(string userId, string title)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetBooksWithYear(string userId, int year)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBook(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}

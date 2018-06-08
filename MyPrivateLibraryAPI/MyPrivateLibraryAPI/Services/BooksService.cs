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

        public Task<List<Book>> GetAll(string userId)
        {
            return _context.Books.Where(x => x.UserId == userId).ToListAsync();
        }

        public Task<List<Book>> GetBooksWithTitle(string userId, string title)
        {
            return _context.Books.Where(x => x.UserId == userId && x.Title.Contains(title)).ToListAsync();
        }

        public Task<List<Book>> GetBooksWithYearBetween(string userId, int start, int end)
        {
            return _context.Books.Where(x => x.UserId == userId && x.PublicationYear >= start && x.PublicationYear <= end).ToListAsync();
        }

        public Task<Book> GetBookWithId(int id)
        {
            return _context.Books.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public Task AddBook(Book book)
        {
            _context.Books.Add(book);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveBook(int id)
        {
            var book = await GetBookWithId(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var toUpdate = await GetBookWithId(book.Id);
            if (toUpdate == null) return false;

            toUpdate.Isbn = book.Isbn;
            toUpdate.PublicationYear = book.PublicationYear;
            toUpdate.ReadingEnd = book.ReadingEnd;
            toUpdate.ReadingStart = book.ReadingStart;
            toUpdate.Title = book.Title;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

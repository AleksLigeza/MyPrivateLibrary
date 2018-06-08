using System;
using System.Collections.Generic;
using System.Text;
using MyPrivateLibraryAPI.DbModels;

namespace MyPrivateLibraryAPI.Tests.Builders
{
    public class BookBuilder
    {
        private Book _book;

        public BookBuilder()
        {
            _book = new Book()
            {
                Id = 1,
                Isbn = "",
                Title = "",
                UserId = "",
                PublicationYear = 0,
                ReadingStart = null,
                ReadingEnd = null,
                User = null
            };
        }

        public BookBuilder WithId(int id)
        {
            _book.Id = id;
            return this;
        }

        public BookBuilder WithIsbn(string isbn)
        {
            _book.Isbn = isbn;
            return this;
        }

        public BookBuilder WithTitle(string title)
        {
            _book.Title = title;
            return this;
        }

        public BookBuilder WithUser(ApplicationUser user)
        {
            _book.UserId = user.Id;
            _book.User = user;
            return this;
        }

        public BookBuilder WithYear(int year)
        {
            _book.PublicationYear = year;
            return this;
        }

        public BookBuilder WithReadingStart(DateTime start)
        {
            _book.ReadingStart = start;
            return this;
        }

        public BookBuilder WithReadingEnd(DateTime end)
        {
            _book.ReadingEnd = end;
            return this;
        }

        public Book Build()
        {
            return _book;
        }


    }
}

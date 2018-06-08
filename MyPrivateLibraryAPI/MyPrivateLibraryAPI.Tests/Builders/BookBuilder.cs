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
                User = new ApplicationUser()
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

        public Book Build()
        {
            return _book;
        }
    }
}

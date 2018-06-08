using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using MyPrivateLibraryAPI.DbModels;

namespace MyPrivateLibraryAPI.Tests.Builders
{
    public class ApplicationUserBuilder
    {
        private ApplicationUser _user;

        public ApplicationUserBuilder()
        {
            _user = new ApplicationUser();
        }

        public ApplicationUserBuilder WithEmail(string email)
        {
            _user.Email = email;
            _user.NormalizedEmail = email.Normalize().ToUpper();
            return this;
        }

        public ApplicationUserBuilder WithUsername(string username)
        {
            _user.UserName = username;
            _user.NormalizedUserName = username.Normalize().ToUpper();
            return this;
        }

        public ApplicationUserBuilder WithPassword(string password)
        {
            _user.PasswordHash = password;
            return this;
        }

        public ApplicationUserBuilder WithFirstName(string firstname)
        {
            _user.FirstName = firstname;
            return this;
        }

        public ApplicationUserBuilder WithLastname(string lastname)
        {
            _user.LastName = lastname;
            return this;
        }

        public ApplicationUserBuilder WithBook(Book book)
        {
            if (_user.Books == null)
            {
                _user.Books = new List<Book>();
            }

            _user.Books.Add(book);
            return this;
        }

        public ApplicationUserBuilder WithId(string id)
        {
            _user.Id = id;
            return this;
        }

        public ApplicationUser Build()
        {
            return _user;
        }
    }
}

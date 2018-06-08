using System;
using System.Collections.Generic;
using System.Text;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Tests.Builders
{
    public class RegisterModelBuilder
    {
        private RegisterRequest _registerRequest;

        public RegisterModelBuilder()
        {
            _registerRequest = new RegisterRequest
            {
                Email = "",
                Firstname = "",
                Lastname = "",
                Password = ""
            };
        }

        public RegisterModelBuilder WithEmail(string email)
        {
            _registerRequest.Email = email;
            return this;
        }

        public RegisterModelBuilder WithFirstname(string firstname)
        {
            _registerRequest.Firstname = firstname;
            return this;
        }

        public RegisterModelBuilder WithLastname(string lastname)
        {
            _registerRequest.Lastname = lastname;
            return this;
        }

        public RegisterModelBuilder WithPassword(string password)
        {
            _registerRequest.Password = password;
            return this;
        }

        public RegisterRequest Build()
        {
            return _registerRequest;
        }
    }
}

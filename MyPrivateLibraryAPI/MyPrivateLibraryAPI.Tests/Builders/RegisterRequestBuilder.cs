using System;
using System.Collections.Generic;
using System.Text;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Tests.Builders
{
    public class RegisterRequestBuilder
    {
        private RegisterRequest _registerRequest;

        public RegisterRequestBuilder()
        {
            _registerRequest = new RegisterRequest
            {
                Email = "",
                Firstname = "",
                Lastname = "",
                Password = ""
            };
        }

        public RegisterRequestBuilder WithEmail(string email)
        {
            _registerRequest.Email = email;
            return this;
        }

        public RegisterRequestBuilder WithFirstname(string firstname)
        {
            _registerRequest.Firstname = firstname;
            return this;
        }

        public RegisterRequestBuilder WithLastname(string lastname)
        {
            _registerRequest.Lastname = lastname;
            return this;
        }

        public RegisterRequestBuilder WithPassword(string password)
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

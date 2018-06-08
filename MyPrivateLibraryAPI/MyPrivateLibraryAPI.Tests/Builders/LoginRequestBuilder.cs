using System;
using System.Collections.Generic;
using System.Text;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Tests.Builders
{
    public class LoginRequestBuilder
    {
        private LoginRequest _loginRequest;

        public LoginRequestBuilder()
        {
            _loginRequest = new LoginRequest
            {
                Email = "",
                Password = ""
            };
        }

        public LoginRequestBuilder WithEmail(string email)
        {
            _loginRequest.Email = email;
            return this;
        }

        public LoginRequestBuilder WithPassword(string password)
        {
            _loginRequest.Password = password;
            return this;
        }

        public LoginRequest Build()
        {
            return _loginRequest;
        }
    }
}

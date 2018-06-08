using System;
using System.Collections.Generic;
using System.Text;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Tests.Builders
{
    public class LoginModelBuilder
    {
        private LoginRequest _loginRequest;

        public LoginModelBuilder()
        {
            _loginRequest = new LoginRequest
            {
                Email = "",
                Password = ""
            };
        }

        public LoginModelBuilder WithEmail(string email)
        {
            _loginRequest.Email = email;
            return this;
        }

        public LoginModelBuilder WithPassword(string password)
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

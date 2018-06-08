using System;
using System.Threading.Tasks;
using MyPrivateLibraryAPI.Models;
using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPrivateLibraryAPI.Controllers;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Tests.Builders;

namespace MyPrivateLibraryAPI.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public async Task Login_ValidUserData_ReturnsToken()
        {
            // Arrange
            var loginRequest = new LoginModelBuilder()
                .WithPassword("pass12345")
                .WithEmail("someemail@email.com")
                .Build();
            var user = new ApplicationUserBuilder()
                .WithEmail(loginRequest.Email)
                .WithUsername(loginRequest.Email)
                .WithPassword(loginRequest.Password)
                .WithId("abcd")
                .Build();
            var userManager = new Mock<MockUserManager>();
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x[It.IsAny<string>()])
                .Returns("SomeReallyLongAndSecretKey");
            var accountController = new AccountController(userManager.Object, configurationMock.Object);

            // Act
            var result = await accountController.Login(loginRequest);

            // Assert
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var json = okResult.Value.ToString();
            json.Should().Contain("token");

        }

        [Fact]
        public async Task Login_NotValidUserData_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequest = new LoginModelBuilder()
                .WithPassword("pass12345")
                .WithEmail("someemail@email.com")
                .Build();
            var user = new ApplicationUserBuilder()
                .WithEmail(loginRequest.Email)
                .WithUsername(loginRequest.Email)
                .WithPassword(loginRequest.Password)
                .WithId("abcd")
                .Build();
            var userManager = new Mock<MockUserManager>();
            userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x[It.IsAny<string>()])
                .Returns("SomeReallyLongAndSecretKey");
            var accountController = new AccountController(userManager.Object, configurationMock.Object);

            // Act
            var result = await accountController.Login(loginRequest);

            // Assert
            result.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public async Task Register_ValidUserData_ReturnsToken()
        {
            // Arrange
            var registerRequest = new RegisterModelBuilder()
                .WithEmail("someemail@email.com")
                .WithFirstname("firstname")
                .WithLastname("lastname")
                .WithPassword("password")
                .Build();
            var userManager = new Mock<MockUserManager>();
            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x[It.IsAny<string>()])
                .Returns("SomeReallyLongAndSecretKey");
            var accountController = new AccountController(userManager.Object, configurationMock.Object);

            // Act
            var result = await accountController.Register(registerRequest);

            // Assert
            result.Should().BeAssignableTo<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var json = okResult.Value.ToString();
            json.Should().Contain("token");
        }

        [Fact]
        public async Task Register_NotValidUserData_ReturnsBadRequest()
        {
            // Arrange
            var registerRequest = new RegisterModelBuilder()
                .WithEmail("someemail@email.com")
                .WithFirstname("firstname")
                .WithLastname("lastname")
                .WithPassword("password")
                .Build();
            var userManager = new Mock<MockUserManager>();
            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x[It.IsAny<string>()])
                .Returns("SomeReallyLongAndSecretKey");
            var accountController = new AccountController(userManager.Object, configurationMock.Object);

            // Act
            var result = await accountController.Register(registerRequest);

            // Assert
            result.Should().BeAssignableTo<BadRequestResult>();
        }
    }
}

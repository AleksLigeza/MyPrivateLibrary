using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using MoviesAPI.Tests.Services;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Services;
using MyPrivateLibraryAPI.Tests.Builders;
using Xunit;

namespace MyPrivateLibraryAPI.Tests.Services
{
    public class BooksServiceTests
    {
        [Fact]
        public async Task GetAll_UserHasNoBooks_ReturnZeroBooks()
        {
            // Arrange
            var userOne = new ApplicationUserBuilder().WithId("1").Build();
            var userTwo = new ApplicationUserBuilder().WithId("2").Build();
            var data = new List<Book>
            {
                new BookBuilder().WithUser(userTwo).Build(),
                new BookBuilder().WithUser(userTwo).Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBooks(userOne.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_UserHasTwoBooks_ReturnTwoBooks()
        {
            // Arrange
            var userOne = new ApplicationUserBuilder().WithId("1").Build();
            var userTwo = new ApplicationUserBuilder().WithId("2").Build();
            var data = new List<Book>
            {
                new BookBuilder().WithUser(userOne).Build(),
                new BookBuilder().WithUser(userOne).Build(),
                new BookBuilder().WithUser(userTwo).Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBooks(userOne.Id);

            // Assert
            result.Should().HaveCount(2);
        }

        #region helpers

        private Mock<DbSet<Book>> GenerateEnumerableDbSetMock(IQueryable<Book> data)
        {
            var dbSet = new Mock<DbSet<Book>>();

            dbSet.As<IAsyncEnumerable<Book>>()
                .Setup(x => x.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Book>(data.GetEnumerator()));
            dbSet.As<IQueryable<Book>>()
                .Setup(x => x.Provider)
                .Returns(new TestAsyncQueryProvider<Book>(data.Provider));
            dbSet.As<IQueryable<Book>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<Book>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<Book>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator);

            return dbSet;
        }

        private Mock<ApplicationDbContext> GenerateEnumerableContextMock(Mock<DbSet<Book>> dbSet)
        {
            var contextOptions = new DbContextOptions<ApplicationDbContext>();
            var context = new Mock<ApplicationDbContext>(contextOptions);
            context.Setup(x => x.Books)
                .Returns(dbSet.Object);
            return context;
        }

        #endregion
    }
}

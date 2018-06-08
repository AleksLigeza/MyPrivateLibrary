using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Moq;
using MoviesAPI.Tests.Services;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Services;
using MyPrivateLibraryAPI.Tests.Builders;
using Xunit;
using Xunit.Sdk;

namespace MyPrivateLibraryAPI.Tests.Services
{
    public class BooksServiceTests
    {
        [Fact]
        public async Task GetAll_UserHasNoBooks_ReturnsZeroBooks()
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
            var result = await booksService.GetAll(userOne.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_UserHasTwoBooks_ReturnsTwoBooks()
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
            var result = await booksService.GetAll(userOne.Id);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBookWithTitle_UserHasTwoBooksWithTitleLike_ReturnsTwoBooks()
        {
            // Arrange
            var userOne = new ApplicationUserBuilder().WithId("1").Build();
            var userTwo = new ApplicationUserBuilder().WithId("2").Build();
            var data = new List<Book>
            {
                new BookBuilder().WithUser(userOne).WithTitle("abc").Build(),
                new BookBuilder().WithUser(userOne).WithTitle("aac").Build(),
                new BookBuilder().WithUser(userOne).WithTitle("bbb").Build(),
                new BookBuilder().WithUser(userTwo).WithTitle("aabbccdd").Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBooksWithTitle(userOne.Id, "b");

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBookWithTitle_UserHasZeroBooksWithTitleLike_ReturnsZeroBooks()
        {
            // Arrange
            var userOne = new ApplicationUserBuilder().WithId("1").Build();
            var userTwo = new ApplicationUserBuilder().WithId("2").Build();
            var data = new List<Book>
            {
                new BookBuilder().WithUser(userOne).WithTitle("a").Build(),
                new BookBuilder().WithUser(userOne).WithTitle("aac").Build(),
                new BookBuilder().WithUser(userTwo).WithTitle("b").Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBooksWithTitle(userOne.Id, "b");

            // Assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetBookWithYearBetween_UserHasZeroBooksWithYear_ReturnsZeroBooks()
        {
            // Arrange
            var userOne = new ApplicationUserBuilder().WithId("1").Build();
            var userTwo = new ApplicationUserBuilder().WithId("2").Build();
            var data = new List<Book>
            {
                new BookBuilder().WithUser(userOne).WithYear(2000).Build(),
                new BookBuilder().WithUser(userOne).WithYear(2001).Build(),
                new BookBuilder().WithUser(userTwo).WithYear(2010).Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBooksWithYearBetween(userOne.Id, 2005, 2015);

            // Assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetBookWithYearBetween_UserHasTwoBooksWithYear_ReturnsTwoBooks()
        {
            // Arrange
            var userOne = new ApplicationUserBuilder().WithId("1").Build();
            var userTwo = new ApplicationUserBuilder().WithId("2").Build();
            var data = new List<Book>
            {
                new BookBuilder().WithUser(userOne).WithYear(2015).Build(),
                new BookBuilder().WithUser(userOne).WithYear(2010).Build(),
                new BookBuilder().WithUser(userTwo).WithYear(2010).Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBooksWithYearBetween(userOne.Id, 2005, 2015);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetBookWithId_ThereIsABookWithId_ReturnsBooks()
        {
            // Arrange
            var book = new BookBuilder().WithId(1).Build();
            var data = new List<Book>
            {
                book,
                new BookBuilder().WithId(5).Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBookWithId(1);

            // Assert
            result.Should().Be(book);
        }

        [Fact]
        public async Task GetBookWithId_ThereIsntABookWithId_ReturnsNull()
        {
            // Arrange
            var data = new List<Book>
            {
                new BookBuilder().WithId(5).Build()
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.GetBookWithId(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddBook_ValidBook_AddsOnceToDbset()
        {
            // Arrange
            var book = new BookBuilder().WithId(1).Build();
            var data = new List<Book>
            {
                new BookBuilder().Build(),
                new BookBuilder().Build(),
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            await booksService.AddBook(book);

            // Assert
            dbset.Verify(x => x.Add(book), Times.Once);
        }

        [Fact]
        public async Task AddBook_ValidBook_SaveChanges()
        {
            // Arrange
            var data = new List<Book>
            {
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            await booksService.AddBook(new BookBuilder().Build());

            // Assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RemoveBook_ValidBook_SaveChanges()
        {
            // Arrange
            var book = new BookBuilder().WithId(1).Build();
            var data = new List<Book>
            {
                book
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            await booksService.RemoveBook(book.Id);

            // Assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RemoveBook_ValidBook_RemovesBookOnce()
        {
            // Arrange
            var book = new BookBuilder().WithId(1).Build();
            var data = new List<Book>
            {
                book
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.RemoveBook(book.Id);

            // Assert
            dbset.Verify(x => x.Remove(It.IsAny<Book>()), Times.Once);
            result.Should().Be(true);
        }

        [Fact]
        public async Task RemoveBook_WithoutBook_ReturnsFalse()
        {
            // Arrange
            var data = new List<Book>
            {
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.RemoveBook(1);

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task UpdateBook_ValidBook_SaveChanges()
        {
            // Arrange
            var book = new BookBuilder().WithId(1).Build();
            var data = new List<Book>
            {
                book
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            await booksService.UpdateBook(book);

            // Assert
            context.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBook_ValidBook_UpdatesBookOnce()
        {
            // Arrange
            var book = new BookBuilder().WithId(1).Build();
            var data = new List<Book>
            {
                book
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);

            // Act
            var result = await booksService.UpdateBook(book);

            // Assert
            result.Should().Be(true);
            dbset.Verify(x => x.Update(It.IsAny<Book>()), Times.Once);

        }

        [Fact]
        public async Task UpdateBook_ValidBook_UpdatesBook()
        {
            // Arrange
            var book = new BookBuilder().WithId(1).Build();
            var data = new List<Book>
            {
                book
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);
            var updatedBook = new BookBuilder()
                .WithId(book.Id)
                .WithTitle("aa")
                .WithIsbn("11")
                .WithReadingEnd(DateTime.Today)
                .WithReadingStart(DateTime.Today)
                .WithYear(2000)
                .Build();

            // Act
            await booksService.UpdateBook(updatedBook);

            // Assert
            book.Should().BeEquivalentTo(updatedBook);
        }

        [Fact]
        public async Task UpdateBook_WithoutBook_ReturnsFalse()
        {
            // Arrange
            var data = new List<Book>
            {
            };
            var dbset = GenerateEnumerableDbSetMock(data.AsQueryable());
            var context = GenerateEnumerableContextMock(dbset);
            var booksService = new BooksService(context.Object);
            var book = new BookBuilder().WithId(1).Build();

            // Act
            var result = await booksService.UpdateBook(book);

            // Assert
            result.Should().Be(false);
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

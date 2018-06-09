using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Interfaces;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Controllers
{
    /// <summary>
    /// Controls book management.
    /// User must be logged in.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBooksService _booksService;

        public BookController(IBooksService booksService, IUserService userService)
        {
            _booksService = booksService;
            _userService = userService;
        }

        /// <summary>
        /// Gets all books for current user.
        /// </summary>
        /// <returns>
        /// List of all user's books
        /// </returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUserBooks()
        {
            var user = await GetCurrentUser();
            var books = await _booksService.GetAll(user.Id);
            return Ok(AutoMapper.Mapper.Map<List<BookResponse>>(books));
        }

        /// <summary>
        /// Gets books after applying filters.
        /// </summary>
        /// <returns>
        /// List of filtered user's books
        /// </returns>
        [HttpGet("Filter/{yearSince}/{yearTo}/{read}/{currentlyReading}/{order}/{title?}")]
        public async Task<IActionResult> GetFilteredBooks(int yearSince, int yearTo, bool read, bool currentlyReading, int order, string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var filters = new BookFilters()
            {
                PublicationYearSince = yearSince,
                PublicationYearTo = yearTo,
                Read = read,
                CurrentlyReading = currentlyReading,
                Title = title,
                order = (OrderByFiled)order
            };

            var user = await GetCurrentUser();
            var books = await _booksService.GetFilteredBooks(user.Id, filters);
            return Ok(AutoMapper.Mapper.Map<List<BookResponse>>(books));
        }

        /// <summary>
        /// Gets single book with given Id if user owns this book.
        /// </summary>
        /// <returns>
        /// Book with Id
        /// </returns>
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var user = await GetCurrentUser();
            var book = await _booksService.GetBookWithId(id);

            if(book.UserId != user.Id)
            {
                return NoContent();
            }

            return Ok(AutoMapper.Mapper.Map<BookResponse>(book));
        }

        /// <summary>
        /// Adds book to the database
        /// </summary>
        /// <returns>
        /// 200 status when successed
        /// </returns>
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] BookRequest bookRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await GetCurrentUser();
            var book = AutoMapper.Mapper.Map<Book>(bookRequest);

            book.UserId = user.Id;
            book.User = user;

            await _booksService.AddBook(book);
            return Ok();
        }

        /// <summary>
        /// Removes book with given Id from the database. Only owner can remve his book.
        /// </summary>
        /// <returns>
        /// 200 status when successed
        /// </returns>
        [HttpDelete("RemoveBook/{id}")]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var user = await GetCurrentUser();

            var book = await _booksService.GetBookWithId(id);

            if(book != null && book.UserId != user.Id)
            {
                return NoContent();
            }

            if(!await _booksService.RemoveBook(id))
            {
                return NoContent();
            }

            return Ok();
        }

        /// <summary>
        /// Updates book in the database
        /// </summary>
        /// <returns>
        /// 200 status when successed
        /// </returns>
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] BookRequest bookRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await GetCurrentUser();
            var book = AutoMapper.Mapper.Map<Book>(bookRequest);

            var toUpdate = await _booksService.GetBookWithId(book.Id);
            if(toUpdate.UserId != user.Id)
            {
                return NoContent();
            }

            book.User = user;
            book.UserId = user.Id;

            if(!await _booksService.UpdateBook(book))
            {
                return NoContent();
            }

            return Ok();
        }

        /// <summary>
        /// Adds ReadingStart to the given book. Only owner can change state of his book.
        /// </summary>
        /// <returns>
        /// 200 status when successed
        /// </returns>
        [HttpPut("SetStartReading")]
        public async Task<IActionResult> SetStartReading([FromBody] BookRequest bookRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await GetCurrentUser();
            var book = AutoMapper.Mapper.Map<Book>(bookRequest);

            var toUpdate = await _booksService.GetBookWithId(book.Id);
            if(toUpdate.UserId != user.Id || toUpdate.ReadingStart != null)
            {
                return NoContent();
            }

            book = toUpdate;
            book.ReadingStart = DateTime.Now;

            if(!await _booksService.UpdateBook(book))
            {
                return NoContent();
            }

            return Ok();
        }

        /// <summary>
        /// Adds ReadingEnd to the given book. Only owner can change state of his book.
        /// </summary>
        /// <returns>
        /// 200 status when successed
        /// </returns>
        [HttpPut("SetEndReading")]
        public async Task<IActionResult> SetEndReading([FromBody] BookRequest bookRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await GetCurrentUser();
            var book = AutoMapper.Mapper.Map<Book>(bookRequest);

            var toUpdate = await _booksService.GetBookWithId(book.Id);
            if(toUpdate.UserId != user.Id || toUpdate.ReadingEnd != null || toUpdate.ReadingStart == null)
            {
                return NoContent();
            }

            book = toUpdate;
            book.ReadingEnd = DateTime.Now;

            if(!await _booksService.UpdateBook(book))
            {
                return NoContent();
            }

            return Ok();
        }

        #region Helpers

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var claims = HttpContext.User.Claims;
            var email = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
            return await _userService.GetUserByEmail(email);
        }

        #endregion
    }
}
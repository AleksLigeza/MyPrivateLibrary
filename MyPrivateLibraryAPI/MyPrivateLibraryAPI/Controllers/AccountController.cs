using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyPrivateLibraryAPI.DbModels;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Controllers
{
    /// <summary>
    /// Controls registration and login operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// Login post method
        /// </summary>
        /// <returns>
        /// JWT token when valid
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await Authenticate(loginModel);

            if(user != null)
            {
                return Authorized(user);
            }

            return BadRequest();
        }

        /// <summary>
        /// Register post method
        /// </summary>
        /// <returns>
        /// JWT token when valid
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest registerModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new ApplicationUser()
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                LastName = registerModel.Lastname,
                FirstName = registerModel.Firstname
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                return Authorized(user);
            }

            return BadRequest();
        }

        #region Helpers

        private IActionResult Authorized(ApplicationUser user)
        {
            var tokenString = BuildToken(user);
            return Ok(new { token = tokenString });
        }

        private async Task<ApplicationUser> Authenticate(LoginRequest model)
        {
            if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return await Task.FromResult<ApplicationUser>(null);

            var userToVerify = await _userManager.FindByEmailAsync(model.Email);
            if(userToVerify == null)
                return await Task.FromResult<ApplicationUser>(null);
    
            var correctPassword = await _userManager.CheckPasswordAsync(userToVerify, model.Password);

            return correctPassword ? userToVerify : null;
        }

        private string BuildToken(ApplicationUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

    }
}
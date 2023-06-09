﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IConfiguration config;

        public AuthController(IAuthRepository authRepository,IConfiguration config)
        {
            this.authRepository = authRepository;
            this.config = config;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] UserToRegisterDto user)
        {
            try
            {
                await authRepository.RegisterAsync(user);
                if(user == null)
                {
                    return BadRequest("Email already exists");
                }

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"An error occurred during registration. Please try again later.");
            }
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult Login([FromBody] UserToLoginDto user)
        {
            try
            {
                var userInfo = authRepository.AuthenticateAsync(user);
                if(userInfo == null)
                {
                    return BadRequest("Email or password are not valid");
                }

                return Ok(CreateJWT(userInfo));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"An error occurred during login. Please try again later.");
            }
        }

        private string CreateJWT(UserAccount user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Country,user.Country)
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
               config["Jwt:Audience"],
               claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

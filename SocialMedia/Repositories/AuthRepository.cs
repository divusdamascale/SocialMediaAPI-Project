using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Data;
using SocialMedia.Models;
using SocialMedia.Models.DTO;
using SocialMedia.Repositories.Interfaces;
using System.Security.Cryptography;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SocialDBContext db;

        public AuthRepository(SocialDBContext db)
        {
            this.db = db;
        }

        public  UserAccount? AuthenticateAsync(UserToLoginDto user)
        {
            var userInfo =  db.UserAccounts.SingleOrDefault(x => x.Email.Equals(user.Email));

            if(userInfo == null || userInfo.PasswordSalt == null)
            {
                return null;
            }

            if(!MatchPasswordHash(user.Password,userInfo.PasswordHash,userInfo.PasswordSalt))
            {
                return null;
            }


            return userInfo;
        }

        public bool MatchPasswordHash(string password,byte[] hash,byte[] salt)
        {
            using(var hmac = new HMACSHA512(salt))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < hash.Length; i++)
                {
                    if(hash[i] != passwordHash[i]) return false;
                }
            }



            return true;
        }

        public async Task<UserToRegisterDto?> RegisterAsync(UserToRegisterDto user)
        {
            if(db.UserAccounts.Any(x => x.Email == user.Email))
            {
                return null;
            }

            byte[] passwordHash, passwordkey;

            using(var hmac = new HMACSHA512())
            {
                passwordkey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Password));
            }

            UserAccount userAccount = new UserAccount
            {
                Email = user.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordkey,
                Country = user.Country,
                Name = user.Name,
                DateOfBirth = user.DateOfBirth
            };
            await db.UserAccounts.AddAsync(userAccount);
            db.SaveChanges();

            return user;
        }
    }
}


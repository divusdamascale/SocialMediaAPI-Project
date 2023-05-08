using Microsoft.AspNetCore.Mvc;
using SocialMedia.Models;
using SocialMedia.Models.DTO;

namespace SocialMedia.Repositories.Interfaces
{
    public interface IAuthRepository
    {

        UserAccount? AuthenticateAsync(UserToLoginDto user);

        Task<UserToRegisterDto?> RegisterAsync(UserToRegisterDto user);

        bool MatchPasswordHash(string password,byte[]hash,byte[]salt);
    }
}

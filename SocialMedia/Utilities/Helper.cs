using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialMedia.Utilities
{
    public static class Helper
    {

        public static int ExtractUserIdFromJwt(string jwtToken)
        {


            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(jwtToken);

            var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if(userIdClaim != null && int.TryParse(userIdClaim.Value,out var userId))
            {
                return userId;
            }

            throw new Exception("Nu s-a putut extrage ID-ul utilizatorului din tokenul JWT.");
        }

    }
}

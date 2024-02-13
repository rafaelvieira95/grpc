using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrpcServiceAuthenticationJWT
{
    public static class JwtAuthenticationManager
    {

        public const string JWT_TOKEN_KEY = "464f0939-f2a1-4fa0-b835-1573d7e073d6";
        private const int JWT_TOKEN_VALIDITY = 300;
        public static AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            if (request.UserName != "admin" || request.Password != "admin")
                return null;

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN_KEY);
            var tokenExpiryDateTime = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("username", request.UserName),
                    new Claim(ClaimTypes.Role, "Administrator")
                }),
                Expires = tokenExpiryDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            

            return new AuthenticationResponse { AccessToken = token, ExpiresIn = (int)tokenExpiryDateTime.Subtract(DateTime.Now).TotalSeconds };
            
        }
    }
}

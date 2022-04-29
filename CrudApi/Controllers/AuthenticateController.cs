using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using CrudApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CrudApi.Controllers
{
    // [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthenticateController
    {
        private IJwtSigningEncodingKey SigningEncodingKey { get; }
        
        public AuthenticateController(IJwtSigningEncodingKey signingEncodingKey)
        {
            SigningEncodingKey = signingEncodingKey;
        }
        
        /// <summary>
        /// метод для получения токена аутентификации, должен быть allowanonimus
        /// </summary>
        /// <returns>токен, который после нужно передавать в заголовке запроса к api</returns>
        [HttpPost] // post так как скорее всего в метод будут приходить данные для валидации
        public Task<ActionResult<string>> GetJwtToken()
        {
            // здесь можно установить валидацию пользователя
            
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };
 
            var token = new JwtSecurityToken(
                issuer: "CrudApi",
                audience: "CrudApiClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                    SigningEncodingKey.GetKey(),
                    SigningEncodingKey.SigningAlgorithm)
            );
 
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult<ActionResult<string>>(jwtToken);
        }
    }
}
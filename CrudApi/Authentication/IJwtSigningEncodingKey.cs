using Microsoft.IdentityModel.Tokens;

namespace CrudApi.Authentication
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }
        SymmetricSecurityKey SecretKey { init; }
 
        SecurityKey GetKey();
    }
}
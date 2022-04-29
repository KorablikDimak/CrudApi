using Microsoft.IdentityModel.Tokens;

namespace CrudApi.Authentication
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
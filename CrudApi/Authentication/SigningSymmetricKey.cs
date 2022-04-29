using Microsoft.IdentityModel.Tokens;

namespace CrudApi.Authentication
{
    /// <summary>
    /// интерфейс IJwtSigningDecodingKey нужен на случай если для авторизации будет использоваться ассимитричное шифрования
    /// </summary>
    public class SigningSymmetricKey : IJwtSigningDecodingKey, IJwtSigningEncodingKey
    {
        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;
        /// <summary>
        /// ключ для шифрования устанавливается лишь один раз и больше не должен меняться
        /// </summary>
        public SymmetricSecurityKey SecretKey { private get; init; }

        public SecurityKey GetKey()
        {
            return SecretKey;
        }
    }
}
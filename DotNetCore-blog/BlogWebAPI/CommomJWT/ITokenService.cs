using System.Collections.Generic;
using System.Security.Claims;

namespace CommomJWT
{
    public interface ITokenService
    {
        string BuildToken(IEnumerable<Claim> claims, JWTOptions options);
    }
}

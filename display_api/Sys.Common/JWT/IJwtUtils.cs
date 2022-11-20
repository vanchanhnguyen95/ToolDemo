using Sys.Common.Models;
using System;

namespace Sys.Common.JWT
{
    public interface IJwtUtils
    {
        string GenerateJwtToken(Auth user, string Id, string secretKey = "");

        string GenerateRefreshToken(Auth user, out DateTime Expire);

        JWTResponse ValidateJwtToken(string token);

        bool ValidateJwtPrincipalSecretKey(string token);

        RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
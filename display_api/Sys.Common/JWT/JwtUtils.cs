using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Sys.Common.Constants;
using Sys.Common.Helper;
using Sys.Common.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sys.Common.JWT
{
    public class JwtUtils : IJwtUtils
    {
        private readonly ILogger<JwtUtils> _logger;
        private readonly IDistributedCache _cache;
        private List<string> _banned;
        private List<string> _reLogin;

        public JwtUtils(ILogger<JwtUtils> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
            _reLogin = GetCacheRelogin();
            _banned = GetCaches();
        }

        public string GenerateJwtToken(Auth user, string Id, string secretKey)
        {
            // generate token that is valid for 15 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SystemConfig.JWT_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", user.UserName.ToString()), new Claim("UserID", Id), new Claim("SecretKey", secretKey) }),
                Expires = DateTime.UtcNow.AddMinutes(SystemConfig.JWT_ACC_EXPIRE),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(Auth user, out DateTime Expire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SystemConfig.JWT_KEY);
            Expire = DateTime.UtcNow.AddMinutes(SystemConfig.JWT_REF_EXPIRE);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", user.UserName.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(SystemConfig.JWT_REF_EXPIRE),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow.AddMilliseconds(DateTime.Now.Millisecond)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public JWTResponse ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SystemConfig.JWT_KEY);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value?.ToString();
                var phonenumber = jwtToken.Claims.FirstOrDefault(x => x.Type == "phonenumber")?.Value?.ToString();
                var secretKey = jwtToken.Claims.FirstOrDefault(x => x.Type == "SecretKey")?.Value?.ToString();
                var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "username")?.Value?.ToString();
                var emp = jwtToken.Claims.FirstOrDefault(x => x.Type == "EmpId")?.Value?.ToString();
                if (_reLogin.Exists(x => x.Equals(userName)))
                {
                    JWTResponse response = new JWTResponse()
                    {
                        UserName = userName,
                        ErrorCode = ErrorCodes.Account.AccountReLogin
                    };
                    return response;
                }
                else if (_banned.Exists(x => x.Equals(userName)))
                {
                    JWTResponse response = new JWTResponse()
                    {
                        UserName = userName,
                        ErrorCode = ErrorCodes.Account.AccountLocked
                    };
                    return response;
                }
                else
                {
                    JWTResponse response = new JWTResponse()
                    {
                        PhoneNumber = phonenumber,
                        UserId = userId,
                        SecretKey = secretKey,
                        UserName = userName,
                        ErrorCode = null,
                        EmpId = emp
                    };
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public bool ValidateJwtPrincipalSecretKey(string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SystemConfig.JWT_KEY);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var secretKey = jwtToken.Claims.FirstOrDefault(x => x.Type == "SecretKey")?.Value?.ToString();

                if (PrincipalHelper.SecretKey.IsNullOrWriteSpace() || secretKey.IsNullOrWriteSpace() || !secretKey.Equals(PrincipalHelper.SecretKey))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            // generate token that is valid for 7 days
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiredDate = DateTime.UtcNow.AddDays(1),
                CreatedDate = DateTime.UtcNow,
                IP = ipAddress
            };

            return refreshToken;
        }

        private List<string> GetCaches()
        {
            List<string> results = new List<string>();
            try
            {
                var items = _cache.GetStringAsync(CacheKey.ACC_BAN).Result;
                if (items != null)
                    results = JsonHelper.Deserialize<List<string>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return results;
        }

        private List<string> GetCacheRelogin()
        {
            List<string> results = new List<string>();
            try
            {
                var items = _cache.GetStringAsync(CacheKey.ACC_RELOGIN).Result;
                if (items != null)
                    results = JsonHelper.Deserialize<List<string>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return results;
        }

        private void SetCache(string username)
        {
            try
            {
                var items = _cache.GetStringAsync(CacheKey.ACC_BAN).Result;
                if (items == null)
                {
                    List<string> lst = new List<string>()
                    {
                        username
                    };
                    string json = JsonHelper.Serialize(lst);
                    _cache.SetString(CacheKey.ACC_BAN, json);
                }
                else
                {
                    var json = JsonHelper.Deserialize<List<string>>(items);
                    if (json != null && !json.Exists(x => x.Equals(username.Trim())))
                        json.Add(username);

                    string jsonString = JsonHelper.Serialize(json);
                    _cache.SetString(CacheKey.ACC_BAN, jsonString);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void ReLogin(string username)
        {
            try
            {
                var items = _cache.GetStringAsync(CacheKey.ACC_RELOGIN).Result;
                if (items == null)
                {
                    List<string> lst = new List<string>()
                    {
                        username
                    };
                    string json = JsonHelper.Serialize(lst);
                    _cache.SetString(CacheKey.ACC_RELOGIN, json);
                }
                else
                {
                    var json = JsonHelper.Deserialize<List<string>>(items);
                    if (json != null && !json.Exists(x => x.Equals(username.Trim())))
                        json.Add(username);

                    string jsonString = JsonHelper.Serialize(json);
                    _cache.SetString(CacheKey.ACC_RELOGIN, jsonString);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
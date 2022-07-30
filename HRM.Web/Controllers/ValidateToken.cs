using System.IdentityModel.Tokens.Jwt;
using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace HRM.Web.Controllers
{
    public class ValidateToken
    {
        public bool IsTokenInvalidOrEmpty(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            var jwtToken = new JwtSecurityToken(token);
            return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
        }
      
    }
}

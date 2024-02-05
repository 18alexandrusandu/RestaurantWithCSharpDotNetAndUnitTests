using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestaurantExtended.Models;
using ServiceStack.Text;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static RestaurantExtended.Areas.Identity.Pages.Account.LoginModel;

namespace RestaurantExtended.Services
{
    public class JWTGenerator
    {


        private static  String secret = "Alexandru_site12345678910111213141516117181920212223242526272829303132";
        private static String issuer_localhost = "https://localhost:7094";
        private static String audiance_localhost = "anybody";
        public static  int jwttime=60;
        public static string GenerateToken(TokenUserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            List<Claim> termsList = new List<Claim>();
            termsList.Add(new Claim(ClaimTypes.NameIdentifier, user.Email));

       

            // You can convert it back to an array if you would like to
  
            foreach (string  role in user.roles)
            {
                Debug.WriteLine("role in token" + role);
                termsList.Add(new Claim(ClaimTypes.Role, role));

            }
            var claims = termsList.ToArray();




            var token = new JwtSecurityToken(issuer_localhost,
                audiance_localhost,
                claims,
                expires: DateTime.Now.AddMinutes(jwttime),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

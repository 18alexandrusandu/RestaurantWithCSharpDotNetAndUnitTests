using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestaurantExtended.Models
{
    public class TokenUserModel
    {

        public string Email{get;set;}
        public string Password { get; set; }

        public List<string>  roles { get; set; }
        
        public string UserName{get;set;}



    }
}

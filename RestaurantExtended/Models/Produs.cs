
using System.Drawing;
using Microsoft.EntityFrameworkCore;
namespace RestaurantExtended.Models
{
    public class Produs
    {
        public String? name { get; set; }
        public int Id { get; set; } 
        public int stoc { get; set; }
        public double pret { get; set; }
        public List<Image> images { get;set; }
    }
}

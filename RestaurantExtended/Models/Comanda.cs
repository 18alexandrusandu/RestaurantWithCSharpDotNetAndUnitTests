
using Microsoft.EntityFrameworkCore;

namespace RestaurantExtended.Models
{
    public class Comanda
    {
        public DateTime Date { get; set; }
        public int Id { get;set; }
        public string? Status { get; set; }
       
        public double pretTotal { get; set; }

        public List<ComandaProdus> produseComanda { get; set; }  

    }
}

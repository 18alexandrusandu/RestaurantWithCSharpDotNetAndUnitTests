namespace RestaurantExtended.Models
{
    public class ComandaProdus
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public int ProductId { get; set; }

        public int quantity { get; set; } = 1;
    }

}

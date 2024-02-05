using RestaurantExtended.Controllers.dtos;
using RestaurantExtended.Models;

namespace RestaurantExtended.Services
{
    public interface IComenziService
    {
        public Task<bool> CreazaComanda(Comanda comanda);

        public List<Comanda> GetComenziByDate(DateTime start, DateTime end);
        public Task<List<RankedProduct>> GetProduseComandateByDate(DateTime start, DateTime end);

        public Task<List<Comanda>> ListAll();


        public Task<Comanda> GetComanda(int id);

        public Task<int> DeleteComanda(int id);

        public Task<int> UpdateComanda(Comanda comanda);

        public ExportResultDto export(int type, String filename, List<object> objects);
    }
}

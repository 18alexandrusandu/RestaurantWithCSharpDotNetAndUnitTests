using RestaurantExtended.Models;

namespace RestaurantExtended.Repositories
{
    public interface IComenziRepository
    {
        public Task<Comanda> Get(int id);


        public Task<IEnumerable<Comanda>> GetAll();


        public Task Add(Comanda entity);

        public void Delete(Comanda entity);

        public void Update(Comanda entity);


        public IEnumerable<Comanda> GetComenziByInterval(DateTime start, DateTime end);

        public IEnumerable<ComandaProdus> GetComenziProduseById(int id);

        public void AddComenziProduse(int id, List<Produs> produse);
    }
}

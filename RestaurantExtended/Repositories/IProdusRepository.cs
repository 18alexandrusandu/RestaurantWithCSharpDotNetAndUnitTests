using RestaurantExtended.Models;

namespace RestaurantExtended.Repositories
{
    public interface IProdusRepository
    {


        public  Task<Produs> Get(int id);


        public  Task<IEnumerable<Produs>> GetAll();


        public Task Add(Produs entity);

        public void Delete(Produs entity);

        public void Update(Produs entity);
        









    }
}

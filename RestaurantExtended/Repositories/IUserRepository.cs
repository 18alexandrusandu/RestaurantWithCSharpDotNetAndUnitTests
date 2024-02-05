using RestaurantExtended.Models;

namespace RestaurantExtended.Repositories
{
    public interface IUserRepository
    {
        public Task<User> Get(int id);


        public Task<IEnumerable<User>> GetAll();


        public Task Add(User entity);

        public void Delete(User entity);

        public void Update(User entity);


    }
}

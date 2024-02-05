using Microsoft.EntityFrameworkCore;
using RestaurantExtended.Data;
using RestaurantExtended.Models;

namespace RestaurantExtended.Repositories.implementation
{
    public class UserRepository:IUserRepository
    {
        public UserRepository(Context2 context)
        {
            _context = context;
        }

        Context2 _context { get; set; }
        public async Task<User> Get(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task Add(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
        }

        public void Delete(User entity)
        {
            _context.Set<User>().Remove(entity);
        }

        public void Update(User entity)
        {
            _context.Set<User>().Update(entity);
        }






    }
}





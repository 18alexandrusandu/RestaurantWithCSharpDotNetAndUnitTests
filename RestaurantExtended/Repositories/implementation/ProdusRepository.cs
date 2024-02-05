using Microsoft.EntityFrameworkCore;
using RestaurantExtended.Data;
using RestaurantExtended.Models;

namespace RestaurantExtended.Repositories.implementation
{
    public class ProdusRepository : GeneralRepository<Produs>,IProdusRepository
    {
        public ProdusRepository(RestaurantDbContext context) : base(context)
        {





        }
        public new async Task<IEnumerable<Produs>> GetAll()
        {

            return await _context.Set<Produs>().Include(produs => produs.images).ToListAsync();
        }
        public async Task<Produs> Get(int id)
        {
            return await _context.Set<Produs>().Include(produs => produs.images).Where(x => x.Id == id).FirstAsync();


        }
    }
}

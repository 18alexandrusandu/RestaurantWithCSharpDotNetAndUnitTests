using ExcelLibrary.BinaryFileFormat;
using Microsoft.EntityFrameworkCore;
using RestaurantExtended.Data;
using RestaurantExtended.Models;

namespace RestaurantExtended.Repositories.implementation
{
    public class ComenziRepository : GeneralRepository<Comanda>, IComenziRepository
    {
        public ComenziRepository(RestaurantDbContext context) : base(context)
        {
        }






        public IEnumerable<Comanda> GetComenziByInterval(DateTime start, DateTime end)
        {

            return _context.Comanda.Include(C => C.produseComanda).Where(a => a.Date >= start && a.Date <= end)

                .ToList();
        }

        public new async Task<Comanda> Get(int id)
        {

            return _context.Comanda.Include(C => C.produseComanda).Where(a => a.Id == id).ToList().First();
        }








        public new async Task<IEnumerable<Comanda>> GetAll()
        {

            return await _context.Comanda.Include(C => C.produseComanda).ToListAsync();
        }
        public IEnumerable<ComandaProdus> GetComenziProduseById(int id)
        {

            return _context.comandaProdus.Where(a => a.ComandaId == id).ToList();
        }
        public void AddComenziProduse(int id, List<Produs> produse)
        {
            foreach (Produs p in produse)
            {
                ComandaProdus comandaProdus = new ComandaProdus();
                comandaProdus.ProductId = p.Id;
                comandaProdus.ComandaId = id;
                comandaProdus.quantity = 1;


            }





        }

    }
}

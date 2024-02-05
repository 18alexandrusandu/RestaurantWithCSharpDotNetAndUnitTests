using RestaurantExtended.Data;

namespace RestaurantExtended.Repositories.implementation
{
    public class UnitOfWork : IUnitOfWork
    {


        public UnitOfWork(IUserRepository userRepository, IProdusRepository produsRepository, IComenziRepository comenziRepository, RestaurantDbContext context, Context2 context2)
        {
            UserRepository = userRepository;
            ProdusRepository = produsRepository;
            ComenziRepository = comenziRepository;
            _context = context;
            _context2 = context2;
        }



        public RestaurantDbContext _context { get; set; }
        public Context2 _context2 { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IProdusRepository ProdusRepository { get; set; }
        public IComenziRepository ComenziRepository { get; set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> Save()
        {
            return _context.SaveChangesAsync();
        }

        public void SaveAsync()
        {
            throw new NotImplementedException();
        }


    }
}

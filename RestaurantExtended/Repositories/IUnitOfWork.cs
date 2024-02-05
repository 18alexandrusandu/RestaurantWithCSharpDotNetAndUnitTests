using RestaurantExtended.Repositories.implementation;

namespace RestaurantExtended.Repositories
{
    public interface IUnitOfWork
    {
        public  abstract IUserRepository UserRepository { get; set; }
        public abstract IProdusRepository ProdusRepository { get; set; }

        public abstract IComenziRepository ComenziRepository { get; set; }
        public Task<int> Save();

        public void SaveAsync();


        public void Dispose();
    }
}

using RestaurantExtended.Models;

namespace RestaurantExtended.Services
{
    public interface IProductService
    {
        public Task<int> addProduct(Produs produs);
        public  Task<Produs> getProduct(int id);

        public Task<int> deleteProduct(int id);
        public Task<int> editProduct(Produs produs);

        public Task<List<Produs>> ListProducts();

    }
}

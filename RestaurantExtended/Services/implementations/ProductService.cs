using RestaurantExtended.Models;
using RestaurantExtended.Repositories;

namespace RestaurantExtended.Services.implementations
{
    public class ProductService:IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> addProduct(Produs produs)
        {
            if (produs.stoc < 0)
                produs.stoc = 0;

            await _unitOfWork.ProdusRepository.Add(produs);

              return await _unitOfWork.Save();
        }

        public async Task<Produs> getProduct(int id)
        {
           Produs prod= await _unitOfWork.ProdusRepository.Get(id);

            return prod;
        }

        public async Task<int> editProduct(Produs produs)
        {

            if (produs.stoc < 0)
                produs.stoc = 0;

           _unitOfWork.ProdusRepository.Update(produs);

            return await _unitOfWork.Save();
        }

        public async Task<int> deleteProduct(int id)
        {
          Produs prod=await  _unitOfWork.ProdusRepository.Get(id);

            _unitOfWork.ProdusRepository.Delete(prod);

            return await _unitOfWork.Save();
        }

        public async Task<List<Produs>> ListProducts()
        {
            return  (List<Produs>)await _unitOfWork.ProdusRepository.GetAll();
        }
    }
}

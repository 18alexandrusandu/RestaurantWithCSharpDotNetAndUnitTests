using RestaurantExtended.Repositories;

namespace RestaurantExtended.Services.implementations
{
    public class UserService:IUserService
    {
        public IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}

using Persistence.UnitOfWork;

namespace Business.Services;

public class UserService(IUnitOfWork unitOfWork) {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    
}
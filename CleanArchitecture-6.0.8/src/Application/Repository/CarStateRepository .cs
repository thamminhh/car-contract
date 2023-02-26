using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarStateRepository : ICarStateRepository
    {
        private readonly ContractContext _contractContext;

        public CarStateRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarState GetCarStateById(int id)
        {
            return _contractContext.CarStates.Where(c => c.Id == id).FirstOrDefault();
        }

        public CarState GetCarStateByCarId(int carId)
        {
            return _contractContext.CarStates.Where(c => c.CarId == carId).FirstOrDefault();
        }

        public bool CarStateExit(int id)
        {
            return _contractContext.CarStates.Any(c => c.Id == id);
        }
    }
}

using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarFileRepository : ICarFileRepository
    {
        private readonly ContractContext _contractContext;

        public CarFileRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarFile GetCarFileById(int id)
        {
            return _contractContext.CarFiles.Where(c => c.Id == id).FirstOrDefault();
        }

        public CarFile GetCarFileByCarId(int carId)
        {
            return _contractContext.CarFiles.Where(c => c.CarId == carId).FirstOrDefault();
        }
        public bool CarFileExit(int id)
        {
            return _contractContext.CarFiles.Any(c => c.Id == id);
        }
    }
}

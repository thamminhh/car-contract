using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarGenerallInfoRepository : ICarGenerallInfoRepository
    {
        private readonly ContractContext _contractContext;

        public CarGenerallInfoRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarGenerallInfo GetCarGenerallInfoById(int id)
        {
            return _contractContext.CarGenerallInfos.Where(c => c.Id == id).FirstOrDefault();
        }

        public CarGenerallInfo GetCarGenerallInfoByCarId(int carId)
        {
            return _contractContext.CarGenerallInfos.Where(c => c.CarId == carId).FirstOrDefault();
        }


        public bool CarGenerallInfoExit(int id)
        {
            return _contractContext.CarGenerallInfos.Any(c => c.Id == id);
        }
    }
}

using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarTrackingRepository : ICarTrackingRepository
    {
        private readonly ContractContext _contractContext;

        public CarTrackingRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarTracking GetCarTrackingById(int id)
        {
            return _contractContext.CarTrackings.Where(c => c.Id == id).FirstOrDefault();
        }

        public CarTracking GetCarTrackingByCarId(int carId)
        {
            return _contractContext.CarTrackings.Where(c => c.CarId == carId).FirstOrDefault();
        }


        public bool CarTrackingExit(int id)
        {
            return _contractContext.CarTrackings.Any(c => c.Id == id);
        }
    }
}

using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarTrackingRepository
    {

        CarTracking GetCarTrackingById(int id);

        CarTracking GetCarTrackingByCarId(int carId);

        bool CarTrackingExit(int id);  

    }
}

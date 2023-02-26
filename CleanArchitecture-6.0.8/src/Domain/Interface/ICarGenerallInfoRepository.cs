using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarGenerallInfoRepository
    {

        CarGenerallInfo GetCarGenerallInfoById(int id);

        CarGenerallInfo GetCarGenerallInfoByCarId(int carId);

        bool CarGenerallInfoExit(int id);  

    }
}

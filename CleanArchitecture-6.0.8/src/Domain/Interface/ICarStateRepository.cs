using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarStateRepository
    {

        CarState GetCarStateById(int id);

        CarState GetCarStateByCarId(int carId);

        bool CarStateExit(int id);  

    }
}

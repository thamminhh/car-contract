using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarFileRepository
    {

        CarFile GetCarFileById(int id);

        CarFile GetCarFileByCarId(int carId);

        bool CarFileExit(int id);  

    }
}

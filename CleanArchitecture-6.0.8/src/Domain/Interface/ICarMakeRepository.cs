using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarMakeRepository
    {

        CarMake GetCarMakeById(int id);

        ICollection<CarMake> GetCarMakes();

        int GetCarMakeIdByName (string name);


        bool CarMakeExit(int id);

        bool CreateCarMake(CarMake carmake);
        bool UpdateCarMake(CarMake carMake);
        bool Save();

    }
}

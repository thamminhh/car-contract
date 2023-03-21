using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarModelRepository
    {

        CarModel GetCarModelById(int id);

        ICollection<CarModel> GetCarModelsByCarMakeId(int carMakeId);

        int GetCarModelIdByName(string name);

        bool CarModelExit(int id);  

    }
}

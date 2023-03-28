using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarModel.Sub_Model;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarModelRepository
    {

        CarModel GetCarModelById(int id);

        public ICollection<CarModel> GetCarModels();

        ICollection<CarModel> GetCarModelsByCarMakeId(int carMakeId);

        int GetCarModelIdByName(string name);

        bool CarModelExit(int id);

        public bool UpdateCarModel(int id, CarModelUpdateModel request, out string errorMessage);

    }
}

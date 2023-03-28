using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarMakeRepository
    {

        CarMake GetCarMakeById(int id);

        ICollection<CarMake> GetCarMakes();

        int GetCarMakeIdByName (string name);


        bool CarMakeExit(int id);

        //public bool CreateCarMake(CarMakeCreateModel request, out string errorMessage);
        public bool UpdateCarMake(int id, CarMakeUpdateModel request, out string errorMessage);
        bool Save();

    }
}

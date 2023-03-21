using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly ContractContext _contractContext;

        public CarModelRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarModel GetCarModelById(int id)
        {
            return _contractContext.CarModels.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<CarModel>  GetCarModelsByCarMakeId(int carMakeId)
        {
            return _contractContext.CarModels.Where(c => c.CarMakeId == carMakeId).OrderBy(c => c.Id).ToList();
        }

        public bool CarModelExit(int id)
        {
            return _contractContext.CarModels.Any(c => c.Id == id);
        }

        public int GetCarModelIdByName(string carModelName)
        {
            CarModel carModel = _contractContext.CarModels.Where(c => c.Name == carModelName).FirstOrDefault();
            return carModel.Id;
        }
    }
}

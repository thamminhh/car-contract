using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarModel.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarModel.Sub_Model;
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

        public ICollection<CarModel> GetCarModels()
        {
            return _contractContext.CarModels.OrderBy(c => c.Id).ToList();
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

        public bool UpdateCarModel(int id, CarModelUpdateModel request, out string errorMessage)
        {
            var carModel = _contractContext.CarModels.Find(id);
            errorMessage = string.Empty;
            if (request.CarModelName == null)
            {
                errorMessage = "Car model name don't allow null";
                return false;
            }
            if (_contractContext.CarModels.Any(u => u.Name == request.CarModelName && request.CarModelName != carModel.Name))
            {
                errorMessage = "This car model already exists";
                return false;
            }
            if (!_contractContext.CarMakes.Any(u => u.Id == request.CarMakeId))
            {
                errorMessage = "This car make don't exists";
                return false;
            }
            carModel.CarMakeId = request.CarMakeId;
            carModel.Name = request.CarModelName;

            _contractContext.CarModels.Update(carModel);
            return Save();

        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

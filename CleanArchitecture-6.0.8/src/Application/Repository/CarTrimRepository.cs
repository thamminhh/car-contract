using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarSeries.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarTrim.Sub_Model;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarTrimRepository : ICarTrimRepository
    {
        private readonly ContractContext _contractContext;

        public CarTrimRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarTrim GetCarTrimById(int id)
        {
            return _contractContext.CarTrims.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<CarTrim> GetCarTrimByCarModelAndCarSeries(int carModelId, int carSeriesId)
        {
            return _contractContext.CarTrims
                .Where(c => c.CarModelId == carModelId && c.CarSeriesId == carSeriesId)
                .OrderBy(c => c.Id).ToList();
        }

        public ICollection<CarTrim> GetCarTrimByCarModelId(int carModelId)
        {
            return _contractContext.CarTrims
                .Where(c => c.CarModelId == carModelId)
                .OrderBy(c => c.Id).ToList();
        }


        public bool CarTrimExit(int id)
        {
            return _contractContext.CarTrims.Any(c => c.Id == id);
        }

        public bool UpdateCarTrim(int id, CarTrimUpdateModel request, out string errorMessage)
        {
            var carTrim = _contractContext.CarTrims.Find(id);
            errorMessage = string.Empty;
            if (request.CarTrimName == null)
            {
                errorMessage = "Car series name don't allow null";
                return false;
            }
            if (!_contractContext.CarModels.Any(u => u.Id == request.CarModelId))
            {
                errorMessage = "This car model don't exists";
                return false;
            }
            if (!_contractContext.CarSeries.Any(u => u.Id == request.CarSeriesId))
            {
                errorMessage = "This car series don't exists";
                return false;
            }
            carTrim.Name = request.CarTrimName;
            carTrim.CarModelId = request.CarModelId;
            carTrim.CarSeriesId = request.CarSeriesId;
            carTrim.StartProductYear = request.StartProductYear;
            carTrim.EndProductYear = request.EndProductYear;

            _contractContext.CarTrims.Update(carTrim);
            return Save();

        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

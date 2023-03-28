using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarSeries.Sub_Model;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarSeriesRepository : ICarSeriesRepository
    {
        private readonly ContractContext _contractContext;

        public CarSeriesRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarSeries GetCarSeriesById(int id)
        {
            return _contractContext.CarSeries.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<CarSeries> GetCarSeriesByCarModelAndCarGeneration(int carModelId, int carGenerationId)
        {
            return _contractContext.CarSeries
                .Where(c => c.CarModelId == carModelId && c.CarGenerationId == carGenerationId)
                .OrderBy(c => c.Id).ToList();
        }


        public bool CarSeriesExit(int id)
        {
            return _contractContext.CarSeries.Any(c => c.Id == id);
        }

        public bool UpdateCarSeries(int id, CarSeriesUpdateModel request, out string errorMessage)
        {
            var carSeries = _contractContext.CarSeries.Find(id);
            errorMessage = string.Empty;
            if (request.CarSeriesName == null)
            {
                errorMessage = "Car series name don't allow null";
                return false;
            }
            if (!_contractContext.CarModels.Any(u => u.Id == request.CarModelId))
            {
                errorMessage = "This car model don't exists";
                return false;
            }
            if (!_contractContext.CarGenerations.Any(u => u.Id == request.CarGenerationId))
            {
                errorMessage = "This car generation don't exists";
                return false;
            }
            carSeries.Name = request.CarSeriesName;
            carSeries.CarModelId = request.CarModelId;
            carSeries.CarGenerationId = request.CarGenerationId;

            _contractContext.CarSeries.Update(carSeries);
            return Save();

        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

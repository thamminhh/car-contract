using CleanArchitecture.Domain.Entities;
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
    }
}

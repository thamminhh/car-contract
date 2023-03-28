using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarSeries.Sub_Model;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarSeriesRepository
    {

        CarSeries GetCarSeriesById(int id);

        ICollection <CarSeries> GetCarSeriesByCarModelAndCarGeneration(int carModelId, int carGenerationId);

        bool CarSeriesExit(int id);

        public bool UpdateCarSeries(int id, CarSeriesUpdateModel request, out string errorMessage);

    }
}

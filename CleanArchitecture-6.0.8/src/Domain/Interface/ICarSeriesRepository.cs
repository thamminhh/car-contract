using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarSeriesRepository
    {

        CarSeries GetCarSeriesById(int id);

        ICollection <CarSeries> GetCarSeriesByCarModelAndCarGeneration(int carModelId, int carGenerationId);

        bool CarSeriesExit(int id);  

    }
}

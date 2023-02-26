using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarGenerationRepository
    {

        CarGeneration GetCarGenerationById(int id);

        ICollection <CarGeneration> GetCarGenerationByCarModelId(int carModelId);

        bool CarGenerationExit(int id);  

    }
}

using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarGeneration.Sub_Model;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarGenerationRepository
    {

        CarGeneration GetCarGenerationById(int id);

        ICollection <CarGeneration> GetCarGenerationByCarModelId(int carModelId);

        bool CarGenerationExit(int id);
        bool UpdateCarGeneration(int carGenerationId, CarGenerationUpdateModel request, out string errorMessage);

    }
}

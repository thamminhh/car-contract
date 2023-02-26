using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarGenerationRepository : ICarGenerationRepository
    {
        private readonly ContractContext _contractContext;

        public CarGenerationRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarGeneration GetCarGenerationById(int id)
        {
            return _contractContext.CarGenerations.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<CarGeneration> GetCarGenerationByCarModelId(int carModelId)
        {
            return _contractContext.CarGenerations.Where(c => c.CarModelId == carModelId).OrderBy(c => c.Id).ToList();
        }


        public bool CarGenerationExit(int id)
        {
            return _contractContext.CarGenerations.Any(c => c.Id == id);
        }
    }
}

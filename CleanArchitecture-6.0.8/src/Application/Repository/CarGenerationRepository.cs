using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarGeneration.Sub_Model;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;
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

        public bool UpdateCarGeneration(int id, CarGenerationUpdateModel request, out string errorMessage)
        {
            var carGeneration = _contractContext.CarGenerations.Find(id);
            errorMessage = string.Empty;
            if (request.CarGenerationName == null)
            {
                errorMessage = "Car generation name don't allow null";
                return false;
            }
            if (!_contractContext.CarModels.Any(u => u.Id == request.CarModelId ))
            {
                errorMessage = "This car model don't exists";
                return false;
            }
            carGeneration.Name = request.CarGenerationName;
            carGeneration.CarModelId = request.CarModelId;
            carGeneration.YearBegin = request.YearBegin;
            carGeneration.YearEnd = request.YearEnd;

            _contractContext.CarGenerations.Update(carGeneration);
            return Save();

        }
        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}

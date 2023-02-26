using CleanArchitecture.Domain.Entities;
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


        public bool CarTrimExit(int id)
        {
            return _contractContext.CarTrims.Any(c => c.Id == id);
        }
    }
}

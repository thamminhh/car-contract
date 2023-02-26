using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class ForControlRepository : IForControlRepository
    {
        private readonly ContractContext _contractContext;

        public ForControlRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public ForControl GetForControlById(int id)
        {
            return _contractContext.ForControls.Where(c => c.Id == id).FirstOrDefault();
        }

        public ForControl GetForControlByCarId(int carId)
        {
            return _contractContext.ForControls.Where(c => c.CarId == carId).FirstOrDefault();
        }


        public bool ForControlExit(int id)
        {
            return _contractContext.ForControls.Any(c => c.Id == id);
        }
    }
}

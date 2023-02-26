using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarLoanInfoRepository : ICarLoanInfoRepository
    {
        private readonly ContractContext _contractContext;

        public CarLoanInfoRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarLoanInfo GetCarLoanInfoById(int id)
        {
            return _contractContext.CarLoanInfos.Where(c => c.Id == id).FirstOrDefault();
        }

        public CarLoanInfo GetCarLoanInfoByCarId(int carId)
        {
            return _contractContext.CarLoanInfos.Where(c => c.CarId == carId).FirstOrDefault();
        }


        public bool CarLoanInfoExit(int id)
        {
            return _contractContext.CarLoanInfos.Any(c => c.Id == id);
        }

    }
}

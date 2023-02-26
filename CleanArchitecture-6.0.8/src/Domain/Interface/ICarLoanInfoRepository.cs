using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarLoanInfoRepository
    {

        CarLoanInfo GetCarLoanInfoById(int id);

        CarLoanInfo GetCarLoanInfoByCarId(int carId);

        bool CarLoanInfoExit(int id);  

    }
}

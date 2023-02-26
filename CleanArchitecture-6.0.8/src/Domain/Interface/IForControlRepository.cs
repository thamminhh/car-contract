using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface IForControlRepository
    {

        ForControl GetForControlById(int id);

        ForControl GetForControlByCarId(int carId);

        bool ForControlExit(int id);  

    }
}

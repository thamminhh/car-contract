using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarStatusRepository
    {

        CarStatus GetCarStatusById(int id);

        ICollection<CarStatus> GetCarStatuss();

        bool CarStatusExit(int id);

        bool CreateCarStatus(CarStatus CarStatus);
        bool UpdateCarStatus(CarStatus CarStatus);
        bool Save();

        int GetCarStatusIdByName(string name);    

    }
}

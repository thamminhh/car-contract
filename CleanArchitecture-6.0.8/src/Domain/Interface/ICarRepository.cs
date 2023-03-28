using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarRepository
    {

        ICollection<CarDataModel> GetCars(int page, int pageSize, CarFilter filter);

        ICollection<CarDataModel> GetCarsActive(int page, int pageSize, CarFilter filter);
        ICollection<CarDataModel> GetCarsMaintenance(int page, int pageSize, out int count);
        //ICollection<CarDataModel> GetCarsRegistry(/*int page, int pageSize, out int count*/);
        ICollection<Car> GetCarsByStatusId(int page, int pageSize, int carStatus);

        ICollection<Car> GetCarsByCarMakeId(int page, int pageSize, int carMakeId);

        CarDataModel GetCarById(int id);

        bool CarExit(int id);

        bool CreateCar(CarCreateModel request, out string errorMessage);

        void UpdateCar(int id, CarUpdateModel request);

        bool UpdateCarStatus(int id, CarUpdateStatusModel request); 

        bool DeleteCar(int id);

        public int GetNumberOfCars(CarFilter filter);

        public int GetNumberOfCarsActive(CarFilter filter);

        public int GetNumberOfCarsByStatusId(int carStatus);

    }
}

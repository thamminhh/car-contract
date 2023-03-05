using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarSchedule;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarScheduleRepository
    {

        ICollection<CarScheduleDataModel> GetCarSchedules();


        ICollection<CarScheduleDataModel> GetCarSchedulesByCarId(int carId);
        ICollection<CarScheduleDataModel> GetCarSchedulesStatusId(int carStatusId);

        public CarScheduleDataModel GetCarScheduleById(int CarScheduleId);

        void CreateCarSchedule(CarScheduleCreateModel request);

        void UpdateCarSchedule(int id, CarScheduleUpdateModel request);


    }
}

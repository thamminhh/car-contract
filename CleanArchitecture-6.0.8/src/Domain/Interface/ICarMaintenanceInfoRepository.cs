using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarMaintenanceInfo.SubModel;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarMaintenanceInfoRepository
    {

        CarMaintenanceInfo GetCarMaintenanceInfoById(int id);

        ICollection <CarMaintenanceInfo> GetCarMaintenanceInfoByCarId(int carId);

        void CreateCarMaintenanceInfo(CarMaintenanceInfoCreateModel request);

        void UpdateCarMaintenanceInfo(int id, CarMaintenanceInfo request);

        bool CarMaintenanceInfoExit(int id);

    }
}

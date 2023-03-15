using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarRegistryInfo.SubModel;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarRegistryInfoRepository
    {

        CarRegistryInfo GetCarRegistryInfoById(int id);

        ICollection <CarRegistryInfo> GetCarRegistryInfoByCarId(int carId);

        void CreateCarRegistryInfo(CarRegistryInfoCreateModel request);

        void UpdateCarRegistryInfo(int id, CarRegistryInfo request);

        bool CarRegistryInfoExit(int id);

    }
}

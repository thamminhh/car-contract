using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarTrim.Sub_Model;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarTrimRepository
    {

        CarTrim GetCarTrimById(int id);

        ICollection <CarTrim> GetCarTrimByCarModelAndCarSeries(int carModelId, int carSeriesId);

        public ICollection<CarTrim> GetCarTrimByCarModelId(int carModelId);

        bool CarTrimExit(int id);

        public bool UpdateCarTrim(int id, CarTrimUpdateModel request, out string errorMessage);

    }
}

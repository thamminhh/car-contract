using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarTrimRepository
    {

        CarTrim GetCarTrimById(int id);

        ICollection <CarTrim> GetCarTrimByCarModelAndCarSeries(int carModelId, int carSeriesId);

        bool CarTrimExit(int id);  

    }
}

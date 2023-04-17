
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarSchedules.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class CarScheduleRepository : ICarScheduleRepository
    {
        private readonly ContractContext _contractContext;

        public CarScheduleRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }


        public CarScheduleDataModel GetCarScheduleById(int CarScheduleId)
        {
            CarSchedule CarSchedule = _contractContext.CarSchedules
                .Include(c => c.CarStatus)
                .FirstOrDefault(c => c.Id == CarScheduleId);

            return new CarScheduleDataModel
            {
                Id = CarSchedule.Id,
                CarId = CarSchedule.CarId,
                DateStart = CarSchedule.DateStart,
                DateEnd = CarSchedule.DateEnd,
                CarStatusId = CarSchedule.CarStatusId,
                CarStatusName = CarSchedule.CarStatus.Name
            };
        }

        public ICollection<CarScheduleDataModel> GetCarSchedules()
        {
            IQueryable<CarSchedule> CarSchedules = _contractContext.CarSchedules
               .Include(c => c.Car)
               .Include(c => c.CarStatus)
               .AsQueryable();

            var CarScheduleDataModels = CarSchedules
                .OrderBy(c => c.Id)
                .Select(c => new CarScheduleDataModel
                {
                    Id = c.Id,
                    CarId = c.CarId,
                    CarLicensePlates = c.Car.CarLicensePlates,
                    DateStart = c.DateStart,
                    DateEnd = c.DateEnd,
                    CarStatusId = c.CarStatusId,
                    CarStatusName = c.CarStatus.Name,
                })
                .ToList();

            return CarScheduleDataModels;
        }

        public ICollection<CarScheduleDataModel> GetCarSchedulesStatusId(int carStatusId)
        {
            IQueryable<CarSchedule> CarSchedules = _contractContext.CarSchedules
               .Include(c => c.CarStatus)
               .Where(c => c.CarStatusId == carStatusId)
               .AsQueryable();

            var CarScheduleDataModels = CarSchedules
                .OrderBy(c => c.Id)
                .Select(c => new CarScheduleDataModel
                {
                    Id = c.Id,
                    CarId = c.CarId,
                    DateStart = c.DateStart,
                    DateEnd = c.DateEnd,
                    CarStatusId = c.CarStatusId,
                    CarStatusName = c.CarStatus.Name,
                })
                .ToList();

            return CarScheduleDataModels;
        }

        public ICollection<CarScheduleDataModel> GetCarSchedulesByCarId(int carId)
        {

            IQueryable<CarSchedule> CarSchedules = _contractContext.CarSchedules
                .Include(c => c.CarStatus)
                .Where(c => c.CarId == carId)
                .AsQueryable();

            var CarScheduleDataModels = CarSchedules
                .OrderBy(c => c.Id)
                .Select(c => new CarScheduleDataModel
                {
                    Id = c.Id,
                    CarId = c.CarId,
                    DateStart = c.DateStart,
                    DateEnd = c.DateEnd,
                    CarStatusId = c.CarStatusId,
                    CarStatusName = c.CarStatus.Name,
                })
                .ToList();

            return CarScheduleDataModels;
        }

        public void CreateCarSchedule(CarScheduleCreateModel request)
        {
            var CarSchedule = new CarSchedule
            {
                CarId = request.CarId,
                DateStart = request.DateStart,
                DateEnd = request.DateEnd,
                CarStatusId = request.CarStatusId,
            };

            // Save the new CarSchedule to the database
            _contractContext.CarSchedules.Add(CarSchedule);
            _contractContext.SaveChanges();
        }

        public void UpdateCarSchedule(int id, CarScheduleUpdateModel request)
        {
            var CarSchedule = _contractContext.CarSchedules.Find(id);

            // Update the properties of the CarSchedule object
            CarSchedule.CarId = request.CarId;
            CarSchedule.DateStart = request.DateStart;
            CarSchedule.DateEnd = request.DateEnd;
            CarSchedule.CarStatusId = request.CarStatusId;

            // Save the changes to the database
            _contractContext.CarSchedules.Update(CarSchedule);
            _contractContext.SaveChanges();

        }

        public async Task<bool> DeleteCarSchedule(int carScheduleId)
        {
            var carSchedule = await _contractContext.CarSchedules.FindAsync(carScheduleId);

            if (carSchedule == null)
            {
                return false; // Object not found
            }

            _contractContext.CarSchedules.Remove(carSchedule);
            await _contractContext.SaveChangesAsync();

            return true; // Object deleted successfully
        }

    }
}

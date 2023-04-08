
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore;
using PdfSharpCore.Pdf.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;

namespace CleanArchitecture.Application.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ContractContext _contractContext;
        private readonly ICarMakeRepository _carMakeController;
        private readonly ICarModelRepository _carModelController;
        private readonly ICarScheduleRepository _carScheduleRepository;



        public CarRepository(ContractContext contractContext,
            ICarMakeRepository carMakeController, ICarScheduleRepository carScheduleRepository, ICarModelRepository carModelRepository)
        {
            _contractContext = contractContext;
            _carMakeController = carMakeController;
            _carScheduleRepository = carScheduleRepository;
            _carModelController = carModelRepository;
        }

        public int GetNumberOfCars(CarFilter filter)
        {
            IQueryable<Car> cars = _contractContext.Cars;

            if (filter != null)
            {
                if (filter.CarStatusId.HasValue)
                {
                    cars = cars.Where(c => c.CarStatusId == filter.CarStatusId);
                }
                if (filter.ParkingLotId.HasValue)
                {
                    cars = cars.Where(c => c.ParkingLotId == filter.ParkingLotId);
                }
                if (!string.IsNullOrWhiteSpace(filter.CarLicensePlates))
                {
                    cars = cars.Where(c => c.CarLicensePlates.Contains(filter.CarLicensePlates));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarMakeName))
                {
                    int carMakeId = _carMakeController.GetCarMakeIdByName(filter.CarMakeName);
                    cars = cars.Where(c => c.CarMakeId.Equals(carMakeId));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarModelName))
                {
                    int carMakeId = _carModelController.GetCarModelIdByName(filter.CarModelName);
                    cars = cars.Where(c => c.CarModelId.Equals(carMakeId));
                }
                if (filter.SeatNumber.HasValue)
                {
                    cars = cars.Where(c => c.SeatNumber == filter.SeatNumber.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.CarColor))
                {
                    cars = cars.Where(c => c.CarColor.Equals(filter.CarColor));
                }
                if (filter.CarTrimId.HasValue)
                {
                    cars = cars.Where(c => c.CarTrimId == filter.CarTrimId);
                }
                if (filter.DateStart.HasValue && filter.DateEnd.HasValue)
                {
                    // Find the car ids that have schedules during the specified time period
                    var conflictingCarIds = _contractContext.CarSchedules
                        .Where(s => s.DateStart < filter.DateEnd && s.DateEnd > filter.DateStart)
                        .Select(s => s.CarId)
                        .Distinct();

                    // Exclude the cars that have schedules during the specified time period
                    cars = cars.Where(c => !conflictingCarIds.Contains(c.Id));
                }
            }
            return cars.Count();
        }

        public int GetNumberOfCarsActive(CarFilter filter)
        {
            IQueryable<Car> cars = _contractContext.Cars.Where(c => c.IsDeleted == false);

            if (filter != null)
            {
                if (filter.CarStatusId.HasValue)
                {
                    cars = cars.Where(c => c.CarStatusId == filter.CarStatusId);
                }
                if (filter.ParkingLotId.HasValue)
                {
                    cars = cars.Where(c => c.ParkingLotId == filter.ParkingLotId);
                }
                if (!string.IsNullOrWhiteSpace(filter.CarLicensePlates))
                {
                    cars = cars.Where(c => c.CarLicensePlates.Contains(filter.CarLicensePlates));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarMakeName))
                {
                    int carMakeId = _carMakeController.GetCarMakeIdByName(filter.CarMakeName);
                    cars = cars.Where(c => c.CarMakeId.Equals(carMakeId));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarModelName))
                {
                    int carMakeId = _carModelController.GetCarModelIdByName(filter.CarModelName);
                    cars = cars.Where(c => c.CarModelId.Equals(carMakeId));
                }
                if (filter.SeatNumber.HasValue)
                {
                    cars = cars.Where(c => c.SeatNumber == filter.SeatNumber.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.CarColor))
                {
                    cars = cars.Where(c => c.CarColor.Equals(filter.CarColor));
                }
                if (filter.CarTrimId.HasValue)
                {
                    cars = cars.Where(c => c.CarTrimId == filter.CarTrimId);
                }
                if (filter.DateStart.HasValue && filter.DateEnd.HasValue)
                {
                    // Find the car ids that have schedules during the specified time period
                    var conflictingCarIds = _contractContext.CarSchedules
                        .Where(s => s.DateStart < filter.DateEnd && s.DateEnd > filter.DateStart)
                        .Select(s => s.CarId)
                        .Distinct();

                    // Exclude the cars that have schedules during the specified time period
                    cars = cars.Where(c => !conflictingCarIds.Contains(c.Id));
                }
            }

            return cars.Count();
        }

        public int GetNumberOfCarsByStatusId(int carStatusId)
        {
            return _contractContext.Cars.Count(c => c.IsDeleted == false && c.CarStatusId == carStatusId);
        }

        public CarDataModel GetCarById(int carId)
        {
            Car car = _contractContext.Cars
                .Include(c => c.CarStatus)
                .Include(c => c.CarGenerallInfo)
                .Include(c => c.CarState)
                .Include(c => c.CarLoanInfo)
                .Include(c => c.CarFile)
                .Include(c => c.CarTracking)
                .Include(c => c.ForControl)
                .FirstOrDefault(c => c.Id == carId);

            var parkingLot = _contractContext.ParkingLots.Find(car.ParkingLotId);
            var make = _contractContext.CarMakes.Find(car.CarMakeId);
            var model = _contractContext.CarModels.Find(car.CarModelId);
            var generation = _contractContext.CarGenerations.Find(car.CarGenerationId);
            var series = _contractContext.CarSeries.Where(c => c.Id == car.CarSeriesId).FirstOrDefault();
            var trim = _contractContext.CarTrims.Where(c => c.Id == car.CarTrimId).FirstOrDefault();
            var carSchedule = _carScheduleRepository.GetCarSchedulesByCarId(car.Id);
            var carMaintenanceInfo = _contractContext.CarMaintenanceInfos.Where(c => c.CarId == car.Id).OrderByDescending(c => c.Id).LastOrDefault();
            var carRegistryInfo = _contractContext.CarRegistryInfos.Where(c => c.CarId == car.Id).OrderByDescending(c => c.Id).LastOrDefault();

            return new CarDataModel
            {
                Id = car.Id,
                ParkingLotId = car.ParkingLotId,
                ParkingLotName = parkingLot.Name,
                CarStatusId = car.CarStatusId,
                CarStatus = car.CarStatus.Name,
                CarLicensePlates = car.CarLicensePlates,
                SeatNumber = car.SeatNumber,
                ModelYear = car.ModelYear,
                CarMakeId = car.CarMakeId,
                MakeName = make.Name,
                CarModelId = car.CarModelId,
                ModelName = model.Name,
                CarGenerationId = car.CarGenerationId,
                GenerationName = generation.Name,
                GenerationYearBegin = generation.YearBegin,
                GenerationYearEnd = generation.YearEnd,
                CarSeriesId = car.CarSeriesId,
                SeriesName = series.Name,
                CarTrimId = car.CarTrimId,
                TrimName = trim.Name,
                CarDescription = car.CarDescription,
                TrimStartProductYear = trim.StartProductYear,
                TrimEndProductYear = trim.EndProductYear,
                CreatedDate = car.CreatedDate,
                IsDeleted = car.IsDeleted,
                CarColor = car.CarColor,
                CarFuel = car.CarFuel,
                PeriodicMaintenanceLimit = car.PeriodicMaintenanceLimit,
                PriceForNormalDay = car.CarGenerallInfo.PriceForNormalDay,
                PriceForWeekendDay = car.CarGenerallInfo.PriceForWeekendDay,
                PriceForMonth = car.CarGenerallInfo.PriceForMonth,
                LimitedKmForMonth = car.CarGenerallInfo.LimitedKmForMonth,
                OverLimitedMileage = car.CarGenerallInfo.OverLimitedMileage,
                CarStatusDescription = car.CarState.CarStatusDescription,
                CurrentEtcAmount = car.CarState.CurrentEtcAmount,
                FuelPercent = car.CarState.FuelPercent,
                SpeedometerNumber = car.CarState.SpeedometerNumber,
                CarOwnerName = car.CarLoanInfo.CarOwnerName,
                RentalMethod = car.CarLoanInfo.RentalMethod,
                RentalDate = car.CarLoanInfo.RentalDate,
                SpeedometerNumberReceive = car.CarLoanInfo.SpeedometerNumberReceive,
                OwnerSlitRatio = car.CarLoanInfo.OwnerSlitRatio,
                PriceForDayReceive = car.CarLoanInfo.PriceForDayReceive,
                PriceForMonthReceive = car.CarLoanInfo.PriceForMonthReceive,
                Insurance = car.CarLoanInfo.Insurance,
                Maintenance = car.CarLoanInfo.Maintenance,
                LimitedKmForMonthReceive = car.CarLoanInfo.LimitedKmForMonthReceive,
                OverLimitedMileageReceive = car.CarLoanInfo.OverLimitedMileageReceive,
                FilePath = car.CarFile != null ? car.CarFile.FilePath : null,
                FrontImg = car.CarFile != null ? car.CarFile.FrontImg : null,
                BackImg = car.CarFile != null ? car.CarFile.BackImg : null,
                LeftImg = car.CarFile != null ? car.CarFile.LeftImg : null,
                RightImg = car.CarFile != null ? car.CarFile.BackImg : null,
                OrtherImg = car.CarFile != null ? car.CarFile.OrtherImg : null,
                CarFileCreatedDate = car.CarFile?.CreatedDate,
                CarKmLastMaintenance = carMaintenanceInfo.CarKmlastMaintenance,
                KmTraveled = carMaintenanceInfo.KmTraveled,
                RegistrationDeadline = carRegistryInfo.RegistrationDeadline,
                LinkTracking = car.CarTracking.LinkTracking,
                TrackingUsername = car.CarTracking.TrackingUsername,
                TrackingPassword = car.CarTracking.TrackingPassword,
                Etcusername = car.CarTracking.Etcusername,
                Etcpassword = car.CarTracking.Etcpassword,
                CarSchedules = carSchedule,
                LinkForControl = car.ForControl != null ? car.ForControl.LinkForControl : null,
                PaymentMethod = car.ForControl != null ? car.ForControl.PaymentMethod : null,
                ForControlDay = car.ForControl != null ? car.ForControl.ForControlDay : null,
                DayOfPayment = car.ForControl != null ? car.ForControl.DayOfPayment : null
            };
        }

        public ICollection<CarDataModel> GetCars(int page, int pageSize, CarFilter filter)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int skip = (page - 1) * pageSize;

            IQueryable<Car> cars = _contractContext.Cars.AsQueryable();

            if (filter != null)
            {
                if (filter.CarStatusId.HasValue)
                {
                    cars = cars.Where(c => c.CarStatusId == filter.CarStatusId);
                }

                if (!string.IsNullOrWhiteSpace(filter.CarLicensePlates))
                {
                    cars = cars.Where(c => c.CarLicensePlates.Contains(filter.CarLicensePlates));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarMakeName))
                {
                    int carMakeId = _carMakeController.GetCarMakeIdByName(filter.CarMakeName);
                    cars = cars.Where(c => c.CarMakeId.Equals(carMakeId));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarModelName))
                {
                    int carMakeId = _carModelController.GetCarModelIdByName(filter.CarModelName);
                    cars = cars.Where(c => c.CarModelId.Equals(carMakeId));
                }
                if (filter.SeatNumber.HasValue)
                {
                    cars = cars.Where(c => c.SeatNumber == filter.SeatNumber.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.CarColor))
                {
                    cars = cars.Where(c => c.CarColor.Equals(filter.CarColor));
                }
                if (filter.CarTrimId.HasValue)
                {
                    cars = cars.Where(c => c.CarTrimId == filter.CarTrimId);
                }
                if (filter.DateStart.HasValue && filter.DateEnd.HasValue)
                {
                    // Find the car ids that have schedules during the specified time period
                    var conflictingCarIds = _contractContext.CarSchedules
                        .Where(s => s.DateStart < filter.DateEnd && s.DateEnd > filter.DateStart)
                        .Select(s => s.CarId)
                        .Distinct();

                    // Exclude the cars that have schedules during the specified time period
                    cars = cars.Where(c => !conflictingCarIds.Contains(c.Id));
                }
            }
           
            return (from c in cars
                    join cstatus in _contractContext.CarStatuses on c.CarStatusId equals cstatus.Id
                    join cf in _contractContext.CarFiles on c.Id equals cf.CarId into CarFile 
                    from cf in CarFile.DefaultIfEmpty()
                    join cm in _contractContext.CarMakes on c.CarMakeId equals cm.Id
                    join cmo in _contractContext.CarModels on c.CarModelId equals cmo.Id
                    join cg in _contractContext.CarGenerations on c.CarGenerationId equals cg.Id
                    select new CarDataModel
                    {
                        Id = c.Id,
                        ParkingLotId = c.ParkingLotId,
                        CarStatusId = c.CarStatusId,
                        CarStatus = cstatus.Name,
                        CarLicensePlates = c.CarLicensePlates,
                        SeatNumber = c.SeatNumber,
                        ModelYear = c.ModelYear,
                        CarMakeId = c.CarMakeId,
                        MakeName = cm.Name,
                        CarModelId = c.CarModelId,
                        ModelName = cmo.Name,
                        CarGenerationId = c.CarGenerationId,
                        GenerationName = cg.Name,
                        IsDeleted = c.IsDeleted,
                        CarColor = c.CarColor,
                        CarFuel = c.CarFuel,
                        FrontImg = cf != null ? cf.FrontImg : null,

                    }).OrderBy(c => c.Id).Skip(skip).Take(pageSize).ToList();
        }

        //public ICollection<CarDataModel> GetCars(int page, int pageSize, CarFilter filter)
        //{
        //    if (page < 1)
        //    {
        //        page = 1;
        //    }

        //    if (pageSize < 1)
        //    {
        //        pageSize = 10;
        //    }
        //    int skip = (page - 1) * pageSize;

        //    IQueryable<Car> cars = _contractContext.Cars
        //        .Include(c => c.CarFile)
        //        .Include(c => c.CarStatus)
        //        .Include(c => c.CarMake)
        //        .Include(c => c.CarModel)
        //        .Include(c => c.CarGeneration)
        //        .AsQueryable();

        //    if (filter != null)
        //    {
        //        if (filter.CarStatusId.HasValue)
        //        {
        //            cars = cars.Where(c => c.CarStatusId == filter.CarStatusId);
        //        }
        //        if (filter.ParkingLotId.HasValue)
        //        {
        //            cars = cars.Where(c => c.ParkingLotId == filter.ParkingLotId);
        //        }
        //        if (!string.IsNullOrWhiteSpace(filter.CarLicensePlates))
        //        {
        //            cars = cars.Where(c => c.CarLicensePlates.Contains(filter.CarLicensePlates));
        //        }
        //        if (!string.IsNullOrWhiteSpace(filter.CarMakeName))
        //        {
        //            int carMakeId = _carMakeController.GetCarMakeIdByName(filter.CarMakeName);
        //            cars = cars.Where(c => c.CarMakeId.Equals(carMakeId));
        //        }
        //        if (!string.IsNullOrWhiteSpace(filter.CarModelName))
        //        {
        //            int carMakeId = _carModelController.GetCarModelIdByName(filter.CarModelName);
        //            cars = cars.Where(c => c.CarModelId.Equals(carMakeId));
        //        }
        //        if (filter.SeatNumber.HasValue)
        //        {
        //            cars = cars.Where(c => c.SeatNumber == filter.SeatNumber.Value);
        //        }
        //        if (!string.IsNullOrWhiteSpace(filter.CarColor))
        //        {
        //            cars = cars.Where(c => c.CarColor.Equals(filter.CarColor));
        //        }
        //        if (filter.CarTrimId.HasValue)
        //        {
        //            cars = cars.Where(c => c.CarTrimId == filter.CarTrimId);
        //        }
        //    }
        //    var carDataModels = cars
        //        .OrderBy(c => c.Id)
        //        .Skip(skip)
        //        .Take(pageSize)
        //        .Select(c => new CarDataModel
        //        {
        //            Id = c.Id,
        //            ParkingLotId = c.ParkingLotId,
        //            CarStatusId = c.CarStatusId,
        //            CarStatus = c.CarStatus.Name,
        //            CarLicensePlates = c.CarLicensePlates,
        //            SeatNumber = c.SeatNumber,
        //            ModelYear = c.ModelYear,
        //            CarMakeId = c.CarMakeId,
        //            MakeName = c.CarMake.Name,
        //            CarModelId = c.CarModelId,
        //            ModelName = c.CarModel.Name,
        //            IsDeleted = c.IsDeleted,
        //            CarColor = c.CarColor,
        //            CarFuel = c.CarFuel,
        //            FrontImg = c.CarFile != null ? c.CarFile.FrontImg : null,
        //        }).ToList();
        //    return carDataModels;

        //}

        public ICollection<CarDataModel> GetCarsActive(int page, int pageSize, CarFilter filter)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int skip = (page - 1) * pageSize;

            IQueryable<Car> cars = _contractContext.Cars.Where(c => c.IsDeleted == false)
                .Include(c => c.CarFile)
                .Include(c => c.CarStatus)
                .Include(c => c.CarMake)
                .Include(c => c.CarModel)
                .Include(c => c.CarGeneration)
                .AsQueryable(); ;


            if (filter != null)
            {
                if (filter.CarStatusId.HasValue)
                {
                    cars = cars.Where(c => c.CarStatusId == filter.CarStatusId);
                }
                if (filter.ParkingLotId.HasValue)
                {
                    cars = cars.Where(c => c.ParkingLotId == filter.ParkingLotId);
                }
                if (!string.IsNullOrWhiteSpace(filter.CarLicensePlates))
                {
                    cars = cars.Where(c => c.CarLicensePlates.Contains(filter.CarLicensePlates));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarMakeName))
                {
                    int carMakeId = _carMakeController.GetCarMakeIdByName(filter.CarMakeName);
                    cars = cars.Where(c => c.CarMakeId.Equals(carMakeId));
                }
                if (!string.IsNullOrWhiteSpace(filter.CarModelName))
                {
                    int carMakeId = _carModelController.GetCarModelIdByName(filter.CarModelName);
                    cars = cars.Where(c => c.CarModelId.Equals(carMakeId));
                }
                if (filter.SeatNumber.HasValue)
                {
                    cars = cars.Where(c => c.SeatNumber == filter.SeatNumber.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.CarColor))
                {
                    cars = cars.Where(c => c.CarColor.Equals(filter.CarColor));
                }
                if (filter.CarTrimId.HasValue)
                {
                    cars = cars.Where(c => c.CarTrimId == filter.CarTrimId);
                }
                if (filter.DateStart.HasValue && filter.DateEnd.HasValue)
                {
                    // Find the car ids that have schedules during the specified time period
                    var conflictingCarIds = _contractContext.CarSchedules
                        .Where(s => s.DateStart < filter.DateEnd && s.DateEnd > filter.DateStart)
                        .Select(s => s.CarId)
                        .Distinct();

                    // Exclude the cars that have schedules during the specified time period
                    cars = cars.Where(c => !conflictingCarIds.Contains(c.Id));
                }
            }
            var carDataModels = cars
                    .OrderBy(c => c.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(c => new CarDataModel
                    {
                        Id = c.Id,
                        ParkingLotId = c.ParkingLotId,
                        CarStatusId = c.CarStatusId,
                        CarStatus = c.CarStatus.Name,
                        CarLicensePlates = c.CarLicensePlates,
                        SeatNumber = c.SeatNumber,
                        ModelYear = c.ModelYear,
                        CarMakeId = c.CarMakeId,
                        MakeName = c.CarMake.Name,
                        CarModelId = c.CarModelId,
                        ModelName = c.CarModel.Name,
                        CarGenerationId = c.CarGenerationId,
                        GenerationName = c.CarGeneration.Name,
                        IsDeleted = c.IsDeleted,
                        CarColor = c.CarColor,
                        CarFuel = c.CarFuel,
                        FrontImg = c.CarFile != null ? c.CarFile.FrontImg : null,
                    }).ToList();
            return carDataModels;
        }

        public ICollection<CarDataModel> GetCarsMaintenance(int page, int pageSize, out int count)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int skip = (page - 1) * pageSize;

            var cars = _contractContext.Cars
            .Include(c => c.CarStatus)
            .Include(c => c.CarFile)
            .Select(c => new CarDataModel
            {
                Id = c.Id,
                CarStatusId = c.CarStatusId,
                CarStatus = c.CarStatus.Name,
                CarLicensePlates = c.CarLicensePlates,
                SeatNumber = c.SeatNumber,
                ModelYear = c.ModelYear,
                CarModelId = c.CarModelId,
                ModelName = c.CarModel.Name, // Set the ModelName property to the CarModel's Name property
                IsDeleted = c.IsDeleted,
                CarColor = c.CarColor,
                CarFuel = c.CarFuel,
                FrontImg = c.CarFile.FrontImg,
                PeriodicMaintenanceLimit = c.PeriodicMaintenanceLimit,
                KmTraveled = c.CarMaintenanceInfos.OrderByDescending(m => m.Id).FirstOrDefault().KmTraveled
            })
            .Where(c => c.KmTraveled >= (c.PeriodicMaintenanceLimit * 0.9));

            count = cars.Count();
            var response = cars.Skip(skip).Take(pageSize).ToList();
            return response;
        }
        public ICollection<CarDataModel> GetCarsMaintenanceByParkingLotId(int page, int pageSize,int parkingLotId, out int count)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int skip = (page - 1) * pageSize;

            var cars = _contractContext.Cars
            .Include(c => c.CarStatus)
            .Include(c => c.CarFile)
            .Where(c => c.ParkingLotId == parkingLotId)
            .Select(c => new CarDataModel
            {
                Id = c.Id,
                CarStatusId = c.CarStatusId,
                CarStatus = c.CarStatus.Name,
                CarLicensePlates = c.CarLicensePlates,
                SeatNumber = c.SeatNumber,
                ModelYear = c.ModelYear,
                CarModelId = c.CarModelId,
                ModelName = c.CarModel.Name, // Set the ModelName property to the CarModel's Name property
                IsDeleted = c.IsDeleted,
                CarColor = c.CarColor,
                CarFuel = c.CarFuel,
                FrontImg = c.CarFile.FrontImg,
                PeriodicMaintenanceLimit = c.PeriodicMaintenanceLimit,
                KmTraveled = c.CarMaintenanceInfos.OrderByDescending(m => m.Id).FirstOrDefault().KmTraveled
            })
            .Where(c => c.KmTraveled >= (c.PeriodicMaintenanceLimit * 0.9));

            count = cars.Count();
            var response = cars.Skip(skip).Take(pageSize).ToList();
            return response;
        }


        //public ICollection<CarDataModel> GetCarsRegistry()
        //{
        //    var currentDateTime = DateTime.Today;

        //    var cars = _contractContext.Cars
        //        .Include(c => c.CarStatus)
        //        .Include(c => c.CarFile)
        //        .Where(c => c.CarRegistryInfos.Any(m => EF.Functions.DateDiffDay(m.RegistrationDeadline, currentDateTime) <= 30))
        //        .Select(c => new CarDataModel
        //        {
        //            Id = c.Id,
        //            CarStatusId = c.CarStatusId,
        //            CarStatus = c.CarStatus.Name,
        //            CarLicensePlates = c.CarLicensePlates,
        //            SeatNumber = c.SeatNumber,
        //            ModelYear = c.ModelYear,
        //            CarModelId = c.CarModelId,
        //            ModelName = c.CarModel.Name,
        //            IsDeleted = c.IsDeleted,
        //            CarColor = c.CarColor,
        //            CarFuel = c.CarFuel,
        //            FrontImg = c.CarFile.FrontImg,
        //            RegistrationDeadline = c.CarRegistryInfos
        //                .OrderByDescending(m => m.Id)
        //                .FirstOrDefault().RegistrationDeadline,
        //        })
        //        .ToList();

        //    return cars;
        //}

        public ICollection<CarDataModel> GetCarsRegistry(int page, int pageSize, out int count)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int skip = (page - 1) * pageSize;

            var currentDateTime = DateTime.Today;

            var cars = _contractContext.Cars
                .Include(c => c.CarStatus)
                .Include(c => c.CarFile)
                .Select(c => new CarDataModel
                {
                    Id = c.Id,
                    CarStatusId = c.CarStatusId,
                    CarStatus = c.CarStatus.Name,
                    CarLicensePlates = c.CarLicensePlates,
                    SeatNumber = c.SeatNumber,
                    ModelYear = c.ModelYear,
                    CarModelId = c.CarModelId,
                    ModelName = c.CarModel.Name,
                    IsDeleted = c.IsDeleted,
                    CarColor = c.CarColor,
                    CarFuel = c.CarFuel,
                    FrontImg = c.CarFile.FrontImg,
                    RegistrationDeadline = c.CarRegistryInfos
                        .OrderByDescending(m => m.Id)
                        .FirstOrDefault().RegistrationDeadline,
                })
                .Where(c => (EF.Functions.DateDiffDay(currentDateTime, c.RegistrationDeadline) <= 30))

                .ToList();
            count = cars.Count();
            var response = cars.Skip(skip).Take(pageSize).ToList();
            return response;
        }
        public ICollection<Car> GetCarsByStatusId(int page, int pageSize, int carStatusId)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int skip = (page - 1) * pageSize;

            return _contractContext.Cars.
                Where(c => c.IsDeleted == false && c.CarStatusId == carStatusId)
                .OrderBy(c => c.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        public bool CarExit(int id)
        {
            return _contractContext.Cars.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateCar(CarCreateModel request, out string errorMessage)

        {
            errorMessage = string.Empty;

            if (_contractContext.Cars.Any(u => u.CarLicensePlates == request.CarLicensePlates))
            {
                errorMessage = "Car with this license plates already exists";
                return false;
            }

            // Create new Car object and set its properties
            int defaultCarSatusId = Constant.CarStatusConstants.Expertising;
            var car = new Car
            {
                ParkingLotId = request.ParkingLotId,
                CarStatusId = defaultCarSatusId,
                CarLicensePlates = request.CarLicensePlates,
                ModelYear = request.ModelYear,
                SeatNumber = request.SeatNumber,
                CarMakeId = request.CarMakeId,
                CarModelId = request.CarModelId,
                CarGenerationId = request.CarGenerationId,
                CarSeriesId = request.CarSeriesId,
                CarDescription = request.CarDescription,
                CarTrimId = request.CarTrimId,
                CreatedDate = request.CreatedDate,
                IsDeleted = request.IsDeleted,
                CarColor = request.CarColor,
                CarFuel = request.CarFuel,
                PeriodicMaintenanceLimit = request.PeriodicMaintenanceLimit

            };

            // Save the new car to the database
            _contractContext.Cars.Add(car);
            _contractContext.SaveChanges();

            // Create new CarGenerallInfos object and set its properties
            var carGenerallInfos = new CarGenerallInfo
            {
                CarId = car.Id,
                PriceForNormalDay = request.PriceForNormalDay,
                PriceForWeekendDay = request.PriceForWeekendDay,
                PriceForMonth = request.PriceForMonth,
                LimitedKmForMonth = request.LimitedKmForMonth,
                OverLimitedMileage = request.OverLimitedMileage
            };

            // Save the new CarGenerallInfos object to the database
            _contractContext.CarGenerallInfos.Add(carGenerallInfos);
            _contractContext.SaveChanges();

            // Create new CarStates object and set its properties
            var carStates = new CarState
            {
                CarId = car.Id,
                CarStatusDescription = request.CarStatusDescription,
                CurrentEtcAmount = request.CurrentEtcAmount,
                FuelPercent = request.FuelPercent,
                SpeedometerNumber = request.SpeedometerNumber
            };
            // Save the new CarStates object to the database
            _contractContext.CarStates.Add(carStates);
            _contractContext.SaveChanges();

            var carLoadnInfo = new CarLoanInfo
            {
                CarId = car.Id,
                CarOwnerName = request.CarOwnerName,
                RentalMethod = request.RentalMethod,
                RentalDate = request.RentalDate,
                SpeedometerNumberReceive = request.SpeedometerNumberReceive,
                OwnerSlitRatio = request.OwnerSlitRatio,
                PriceForDayReceive = request.PriceForDayReceive,
                PriceForMonthReceive = request.PriceForMonthReceive,
                Insurance = request.Insurance,
                LimitedKmForMonthReceive = request.LimitedKmForMonthReceive,
                OverLimitedMileageReceive = request.OverLimitedMileageReceive,
            };

            // Save the new CarLoanInfo object to the database
            _contractContext.CarLoanInfos.Add(carLoadnInfo);
            _contractContext.SaveChanges();

            if (request.FrontImg != null)
            {
                var carFile = new CarFile
                {
                    CarId = car.Id,
                    FrontImg = request.FrontImg,
                    BackImg = request.BackImg,
                    LeftImg = request.LeftImg,
                    RightImg = request.RightImg,
                    OrtherImg = request.OrtherImg,
                };
                // Save the new CarFile object to the database
                _contractContext.CarFiles.Add(carFile);
                _contractContext.SaveChanges();
            }

            var carMaintenanceInfo = new CarMaintenanceInfo
            {
                CarId = car.Id,
                CarKmlastMaintenance = request.CarKmLastMaintenance,
                KmTraveled = request.SpeedometerNumber - request.CarKmLastMaintenance,
            };
            _contractContext.CarMaintenanceInfos.Add(carMaintenanceInfo);
            _contractContext.SaveChanges();

            var carRegistryInfo = new CarRegistryInfo
            {
                CarId = car.Id,
                RegistrationDeadline = request.RegistrationDeadline,
            };
            _contractContext.CarRegistryInfos.Add(carRegistryInfo);
            _contractContext.SaveChanges();

            var carTracking = new CarTracking
            {
                CarId = car.Id,
                LinkTracking = request.LinkTracking,
                TrackingUsername = request.TrackingUsername,
                TrackingPassword = request.TrackingPassword,
                Etcusername = request.Etcusername,
                Etcpassword = request.Etcpassword
            };
            // Save the new CarTracking object to the database
            _contractContext.CarTrackings.Add(carTracking);
            _contractContext.SaveChanges();

            return true;

        }

        public void UpdateCar(int id, CarUpdateModel request)
        {
            // Retrieve the car object from the database
            var car = _contractContext.Cars.Find(id);

            // Update the properties of the car object
            car.ParkingLotId = request.ParkingLotId;
            car.CarStatusId = request.CarStatusId;
            car.CarLicensePlates = request.CarLicensePlates;
            car.SeatNumber = request.SeatNumber;
            car.CarMakeId = request.CarMakeId;
            car.CarModelId = request.CarModelId;
            car.CarGenerationId = request.CarGenerationId;
            car.CarSeriesId = request.CarSeriesId;
            car.CarTrimId = request.CarTrimId;
            car.CarDescription = request.CarDescription;
            car.CreatedDate = request.CreatedDate;
            car.IsDeleted = request.IsDeleted;
            car.CarColor = request.CarColor;
            car.CarFuel = request.CarFuel;
            car.PeriodicMaintenanceLimit = request.PeriodicMaintenanceLimit;

            // Save the changes to the database
            _contractContext.Cars.Update(car);
            _contractContext.SaveChanges();

            // Retrieve the CarGenerallInfos object from the database
            var carGenerallInfos = _contractContext.CarGenerallInfos.Where(c => c.CarId == id).FirstOrDefault();

            // Update the properties of the CarGenerallInfos object
            carGenerallInfos.PriceForNormalDay = request.PriceForNormalDay;
            carGenerallInfos.PriceForWeekendDay = request.PriceForWeekendDay;
            carGenerallInfos.PriceForMonth = request.PriceForMonth;
            carGenerallInfos.LimitedKmForMonth = request.LimitedKmForMonth;
            carGenerallInfos.OverLimitedMileage = request.OverLimitedMileage;

            // Save the changes to the database
            _contractContext.CarGenerallInfos.Update(carGenerallInfos);
            _contractContext.SaveChanges();

            // Retrieve the CarStates object from the database
            var carStates = _contractContext.CarStates.Where(c => c.CarId == id).FirstOrDefault();

            // Update the properties of the CarStates object
            carStates.CarStatusDescription = request.CarStatusDescription;
            carStates.CurrentEtcAmount = request.CurrentEtcAmount;
            carStates.FuelPercent = request.FuelPercent;
            carStates.SpeedometerNumber = request.SpeedometerNumber;

            // Save the changes to the database
            _contractContext.CarStates.Update(carStates);
            _contractContext.SaveChanges();

            // Retrieve the CarLoanInfo object from the database
            var carLoanInfo = _contractContext.CarLoanInfos.Where(c => c.CarId == id).FirstOrDefault();
            carLoanInfo.CarOwnerName = request.CarOwnerName;
            carLoanInfo.RentalMethod = request.RentalMethod;
            carLoanInfo.RentalDate = request.RentalDate;
            carLoanInfo.SpeedometerNumberReceive = request.SpeedometerNumberReceive;
            carLoanInfo.OwnerSlitRatio = request.OwnerSlitRatio;
            carLoanInfo.PriceForDayReceive = request.PriceForDayReceive;
            carLoanInfo.PriceForMonthReceive = request.PriceForMonthReceive;
            carLoanInfo.Insurance = request.Insurance;
            carLoanInfo.LimitedKmForMonthReceive = request.LimitedKmForMonthReceive;
            carLoanInfo.OverLimitedMileageReceive = request.OverLimitedMileageReceive;
            _contractContext.CarLoanInfos.Update(carLoanInfo);
            _contractContext.SaveChanges();

            // Retrieve the CarFile object from the database
            var carFile = _contractContext.CarFiles.Where(c => c.CarId == id).FirstOrDefault();
            if (carFile != null)
            {
                carFile.FrontImg = request.FrontImg;
                carFile.BackImg = request.BackImg;
                carFile.LeftImg = request.LeftImg;
                carFile.RightImg = request.RightImg;
                carFile.OrtherImg = request.OrtherImg;
                carFile.CreatedDate = request.CarFileCreatedDate;
                _contractContext.CarFiles.Update(carFile);
                _contractContext.SaveChanges();
            }
            if (carFile == null)
            {
                var newCarFile = new CarFile
                {
                    CarId = car.Id,
                    FrontImg = request.FrontImg,
                    BackImg = request.BackImg,
                    LeftImg = request.LeftImg,
                    RightImg = request.RightImg,
                    OrtherImg = request.OrtherImg,
                };
                // Save the new CarFile object to the database
                _contractContext.CarFiles.Add(newCarFile);
                _contractContext.SaveChanges();
            }


            // Retrieve the CarTracking object from the database
            var carTracking = _contractContext.CarTrackings.Where(c => c.CarId == id).FirstOrDefault();
            carTracking.LinkTracking = request.LinkTracking;
            carTracking.TrackingUsername = request.TrackingUsername;
            carTracking.TrackingPassword = request.TrackingPassword;
            carTracking.Etcusername = request.Etcusername;
            carTracking.Etcpassword = request.Etcpassword;
            _contractContext.CarTrackings.Update(carTracking);
            _contractContext.SaveChanges();

            // Retrieve the ForControl object from the database
            //var forControl = _contractContext.ForControls.Where(c => c.CarId == id).FirstOrDefault();
            //forControl.LinkForControl = request.LinkForControl;
            //forControl.PaymentMethod = request.PaymentMethod;
            //forControl.ForControlDay = request.ForControlDay;
            //forControl.DayOfPayment = request.DayOfPayment;
            //_contractContext.ForControls.Update(forControl);
            //_contractContext.SaveChanges();

        }

        public bool UpdateCarStatus(int id, CarUpdateStatusModel request)
        {
            var car = _contractContext.Cars.Where(c => c.Id == id).FirstOrDefault();
            if (car == null)
                return false;

            car.CarStatusId = request.CarStatusId;
            return Save();

        }

        public bool DeleteCar(int id)
        {
            var car = _contractContext.Cars.Where(c => c.Id == id).FirstOrDefault();
            if (car == null)
                return false;

            car.IsDeleted = true;
            _contractContext.Cars.Update(car);
            return Save();

        }

        public ICollection<Car> GetCarsByCarMakeId(int page, int pageSize, int carMakeId)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }
            int skip = (page - 1) * pageSize;

            return _contractContext.Cars
                .Where(c => c.IsDeleted == false && c.CarStatusId == 2)
                .Where(c => c.CarMakeId == carMakeId)
                .OrderBy(c => c.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

    }


    //public CarDataModel GetCarById(int carId)
    //{
    //    CarDataModel car = (from c in _contractContext.Cars
    //               join cstatus in _contractContext.CarStatuses on c.CarStatusId equals cstatus.Id
    //               join gi in _contractContext.CarGenerallInfos on c.Id equals gi.CarId
    //               join cs in _contractContext.CarStates on c.Id equals cs.CarId
    //               join cli in _contractContext.CarLoanInfos on c.Id equals cli.CarId
    //               join cf in _contractContext.CarFiles on c.Id equals cf.CarId 
    //               join ctk in _contractContext.CarTrackings on c.Id equals ctk.CarId
    //                        join fc in _contractContext.ForControls.Where(x => x.CarId == carId).DefaultIfEmpty()
    //               on true equals true
    //               join cm in _contractContext.CarMakes on c.CarMakeId equals cm.Id
    //               join cmo in _contractContext.CarModels on c.CarModelId equals cmo.Id
    //               join cg in _contractContext.CarGenerations on c.CarGenerationId equals cg.Id
    //               join cse in _contractContext.CarSeries on c.CarSeriesId equals cse.Id
    //               join ct in _contractContext.CarTrims on c.CarTrimId equals ct.Id
    //               where c.Id == carId

    //               select new CarDataModel
    //               {

    //                   Id = c.Id,
    //                   ParkingLotId = c.ParkingLotId,
    //                   CarStatusId = c.CarStatusId,
    //                   CarStatus = cstatus.Name,
    //                   CarId = c.CarId,
    //                   CarLicensePlates = c.CarLicensePlates,
    //                   SeatNumber = c.SeatNumber,
    //                   ModelYear = c.ModelYear,
    //                   CarMakeId = c.CarMakeId,
    //                   MakeName = cm.Name,
    //                   CarModelId = c.CarModelId,
    //                   ModelName = cmo.Name,
    //                   CarGenerationId = c.CarGenerationId,
    //                   GenerationName = cg.Name,
    //                   GenerationYearBegin = cg.YearBegin,
    //                   GenerationYearEnd = cg.YearEnd,
    //                   CarSeriesId = c.CarSeriesId,
    //                   SeriesName = cse.Name,
    //                   CarTrimId = c.CarTrimId,
    //                   TrimName = ct.Name,
    //                   CarDescription = c.CarDescription,
    //                   TrimStartProductYear = ct.StartProductYear,
    //                   TrimEndProductYear = ct.EndProductYear,
    //                   CreatedDate = c.CreatedDate,
    //                   IsDeleted = c.IsDeleted,
    //                   CarColor = c.CarColor,
    //                   CarFuel = c.CarFuel,
    //                   PriceForNormalDay = gi.PriceForNormalDay,
    //                   PriceForWeekendDay = gi.PriceForWeekendDay,
    //                   PriceForMonth = gi.PriceForMonth,
    //                   LimitedKmForMonth = gi.LimitedKmForMonth,
    //                   OverLimitedMileage = gi.OverLimitedMileage,
    //                   CarStatusDescription = cs.CarStatusDescription,
    //                   CurrentEtcAmount = cs.CurrentEtcAmount,
    //                   FuelPercent = cs.FuelPercent,
    //                   SpeedometerNumber = cs.SpeedometerNumber,
    //                   CarOwnerName = cli.CarOwnerName,
    //                   RentalMethod = cli.RentalMethod,
    //                   RentalDate = cli.RentalDate,
    //                   SpeedometerNumberReceive = cli.SpeedometerNumberReceive,
    //                   PriceForDayReceive = cli.PriceForDayReceive,
    //                   PriceForMonthReceive = cli.PriceForMonthReceive,
    //                   Insurance = cli.Insurance,
    //                   Maintenance = cli.Maintenance,
    //                   LimitedKmForMonthReceive = cli.LimitedKmForMonthReceive,
    //                   OverLimitedMileageReceive = cli.OverLimitedMileageReceive,
    //                   FilePath = cf.FilePath,
    //                   FrontImg = cf.FrontImg,
    //                   BackImg = cf.BackImg,
    //                   LeftImg = cf.LeftImg,
    //                   RightImg = cf.BackImg,
    //                   OrtherImg = cf.OrtherImg,
    //                   CarFileCreatedDate = cf.CreatedDate,
    //                   LinkTracking = ctk.LinkTracking,
    //                   TrackingUsername = ctk.TrackingUsername,
    //                   TrackingPassword = ctk.TrackingPassword,
    //                   Etcusername = ctk.Etcusername,
    //                   Etcpassword = ctk.Etcpassword,
    //                   LinkForControl = fc != null ? fc.LinkForControl : null,
    //                   PaymentMethod = fc != null ? fc.PaymentMethod : null,
    //                   ForControlDay = fc != null ? fc.ForControlDay : null,
    //                   DayOfPayment = fc != null ? fc.DayOfPayment : null
    //               }).FirstOrDefault();
    //    return car;
    //}

    //public ICollection<CarDataModel> GetCarsMaintenance()
    //{

    //    var query = from car in _contractContext.Cars
    //                join carStatus in _contractContext.CarStatuses on car.CarStatusId equals carStatus.Id
    //                join maintenance in _contractContext.CarMaintenanceInfos on car.Id equals maintenance.CarId
    //                select new CarDataModel
    //                {
    //                    Id = car.Id,
    //                    CarLicensePlates = car.CarLicensePlates,
    //                    CarStatusId = car.CarStatusId,
    //                    CarStatus = carStatus.Name,
    //                    PeriodicMaintenanceLimit = car.PeriodicMaintenanceLimit,
    //                    SeatNumber = car.SeatNumber,
    //                    KmTraveled = maintenance.KmTraveled
    //                };

    //    query = query.Where(c => c.KmTraveled >= (c.PeriodicMaintenanceLimit * 0.9));

    //    var response = query.ToList();
    //    return response;
    //}

    //public ICollection<CarDataModel> GetCarsMaintenance(int page, int pageSize, out int count)
    //{
    //    if (page < 1)
    //    {
    //        page = 1;
    //    }

    //    if (pageSize < 1)
    //    {
    //        pageSize = 10;
    //    }
    //    int skip = (page - 1) * pageSize;

    //    IQueryable<Car> cars = _contractContext.Cars
    //        .Include(c => c.CarStatus)
    //        .Include(c => c.CarFile)
    //        .Include(c => c.CarMake)
    //        .Include(c => c.CarModel)
    //       .AsQueryable();

    //    cars.Where(c => c.CarMaintenanceInfos.Any(m => m.KmTraveled >= (c.PeriodicMaintenanceLimit * 0.9)));

    //       var carDataModels = cars
    //        .OrderBy(c => c.Id)
    //        .Skip(skip)
    //        .Take(pageSize)
    //        .Select(c => new CarDataModel
    //         {
    //             Id = c.Id,
    //             CarStatusId = c.CarStatusId,
    //             CarStatus = c.CarStatus.Name,
    //             CarLicensePlates = c.CarLicensePlates,
    //             SeatNumber = c.SeatNumber,
    //             ModelYear = c.ModelYear,
    //             CarMakeId = c.CarMakeId,
    //             MakeName = c.CarMake.Name,
    //             CarModelId = c.CarModelId,
    //             ModelName = c.CarModel.Name,
    //             CarGenerationId = c.CarGenerationId,
    //             GenerationName = c.CarGeneration.Name,
    //             IsDeleted = c.IsDeleted,
    //             CarColor = c.CarColor,
    //             CarFuel = c.CarFuel,
    //             FrontImg = c.CarFile.FrontImg,
    //             PeriodicMaintenanceLimit = c.PeriodicMaintenanceLimit,
    //             KmTraveled = c.CarMaintenanceInfos.OrderByDescending(m => m.Id).FirstOrDefault().KmTraveled
    //         })
    //         .ToList();

    //    count = carDataModels.Count();

    //    return carDataModels;
    //}

    //public ICollection<CarDataModel> GetCarsRegistry()
    //{
    //    var currentUtcDateTime = DateTime.Today;

    //    var cars = _contractContext.Cars
    //        .Include(c => c.CarStatus)
    //        .Include(c => c.CarFile)
    //        .Select(c => new CarDataModel
    //        {
    //            Id = c.Id,
    //            CarStatusId = c.CarStatusId,
    //            CarStatus = c.CarStatus.Name,
    //            CarLicensePlates = c.CarLicensePlates,
    //            SeatNumber = c.SeatNumber,
    //            ModelYear = c.ModelYear,
    //            CarModelId = c.CarModelId,
    //            ModelName = c.CarModel.Name,
    //            IsDeleted = c.IsDeleted,
    //            CarColor = c.CarColor,
    //            CarFuel = c.CarFuel,
    //            FrontImg = c.CarFile.FrontImg,
    //            RegistrationDeadline = c.CarRegistryInfos
    //                .OrderByDescending(m => m.Id)
    //                .FirstOrDefault().RegistrationDeadline,
    //        })
    //        .Where(c => (c.RegistrationDeadline.Value.CompareTo(currentUtcDateTime) <= 30))
    //        .ToList();

    //    return cars;
    //}


    //public ICollection<CarDataModel> GetCarsActive(int page, int pageSize, CarFilter filter)
    //{
    //    if (page < 1)
    //    {
    //        page = 1;
    //    }

    //    if (pageSize < 1)
    //    {
    //        pageSize = 10;
    //    }
    //    int skip = (page - 1) * pageSize;

    //    IQueryable<Car> cars = _contractContext.Cars.AsQueryable();

    //    if (filter != null)
    //    {
    //        if (filter.CarStatusId.HasValue)
    //        {
    //            cars = cars.Where(c => c.CarStatusId == filter.CarStatusId);
    //        }

    //        if (!string.IsNullOrWhiteSpace(filter.CarLicensePlates))
    //        {
    //            cars = cars.Where(c => c.CarLicensePlates.Contains(filter.CarLicensePlates));
    //        }
    //        if (!string.IsNullOrWhiteSpace(filter.CarMakeName))
    //        {
    //            int carMakeId = _carMakeController.GetCarMakeIdByName(filter.CarMakeName);
    //            cars = cars.Where(c => c.CarMakeId.Equals(carMakeId));
    //        }
    //        if (!string.IsNullOrWhiteSpace(filter.CarModelName))
    //        {
    //            int carMakeId = _carModelController.GetCarModelIdByName(filter.CarModelName);
    //            cars = cars.Where(c => c.CarModelId.Equals(carMakeId));
    //        }
    //        if (filter.SeatNumber.HasValue)
    //        {
    //            cars = cars.Where(c => c.SeatNumber == filter.SeatNumber.Value);
    //        }
    //        if (!string.IsNullOrWhiteSpace(filter.CarColor))
    //        {
    //            cars = cars.Where(c => c.CarColor.Equals(filter.CarColor));
    //        }
    //        if (filter.CarTrimId.HasValue)
    //        {
    //            cars = cars.Where(c => c.CarTrimId == filter.CarTrimId);
    //        }

    //    }
    //    return (from c in cars
    //            join cstatus in _contractContext.CarStatuses on c.CarStatusId equals cstatus.Id
    //            join cf in _contractContext.CarFiles on c.Id equals cf.CarId
    //            join cm in _contractContext.CarMakes on c.CarMakeId equals cm.Id
    //            join cmo in _contractContext.CarModels on c.CarModelId equals cmo.Id
    //            join cg in _contractContext.CarGenerations on c.CarGenerationId equals cg.Id
    //            select new CarDataModel
    //            {
    //                Id = c.Id,
    //                ParkingLotId = c.ParkingLotId,
    //                CarStatusId = c.CarStatusId,
    //                CarStatus = cstatus.Name,
    //                CarLicensePlates = c.CarLicensePlates,
    //                SeatNumber = c.SeatNumber,
    //                ModelYear = c.ModelYear,
    //                CarMakeId = c.CarMakeId,
    //                MakeName = cm.Name,
    //                CarModelId = c.CarModelId,
    //                ModelName = cmo.Name,
    //                CarGenerationId = c.CarGenerationId,
    //                GenerationName = cg.Name,
    //                IsDeleted = c.IsDeleted,
    //                CarColor = c.CarColor,
    //                CarFuel = c.CarFuel,
    //                FrontImg = cf != null ? cf.FrontImg : null,
    //            }).Where(c => c.IsDeleted == false)
    //                    .OrderBy(c => c.Id)
    //                    .Skip(skip)
    //                    .Take(pageSize)
    //                    .ToList();
    //}
}

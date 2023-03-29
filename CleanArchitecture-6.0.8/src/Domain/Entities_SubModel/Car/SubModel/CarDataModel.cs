﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarSchedules.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.Car.SubModel
{
    public class CarDataModel
    {

        public int Id { get; set; }
        public int? ParkingLotId { get; set; }

        public string? ParkingLotName { get; set; }
        public int? CarStatusId { get; set; }
        public string CarStatus { get; set; }
        public string CarLicensePlates { get; set; }
        public int? SeatNumber { get; set; }
        public int? CarMakeId { get; set; }
        public int? CarModelId { get; set; }

        public int? ModelYear { get; set; }
        public int? CarGenerationId { get; set; }
        public int? CarSeriesId { get; set; }
        public int?   CarTrimId { get; set; }
        public string? MakeName { get; set; }
        public string? ModelName { get; set; }
        public string? GenerationName { get; set; }
        public string? SeriesName { get; set; }
        public string? TrimName { get; set; }

        public string? CarDescription { get; set; }
        public int? GenerationYearBegin { get; set; }

        public int? GenerationYearEnd { get; set; }

        public int? TrimStartProductYear { get; set; }

        public int? TrimEndProductYear { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string CarColor { get; set; }
        public string CarFuel { get; set; }
        public double? PeriodicMaintenanceLimit { get; set; }
        public double? PriceForNormalDay { get; set; }
        public double? PriceForWeekendDay { get; set; }
        public double? PriceForMonth { get; set; }
        public double? LimitedKmForMonth { get; set; }
        public double? OverLimitedMileage { get; set; }
        public string? CarStatusDescription { get; set; }
        public double? CurrentEtcAmount { get; set; }
        public int? FuelPercent { get; set; }
        public double? SpeedometerNumber { get; set; }

        public string CarOwnerName { get; set; }

        public string RentalMethod { get; set; }

        public DateTime? RentalDate { get; set; }

        public double? SpeedometerNumberReceive { get; set; }

        public double? OwnerSlitRatio { get; set; }

        public double? PriceForDayReceive { get; set; }

        public double? PriceForMonthReceive { get; set; }

        public bool? Insurance { get; set; }

        public bool? Maintenance { get; set; }

        public double? LimitedKmForMonthReceive { get; set; }

        public double? OverLimitedMileageReceive { get; set; }

        public string? FilePath { get; set; }

        public string? FrontImg { get; set; }

        public string? BackImg { get; set; }

        public string? LeftImg { get; set; }

        public string? RightImg { get; set; }

        public string? OrtherImg { get; set; }

        public DateTime? CarFileCreatedDate { get; set; }

        public string? LinkTracking { get; set; }

        public string? TrackingUsername { get; set; }

        public string? TrackingPassword { get; set; }

        public string? Etcusername { get; set; }

        public string? Etcpassword { get; set; }

        public string? LinkForControl { get; set; }

        public string? PaymentMethod { get; set; }

        public string? ForControlDay { get; set; }

        public string? DayOfPayment { get; set; }

        public double? CarKmLastMaintenance { get; set; }

        public double? KmTraveled { get; set; }

        public DateTime? RegistrationDeadline { get; set; }

        public ICollection<CarScheduleDataModel>? CarSchedules { get; set; }


    }
}

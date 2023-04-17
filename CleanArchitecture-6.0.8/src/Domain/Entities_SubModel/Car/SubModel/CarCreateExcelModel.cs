using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Domain.Entities_SubModel.Car.SubModel
{
    public class CarCreateExcelModel
    {
        public string ParkingLotName { get; set; }
        public string CarLicensePlates { get; set; }
        public int SeatNumber { get; set; }
        public int ModelYear { get; set; }
        public string CarMakeName { get; set; }
        public string CarModelName { get; set; }
        public string CarGenerationName { get; set; }
        public string CarSeriesName { get; set; }
        public string CarTrimName { get; set; }
        public string? CarDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string CarColor { get; set; }
        public string CarFuel { get; set; }
        public double? PeriodicMaintenanceLimit { get; set; }
        public decimal? TankCapacity { get; set; }
        public double PriceForNormalDay { get; set; }
        public double PriceForWeekendDay { get; set; }
        public double PriceForMonth { get; set; }
        public double LimitedKmForMonth { get; set; }
        public double OverLimitedMileage { get; set; }
        public string CarStatusDescription { get; set; }
        public double CurrentEtcAmount { get; set; }
        public int FuelPercent { get; set; }
        public double SpeedometerNumber { get; set; }

        public string CarOwnerName { get; set; }

        public string RentalMethod { get; set; }

        public DateTime RentalDate { get; set; }

        public double SpeedometerNumberReceive { get; set; }

        public double? OwnerSlitRatio { get; set; }

        public double? PriceForDayReceive { get; set; }

        public double PriceForMonthReceive { get; set; }

        public bool Insurance { get; set; }

        public double LimitedKmForMonthReceive { get; set; }

        public double OverLimitedMileageReceive { get; set; }

        public string? FrontImg { get; set; }

        public string? BackImg { get; set; }

        public string? LeftImg { get; set; }

        public string? RightImg { get; set; }

        public string? OrtherImg { get; set; }

        public double CarKmLastMaintenance { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public string LinkTracking { get; set; }

        public string TrackingUsername { get; set; }

        public string TrackingPassword { get; set; }

        public string Etcusername { get; set; }

        public string Etcpassword { get; set; }

    }
}

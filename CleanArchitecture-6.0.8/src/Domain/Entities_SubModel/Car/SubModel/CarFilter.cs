using System.Diagnostics.CodeAnalysis;

namespace CleanArchitecture.Domain.Entities_SubModel.Car.SubModel
{
    public class CarFilter
    {
        [AllowNull]
        public int? CarStatusId { get; set; }

        [AllowNull]
        public int? ParkingLotId { get; set; }

        [AllowNull]
        public string? CarLicensePlates { get; set; }

        [AllowNull]
        public int? SeatNumber { get; set; }

        [AllowNull]
        public string? CarMakeName { get; set; }
        [AllowNull]
        public string? CarModelName { get; set; }
        [AllowNull]
        public string? CarColor { get; set;}

        [AllowNull]
        public int? CarTrimId { get; set; }
    }
}

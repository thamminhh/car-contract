using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class Car
    {
        public Car()
        {
            CarMaintenanceInfos = new HashSet<CarMaintenanceInfo>();
            CarRegistryInfos = new HashSet<CarRegistryInfo>();
            CarSchedules = new HashSet<CarSchedule>();
            ContractGroups = new HashSet<ContractGroup>();
        }

        public int Id { get; set; }
        public int? ParkingLotId { get; set; }
        public int? CarStatusId { get; set; }
        public string? CarLicensePlates { get; set; }
        public int? SeatNumber { get; set; }
        public int? CarMakeId { get; set; }
        public int? CarModelId { get; set; }
        public int? CarGenerationId { get; set; }
        public int? CarSeriesId { get; set; }
        public int? CarTrimId { get; set; }
        public string? CarDescription { get; set; }
        public string? CarColor { get; set; }
        public string? CarFuel { get; set; }
        public double? PeriodicMaintenanceLimit { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ModelYear { get; set; }
        public decimal? TankCapacity { get; set; }

        public virtual CarGeneration? CarGeneration { get; set; }
        public virtual CarMake? CarMake { get; set; }
        public virtual CarModel? CarModel { get; set; }
        public virtual CarSeries? CarSeries { get; set; }
        public virtual CarStatus? CarStatus { get; set; }
        public virtual CarTrim? CarTrim { get; set; }
        public virtual ParkingLot? ParkingLot { get; set; }
        public virtual CarFile? CarFile { get; set; }
        public virtual CarGenerallInfo? CarGenerallInfo { get; set; }
        public virtual CarLoanInfo? CarLoanInfo { get; set; }
        public virtual CarState? CarState { get; set; }
        public virtual CarTracking? CarTracking { get; set; }
        public virtual ForControl? ForControl { get; set; }
        public virtual ICollection<CarMaintenanceInfo> CarMaintenanceInfos { get; set; }
        public virtual ICollection<CarRegistryInfo> CarRegistryInfos { get; set; }
        public virtual ICollection<CarSchedule> CarSchedules { get; set; }
        public virtual ICollection<ContractGroup> ContractGroups { get; set; }
    }
}

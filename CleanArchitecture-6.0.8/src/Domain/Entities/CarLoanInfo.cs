using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarLoanInfo
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public string? CarOwnerName { get; set; }
        public string? RentalMethod { get; set; }
        public DateTime? RentalDate { get; set; }
        public double? SpeedometerNumberReceive { get; set; }
        public string? PriceForDayReceive { get; set; }
        public double? PriceForMonthReceive { get; set; }
        public bool? Insurance { get; set; }
        public bool? Maintenance { get; set; }
        public double? LimitedKmForMonthReceive { get; set; }
        public double? OverLimitedMileageReceive { get; set; }

        public virtual Car? Car { get; set; }
    }
}

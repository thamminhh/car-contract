using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarGenerallInfo
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public double? PriceForNormalDay { get; set; }
        public double? PriceForWeekendDay { get; set; }
        public double? PriceForMonth { get; set; }
        public double? LimitedKmForMonth { get; set; }
        public double? OverLimitedMileage { get; set; }

        public virtual Car? Car { get; set; }
    }
}

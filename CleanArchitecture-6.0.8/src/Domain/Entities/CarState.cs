using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarState
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public string? CarStatusDescription { get; set; }
        public double? CurrentEtcAmount { get; set; }
        public int? FuelPercent { get; set; }
        public double? SpeedometerNumber { get; set; }

        public virtual Car? Car { get; set; }
    }
}

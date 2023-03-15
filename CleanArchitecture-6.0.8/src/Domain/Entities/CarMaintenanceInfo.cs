using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarMaintenanceInfo
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public double? CarKmlastMaintenance { get; set; }
        public double? KmTraveled { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public string? MaintenanceInvoice { get; set; }
        public double? MaintenanceAmount { get; set; }

        public virtual Car? Car { get; set; }
    }
}

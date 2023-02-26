using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarTracking
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public string? LinkTracking { get; set; }
        public string? TrackingUsername { get; set; }
        public string? TrackingPassword { get; set; }
        public string? Etcusername { get; set; }
        public string? Etcpassword { get; set; }

        public virtual Car? Car { get; set; }
    }
}

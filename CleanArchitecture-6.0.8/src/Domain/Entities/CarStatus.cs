using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarStatus
    {
        public CarStatus()
        {
            CarSchedules = new HashSet<CarSchedule>();
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<CarSchedule> CarSchedules { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}

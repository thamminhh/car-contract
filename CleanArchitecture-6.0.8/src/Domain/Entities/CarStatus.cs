using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarStatus
    {
        public CarStatus()
        {
            CarShedules = new HashSet<CarShedule>();
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<CarShedule> CarShedules { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}

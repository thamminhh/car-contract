using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarTrim
    {
        public CarTrim()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public int? CarModelId { get; set; }
        public int? CarSeriesId { get; set; }
        public string? Name { get; set; }
        public int? StartProductYear { get; set; }
        public int? EndProductYear { get; set; }

        public virtual CarModel? CarModel { get; set; }
        public virtual CarSeries? CarSeries { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarModel
    {
        public CarModel()
        {
            CarGenerations = new HashSet<CarGeneration>();
            CarSeries = new HashSet<CarSeries>();
            CarTrims = new HashSet<CarTrim>();
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public int? CarMakeId { get; set; }
        public string? Name { get; set; }

        public virtual CarMake? CarMake { get; set; }
        public virtual ICollection<CarGeneration> CarGenerations { get; set; }
        public virtual ICollection<CarSeries> CarSeries { get; set; }
        public virtual ICollection<CarTrim> CarTrims { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}

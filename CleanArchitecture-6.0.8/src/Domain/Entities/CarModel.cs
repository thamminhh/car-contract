using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarModel
    {
        public CarModel()
        {
            CarCarGenerations = new HashSet<Car>();
            CarCarMakes = new HashSet<Car>();
            CarCarModels = new HashSet<Car>();
            CarCarSeries = new HashSet<Car>();
            CarCarTrims = new HashSet<Car>();
            CarGenerations = new HashSet<CarGeneration>();
            CarSeries = new HashSet<CarSeries>();
            CarTrims = new HashSet<CarTrim>();
        }

        public int Id { get; set; }
        public int? CarMakeId { get; set; }
        public string? Name { get; set; }

        public virtual CarMake? CarMake { get; set; }
        public virtual ICollection<Car> CarCarGenerations { get; set; }
        public virtual ICollection<Car> CarCarMakes { get; set; }
        public virtual ICollection<Car> CarCarModels { get; set; }
        public virtual ICollection<Car> CarCarSeries { get; set; }
        public virtual ICollection<Car> CarCarTrims { get; set; }
        public virtual ICollection<CarGeneration> CarGenerations { get; set; }
        public virtual ICollection<CarSeries> CarSeries { get; set; }
        public virtual ICollection<CarTrim> CarTrims { get; set; }
    }
}

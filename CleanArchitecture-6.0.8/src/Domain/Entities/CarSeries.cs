using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarSeries
    {
        public CarSeries()
        {
            CarTrims = new HashSet<CarTrim>();
        }

        public int Id { get; set; }
        public int? CarModelId { get; set; }
        public int? CarGenerationId { get; set; }
        public string? Name { get; set; }

        public virtual CarGeneration? CarGeneration { get; set; }
        public virtual CarModel? CarModel { get; set; }
        public virtual ICollection<CarTrim> CarTrims { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarGeneration
    {
        public int Id { get; set; }
        public int? CarModelId { get; set; }
        public string? Name { get; set; }
        public int? YearBegin { get; set; }
        public int? YearEnd { get; set; }

        public virtual CarModel? CarModel { get; set; }
    }
}

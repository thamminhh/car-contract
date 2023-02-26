using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarMake
    {
        public CarMake()
        {
            CarModels = new HashSet<CarModel>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CarMakeImg { get; set; }

        public virtual ICollection<CarModel> CarModels { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarShedule
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? CarStatusId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual CarStatus? CarStatus { get; set; }
    }
}

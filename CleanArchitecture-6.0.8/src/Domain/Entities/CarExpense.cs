using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarExpense
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public string? Title { get; set; }
        public DateTime? Day { get; set; }
        public double? Amount { get; set; }

        public virtual Car? Car { get; set; }
    }
}

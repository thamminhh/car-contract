using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ForControl
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public string? LinkForControl { get; set; }
        public string? PaymentMethod { get; set; }
        public string? ForControlDay { get; set; }
        public string? DayOfPayment { get; set; }

        public virtual Car? Car { get; set; }
    }
}

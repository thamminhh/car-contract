using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CustomerFile
    {
        public int Id { get; set; }
        public int? CustomerInfoId { get; set; }
        public string? TypeOfDocument { get; set; }
        public string? Title { get; set; }
        public string? DocumentImg { get; set; }
        public string? DocumentDescription { get; set; }

        public virtual CustomerInfo? CustomerInfo { get; set; }
    }
}

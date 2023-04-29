using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class RentContractFile
    {
        public int Id { get; set; }
        public int? RentContractId { get; set; }
        public string? Title { get; set; }
        public string? DocumentImg { get; set; }

        public virtual RentContract? RentContract { get; set; }
    }
}

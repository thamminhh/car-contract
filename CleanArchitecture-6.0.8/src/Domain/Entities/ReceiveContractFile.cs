using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ReceiveContractFile
    {
        public int Id { get; set; }
        public int? ReceiveContractId { get; set; }
        public string? Title { get; set; }
        public string? DocumentImg { get; set; }
        public string? DocumentDescription { get; set; }

        public virtual ReceiveContract? ReceiveContract { get; set; }
    }
}

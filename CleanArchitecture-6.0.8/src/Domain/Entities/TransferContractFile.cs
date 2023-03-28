using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class TransferContractFile
    {
        public int Id { get; set; }
        public int? TransferContractId { get; set; }
        public string? Title { get; set; }
        public string? DocumentImg { get; set; }
        public string? DocumentDescription { get; set; }

        public virtual TransferContract? TransferContract { get; set; }
    }
}

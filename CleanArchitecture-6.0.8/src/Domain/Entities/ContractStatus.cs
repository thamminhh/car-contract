using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractStatus
    {
        public ContractStatus()
        {
            ReceiveContracts = new HashSet<ReceiveContract>();
            TransferContracts = new HashSet<TransferContract>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ReceiveContract> ReceiveContracts { get; set; }
        public virtual ICollection<TransferContract> TransferContracts { get; set; }
    }
}

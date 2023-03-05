using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractStatus
    {
        public ContractStatus()
        {
            ExpertiseContracts = new HashSet<ExpertiseContract>();
            ReceiveContracts = new HashSet<ReceiveContract>();
            RentContracts = new HashSet<RentContract>();
            TransferContracts = new HashSet<TransferContract>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ExpertiseContract> ExpertiseContracts { get; set; }
        public virtual ICollection<ReceiveContract> ReceiveContracts { get; set; }
        public virtual ICollection<RentContract> RentContracts { get; set; }
        public virtual ICollection<TransferContract> TransferContracts { get; set; }
    }
}

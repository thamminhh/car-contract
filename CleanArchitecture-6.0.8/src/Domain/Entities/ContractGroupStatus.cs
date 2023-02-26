using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class ContractGroupStatus
    {
        public ContractGroupStatus()
        {
            ContractGroups = new HashSet<ContractGroup>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ContractGroup> ContractGroups { get; set; }
    }
}

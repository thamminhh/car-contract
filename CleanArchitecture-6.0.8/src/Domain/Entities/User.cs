using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            ContractGroups = new HashSet<ContractGroup>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Job { get; set; }
        public string? CurrentAddress { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? CitizenIdentificationInfoNumber { get; set; }
        public string? CitizenIdentificationInfoAddress { get; set; }
        public DateTime? CitizenIdentificationInfoDateReceive { get; set; }
        public string? PassportInfoNumber { get; set; }
        public string? PassportInfoAddress { get; set; }
        public DateTime? PassportInfoDateReceive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ExpertiseContract? ExpertiseContract { get; set; }
        public virtual ReceiveContract? ReceiveContract { get; set; }
        public virtual RentContract? RentContract { get; set; }
        public virtual TransferContract? TransferContract { get; set; }
        public virtual ICollection<ContractGroup> ContractGroups { get; set; }
    }
}

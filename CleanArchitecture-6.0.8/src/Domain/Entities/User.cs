using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class User
    {
        public User()
        {
            AppraisalRecords = new HashSet<AppraisalRecord>();
            ContractGroups = new HashSet<ContractGroup>();
            ReceiveContracts = new HashSet<ReceiveContract>();
            RentContracts = new HashSet<RentContract>();
            TransferContracts = new HashSet<TransferContract>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Job { get; set; }
        public string? CurrentAddress { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? CardImage { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? Role { get; set; }
        public string? CitizenIdentificationInfoNumber { get; set; }
        public string? CitizenIdentificationInfoAddress { get; set; }
        public DateTime? CitizenIdentificationInfoDateReceive { get; set; }
        public string? PassportInfoNumber { get; set; }
        public string? PassportInfoAddress { get; set; }
        public DateTime? PassportInfoDateReceive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<AppraisalRecord> AppraisalRecords { get; set; }
        public virtual ICollection<ContractGroup> ContractGroups { get; set; }
        public virtual ICollection<ReceiveContract> ReceiveContracts { get; set; }
        public virtual ICollection<RentContract> RentContracts { get; set; }
        public virtual ICollection<TransferContract> TransferContracts { get; set; }
    }
}

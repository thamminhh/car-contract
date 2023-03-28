using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CustomerInfo
    {
        public CustomerInfo()
        {
            ContractGroups = new HashSet<ContractGroup>();
            CustomerFiles = new HashSet<CustomerFile>();
        }

        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CitizenIdentificationInfoNumber { get; set; }
        public string? CitizenIdentificationInfoAddress { get; set; }
        public DateTime? CitizenIdentificationInfoDateReceive { get; set; }
        public string? CustomerSocialInfoZalo { get; set; }
        public string? CustomerSocialInfoFacebook { get; set; }
        public string? RelativeTel { get; set; }
        public string? CompanyInfo { get; set; }
        public string? CustomerEmail { get; set; }

        public virtual ICollection<ContractGroup> ContractGroups { get; set; }
        public virtual ICollection<CustomerFile> CustomerFiles { get; set; }
    }
}

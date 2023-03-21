using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CustomerInfo
    {
        public CustomerInfo()
        {
            ContractGroups = new HashSet<ContractGroup>();
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
        public string? CustomerSocialInfoLinkedin { get; set; }
        public string? CustomerSocialInfoOther { get; set; }
        public string? AddtionalInfo { get; set; }
        public string? RelativeTel { get; set; }
        public bool? ExpertiseInfoIsFirstTimeRent { get; set; }
        public string? ExpertiseInfoTrustLevel { get; set; }
        public string? CompanyInfo { get; set; }

        public virtual ICollection<ContractGroup> ContractGroups { get; set; }
    }
}

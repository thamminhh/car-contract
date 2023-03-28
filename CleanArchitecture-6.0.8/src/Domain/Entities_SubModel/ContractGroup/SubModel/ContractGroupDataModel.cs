using CleanArchitecture.Domain.Entities_SubModel.CustomerFiles.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel
{
    public class ContractGroupDataModel
    {
        public int Id { get; set; }
        public int? CustomerInfoId { get; set; }
        public int? UserId { get; set; }
        public string? StaffEmail { get; set; } // public int? UserId { get; set; }
        public int? CarId { get; set; }
        public string? RentPurpose { get; set; }
        public DateTime? RentFrom { get; set; }
        public DateTime? RentTo { get; set; }
        public string? RequireDescriptionInfoCarBrand { get; set; }
        public int? RequireDescriptionInfoSeatNumber { get; set; }
        public double? RequireDescriptionInfoPriceForDay { get; set; }
        public string? RequireDescriptionInfoCarColor { get; set; }
        public string? RequireDescriptionInfoGearBox { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? ContractGroupStatusId { get; set; }
        public string? ContractGroupStatusName { get; set; } // public int? ContractGroupStatusId { get; set; }

        // public int? CustomerInfoId { get; set; }
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
        //
        public ICollection<CustomerFileDataModel> CustomerFiles { get; set; }

    }
}

namespace CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel
{
    public class ContractGroupDataModel
    {

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CarId { get; set; }
        public string? RentPurpose { get; set; }
        public DateTime? RentFrom { get; set; }
        public DateTime? RentTo { get; set; }
        public string? RequireDescriptionInfoCarBrand { get; set; }
        public int? RequireDescriptionInfoSeatNumber { get; set; }
        public int? RequireDescriptionInfoYearCreate { get; set; }
        public string? RequireDescriptionInfoCarColor { get; set; }
        public string? DeliveryAddress { get; set; }

        public int? ContractGroupStatusId { get; set; }
        public string? ContractGroupStatusName { get; set; }

        public int? CustomerInfoId { get; set; }
        public string? StaffEmail { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CustomerSocialInfoZalo { get; set; }
        public string? CustomerSocialInfoFacebook { get; set; }
        public string? CustomerSocialInfoLinkedin { get; set; }
        public string? CustomerSocialInfoOther { get; set; }
        public string? AddtionalInfo { get; set; }
        public string? RelativeTel { get; set; }
        public bool? ExpertiseInfoIsFirstTimeRent { get; set; }
        public string? ExpertiseInfoTrustLevel { get; set; }
        public string? CompanyInfo { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }


        public string? Path { get; set; }
        public string? CitizenIdentifyImage1 { get; set; }
        public string? CitizenIdentifyImage2 { get; set; }
        public string? DrivingLisenceImage1 { get; set; }
        public string? DrivingLisenceImage2 { get; set; }
        public string? HousePaperImages { get; set; }
        public string? PassportImages { get; set; }
        public string? OtherImages { get; set; }
        public string? ExpertiseContracts { get; set; }
        public string? RentContracts { get; set; }
        public string? TransferContracts { get; set; }
        public string? ReceiveContracts { get; set; }

        public int? ExpertiseContractId { get; set; }
        public int? ExpertiseContractStatusId { get; set; }
        public string? ExpertiseContractStatusName { get; set; }

        public int? RentContractId { get; set; }
        public int? RentContractStatusId { get; set; }
        public string? RentContractStatusName { get; set; }

        public int? TransferContractId { get; set; }
        public int? TransferContractStatusId { get; set; }
        public string? TransferContractStatusName { get; set; }

        public int? ReceiveContractId { get; set; }
        public int? ReceiveContractStatusId { get; set; }
        public string? ReceiveContractStatusName { get; set; }


    }
}

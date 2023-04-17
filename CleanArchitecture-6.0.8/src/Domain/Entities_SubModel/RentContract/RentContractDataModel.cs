using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.RentContract;
public class RentContractDataModel
{
    public int Id { get; set; }
    public int? ContractGroupId { get; set; }
    public int? RepresentativeId { get; set; }
    public string? RepresentativeName { get; set; } //    public int? RepresentativeId { get; set; }
    public string? RepresentativePhoneNumber { get; set; } //    public int? RepresentativeId { get; set; }
    public string? RepresentativeAddress { get; set; }//     public int? RepresentativeId { get; set; }
    public string? CustomerCitizenIdentificationInfoNumber { get; set; }
    public string? CustomerCitizenIdentificationInfoAddress { get; set; }
    public DateTime? CustomerCitizenIdentificationInfoDateReceive { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerPhoneNumber { get; set; }
    public string? CarModel { get; set; }
    public string? CarLicensePlates { get; set; }
    public int? SeatNumber { get; set; }
    public DateTime? RentFrom { get; set; }
    public DateTime? RentTo { get; set; }

    public string? DeliveryAddress { get; set; }
    public double? CarGeneralInfoAtRentPriceForNormalDay { get; set; }
    public double? CarGeneralInfoAtRentPriceForWeekendDay { get; set; }
    public double? CarGeneralInfoAtRentPricePerKmExceed { get; set; }
    public double? CarGeneralInfoAtRentPricePerHourExceed { get; set; }
    public double? CarGeneralInfoAtRentLimitedKmForMonth { get; set; }
    public double? CarGeneralInfoAtRentPriceForMonth { get; set; }
    public double? DeliveryFee { get; set; }
    public DateTime? CreatedDate { get; set; }
    public double? PaymentAmount { get; set; }
    public double? DepositItemDownPayment { get; set; }
    public string? CustomerSignature { get; set; }
    public string? StaffSignature { get; set; }
    public string? FilePath { get; set; }
    public string? FileWithSignsPath { get; set; }
    public bool? IsExported { get; set; }
    public string? CancelReason { get; set; }
    public int? ContractStatusId { get; set; }
}

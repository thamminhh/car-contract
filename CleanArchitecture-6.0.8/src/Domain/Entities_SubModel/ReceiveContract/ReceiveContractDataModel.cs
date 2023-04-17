using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContractFile.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
public class ReceiveContractDataModel
{
    public int Id { get; set; }
    public int? ReceiverId { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverPhoneNumber { get; set; }
    public int? ContractGroupId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerPhoneNumber { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerCitizenIdentificationInfoNumber { get; set; }
    public DateTime? CustomerCitizenIdentificationInfoDateReceive { get; set; }
    public string? CustomerCitizenIdentificationInfoAddress { get; set; }
    public int TransferContractId { get; set; }
    public DateTime? DateReceive { get; set; }
    public string? ReceiveAddress { get; set; }
    public int? CurrentCarStateSpeedometerNumber { get; set; }
    public int? CurrentCarStateFuelPercent { get; set; }
    public double? CurrentCarStateCurrentEtcAmount { get; set; }
    public string? CurrentCarStateCarStatusDescription { get; set; }
    public bool? OriginalCondition { get; set; }
    public double? DepositItemDownPayment { get; set; }
    public bool? ReturnDepostiItem { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? FilePath { get; set; }
    public string? StaffSignature { get; set; }
    public string? CustomerSignature { get; set; }
    public string? FileWithSignsPath { get; set; }
    public int? ContractStatusId { get; set; }
    public double? TotalKilometersTraveled { get; set; }
    public string? CurrentCarStateCarDamageDescription { get; set; }
    public double? InsuranceMoney { get; set; }
    public double? ExtraTime { get; set; }
    public bool? DetectedViolations { get; set; }
    public string? SpeedingViolationDescription { get; set; }
    public string? ForbiddenRoadViolationDescription { get; set; }
    public string? TrafficLightViolationDescription { get; set; }
    public string? OrtherViolation { get; set; }
    public double? ViolationMoney { get; set; }

    public double? TotalPayment { get; set; }
    public ICollection<ReceiveContractFileDataModel> ReceiveContractFileDataModels { get; set; }

}

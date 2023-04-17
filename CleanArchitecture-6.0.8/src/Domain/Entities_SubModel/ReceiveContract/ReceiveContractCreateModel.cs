

using CleanArchitecture.Domain.Entities_SubModel.ReceiveContractFile.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.TransferContractFile.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
public class ReceiveContractCreateModel
{
    public int? ReceiverId { get; set; }
    public int ContractGroupId { get; set; }
    public int TransferContractId { get; set; }
    public DateTime? DateReceive { get; set; }
    public string? ReceiveAddress { get; set; }
    public bool? OriginalCondition { get; set; }
    public int? CurrentCarStateSpeedometerNumber { get; set; }
    public int? CurrentCarStateFuelPercent { get; set; }
    public double? CurrentCarStateCurrentEtcAmount { get; set; }
    public string? CurrentCarStateCarStatusDescription { get; set; }
    public double? DepositItemDownPayment { get; set; }
    public bool? ReturnDepostiItem { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CurrentCarStateCarDamageDescription { get; set; }
    public double? InsuranceMoney { get; set; }
    public double? ExtraTime { get; set; }
    public bool? DetectedViolations { get; set; }
    public string? SpeedingViolationDescription { get; set; }
    public string? ForbiddenRoadViolationDescription { get; set; }
    public string? TrafficLightViolationDescription { get; set; }
    public string? OrtherViolation { get; set; }
    public double? ViolationMoney { get; set; }
    public ICollection<ReceiveContractFileCreateModel> ReceiveContractFileCreateModels{ get; set; }

}

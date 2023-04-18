using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContractFile.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.TransferContractFile.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
public class ReceiveContractUpdateModel
{
    public int Id { get; set; }
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
    public string? StaffSignature { get; set; }
    public string? CustomerSignature { get; set; }
    public int? ContractStatusId { get; set; }
    public string? CurrentCarStateCarDamageDescription { get; set; }
    public double? InsuranceMoney { get; set; }
    public double? ExtraTime { get; set; }
    public bool? DetectedViolations { get; set; }
    public string? SpeedingViolationDescription { get; set; }
    public string? ForbiddenRoadViolationDescription { get; set; }
    public string? TrafficLightViolationDescription { get; set; }
    public string? OrtherViolation { get; set; }
    public double? ViolationMoney { get; set; }

    public double? CurrentFuelMoney { get; set; }
    public ICollection<ReceiveContractFileDataModel> ReceiveContractFileDataModels { get; set; }

}

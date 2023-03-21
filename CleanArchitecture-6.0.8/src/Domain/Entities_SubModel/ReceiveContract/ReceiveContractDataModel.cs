using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public string? CurrentCarStateCarFrontImg { get; set; }
    public string? CurrentCarStateCarBackImg { get; set; }
    public string? CurrentCarStateCarLeftImg { get; set; }
    public string? CurrentCarStateCarRightImg { get; set; }
    public string? CurrentCarStateCarInteriorImg { get; set; }
    public string? CurrentCarStateCarBackSeatImg { get; set; }
    public string? CurrentCarStateCarPhysicalDamage { get; set; }
    public string? DepositItemAsset { get; set; }
    public bool? OriginalCondition { get; set; }
    public string? CurrentCarStateCarDamageDescription { get; set; }
    public string? OrtherViolation { get; set; }
    public double? CarInsuranceMoney { get; set; }
    public bool? DetectedViolations { get; set; }
    public string? SpeedingViolationDescription { get; set; }
    public string? ForbiddenRoadViolationDescription { get; set; }
    public string? TrafficLightViolationDescription { get; set; }
    public double? ExtraTime { get; set; }
    public double? UnpaidTicketMoney { get; set; }
    public DateTime? CreatedDate { get; set; }
    public bool? IsExported { get; set; }
    public string? FilePath { get; set; }
    public string? FileWithSignsPath { get; set; }
    public string? CustomerSignature { get; set; }
    public string? StaffSignature { get; set; }
    public int? ContractStatusId { get; set; }
}

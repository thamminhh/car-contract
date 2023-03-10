

namespace CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;
public class ReceiveContractCreateModel
{
    public int? ReceiverId { get; set; }
    public int ContractGroupId { get; set; }
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
    public double? DepositItemAsset { get; set; }
    public double? CarInsuranceMoney { get; set; }
    public DateTime? CreatedDate { get; set; }

}

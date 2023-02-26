using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.TransferContract;
public class TransferContractCreateModel
{
    public int? TransfererId { get; set; }
    public int? ContractGroupId { get; set; }
    public DateTime? DateTransfer { get; set; }
    public string? DeliveryAddress { get; set; }
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
    public string? DepositItemPaper { get; set; }
    public double? DepositItemAsset { get; set; }
    public string? DepositItemAssetInfo { get; set; }
    public DateTime? CreatedDate { get; set; }

}

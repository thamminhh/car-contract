using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities_SubModel.TransferContractFile.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.TransferContract;
public class TransferContractCreateModel
{
    public int? TransfererId { get; set; }
    public int ContractGroupId { get; set; }
    public DateTime? DateTransfer { get; set; }
    public string? DeliveryAddress { get; set; }
    public int? CurrentCarStateSpeedometerNumber { get; set; }
    public int? CurrentCarStateFuelPercent { get; set; }
    public double? CurrentCarStateCurrentEtcAmount { get; set; }
    public string? CurrentCarStateCarStatusDescription { get; set; }
    public double? DepositItemDownPayment { get; set; }
    public string? DepositItemAsset { get; set; }
    public string? DepositItemDescription { get; set; }
    public DateTime? CreatedDate { get; set; }
    public ICollection<TransferContractFileCreateModel> TransferContractFileCreateModels { get; set; }
}

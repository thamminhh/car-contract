using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.TransferContractFile.SubModel;
public class TransferContractFileDataModel
{
    public int Id { get; set; }
    public int? TransferContractId { get; set; }
    public string? Title { get; set; }
    public string? DocumentImg { get; set; }
    public string? DocumentDescription { get; set; }
}

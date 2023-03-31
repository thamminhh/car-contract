using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.ReceiveContractFile.SubModel;
public class ReceiveContractFileDataModel
{
    public int Id { get; set; }
    public int? ReceiveContractId { get; set; }
    public string? Title { get; set; }
    public string? DocumentImg { get; set; }
    public string? DocumentDescription { get; set; }

}

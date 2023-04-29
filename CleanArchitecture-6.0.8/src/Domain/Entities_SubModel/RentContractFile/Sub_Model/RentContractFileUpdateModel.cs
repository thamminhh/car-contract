using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.RentContractFile.Sub_Model;
public class RentContractFileUpdateModel
{
    public int Id { get; set; }
    public int? RentContractId { get; set; }
    public string? Title { get; set; }
    public string? DocumentImg { get; set; }
}

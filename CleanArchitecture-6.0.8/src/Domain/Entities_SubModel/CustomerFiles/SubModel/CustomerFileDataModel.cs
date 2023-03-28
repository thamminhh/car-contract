using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities_SubModel.CustomerFiles.SubModel;
public class CustomerFileDataModel
{
    public int Id { get; set; }
    public int CustomerInfoId { get; set; }
    public string? TypeOfDocument { get; set; }
    public string? Title { get; set; }
    public string? DocumentImg { get; set; }
    public string? DocumentDescription { get; set; }
}

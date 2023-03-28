using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities_SubModel.CustomerFiles.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.CustomerInfos;
public class CustomerInfoUpdateModel
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CitizenIdentificationInfoNumber { get; set; }
    public string? CitizenIdentificationInfoAddress { get; set; }
    public DateTime? CitizenIdentificationInfoDateReceive { get; set; }
    public string? CustomerSocialInfoZalo { get; set; }
    public string? CustomerSocialInfoFacebook { get; set; }
    public string? RelativeTel { get; set; }
    public string? CompanyInfo { get; set; }
    public string? CustomerEmail { get; set; }
    public virtual ICollection<CustomerFileDataModel> CustomerFiles { get; set; }
}

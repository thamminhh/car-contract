using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Endpoints;
public class StatisticEndpoints
{
    public const string Area = "";
    public const string Base = Area + "/statistic";
    public const string GetSingle = Base + "/contractStatisticId/{contractStatisticId}";
    public const string GetByContractGroupId = Base + "/contractGroupId/{contractGroupId}";
    public const string GetByContractGroup = Base + "/contractGroup";
    public const string GetByCar = Base + "/car";
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractStatistic.Sub_Model;

namespace CleanArchitecture.Domain.Interface;
public interface IContractStatisticRepository
{
    ICollection<ContractStatisticDataModel> GetContractStatistic(DateTime from, DateTime to);
    void CreateContractStatistic(ContractStatisticCreateModel request);

    void UpdateContractStatistic(ContractStatisticUpdateModel request);
}

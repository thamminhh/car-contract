using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractStatistic.Sub_Model;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Repository
{
    public class ContractStatisticRepository : IContractStatisticRepository
    {
        private readonly ContractContext _contractContext;

        public ContractStatisticRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }


        public bool ContractStatisticExit(int id)
        {
            return _contractContext.ContractStatistics.Any(c => c.Id == id);
        }

        public void CreateContractStatistic(ContractStatisticCreateModel request)
        {
            var contractStatistic = new ContractStatistic
            {
                ContractGroupId = request.ContractGroupId,
                EtcmoneyUsing = request.EtcmoneyUsing,
                FuelMoneyUsing = request.FuelMoneyUsing,
                ExtraTimeMoney = request.ExtraTimeMoney,
                ExtraKmMoney = request.ExtraKmMoney,
                PaymentAmount = request.PaymentAmount,

                Total = calTotal(request.EtcmoneyUsing, request.FuelMoneyUsing, request.ExtraTimeMoney, request.ExtraKmMoney, request.PaymentAmount),
            };
            _contractContext.ContractStatistics.Add(contractStatistic);
            _contractContext.SaveChanges();

        }
        private double? calTotal(double? etc, double? fuel, double? extraTime,double? extraKm, double? paymentAmount)
        {
            double? total;
            return total = etc + fuel + extraTime + extraKm + paymentAmount;  
        }

        public ICollection<ContractStatisticDataModel> GetContractStatistic(DateTime from, DateTime to)
        {
            IQueryable<ContractStatistic> contractStatistics = _contractContext.ContractStatistics
                .Include(c => c.ContractGroup)
                .Where(c => c.ContractGroup.ContractGroupStatusId == Constant.ContractGroupConstant.ReceiveContractSigned)
                .AsQueryable();

            contractStatistics = contractStatistics.Where(c => c.ContractGroup.RentFrom < to && c.ContractGroup.RentTo > from);
            var contractStatisticDataModel = contractStatistics
                .OrderBy(c => c.Id)
                .Select(c => new ContractStatisticDataModel
                { 
                    Id = c.Id,
                    ContractGroupId = c.ContractGroupId,
                    ExtraTimeMoney= c.ExtraTimeMoney,
                    EtcmoneyUsing = c.EtcmoneyUsing,
                    FuelMoneyUsing = c.FuelMoneyUsing,
                    ExtraKmMoney = c.ExtraKmMoney,
                    PaymentAmount = c.PaymentAmount,
                    Total = c.Total,
                }).ToList();
            return contractStatisticDataModel;
        }

        public void UpdateContractStatistic(ContractStatisticUpdateModel request)
        {
            var contractStatistc = _contractContext.ContractStatistics.Find(request.Id);

            contractStatistc.ExtraTimeMoney = request.ExtraTimeMoney;
            contractStatistc.EtcmoneyUsing = request.EtcmoneyUsing;
            contractStatistc.FuelMoneyUsing = request.FuelMoneyUsing;
            contractStatistc.ExtraKmMoney = request.ExtraKmMoney;
            contractStatistc.PaymentAmount = request.PaymentAmount;
            contractStatistc.Total = calTotal(request.EtcmoneyUsing, request.FuelMoneyUsing, request.ExtraTimeMoney, request.ExtraKmMoney, request.PaymentAmount);

            //save update 
            _contractContext.ContractStatistics.Update(contractStatistc);
            _contractContext.SaveChanges();
        }
    }
}

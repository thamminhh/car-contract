using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
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
                InsuranceMoney = request.InsuranceMoney,
                ViolationMoney = request.ViolationMoney,
                PaymentAmount = request.PaymentAmount,

                Total = calTotal(request.EtcmoneyUsing, request.FuelMoneyUsing, request.ExtraTimeMoney, request.ExtraKmMoney,request.InsuranceMoney, request.ViolationMoney, request.PaymentAmount),
            };
            _contractContext.ContractStatistics.Add(contractStatistic);
            _contractContext.SaveChanges();

        }
        private double? calTotal(double? etc, double? fuel, double? extraTime,double? extraKm,double? insuranceMoney, double? violationMoney, double? paymentAmount)
        {
            double? total;
            return total = etc + fuel + extraTime + extraKm + insuranceMoney + violationMoney + paymentAmount;  
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
                    CreatedDate = c.ContractGroup.RentFrom,
                    ExtraTimeMoney= c.ExtraTimeMoney,
                    EtcmoneyUsing = c.EtcmoneyUsing,
                    FuelMoneyUsing = c.FuelMoneyUsing,
                    ExtraKmMoney = c.ExtraKmMoney,
                    InsuranceMoney = c.InsuranceMoney,
                    ViolationMoney = c.ViolationMoney,
                    PaymentAmount = c.PaymentAmount,
                    Total = c.Total,
                }).ToList();
            return contractStatisticDataModel;
        }

        public ICollection<CarRevenue> GetContractStatisticForCar(DateTime from, DateTime to, int carId)
        {
            IQueryable<ContractStatistic> contractStatistics = _contractContext.ContractStatistics
                .Include(c => c.ContractGroup)
                .Where(c => c.ContractGroup.ContractGroupStatusId == Constant.ContractGroupConstant.ReceiveContractSigned)
                .Where(c => c.ContractGroup.CarId == carId)
                .AsQueryable();

            contractStatistics = contractStatistics.Where(c => c.ContractGroup.RentFrom < to && c.ContractGroup.RentTo > from);
            var contractStatisticDataModel = contractStatistics
                .OrderBy(c => c.Id)
                .Select(c => new CarRevenue
                {
                    Id = c.Id,
                    ContractGroupId = c.ContractGroupId,
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
            contractStatistc.InsuranceMoney = request.InsuranceMoney;
            contractStatistc.ViolationMoney = request.ViolationMoney;
            contractStatistc.PaymentAmount = request.PaymentAmount;
            contractStatistc.Total = calTotal(request.EtcmoneyUsing, request.FuelMoneyUsing, request.ExtraTimeMoney, request.ExtraKmMoney,request.InsuranceMoney, request.ViolationMoney, request.PaymentAmount);

            //save update 
            _contractContext.ContractStatistics.Update(contractStatistc);
            _contractContext.SaveChanges();
        }
    }
}

using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarMaintenanceInfo;
using CleanArchitecture.Domain.Interface;
using MediatR;
using PdfSharpCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using CleanArchitecture.Domain.Entities_SubModel.CarMaintenanceInfo.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarExpense.Sub_Model;

namespace CleanArchitecture.Application.Repository
{
    public class CarMaintenanceInfoRepository : ICarMaintenanceInfoRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupController;
        private readonly FileRepository _fileRepository;
        private readonly ICarExpenseRepository _carExpenseRepository;

        public CarMaintenanceInfoRepository(ContractContext contractContext, IContractGroupRepository contractGroupController, FileRepository fileRepository,
            ICarExpenseRepository carExpenseRepository)
        {
            _contractContext = contractContext;
            _contractGroupController = contractGroupController;
            _fileRepository = fileRepository;
            _carExpenseRepository = carExpenseRepository;
        }

        public CarMaintenanceInfo GetCarMaintenanceInfoById(int id)
        {
            var carMaintenanceInfos = _contractContext.CarMaintenanceInfos.Where(c => c.Id == id).FirstOrDefault();

            return new CarMaintenanceInfo
            {
                Id = id,
                CarId = carMaintenanceInfos.CarId,
                CarKmlastMaintenance = carMaintenanceInfos.CarKmlastMaintenance,
                KmTraveled = carMaintenanceInfos.KmTraveled,
                MaintenanceDate = carMaintenanceInfos.MaintenanceDate,
                MaintenanceInvoice = carMaintenanceInfos.MaintenanceInvoice,
                MaintenanceAmount = carMaintenanceInfos.MaintenanceAmount

            };
        }

        public ICollection <CarMaintenanceInfo> GetCarMaintenanceInfoByCarId(int carId)
        {
            var carMaintenanceInfos = _contractContext.CarMaintenanceInfos.Where(c => c.CarId == carId).ToList();
            return carMaintenanceInfos;
         }


        public bool CarMaintenanceInfoExit(int id)
        {
            return _contractContext.CarMaintenanceInfos.Any(c => c.Id == id);
        }

        public void CreateCarMaintenanceInfo(CarMaintenanceInfoCreateModel request)
        {

            var carMaintenanceInfo = new CarMaintenanceInfo
            {
                CarId = request.CarId,
                CarKmlastMaintenance = request.CarKmlastMaintenance,
                KmTraveled = 0,
                MaintenanceDate = request.MaintenanceDate,
                MaintenanceInvoice = request.MaintenanceInvoice,
                MaintenanceAmount = request.MaintenanceAmount
            };
            _contractContext.CarMaintenanceInfos.Add(carMaintenanceInfo);
            _contractContext.SaveChanges();

            //Update CarState
            var carState = _contractContext.CarStates.Where(c => c.CarId == request.CarId).FirstOrDefault();
            carState.SpeedometerNumber = request.CarKmlastMaintenance;
            _contractContext.CarStates.Update(carState);
            _contractContext.SaveChanges();

            //Create CarExpense 
            var carExpense = new CarExpenseCreateModel();
            carExpense.CarId = request.CarId;
            carExpense.Title = "Bảo dưỡng";
            carExpense.Day = request.MaintenanceDate;
            carExpense.Amount= request.MaintenanceAmount;
            _carExpenseRepository.CreateCarExpense(carExpense);

        }

        public void UpdateCarMaintenanceInfo(int id, CarMaintenanceInfo request)
        {
            var carMaintenanceInfo = _contractContext.CarMaintenanceInfos.Find(id);

            carMaintenanceInfo.CarId = request.CarId;
            carMaintenanceInfo.CarKmlastMaintenance = request.CarKmlastMaintenance;
            carMaintenanceInfo.KmTraveled = request.KmTraveled;
            carMaintenanceInfo.MaintenanceDate = request.MaintenanceDate;
            carMaintenanceInfo.MaintenanceInvoice = request.MaintenanceInvoice;
            carMaintenanceInfo.MaintenanceAmount = request.MaintenanceAmount;

            _contractContext.CarMaintenanceInfos.Update(carMaintenanceInfo);
            _contractContext.SaveChanges();

            //Update CarState
            var carState = _contractContext.CarStates.Where(c => c.CarId == request.CarId).FirstOrDefault();
            carState.SpeedometerNumber = request.CarKmlastMaintenance;
            _contractContext.CarStates.Update(carState);
            _contractContext.SaveChanges();


        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

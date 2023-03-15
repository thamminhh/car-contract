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

namespace CleanArchitecture.Application.Repository
{
    public class CarMaintenanceInfoRepository : ICarMaintenanceInfoRepository
    {
        private readonly ContractContext _contractContext;
        private readonly IContractGroupRepository _contractGroupController;
        private readonly FileRepository _fileRepository;

        public CarMaintenanceInfoRepository(ContractContext contractContext, IContractGroupRepository contractGroupController, FileRepository fileRepository)
        {
            _contractContext = contractContext;
            _contractGroupController = contractGroupController;
            _fileRepository = fileRepository;
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
                KmTraveled = request.KmTraveled,
                MaintenanceDate = request.MaintenanceDate,
                MaintenanceInvoice = request.MaintenanceInvoice,
                MaintenanceAmount = request.MaintenanceAmount
            };
            _contractContext.CarMaintenanceInfos.Add(carMaintenanceInfo);
            _contractContext.SaveChanges();
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
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

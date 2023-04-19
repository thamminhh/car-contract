using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarRegistryInfo;
using CleanArchitecture.Domain.Interface;
using MediatR;
using PdfSharpCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using CleanArchitecture.Domain.Entities_SubModel.CarRegistryInfo.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarExpense.Sub_Model;

namespace CleanArchitecture.Application.Repository
{
    public class CarRegistryInfoRepository : ICarRegistryInfoRepository
    {
        private readonly ContractContext _contractContext;
        private readonly ICarExpenseRepository _carExpenseRepository;

        public CarRegistryInfoRepository(ContractContext contractContext, ICarExpenseRepository carExpenseRepository)
        {
            _contractContext = contractContext;
            _carExpenseRepository = carExpenseRepository;
        }

        public CarRegistryInfo GetCarRegistryInfoById(int id)
        {
            var carRegistryInfos = _contractContext.CarRegistryInfos.Where(c => c.Id == id).FirstOrDefault();

            return new CarRegistryInfo
            {
                Id = id,
                CarId = carRegistryInfos.CarId,
                RegistrationDeadline = carRegistryInfos.RegistrationDeadline,
                RegistryAmount = carRegistryInfos.RegistryAmount,
                RegistryInvoice = carRegistryInfos.RegistryInvoice,
                RegistryAddress = carRegistryInfos.RegistryAddress,
                CertificateRegistryDocument = carRegistryInfos.CertificateRegistryDocument,
            };
        }

        public ICollection<CarRegistryInfo> GetCarRegistryInfoByCarId(int carId)
        {
            var CarRegistryInfos = _contractContext.CarRegistryInfos.Where(c => c.CarId == carId).ToList();
            return CarRegistryInfos;
        }


        public bool CarRegistryInfoExit(int id)
        {
            return _contractContext.CarRegistryInfos.Any(c => c.Id == id);
        }

        public void CreateCarRegistryInfo(CarRegistryInfoCreateModel request)
        {

            var carRegistryInfo = new CarRegistryInfo
            {
                CarId = request.CarId,
                RegistrationDeadline = request.RegistrationDeadline,
                RegistryAmount = request.RegistryAmount,
                RegistryInvoice = request.RegistryInvoice,
                RegistryAddress = request.RegistryAddress,
                CertificateRegistryDocument = request.CertificateRegistryDocument,

            };
            _contractContext.CarRegistryInfos.Add(carRegistryInfo);
            _contractContext.SaveChanges();

            //Create CarExpense 
            var carExpense = new CarExpenseCreateModel();
            carExpense.CarId = request.CarId;
            carExpense.Title = "Đăng kiểm";
            carExpense.Day = DateTime.Today;
            carExpense.Amount = request.RegistryAmount;
            _carExpenseRepository.CreateCarExpense(carExpense);
        }

        public void UpdateCarRegistryInfo(int id, CarRegistryInfo request)
        {
            var carRegistryInfo = _contractContext.CarRegistryInfos.Find(id);

            carRegistryInfo.CarId = request.CarId;
            carRegistryInfo.RegistrationDeadline = request.RegistrationDeadline;
            carRegistryInfo.RegistryAmount = request.RegistryAmount;
            carRegistryInfo.RegistryInvoice = request.RegistryInvoice;
            carRegistryInfo.RegistryAddress = request.RegistryAddress;
            carRegistryInfo.CertificateRegistryDocument = request.CertificateRegistryDocument;

            _contractContext.CarRegistryInfos.Update(carRegistryInfo);
            _contractContext.SaveChanges();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}


using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContractFile.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class ReceiveContractFileRepository : IReceiveContractFileRepository
    {
        private readonly ContractContext _contractContext;

        public ReceiveContractFileRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public void CreateReceiveContractFile(ReceiveContractFileCreateModel request)
        {
            throw new NotImplementedException();
        }

        public ReceiveContractFileDataModel GetReceiveContractFileById(int ReceiveContractFileId)
        {
            throw new NotImplementedException();
        }

        public ICollection<ReceiveContractFileDataModel> GetReceiveContractFiles()
        {
            throw new NotImplementedException();
        }

        //public ReceiveContractFileDataModel GetReceiveContractFileById(int ReceiveContractFileId)
        //{
        //    ReceiveContractFile ReceiveContractFile = _contractContext.ReceiveContractFiles
        //        .FirstOrDefault(c => c.Id == ReceiveContractFileId);

        //    return new ReceiveContractFileDataModel
        //    {
        //        Id = ReceiveContractFile.Id,

        //    };
        //}

        //public ICollection<ReceiveContractFileDataModel> GetReceiveContractFiles()
        //{
        //    IQueryable<ReceiveContractFile> ReceiveContractFiles = _contractContext.ReceiveContractFiles
        //       .AsQueryable();

        //    var ReceiveContractFileDataModels = ReceiveContractFiles
        //        .OrderBy(c => c.Id)
        //        .Select(c => new ReceiveContractFileDataModel
        //        {
        //            Id = c.Id,

        //        })
        //        .ToList();

        //    return ReceiveContractFileDataModels;
        //}

        public ICollection<ReceiveContractFileDataModel> GetReceiveContractFilesByReceiveContractId(int receiveContractId)
        {
            var receiveContractFiles = _contractContext.ReceiveContractFiles
                .Where(cf => cf.ReceiveContractId == receiveContractId)
                .ToList();
            return receiveContractFiles.Select(cf => new ReceiveContractFileDataModel
            {
                Id = cf.Id,
                ReceiveContractId = (int)cf.ReceiveContractId,
                Title = cf.Title,
                DocumentImg = cf.DocumentImg,
                DocumentDescription = cf.DocumentDescription
            }).ToList();
        }

        //public ICollection<ReceiveContractFileDataModel> GetReceiveContractFilesByCustomerInfoId(int customerInfoId)
        //{

        //    IQueryable<ReceiveContractFile> ReceiveContractFiles = _contractContext.ReceiveContractFiles
        //        .Where(c => c.CustomerInfoId == customerInfoId)
        //        .AsQueryable();

        //    var ReceiveContractFileDataModels = ReceiveContractFiles
        //        .OrderBy(c => c.Id)
        //        .Select(c => new ReceiveContractFileDataModel
        //        {
        //            Id = c.Id,
        //            CustomerInfoId = c.CustomerInfoId,
        //            TypeOfDocument = c.TypeOfDocument,
        //            Title = c.Title,
        //            DocumentImg = c.DocumentImg,
        //            DocumentDescription = c.DocumentDescription,
        //        })
        //        .ToList();

        //    return ReceiveContractFileDataModels;
        //}

        //public void CreateReceiveContractFile(ReceiveContractFileCreateModel request)
        //{
        //    var ReceiveContractFile = new ReceiveContractFile
        //    {
        //        TypeOfDocument = request.TypeOfDocument,
        //        Title = request.Title,
        //        DocumentImg = request.DocumentImg,
        //        DocumentDescription = request.DocumentDescription,
        //    };

        //    // Save the new ReceiveContractFile to the database
        //    _contractContext.ReceiveContractFiles.Add(ReceiveContractFile);
        //    _contractContext.SaveChanges();

        //}

        //public void UpdateReceiveContractFile(int id, ReceiveContractFileUpdateModel request)
        //{
        //    var ReceiveContractFile = _contractContext.ReceiveContractFiles.Find(id);

        //    // Update the properties of the ReceiveContractFile object
        //    ReceiveContractFile.CarId = request.CarId;
        //    ReceiveContractFile.DateStart = request.DateStart;
        //    ReceiveContractFile.DateEnd = request.DateEnd;
        //    ReceiveContractFile.CarStatusId = request.CarStatusId;

        //    // Save the changes to the database
        //    _contractContext.ReceiveContractFiles.Update(ReceiveContractFile);
        //    _contractContext.SaveChanges();


        //}

    }
}

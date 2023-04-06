
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.TransferContractFile.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class TransferContractFileRepository : ITransferContractFileRepository
    {
        private readonly ContractContext _contractContext;

        public TransferContractFileRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public void CreateTransferContractFile(TransferContractFileCreateModel request)
        {
            throw new NotImplementedException();
        }

        public TransferContractFileDataModel GetTransferContractFileById(int TransferContractFileId)
        {
            throw new NotImplementedException();
        }

        public ICollection<TransferContractFileDataModel> GetTransferContractFiles()
        {
            throw new NotImplementedException();
        }

        //public TransferContractFileDataModel GetTransferContractFileById(int TransferContractFileId)
        //{
        //    TransferContractFile TransferContractFile = _contractContext.TransferContractFiles
        //        .FirstOrDefault(c => c.Id == TransferContractFileId);

        //    return new TransferContractFileDataModel
        //    {
        //        Id = TransferContractFile.Id,

        //    };
        //}

        //public ICollection<TransferContractFileDataModel> GetTransferContractFiles()
        //{
        //    IQueryable<TransferContractFile> TransferContractFiles = _contractContext.TransferContractFiles
        //       .AsQueryable();

        //    var TransferContractFileDataModels = TransferContractFiles
        //        .OrderBy(c => c.Id)
        //        .Select(c => new TransferContractFileDataModel
        //        {
        //            Id = c.Id,

        //        })
        //        .ToList();

        //    return TransferContractFileDataModels;
        //}

        public ICollection<TransferContractFileDataModel> GetTransferContractFilesByTransferContractId(int transferContractId)
        {
            var TransferContractFiles = _contractContext.TransferContractFiles
                .Where(cf => cf.TransferContractId == transferContractId)
                .ToList();
            return TransferContractFiles.Select(cf => new TransferContractFileDataModel
            {
                Id = cf.Id,
                TransferContractId = (int)cf.TransferContractId,
                Title = cf.Title,
                DocumentImg = cf.DocumentImg,
                DocumentDescription = cf.DocumentDescription
            }).ToList();
        }
        //public ICollection<TransferContractFileDataModel> GetTransferContractFilesByCustomerInfoId(int customerInfoId)
        //{

        //    IQueryable<TransferContractFile> TransferContractFiles = _contractContext.TransferContractFiles
        //        .Where(c => c.CustomerInfoId == customerInfoId)
        //        .AsQueryable();

        //    var TransferContractFileDataModels = TransferContractFiles
        //        .OrderBy(c => c.Id)
        //        .Select(c => new TransferContractFileDataModel
        //        {
        //            Id = c.Id,
        //            CustomerInfoId = c.CustomerInfoId,
        //            TypeOfDocument = c.TypeOfDocument,
        //            Title = c.Title,
        //            DocumentImg = c.DocumentImg,
        //            DocumentDescription = c.DocumentDescription,
        //        })
        //        .ToList();

        //    return TransferContractFileDataModels;
        //}

        //public void CreateTransferContractFile(TransferContractFileCreateModel request)
        //{
        //    var TransferContractFile = new TransferContractFile
        //    {
        //        TypeOfDocument = request.TypeOfDocument,
        //        Title = request.Title,
        //        DocumentImg = request.DocumentImg,
        //        DocumentDescription = request.DocumentDescription,
        //    };

        //    // Save the new TransferContractFile to the database
        //    _contractContext.TransferContractFiles.Add(TransferContractFile);
        //    _contractContext.SaveChanges();

        //}

        //public void UpdateTransferContractFile(int id, TransferContractFileUpdateModel request)
        //{
        //    var TransferContractFile = _contractContext.TransferContractFiles.Find(id);

        //    // Update the properties of the TransferContractFile object
        //    TransferContractFile.CarId = request.CarId;
        //    TransferContractFile.DateStart = request.DateStart;
        //    TransferContractFile.DateEnd = request.DateEnd;
        //    TransferContractFile.CarStatusId = request.CarStatusId;

        //    // Save the changes to the database
        //    _contractContext.TransferContractFiles.Update(TransferContractFile);
        //    _contractContext.SaveChanges();


        //}

        public async Task<bool> DeleteTransferContractFile(int transferContractFileId)
        {
            var transferContractFile = await _contractContext.TransferContractFiles.FindAsync(transferContractFileId);

            if (transferContractFile == null)
            {
                return false; // Object not found
            }

            _contractContext.TransferContractFiles.Remove(transferContractFile);
            await _contractContext.SaveChangesAsync();

            return true; // Object deleted successfully
        }
    }
}

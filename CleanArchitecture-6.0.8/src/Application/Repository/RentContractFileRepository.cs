
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.RentContractFile.Sub_Model;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class RentContractFileRepository : IRentContractFileRepository
    {
        private readonly ContractContext _contractContext;

        public RentContractFileRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public async Task CreateRentContractFiles(List<RentContractFileCreateModel> rentContractFiles)
        {
            if (rentContractFiles == null || !rentContractFiles.Any())
            {
                throw new ArgumentNullException(nameof(rentContractFiles), "No rent contract files provided.");
            }

            var rentContractFilesToCreate = rentContractFiles.Select(rcf => new RentContractFile
            {
                RentContractId = rcf.RentContractId,
                Title = rcf.Title,
                DocumentImg = rcf.DocumentImg
            }).ToList();

            await _contractContext.RentContractFiles.AddRangeAsync(rentContractFilesToCreate);
            await _contractContext.SaveChangesAsync();
        }

        //    public RentContractFileDataModel GetRentContractFileById(int RentContractFileId)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public ICollection<RentContractFileDataModel> GetRentContractFiles()
        //    {
        //        throw new NotImplementedException();

        public ICollection<RentContractFileDataModel> GetRentContractFilesByRentContractId(int rentContractId)
        {
            var RentContractFiles = _contractContext.RentContractFiles
                .Where(cf => cf.RentContractId == rentContractId)
                .ToList();
            return RentContractFiles.Select(cf => new RentContractFileDataModel
            {
                Id = cf.Id,
                RentContractId = (int)cf.RentContractId,
                Title = cf.Title,
                DocumentImg = cf.DocumentImg,
            }).ToList();
        }

        public async Task UpdateRentContractFiles(List<RentContractFileUpdateModel> rentContractFiles)
        {
            if (rentContractFiles == null || !rentContractFiles.Any())
            {
                throw new ArgumentNullException(nameof(rentContractFiles), "No rent contract files provided.");
            }

            foreach (var rcf in rentContractFiles)
            {
                var existingRentContractFile = await _contractContext.RentContractFiles
                    .FirstOrDefaultAsync(f => f.Id == rcf.Id && f.RentContractId == rcf.RentContractId);

                if (existingRentContractFile != null)
                {
                    existingRentContractFile.Title = rcf.Title;
                    existingRentContractFile.DocumentImg = rcf.DocumentImg;
                }
                else
                {
                    var newRentContractFile = new RentContractFile
                    {
                        RentContractId = rcf.RentContractId,
                        Title = rcf.Title,
                        DocumentImg = rcf.DocumentImg
                    };

                    _contractContext.RentContractFiles.Add(newRentContractFile);
                }
            }

            await _contractContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteRentContractFile(int rentContractFileId)
        {
            var RentContractFile = await _contractContext.RentContractFiles.FindAsync(rentContractFileId);

            if (RentContractFile == null)
            {
                return false; // Object not found
            }

            _contractContext.RentContractFiles.Remove(RentContractFile);
            await _contractContext.SaveChangesAsync();

            return true; // Object deleted successfully
        }
    }
}


using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CustomerFiles.SubModel;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class CustomerFileRepository : ICustomerFileRepository
    {
        private readonly ContractContext _contractContext;

        public CustomerFileRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public void CreateCustomerFile(CustomerFileCreateModel request)
        {
            throw new NotImplementedException();
        }

        public CustomerFileDataModel GetCustomerFileById(int customerFileId)
        {
            throw new NotImplementedException();
        }

        public ICollection<CustomerFileDataModel> GetCustomerFiles()
        {
            throw new NotImplementedException();
        }

        //public CustomerFileDataModel GetCustomerFileById(int CustomerFileId)
        //{
        //    CustomerFile CustomerFile = _contractContext.CustomerFiles
        //        .FirstOrDefault(c => c.Id == CustomerFileId);

        //    return new CustomerFileDataModel
        //    {
        //        Id = CustomerFile.Id,

        //    };
        //}

        //public ICollection<CustomerFileDataModel> GetCustomerFiles()
        //{
        //    IQueryable<CustomerFile> CustomerFiles = _contractContext.CustomerFiles
        //       .AsQueryable();

        //    var CustomerFileDataModels = CustomerFiles
        //        .OrderBy(c => c.Id)
        //        .Select(c => new CustomerFileDataModel
        //        {
        //            Id = c.Id,

        //        })
        //        .ToList();

        //    return CustomerFileDataModels;
        //}

        public ICollection<CustomerFileDataModel> GetCustomerFilesByCustomerInfoId(int customerInfoId)
        {
            var customerFiles = _contractContext.CustomerFiles
                .Where(cf => cf.CustomerInfoId == customerInfoId)
                .ToList();
            return customerFiles.Select(cf => new CustomerFileDataModel
            {
                Id = cf.Id,
                CustomerInfoId = (int)cf.CustomerInfoId,
                TypeOfDocument = cf.TypeOfDocument,
                Title = cf.Title,
                DocumentImg = cf.DocumentImg,
                DocumentDescription = cf.DocumentDescription
            }).ToList();
        }
        //public ICollection<CustomerFileDataModel> GetCustomerFilesByCustomerInfoId(int customerInfoId)
        //{

        //    IQueryable<CustomerFile> customerFiles = _contractContext.CustomerFiles
        //        .Where(c => c.CustomerInfoId == customerInfoId)
        //        .AsQueryable();

        //    var customerFileDataModels = customerFiles
        //        .OrderBy(c => c.Id)
        //        .Select(c => new CustomerFileDataModel
        //        {
        //            Id = c.Id,
        //            CustomerInfoId = c.CustomerInfoId,
        //            TypeOfDocument = c.TypeOfDocument,
        //            Title = c.Title,
        //            DocumentImg = c.DocumentImg,
        //            DocumentDescription = c.DocumentDescription,
        //        })
        //        .ToList();

        //    return customerFileDataModels;
        //}

        //public void CreateCustomerFile(CustomerFileCreateModel request)
        //{
        //    var customerFile = new CustomerFile
        //    {
        //        TypeOfDocument = request.TypeOfDocument,
        //        Title = request.Title,
        //        DocumentImg = request.DocumentImg,
        //        DocumentDescription = request.DocumentDescription,
        //    };

        //    // Save the new CustomerFile to the database
        //    _contractContext.CustomerFiles.Add(customerFile);
        //    _contractContext.SaveChanges();

        //}

        //public void UpdateCustomerFile(int id, CustomerFileUpdateModel request)
        //{
        //    var CustomerFile = _contractContext.CustomerFiles.Find(id);

        //    // Update the properties of the CustomerFile object
        //    CustomerFile.CarId = request.CarId;
        //    CustomerFile.DateStart = request.DateStart;
        //    CustomerFile.DateEnd = request.DateEnd;
        //    CustomerFile.CarStatusId = request.CarStatusId;

        //    // Save the changes to the database
        //    _contractContext.CustomerFiles.Update(CustomerFile);
        //    _contractContext.SaveChanges();


        //}

        public async Task<bool> DeleteCustomerFile(int id)
        {
            var customerFile = await _contractContext.CustomerFiles.FindAsync(id);

            if (customerFile == null)
            {
                return false; // Object not found
            }

            _contractContext.CustomerFiles.Remove(customerFile);
            await _contractContext.SaveChangesAsync();

            return true; // Object deleted successfully
        }
    }
}

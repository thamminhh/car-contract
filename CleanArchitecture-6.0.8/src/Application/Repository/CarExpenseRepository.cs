
using CleanArchitecture.Application.Constant;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.CarExpense.Sub_Model;
using CleanArchitecture.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace CleanArchitecture.Application.Repository
{
    public class CarExpenseRepository : ICarExpenseRepository
    {
        private readonly ContractContext _contractContext;

        public CarExpenseRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }


        public CarExpenseUpdateModel GetCarExpenseById(int carExpenseId)
        {
            CarExpense carExpense = _contractContext.CarExpenses
                .FirstOrDefault(c => c.Id == carExpenseId);

            return new CarExpenseUpdateModel
            {
                Id = carExpense.Id,
                CarId = carExpense.CarId,
                Title = carExpense.Title,
                Day = carExpense.Day,
                Amount = carExpense.Amount,
            };
        }

        public ICollection<CarExpenseUpdateModel> GetCarExpensesByCarId(int carId)
        {

            IQueryable<CarExpense> carExpenses = _contractContext.CarExpenses
                .Where(c => c.CarId == carId)
                .AsQueryable();

            var carExpenseDataModels = carExpenses
                .OrderBy(c => c.Id)
                .Select(c => new CarExpenseUpdateModel
                {
                    Id = c.Id,
                    CarId = c.CarId,
                    Title = c.Title,
                    Day = c.Day,
                    Amount = c.Amount,
                })
                .ToList();

            return carExpenseDataModels;
        }

        public void CreateCarExpense(CarExpenseCreateModel request)
        {
            var carExpense = new CarExpense
            {
                CarId = request.CarId,
                Title = request.Title,
                Day = request.Day,
                Amount = request.Amount,
            };

            // Save the new CarExpense to the database
            _contractContext.CarExpenses.Add(carExpense);
            _contractContext.SaveChanges();
        }

        public void UpdateCarExpense(int id, CarExpenseUpdateModel request)
        {
            var carExpense = _contractContext.CarExpenses.Find(id);

            // Update the properties of the CarExpense object
            carExpense.CarId = request.CarId;
            carExpense.Title = request.Title;
            carExpense.Day = request.Day;
            carExpense.Amount = request.Amount;

            // Save the changes to the database
            _contractContext.CarExpenses.Update(carExpense);
            _contractContext.SaveChanges();

        }

        public async Task<bool> DeleteCarExpense(int carExpenseId)
        {
            var carExpense = await _contractContext.CarExpenses.FindAsync(carExpenseId);

            if (carExpense == null)
            {
                return false; // Object not found
            }

            _contractContext.CarExpenses.Remove(carExpense);
            await _contractContext.SaveChangesAsync();

            return true; // Object deleted successfully
        }



    }
}

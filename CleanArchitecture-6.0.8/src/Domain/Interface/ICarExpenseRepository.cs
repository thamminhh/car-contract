using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarExpense.Sub_Model;

namespace CleanArchitecture.Domain.Interface
{
    public interface ICarExpenseRepository
    {


        ICollection<CarExpenseUpdateModel> GetCarExpensesByCarId(int carId);

        public CarExpenseUpdateModel GetCarExpenseById(int carExpenseId);

        void CreateCarExpense(CarExpenseCreateModel request);

        void UpdateCarExpense(int id, CarExpenseUpdateModel request);

        Task<bool> DeleteCarExpense(int carExpenseId);


    }
}

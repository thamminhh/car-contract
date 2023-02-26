
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarMakeRepository : ICarMakeRepository
    {
        private readonly ContractContext _contractContext;

        public CarMakeRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public CarMake GetCarMakeById(int id)
        {
            return _contractContext.CarMakes.Where(c => c.Id == id).FirstOrDefault();
        }

        public CarMake GetCarMakeByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<CarMake> GetCarMakes()
        {
            // Calculate the number of records to skip
          
            return _contractContext.CarMakes.OrderBy(c => c.Id).ToList();
        }

        public bool CarMakeExit(int id)
        {
            return _contractContext.CarMakes.Any(c => c.Id == id);
        }

        public bool CreateCarMake(CarMake carMake)
        {
            _contractContext.Add(carMake);
            return Save();  
        }

        public bool UpdateCarMake(CarMake carMake)
        {
            _contractContext.Update(carMake);
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public int GetCarMakeIdByName(string carMakeName)
        {
            CarMake carMake = _contractContext.CarMakes.Where(c => c.Name == carMakeName).FirstOrDefault();
            return carMake.Id;
        }
    }
}

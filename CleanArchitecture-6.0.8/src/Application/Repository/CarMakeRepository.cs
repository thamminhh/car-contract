
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.CarMake.Sub_Model;
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
            return _contractContext.CarMakes.OrderBy(c => c.Id).ToList();
        }

        public bool CarMakeExit(int id)
        {
            return _contractContext.CarMakes.Any(c => c.Id == id);
        }

        //public bool CreateCarMake(CarMakeCreateModel request, out string errorMessage)
        //{
        //    errorMessage = string.Empty;

        //    if (request.CarMakeName == null)
        //    {
        //        errorMessage = "Car make name don't allow null";
        //        return false;
        //    }
        //    if (_contractContext.CarMakes.Any(u => u.Name == request.CarMakeName))
        //    {
        //        errorMessage = "This car make already exists";
        //        return false;
        //    }
        //    var carMake = new CarMake();    
        //    carMake.Name= request.CarMakeName;
        //    carMake.CarMakeImg = request.CarMakeImage;
        //    _contractContext.CarMakes.Add(carMake);
        //    return Save();

        //}

        public bool UpdateCarMake(int id, CarMakeUpdateModel request, out string errorMessage)
        {
            var carMake = _contractContext.CarMakes.Find(id);
            errorMessage= string.Empty;
            if (request.CarMakeName == null)
            {
                errorMessage = "Car make name don't allow null";
                return false;
            }
            if (_contractContext.CarMakes.Any(u => u.Name == request.CarMakeName && request.CarMakeName != carMake.Name) )
            {
                errorMessage = "This car make already exists";
                return false;
            }
            carMake.Name = request.CarMakeName;
            carMake.CarMakeImg = request.CarMakeImage;

            _contractContext.CarMakes.Update(carMake);
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

using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class CarStatusRepository : ICarStatusRepository
    {
        private readonly ContractContext _contractContext;

        public CarStatusRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        int ICarStatusRepository.GetCarStatusIdByName(string name)
        {
            CarStatus carStatus = _contractContext.CarStatuses.Where(c => c.Name == name).FirstOrDefault();
            return carStatus.Id;
        }
        public CarStatus GetCarStatusById(int id)
        {
            return _contractContext.CarStatuses.Where(c => c.Id == id).FirstOrDefault();
        }

        public CarStatus GetCarStatusByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<CarStatus> GetCarStatuss()
        {
            return _contractContext.CarStatuses.OrderBy(c => c.Id).ToList();
        }

        public bool CarStatusExit(int id)
        {
            return _contractContext.CarStatuses.Any(c => c.Id == id);
        }

        public bool CreateCarStatus(CarStatus CarStatus)
        {
            _contractContext.Add(CarStatus);
            return Save();  
        }

        public bool UpdateCarStatus(CarStatus CarStatus)
        {
            _contractContext.Update(CarStatus);
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        
    }
}

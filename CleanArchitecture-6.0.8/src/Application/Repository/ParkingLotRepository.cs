using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interface;

namespace CleanArchitecture.Application.Repository
{
    public class ParkingLotRepository : IParkingLotRepository
    {
        private readonly ContractContext _contractContext;

        public ParkingLotRepository(ContractContext contractContext)
        {
            _contractContext = contractContext;
        }

        public ParkingLot GetParkingLotById(int id)
        {
            return _contractContext.ParkingLots.Where(c => c.Id == id).FirstOrDefault();
        }

        public ParkingLot GetParkingLotByName(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<ParkingLot> GetParkingLots()
        {
            return _contractContext.ParkingLots.OrderBy(c => c.Id).ToList();
        }

        public bool ParkingLotExit(int id)
        {
            return _contractContext.ParkingLots.Any(c => c.Id == id);
        }

        public bool CreateParkingLot(ParkingLot ParkingLot)
        {
            _contractContext.Add(ParkingLot);
            return Save();  
        }

        public bool UpdateParkingLot(ParkingLot ParkingLot)
        {
            _contractContext.Update(ParkingLot);
            return Save();
        }

        public bool Save()
        {
            var saved = _contractContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

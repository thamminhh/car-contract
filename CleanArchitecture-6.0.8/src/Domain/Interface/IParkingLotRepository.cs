using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interface
{
    public interface IParkingLotRepository
    {

        ParkingLot GetParkingLotById(int id);

        ICollection<ParkingLot> GetParkingLots();

        bool ParkingLotExit(int id);

        bool CreateParkingLot(ParkingLot ParkingLot);
        bool UpdateParkingLot(ParkingLot ParkingLot);
        bool Save();

    }
}

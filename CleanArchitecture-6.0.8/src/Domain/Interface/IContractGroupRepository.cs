﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.Car.SubModel;
using CleanArchitecture.Domain.Entities_SubModel.ContractGroup.SubModel;


namespace CleanArchitecture.Domain.Interface
{
    public interface IContractGroupRepository
    {

        ICollection<ContractGroupDataModel> GetContractGroups(int page, int pageSize, ContractFilter filter);

        ICollection<ContractGroupDataModel> GetContractGroupsByParkingLotId(int page, int pageSize,int parkingLotId, ContractFilter filter);

        ContractGroupDataModel GetContractGroupById(int contractGroupId);
        
        bool ContractGroupExit(int id);

        void CreateContractGroup(ContractGroupCreateModel request);

        void UpdateContractGroup(int id, ContractGroupUpdateModel request);

        bool UpdateContractGroupStatus(int id, ContractGroupUpdateStatusModel request); 

        bool DeleteContractGroup(int id);

        bool UpdateContractCarId(int id, int? carId);

        int GetNumberOfContracts(ContractFilter filter);
        int GetContractGroupsByParkingLotId(int parkingLotId, ContractFilter filter);

        ICollection<ContractGroupCustomerHistoryModel> GetContractGroupsCustomerHistory(string citizenIdentificationInfoNumber, out int count);

    }
}

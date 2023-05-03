﻿using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities_SubModel.ReceiveContract;

namespace CleanArchitecture.Domain.Interface
{
    public interface IReceiveContractRepository
    {

        ReceiveContractDataModel GetReceiveContractById(int id);

        ReceiveContractDataModel GetReceiveContractByContractGroupId(int contractGroupId);

        void CreateReceiveContract(ReceiveContractCreateModel request);

        void UpdateReceiveContract(int id, ReceiveContractUpdateModel request);
        bool UpdateReceiveContractStatus(int id, ReceiveContractUpdateStatusModel request);

        bool ReceiveContractExit(int id);  

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities_SubModel.TransferContractFile.SubModel;

namespace CleanArchitecture.Domain.Entities_SubModel.TransferContract;
public class TransferContractDataModel
{
    public int Id { get; set; }
    public int? TransfererId { get; set; }
    public string? TransfererName { get; set; }
    public string? TransfererPhoneNumber { get; set; }

    public int? ContractGroupId { get; set; }

    public string? CustomerName { get; set; }
    public string? CustomerPhoneNumber { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerCitizenIdentificationInfoNumber { get; set; }
    public string? CustomerCitizenIdentificationInfoAddress { get; set; }
    public DateTime? CustomerCitizenIdentificationInfoDateReceive { get; set; }

    public string? ModelName { get; set; }
    public string? CarLicensePlates { get; set; }
    public int? SeatNumber { get; set; }

    public DateTime? DateTransfer { get; set; }
    public string? DeliveryAddress { get; set; }
    public int? CurrentCarStateSpeedometerNumber { get; set; }
    public int? CurrentCarStateFuelPercent { get; set; }
    public double? CurrentCarStateCurrentEtcAmount { get; set; }
    public string? CurrentCarStateCarStatusDescription { get; set; }
    public double? DepositItemDownPayment { get; set; }
    public bool? IsExported { get; set; }
    public string? CustomerSignature { get; set; }
    public string? StaffSignature { get; set; }
    public string? FilePath { get; set; }
    public string? FileWithSignsPath { get; set; }
    public int? ContractStatusId { get; set; }

    public ICollection<TransferContractFileDataModel> TransferContractFileDataModels { get; set; }
}

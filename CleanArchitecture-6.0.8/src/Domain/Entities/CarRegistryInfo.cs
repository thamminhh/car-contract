﻿using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarRegistryInfo
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public DateTime? LastRegistryDate { get; set; }
        public DateTime? RegistryDate { get; set; }
        public double? RegistryAmount { get; set; }
        public string? RegistryInvoice { get; set; }

        public virtual Car? Car { get; set; }
    }
}
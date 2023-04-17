using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarRegistryInfo
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public DateTime? RegistrationDeadline { get; set; }
        public double? RegistryAmount { get; set; }
        public string? RegistryInvoice { get; set; }
        public string? RegistryAddress { get; set; }
        public string? CertificateRegistryDocument { get; set; }

        public virtual Car? Car { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarFile
    {
        public int Id { get; set; }
        public int? CarId { get; set; }
        public string? FilePath { get; set; }
        public string? FrontImg { get; set; }
        public string? BackImg { get; set; }
        public string? LeftImg { get; set; }
        public string? RightImg { get; set; }
        public string? OrtherImg { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Car? Car { get; set; }
    }
}

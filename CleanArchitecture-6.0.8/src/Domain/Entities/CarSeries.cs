﻿using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public partial class CarSeries
    {
        public int? Id { get; set; }
        public int? CarModelId { get; set; }
        public int? CarGenerationId { get; set; }
        public string? Name { get; set; }
    }
}

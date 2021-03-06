﻿using Core.Entities;
using System;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int RentalNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string RentDate { get; set; }
        public string ReturnDate { get; set; }
    }
}

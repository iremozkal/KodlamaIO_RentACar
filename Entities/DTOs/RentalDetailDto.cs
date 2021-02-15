using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int RentalNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}

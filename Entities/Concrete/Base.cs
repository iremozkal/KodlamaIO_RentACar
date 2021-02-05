using CarRent.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.Entities.Concrete
{
    public class Base : IEntity
    {
        public int Id { get; set; }
    }
}

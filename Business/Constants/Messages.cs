using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AddSuccess = "(+) Insert operation is succesfully done.";
        public static string AddError = "(-) Insert operation is failed.";
        public static string UpdateSuccess = "(+) Update operation is succesfully done.";
        public static string UpdateError = "(-) Update operation is failed.";
        public static string DeleteSuccess = "(+) Delete operation is succesfully done.";
        public static string DeleteError = "(-) Delete operation is failed.";
        public static string CarNotExist = "(-) There is no such a car.";
        public static string BrandNotExist = "(-) There is no such a brand.";
        public static string ColorNotExist = "(-) There is no such a color.";
        public static string CarIsReturn = "(+) The car has been delivered.";
        public static string CarNotReturn = "(-) The car has already been rented, not been delivered yet.";
    }
}

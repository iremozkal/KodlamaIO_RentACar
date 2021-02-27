using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Constants
{
    public static class Paths
    {
        public static string ImagesFolderPath = Directory.GetCurrentDirectory() + @"\Images";

        public static string DefaultImagePath = Directory.GetCurrentDirectory() + @"\Images\default.png";
    }
}

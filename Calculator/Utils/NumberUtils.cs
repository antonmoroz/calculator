using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Utils
{
    class NumberUtils
    {
        public static float ConvertToFloat(string numberAsString)
        {
            return float.Parse(numberAsString);
        }
    }
}
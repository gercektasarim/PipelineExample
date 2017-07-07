using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parantez.Core.Common
{
    public class ConvertHelper
    {
        public static int? ConvertToInt(string value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HNGHRMS.Infrastructure.Extensions
{
    public static class DateTimeCompareExtensions
    {
        public static Boolean CompareDateByMonthAndYear(this DateTime DateSource,DateTime DateCompare)
        {
            Boolean result = false;
            if (DateSource <= DateCompare)
            {
                result = true;
            }                
            else
            {
                if (DateSource.Month == DateCompare.Month && DateSource.Year == DateCompare.Year)
                    return true;
            }

            return result;
        }
    }
}

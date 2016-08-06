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
            if (DateSource.Date <= DateCompare.Date)
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

        public static Boolean CompareDateByMonthAndYear(this DateTime? DateSource, DateTime DateCompare)
        {
            Boolean result = false;
            if (DateSource.HasValue)
            {
                if (DateSource.Value.Date <= DateCompare.Date)
                {
                    result = true;
                }
                else
                {
                    if (DateSource.Value.Month == DateCompare.Month && DateSource.Value.Year == DateCompare.Year)
                        return true;
                }
            }            
            return result;
        }
        public static Boolean CompareBetweenDateByMonthAndYear(this DateTime DateSource, DateTime DateStartCompare,DateTime DateEndComapare)
        {
            Boolean result = false;
            if (DateSource.Date >= DateStartCompare.Date && DateSource.Date <= DateEndComapare.Date)
            {
                result = true;
            }
            return result;
        }
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
namespace HNGHRMS.Infrastructure.Extensions
{
    public static class StringExtension
    {

        public static DateTime ConvertToDate(this String str)
        {
             DateTime result;
             CultureInfo vi = new CultureInfo("vi-VN");
             DateTime.TryParseExact(str, "dd/MM/yyyy", vi, DateTimeStyles.None, out result);
             return result;
        }

        public static Double ConvertToDouble(this String str)
        {
            Double result;
            Double.TryParse(str, out result);
            return result;
        }

        public static string UniqueNumberCode(this string str,int numberOfDigits = 9)
        {
            
            return str + String.Format("{0:d" + numberOfDigits +"}", (DateTime.Now.Ticks / 10) % 1000000000); 
        }

    }
}
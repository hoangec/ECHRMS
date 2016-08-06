using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNGHRMS.Model.Models;
using HNGHRMS.Data.Repository;
using HNGHRMS.Data.Infrastructure;
using HNGHRMS.Data.Models;
using HNGHRMS.Service;
using HNGHRMS.Service;
using Autofac;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HNGHRMS.Model.Enums;
using HNGHRMS.Infrastructure.Extensions;
namespace TestConsoleApp
{
    public enum TestEnum
    {
        Male = 1,
        Female = 2
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            DateTime startDate = new DateTime(2015, 11, 10);
            DateTime endDate = DateTime.Now;
            var diff = (startDate - endDate).Days;
            Console.WriteLine(diff);
            Console.ReadLine();
        }
    }
}

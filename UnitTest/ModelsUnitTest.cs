using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HNGHRMS.Model.Models;
using HNGHRMS.Infrastructure;
using System.Collections.Generic;
namespace UnitTest
{
    [TestClass]
    public class ModelsUnitTest
    {
        [TestMethod]
        public void TwoObjectNotEquals()
        {
            Employee emp1 = new Employee();
            Employee emp2 = new Employee();
            Assert.IsTrue(emp1 != emp2);
        }
        [TestMethod]
        public void TwoObjectSameIdNotEquals()
        {
            Employee emp1 = new Employee();
            emp1.Id = 1;
            Employee emp2 = new Employee();
            emp2.Id = 1;
            Assert.IsFalse(emp1 == emp2);
        }
        [TestMethod]
        public void ObjectContainsListObject()
        {
            Employee emp1 = new Employee();
            emp1.Id = 1;
            emp1.LastName = "Hoang";
            Employee em2 = new Employee();
            em2.Id = 2;
            em2.LastName = "Hung";
            List<Employee> listEmp = new List<Employee>();
            listEmp.Add(emp1);
            listEmp.Add(em2);
            bool test = listEmp.Contains(emp1);
            Assert.IsTrue(listEmp.Contains(emp1));
        }
        [TestMethod]
        public void ObjectNotContainsListObject()
        {
            Employee emp1 = new Employee();
            emp1.Id = 1;
            emp1.LastName = "Hoang";
            Employee em2 = new Employee();
            em2.Id = 2;
            em2.LastName = "Hung";
            List<Employee> listEmp = new List<Employee>();
            listEmp.Add(emp1); 
            Assert.IsFalse(listEmp.Contains(em2));
        }

    }
}

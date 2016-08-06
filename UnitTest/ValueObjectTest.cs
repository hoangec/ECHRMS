using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HNGHRMS.Model.Models;
namespace UnitTest
{
    [TestClass]
    public class ValueObjectTest
    {
        [TestMethod]
        public void TwoValueObjectIsSame()
        {
            Identity pass1 = new Identity() { IdentityNo = "230623213", DateOfIssue = new DateTime(2015, 12, 22) };
            Identity pass2 = new Identity() { IdentityNo = "230623213", DateOfIssue = new DateTime(2015, 12, 22) };
            Assert.IsTrue(pass1 == pass2);
        }
        [TestMethod]
        public void TwoValueObjectIsDiff()
        {
            Identity pass1 = new Identity() { IdentityNo = "230623213", DateOfIssue = new DateTime(2015, 12, 22) };
            Identity pass2 = new Identity() { IdentityNo = "230623214", DateOfIssue = new DateTime(2015, 12, 22) };
            Assert.IsFalse(pass1 == pass2);
        }
    }
}

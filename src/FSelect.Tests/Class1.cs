using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FSelect;

namespace FSelect.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestMethodName()
        {
            var input = "StackPanel";

            var result = FSelector.Parse(input);

            Assert.AreEqual("StackPanel", result);
        }
    }
}

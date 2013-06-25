using System.Linq;
using System.Collections.Generic;
using FSelect.AST;
using NUnit.Framework;

namespace FSelect.Tests
{
    [TestFixture]
    public class ElementSelectorTests
    {
        [Test]
        public void ElementSelector_exposes_an_element_name()
        {
            var element = new ElementSelector("StackPanel");

            Assert.AreEqual("StackPanel", element.ElementName);
        }

        [Test]
        public void Selector_ToString()
        {
            var result = new ElementSelector("StackPanel");

            Assert.AreEqual("ElementSelector( StackPanel )", result.ToString());
        }
    }
}
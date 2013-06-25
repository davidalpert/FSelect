using System.Linq;
using System.Collections.Generic;
using FSelect.AST;
using NUnit.Framework;

namespace FSelect.Tests
{
    [TestFixture]
    public class ClassSelectorTests
    {
        [Test]
        public void ClassSelector_is_a_wildcard_selector_with_a_class_filter()
        {
            var classSelector = new ClassSelector("awesomeHotSauce");

            Assert.IsInstanceOf<WildcardSelector>(classSelector);
            Assert.AreEqual("*", classSelector.Identifier);
            Assert.AreEqual("awesomeHotSauce", classSelector.ClassName);
            Assert.IsNull(classSelector.ContextSelector);
        }
    }
}
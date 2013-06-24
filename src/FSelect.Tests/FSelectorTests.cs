using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSelect.AST;
using NUnit.Framework;

namespace FSelect.Tests
{
    [TestFixture]
    public class FSelectorTests
    {
        [Test]
        public void Parse_returns_a_SelectorSequence_of_Selectors()
        {
            var input = "StackPanel";

            var result = FSelector.Parse(input);

            Assert.IsInstanceOf<SelectorSequence>(result);
            Assert.AreEqual(1, result.Selectors.Count);
        }

        [Test]
        public void Parse_star_returns_a_WildcardSelector()
        {
            var input = "*";

            var result = FSelector.Parse(input).Selectors[0];

            Assert.IsInstanceOf<WildcardSelector>(result);
            Assert.AreEqual("*", result.Identifier);
        }

        [Test]
        public void Parse_element_returns_an_ElementSelector()
        {
            var input = "StackPanel";

            var result = FSelector.Parse(input).Selectors[0];

            Assert.IsInstanceOf<ElementSelector>(result);
            Assert.AreEqual("StackPanel", result.Identifier);
        }

        [Test]
        public void Parse_class_returns_a_ClassSelector()
        {
            var input = ".needsOfTheMany";

            var result = FSelector.Parse(input).Selectors[0];

            Assert.IsInstanceOf<ClassSelector>(result);
            Assert.AreEqual("needsOfTheMany", result.Identifier);
        }

        [Test]
        public void Parse_id_returs_IdentitySelector()
        {
            var input = "#needsOfTheOne";

            var result = FSelector.Parse(input).Selectors[0];

            Assert.IsInstanceOf<IdentitySelector>(result);
            Assert.AreEqual("needsOfTheOne", result.Identifier);
        }
    }

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

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
        public void Parse_multiple_selectors_returns_a_SelectorSequence_of_multiple_Selectors()
        {
            var input = 
@"StackPanel,
DockPanel";

            var result = FSelector.Parse(input);

            Assert.IsInstanceOf<SelectorSequence>(result);
            Assert.AreEqual(2, result.Selectors.Count);
            Assert.AreEqual("StackPanel", result.Selectors.First().Identifier);
            Assert.AreEqual("DockPanel", result.Selectors.Skip(1).First().Identifier);
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
            Assert.IsNull(result.ContextSelector);
        }

        [Test]
        public void Parse_class_returns_a_ClassSelector()
        {
            var input = ".needsOfTheMany";

            var result = FSelector.Parse(input).Selectors[0];

            Assert.IsInstanceOf<ClassSelector>(result);
            Assert.AreEqual("needsOfTheMany", ((ClassSelector)result).ClassName);
            Assert.IsNull(result.ContextSelector);
        }

        [Test]
        public void Parse_id_returs_IdentitySelector()
        {
            var input = "#needsOfTheOne";

            var result = FSelector.Parse(input).Selectors[0];

            Assert.IsInstanceOf<IdentitySelector>(result);
            Assert.AreEqual("needsOfTheOne", ((IdentitySelector)result).Key);
            Assert.IsNull(result.ContextSelector);
        }

        [Test]
        public void Parse_can_handle_descendent_selector()
        {
            var input = "StackPanel Grid";

            var result = FSelector.Parse(input).Selectors.ElementAt(0);
            
            Assert.IsInstanceOf<CompoundSelector>(result);
            Assert.AreEqual("Grid", result.Identifier);
            Assert.IsNotNull(result.ContextSelector);

            Assert.IsInstanceOf<ElementSelector>(result.ContextSelector);
            Assert.AreEqual("StackPanel", result.ContextSelector.Identifier);
        }

        [Test]
        public void Parse_can_handle_descendent_selector_with_class_context()
        {
            var input = ".manyOfThese Grid";

            var result = FSelector.Parse(input).Selectors.ElementAt(0);
            
            Assert.IsInstanceOf<CompoundSelector>(result);
            Assert.AreEqual("Grid", result.Identifier);
            Assert.IsNotNull(result.ContextSelector);

            Assert.IsInstanceOf<ClassSelector>(result.ContextSelector);
            Assert.AreEqual("manyOfThese", ((ClassSelector) result.ContextSelector).ClassName);
        }

        [Test]
        public void Parse_of_descendent_selector_can_reconstruct_the_input()
        {
            var input = ".manyOfThese Grid";

            var result = FSelector.Parse(input).Selectors.ElementAt(0);
            
            Assert.IsInstanceOf<CompoundSelector>(result);
            Assert.AreEqual(".manyOfThese Grid", result.Text);
            Assert.IsNotNull(result.ContextSelector);

            Assert.AreEqual(".manyOfThese", result.ContextSelector.Text);
        }

        [Test]
        public void Parse_child_selector_returns_ChildSelector()
        {
            var input = "StackPanel > Grid";

            var result = FSelector.Parse(input).Selectors.ElementAt(0);
            
            Assert.IsInstanceOf<ImmediateChildSelector>(result);
            Assert.AreEqual("Grid", result.Identifier);
            Assert.IsNotNull(result.ContextSelector);

            Assert.IsInstanceOf<ElementSelector>(result.ContextSelector);
            Assert.AreEqual("StackPanel", result.ContextSelector.Identifier);
        }

        [Test]
        public void Parse_sibling_selector_returns_AdjacentSiblingSelector()
        {
            var input = "StackPanel + Grid";

            var result = FSelector.Parse(input).Selectors.ElementAt(0);
            
            Assert.AreEqual(input, result.Text);

            Assert.IsInstanceOf<AdjacentSiblingSelector>(result);
            Assert.AreEqual("Grid", result.Identifier);
            Assert.IsNotNull(result.ContextSelector);

            Assert.IsInstanceOf<ElementSelector>(result.ContextSelector);
            Assert.AreEqual("StackPanel", result.ContextSelector.Identifier);
        }

        [Test]
        public void Parse_tracks_compound_selector_depth()
        {
            var input = "StackPanel #navGrid .snazzy Border Button";

            var result = FSelector.Parse(input).Selectors.ElementAt(0);
            
            Assert.AreEqual(input, result.Text);
            Assert.AreEqual(5, result.CalculateDepth());
        }
    }
}

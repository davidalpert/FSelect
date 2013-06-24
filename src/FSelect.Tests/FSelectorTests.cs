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
    }
}

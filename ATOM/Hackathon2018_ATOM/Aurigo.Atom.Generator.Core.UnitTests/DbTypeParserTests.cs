using Aurigo.Atom.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aurigo.Atom.Generator.Core.UnitTests
{
    [TestClass]
    public class DbTypeParseTests
    {
        [TestMethod]
        public void ParseNumber()
        {
            var result = DbTypeParser.GetColumnLength("varchar(10)");
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void ParseString()
        {
            var result = DbTypeParser.GetColumnLength("varchar(max)");
            Assert.AreEqual(4000, result);
        }
    }
}
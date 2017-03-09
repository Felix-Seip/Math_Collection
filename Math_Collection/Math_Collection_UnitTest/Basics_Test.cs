using Math_Collection.Basics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Math_Collection_UnitTest
{
    [TestClass]
    public class Basics_Test
    {
        [TestMethod]
        public void FibonacciTest()
        {
            Assert.AreEqual(8, Basics.FibonacciSequence(6));
        }
    }
}

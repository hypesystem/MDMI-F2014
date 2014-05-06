using System;
using ESOMDataWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESOMDataWrapperTest
{
    [TestClass]
    public class UMatrixTest
    {
        [TestMethod]
        public void TestReadUMatrixFromFile()
        {
            var um = UMatrix.FromFile("testFile.umx");
        }
    }
}

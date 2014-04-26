using System;
using ESOMDataWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESOMDataWrapperTest
{		
		
		
    [TestClass]
    public class ESOMTests
    {
        [TestMethod]
        public void TestReadESOMFromFile()
        {
            var esom = ESOM.Fromfile("testFile.wts");
            var data = new double[1,3,3];
            data[0, 0, 0] = -0.701;
            data[0, 0, 1] = -0.487;
            data[0, 0, 2] = 0.793;
            data[0, 1, 0] = -0.435;
            data[0, 1, 1] = -0.439;
            data[0, 1, 2] = 1.01;
            data[0, 2, 0] = -0.136;
            data[0, 2, 1] = -0.314;
            data[0, 2, 2] = 1.198;
            var expected = new ESOM(data);
            for (var i = 0; i < 1; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    for (var n = 0; n < 3; n++)
                    {
                        Assert.AreEqual(expected.Data[i,j,n],
                            esom.Data[i,j,n]);
                    }
                }
            }
        }
    }
}

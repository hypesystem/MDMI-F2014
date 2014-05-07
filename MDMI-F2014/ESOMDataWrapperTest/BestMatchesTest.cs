using System;
using ESOMDataWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESOMDataWrapperTest
{
    [TestClass]
    public class BestMatchesTest
    {
        [TestMethod]
        public void TestReadBestMatchesFromFile()
        {
            var bm = PlaylistPathFinder.FromFile("testFile.bm");
        }
    }
}

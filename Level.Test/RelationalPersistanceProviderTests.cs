using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level.Test
{
    [TestClass]
    public class RelationalPersitanceProviderTests
    {
        [TestMethod]
        [TestCategory("RelationalPersistanceProvider")]
        public void Initialize_WhenSupportedTypesFound_MapsType()
        {
        }

        [TestMethod]
        [TestCategory("RelationalPersistanceProvider")]
        public void Initialize_WhenNoSupportedTypesFound_DoesNotMapType()
        {

        }
    }
}

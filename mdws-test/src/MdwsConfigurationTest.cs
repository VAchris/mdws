using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using gov.va.medora.mdws.conf;

namespace gov.va.medora.mdws.test
{
    [TestFixture]
    public class MdwsConfigurationTest
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {

        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }


        [Test]
        public void testFacadeNameConstructor()
        {
            MdwsConfiguration config = new MdwsConfiguration("EmrSvc");
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.FacadeConfiguration);
            Assert.IsNotNull(config.AllConfigs);
            Assert.IsFalse(String.IsNullOrEmpty(config.ResourcesPath));
        }
    }
}

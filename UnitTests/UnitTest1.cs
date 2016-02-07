using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoFacInterceptors;
using AutoFacInterceptors.Cache;
using AutoFacInterceptors.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CacheSet_Pass()
        {
            ICache cache = new MemoryCache();
            cache.Set("Item", "Value");
            var item = cache.Get<String>("Item");

            Assert.AreEqual(item, "Value");
        }

        [TestMethod]
        public void CacheSet_EmptyKey_Fail()
        {
            ICache cache = new MemoryCache();

            try
            {
                cache.Set("", "Value");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void CacheSet_NullKey_Fail()
        {
            ICache cache = new MemoryCache();

            try
            {
                cache.Set(null, "Value");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void CacheSet_NullValue_Fail()
        {
            ICache cache = new MemoryCache();

            try
            {
                cache.Set("Key", null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentNullException));
            }
        }
    }
}

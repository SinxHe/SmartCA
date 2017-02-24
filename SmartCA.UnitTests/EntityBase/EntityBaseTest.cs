using SmartCA.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCA.Model.Companies;
using System.Collections.Generic;
using SmartCA.Infrastructure.RepositoryFramework;
using SmartCA.Infrastructure;
using System;
using SmartCA.UnitTests.Mocks;

namespace SmartCA.UnitTests
{
    /// <summary>
    ///This is a test class for EntityBase and is intended
    ///to contain all EntityBase Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityBaseTest
    {
        private IUnitOfWork unitOfWork;
        private ICompanyRepository repository;

        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        [TestMethod]
        public void EntitiesAreEqualIfDifferentEntityAndKeyObjectsButSameKeyValue()
        {

            MockEntity entity1 = new MockEntity();
            Guid id2 = new Guid(entity1.Key.ToString());
            MockEntity entity2 = new MockEntity(id2);
            Assert.AreEqual(entity1.Key, entity2.Key);
            Assert.AreEqual(entity1, entity2);
            Assert.IsTrue(entity1.Equals(entity2));
            Assert.IsTrue(entity1 == entity2);
            Assert.IsFalse(entity1 != entity2);
        }
    }
}
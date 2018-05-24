using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Repoitories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GOOS_Sample.Entities;
using NSubstitute;

namespace GOOS_Sample.Repoitories.Tests
{
    [TestClass()]
    public class RepositoryTests
    {
        [TestMethod()]
        public void test_insert_entity()
        {
            var target = new InMemoryRepository<BudgetEntity, string>();
            var entity = new BudgetEntity
            {
                YearMonth = "2017-01",
                Amount = 800
            };
            target.Save(entity);
            var expected = target.Get(entity.YearMonth);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Amount, entity.Amount);
        }
    }
}
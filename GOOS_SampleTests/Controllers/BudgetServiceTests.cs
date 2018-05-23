using System;
using System.Collections.Generic;
using GOOS_Sample.Entities;
using GOOS_Sample.Models;
using GOOS_Sample.Repoitories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.Routing.Handlers;
using OpenQA.Selenium.Remote;

namespace GOOS_SampleTests.Controllers
{
    [TestClass]
    public class BudgetServiceTests
    {
        private IRepository<BudgetEntity, string> _repository;
        private BudgetService _target;

        [TestInitialize]
        public void Setup()
        {
            _repository = Substitute.For<IRepository<BudgetEntity, string>>();
            _target = new BudgetService(_repository);
            ArrangeMockBudgets();
        }

        [TestMethod]
        public void test_add_budget()
        {
            var model = new BudgetViewModel
            {
                YearMonth = "2015-11",
                Amount = 1000
            };

            _target.Add(model);

            _repository.Received().Save(Arg.Is<BudgetEntity>(e => e.YearMonth == model.YearMonth && e.Amount == model.Amount));
        }

        [TestMethod]
        public void test_update_budget()
        {
            
            var model = new BudgetViewModel()
            {
                YearMonth = "2018-08",
                Amount = 500
            };

            _target.Update(model);

            _repository.Received().Get(Arg.Is(model.YearMonth));
            _repository.Received().Save(Arg.Is<BudgetEntity>(e => e.YearMonth == model.YearMonth && e.Amount == model.Amount));

        }

        [TestMethod]
        public void test_Get_Average_Budget_between_20180415_and_20180515_should_return_620()
        {
            decimal expected = 620;
            
            var actual = _target.GetAverageBudget(DateTime.Parse("2018-04-15"), DateTime.Parse("2018-05-15"));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void test_Get_Average_Budget_between_20180415_and_20180630_should_return_940()
        {
            decimal expected = 940;

            decimal actual = _target.GetAverageBudget(DateTime.Parse("2018-04-15"), DateTime.Parse("2018-06-30"));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void test_Get_Average_Budget_between_20180520_and_20180716_should_return_560()
        {
            decimal expected = 560;
            
            decimal actual = _target.GetAverageBudget(DateTime.Parse("2018-05-20"), DateTime.Parse("2018-07-16"));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void test_Get_Average_Budget_between_20160115_and_20160213_should_return_251()
        {
            decimal expected = 251;
            
            decimal actual = _target.GetAverageBudget(DateTime.Parse("2016-01-15"), DateTime.Parse("2016-02-13"));

            Assert.AreEqual(expected, actual);
        }

        private void ArrangeMockBudgets()
        {
            _repository.Get("2018-08").Returns(new BudgetEntity
            {
                YearMonth = "2018-08",
                Amount = 400
            });

            _repository.GetList("2018-04", "2018-06").Returns(new List<BudgetEntity>()
            {
                new BudgetEntity {YearMonth = "2018-04", Amount = 600},
                new BudgetEntity {YearMonth = "2018-05", Amount = 620}
            });

            _repository.GetList("2018-04", "2018-05").Returns(new List<BudgetEntity>()
            {
                new BudgetEntity{ YearMonth = "2018-04", Amount = 600},
                new BudgetEntity{ YearMonth = "2018-05", Amount = 620}
            });

            _repository.GetList("2018-05", "2018-07").Returns(new List<BudgetEntity>()
            {
                new BudgetEntity{ YearMonth = "2018-05", Amount = 620},
                new BudgetEntity{ YearMonth = "2018-07", Amount = 620}
            });

            _repository.GetList("2016-01", "2016-02").Returns(new List<BudgetEntity>()
            {
                new BudgetEntity{ YearMonth = "2016-02", Amount = 560}
            });
        }
    }
}

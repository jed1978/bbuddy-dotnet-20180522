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
            _repository.Get("2018-08").Returns(new BudgetEntity
            {
                YearMonth = "2018-08",
                Amount = 400
            });
            
            var model = new BudgetViewModel()
            {
                YearMonth = "2018-08",
                Amount = 500
            };

            _target.Update(model);

            _repository.Received().Get(Arg.Is(model.YearMonth));
            _repository.Received().Save(Arg.Is<BudgetEntity>(e => e.YearMonth == model.YearMonth && e.Amount == model.Amount));

        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOOS_Sample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOS_Sample.Models;
using NSubstitute;

namespace GOOS_Sample.Controllers.Tests
{
    [TestClass()]
    public class BudgetsControllerTests
    {
        [TestMethod()]
        public void Add_a_budget_should_succeedTest()
        {
            //Arrange
            var budgetService = Substitute.For<IBudgetService>();
            var budgetsController = new BudgetsController(budgetService);

            //Act
            budgetsController.Add("2018-11", 700);

            //Assert
            budgetService.Received()
                .Add(Arg.Is<BudgetViewModel>(budget => budget.YearMonth == "2018-11" && budget.Amount == 700));
        }
    }
}
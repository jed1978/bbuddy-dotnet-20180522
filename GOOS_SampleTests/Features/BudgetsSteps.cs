using System;
using FluentAutomation;
using GOOS_Sample.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GOOS_SampleTests.Features
{
    [Binding]
    public class BudgetsSteps : FluentTest
    {
        public BudgetsSteps()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Chrome);
        }

        [Given(@"open the budget adding page")]
        public void GivenOpenTheBudgetAddingPage()
        {
            I.Open("http://localhost:58527/budgets/add");
        }

        [When(@"I add budget (.*) for (.*)")]
        public void WhenIAddBudgetFor(int amount, string yearMonth)
        {
            I.Enter(amount.ToString()).In("#amount")
                .Enter(yearMonth).In("#yearMonth")
                .Click("input[type='submit']");
        }

        [Then(@"the result should be displayed")]
        public void ThenTheResultShouldBeDisplayed(Table table)
        {
            I.Assert.Text(table.CreateInstance<BudgetViewModel>().YearMonth).In(".yearMonth");
            I.Assert.Text(table.CreateInstance<BudgetViewModel>().Amount.ToString()).In(".amount");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GOOS_Sample.Models;

namespace GOOS_Sample.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _budgetService;
        private static List<BudgetViewModel> _budgetViewModels;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
            _budgetViewModels = new List<BudgetViewModel>();
        }

        // GET: Budgets
        [Route("add")]
        public ActionResult Add()
        {
            return View(_budgetViewModels);
        }

        [HttpPost]
        public ActionResult Add(string yearMonth, int amount)
        {
            var budget = new BudgetViewModel
            {
                YearMonth = yearMonth,
                Amount = amount
            };
            _budgetViewModels.Add(budget);
            _budgetService.Add(budget);
            return View(_budgetViewModels);
        }

        [HttpPost]
        public ActionResult Add2(string yearMonth, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
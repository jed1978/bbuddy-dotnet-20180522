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
        private static List<BudgetViewModel> _budgetViewModels;

        public BudgetsController()
        {
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
            _budgetViewModels.Add(new BudgetViewModel
            {
                YearMonth = yearMonth,
                Amount = amount
            });
            return View(_budgetViewModels);
        }
    }
}
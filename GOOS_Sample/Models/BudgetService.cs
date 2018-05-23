using System;
using System.Collections.Generic;
using System.Linq;
using GOOS_Sample.Entities;
using GOOS_Sample.Repoitories;

namespace GOOS_Sample.Models
{
    public class BudgetService : IBudgetService
    {
        private readonly IRepository<BudgetEntity, string> _repository;

        public BudgetService(IRepository<BudgetEntity, string> repository)
        {
            _repository = repository;
        }

        public void Add(BudgetViewModel budget)
        {
            _repository.Save(new BudgetEntity()
            {
                YearMonth = budget.YearMonth,
                Amount = budget.Amount
            });
        }

        public void Update(BudgetViewModel budget)
        {
            var entity = _repository.Get(budget.YearMonth);
            entity.Amount = budget.Amount;
            _repository.Save(entity);
        }

        public decimal GetAverageBudget(DateTime startDate, DateTime endDate)
        {
            var startYearMonth = startDate.ToString("yyyy-MM");
            var endYearMonth = endDate.ToString("yyyy-MM");
            var list = _repository.GetList(startYearMonth, endYearMonth);
            var averageBudgets = new Dictionary<string, decimal>();

            foreach (var entity in list)
            {
                averageBudgets.Add(entity.YearMonth, entity.Amount / DateTime.DaysInMonth(
                                                         int.Parse(
                                                             entity.YearMonth
                                                                 .Substring(0, 4)),
                                                         int.Parse(
                                                             entity.YearMonth
                                                                 .Substring(5, 2))));

            }

            var firstMonthDays = DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1;
            var lastMonthDays = endDate.Day;

            if ((endDate.Month - startDate.Month) > 1)
            {
                //todo:
            }

            var sum = averageBudgets[startYearMonth] * firstMonthDays + averageBudgets.Last().Value * lastMonthDays;

            return sum;
        }
    }
}
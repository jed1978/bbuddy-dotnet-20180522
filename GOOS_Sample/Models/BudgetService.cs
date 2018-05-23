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
            var averageBudgets = CalculateAverageBudgets(list);

            var days = DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + 1;
            var firstMonthBudgets = CalculateBudgets(startYearMonth, averageBudgets, days); 
            var lastMonthBudgets = CalculateBudgets(endYearMonth, averageBudgets, endDate.Day); 
            var fullMonthBudgets = CalculateBudgets(startDate, endDate, list, endYearMonth);

            var sum = firstMonthBudgets + lastMonthBudgets + fullMonthBudgets;

            return Decimal.Round(sum);
        }

        private decimal CalculateBudgets(string yearMonth, Dictionary<string, decimal> averageBudgets, int days)
        {
            var averageBudget = 0m;
            averageBudgets.TryGetValue(yearMonth, out averageBudget);
            return averageBudget * days;
        }

        private decimal CalculateBudgets(DateTime startDate, DateTime endDate, List<BudgetEntity> list, string endYearMonth)
        {
            decimal monthBudgets = 0;

            if (endDate.Month - startDate.Month > 1)
            {
                monthBudgets = list.Skip(1).Take(endDate.Month - startDate.Month - 1)
                    .Where(e => e.YearMonth != endYearMonth)
                    .Sum(e => e.Amount);
            }

            return monthBudgets;
        }

        private Dictionary<string, decimal> CalculateAverageBudgets(List<BudgetEntity> list)
        {
            var averageBudgets = new Dictionary<string, decimal>();

            foreach (var entity in list)
            {
                averageBudgets.Add(entity.YearMonth, Convert.ToDecimal(entity.Amount) / DateTime.DaysInMonth(
                                                         int.Parse(
                                                             entity.YearMonth
                                                                 .Substring(0, 4)),
                                                         int.Parse(
                                                             entity.YearMonth
                                                                 .Substring(5, 2))));
            }

            return averageBudgets;
        }
    }
}
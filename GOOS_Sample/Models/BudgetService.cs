using System;
using System.Collections.Generic;
using System.Linq;
using GOOS_Sample.Entities;
using GOOS_Sample.Repoitories;

namespace GOOS_Sample.Models
{
    public class DateRange
    {
        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            StartYearMonth = startDate.ToString("yyyy-MM");
            EndYearMonth = endDate.ToString("yyyy-MM");
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string StartYearMonth { get; private set; }
        public string EndYearMonth { get; private set; }
    }

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
                Id = budget.YearMonth,
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
            var dateRange = MakeDateRange(startDate, endDate);
            var budgets = _repository.GetList(dateRange.StartYearMonth, dateRange.EndYearMonth);
            var budgetAverage = CalculateAverageBudgets(budgets);         
            var budgetSummary = SumBudgets(dateRange, budgetAverage, budgets);
            return budgetSummary;
        }

        private DateRange MakeDateRange(DateTime startDate, DateTime endDate)
        {
            var dateRange = new DateRange(startDate, endDate);
            return dateRange;
        }

        private decimal SumBudgets(DateRange dateRange, Dictionary<string, decimal> averageBudgets, List<BudgetEntity> list)
        {
            var days = DateTime.DaysInMonth(dateRange.StartDate.Year, dateRange.StartDate.Month) - dateRange.StartDate.Day + 1;
            var firstMonthBudgets = CalculateBudgets(dateRange.StartYearMonth, averageBudgets, days);
            var lastMonthBudgets = CalculateBudgets(dateRange.EndYearMonth, averageBudgets, dateRange.EndDate.Day);
            var otherMonthBudgets = CalculateBudgets(dateRange.StartDate, dateRange.EndDate, list, dateRange.EndYearMonth);

            var sum = firstMonthBudgets + lastMonthBudgets + otherMonthBudgets;
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
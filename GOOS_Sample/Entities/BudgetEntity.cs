namespace GOOS_Sample.Entities
{
    public class BudgetEntity : BaseEntity<string>
    {
        public string YearMonth
        {
            get { return Id; }
            set { Id = value; }
        }

        public int Amount { get; set; }
    }

}
namespace RepairServiceCenterASP.ViewModels.Filters
{
    public class EmployeesFilter
    {
        public string InputFullName { get; private set; }
        public int? InputExp { get; private set; }

        public EmployeesFilter(string fullName, int? experiance)
        {
            InputFullName = fullName;
            InputExp = experiance;
        }
    }
}

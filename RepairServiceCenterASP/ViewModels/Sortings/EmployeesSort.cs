using RepairServiceCenterASP.Models;

namespace RepairServiceCenterASP.ViewModels.Sortings
{
    public class EmployeesSort
    {
        public Employee.SortState FullNameSort { get; private set; }
        public Employee.SortState ExperienceSort { get; private set; }
        public Employee.SortState PostSort { get; private set; }
        public Employee.SortState Current { get; private set; }

        public EmployeesSort(Employee.SortState sortOrder)
        {
            FullNameSort = sortOrder == Employee.SortState.FullNameAsc ? Employee.SortState.FullNameDesc
                                                                       : Employee.SortState.FullNameAsc;

            ExperienceSort = sortOrder == Employee.SortState.ExperienceAsc ? Employee.SortState.ExperienceDesc
                                                                           : Employee.SortState.ExperienceAsc;

            PostSort = sortOrder == Employee.SortState.PostAsc ? Employee.SortState.PostDesc
                                                               : Employee.SortState.PostAsc;

            Current = sortOrder;
        }
    }
}

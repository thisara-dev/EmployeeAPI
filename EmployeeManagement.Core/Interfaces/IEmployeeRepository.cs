using PMS.Core.Models.DTO;
using UnitOfWorkDemo.Core.Models;

namespace UnitOfWorkDemo.Core.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeById(int EmployeeId);
        IQueryable<Employee> GetEmployeeAsQuarable();
        IQueryable<Employee> GetEmployeeByEmployeeName(string EmployeeName);
        public IQueryable<GetEmployeeStatisticsDto> GetEmployeeStats();
    }
}

using PMS.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;

namespace UnitOfWorkDemo.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployee(EmployeeDTO EmployeeDetails);

        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeById(int EmployeeId);

        Task<bool> UpdateEmployee(Employee EmployeeDetails);

        Task<bool> DeleteEmployee(int EmployeeId);

        IQueryable<Employee> GetEmployeeRecordsAsQuarable();

        IQueryable<GetEmployeeStatisticsDto> GetEmployeeStats();
    }
}

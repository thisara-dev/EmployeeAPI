using Microsoft.EntityFrameworkCore;
using PMS.Core.Models;
using PMS.Core.Models.DTO;
using UnitOfWorkDemo.Core.Interfaces;
using UnitOfWorkDemo.Core.Models;

namespace UnitOfWorkDemo.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContextClass dbContext) : base(dbContext)
        {}

        public IQueryable<Employee> GetEmployeeByEmployeeName(string EmployeeName)
       => _dbContext.Employees.Where(x => (x.FirstName + x.LastName).Contains(EmployeeName));

        public IQueryable<Employee> GetEmployeeById(int EmployeeId)
            => _dbContext.Employees.Where(u => u.Id == EmployeeId);
        public IQueryable<Employee> GetEmployeeAsQuarable()
            => _dbContext.Employees;


        IQueryable<Employee> IEmployeeRepository.GetEmployeeById(int EmployeeId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GetEmployeeStatisticsDto> GetEmployeeStats()
        {

            var result = _dbContext.GetEmployeeStatisticsDto.FromSqlRaw("exec GetEmployeeStatistics");
            return result;


        }

    }
}

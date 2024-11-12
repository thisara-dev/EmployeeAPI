using PMS.Core.Models;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;

namespace UnitOfWorkDemo.Core.Interfaces
{
    public interface IEmployeeRecordRepository : IGenericRepository<EmployeeRecord>
    {
        IQueryable<EmployeeRecord> GetEmployeeRecordsById(string EmployeeId);
        IQueryable<EmployeeRecord> GetEmployeeRecordsAsQuarable();
        IQueryable<Reason> GetEmployeeMedicalRecordReasonList();
        IQueryable<EmployeeRecord> GetEmployeeRecordsByEmployeeName(string EmployeeName);
        Task<List<EmployeeRecord>>GetRecordByEmployeeId(int EmployeeId);
        Task<Reason> GetEmployeeMedicalReasonRecord(int EmployeeId);
    }
}

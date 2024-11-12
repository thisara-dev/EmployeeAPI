using PMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;

namespace UnitOfWorkDemo.Services.Interfaces
{
    public interface IEmployeeRecordService
    {
        Task<bool> CreateEmployeeRecord(EmployeeRecord EmployeeRecordDetails);

        Task<IEnumerable<EmployeeRecord>> GetAllEmployeeRecords();

        Task<EmployeeRecord> GetEmployeeRecordById(int EmployeeRecordId);

        Task<bool> UpdateEmployeeRecord(EmployeeRecord pEmployeeRecordDetails);

        Task<bool> DeleteEmployeeRecord(int EmployeeRecordId);
        Task<List<EmployeeRecord>> GetRecordByEmployeeId(int EmployeeRecordId);
        Task<Reason> GetEmployeeMedicalReasonRecord(int EmployeeRecordId);

        IQueryable<EmployeeRecord> GetEmployeeRecordsAsQuarable();
        IQueryable<Reason> GetEmployeeMedicalRecordReasonList();
    }
}

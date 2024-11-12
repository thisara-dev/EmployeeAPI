using Microsoft.EntityFrameworkCore;
using PMS.Core.Models;
using System.Collections.Generic;
using UnitOfWorkDemo.Core.Interfaces;
using UnitOfWorkDemo.Core.Models;

namespace UnitOfWorkDemo.Infrastructure.Repositories
{
    public class EmployeeRecordRepository : GenericRepository<EmployeeRecord>, IEmployeeRecordRepository
    {
        public EmployeeRecordRepository(DbContextClass dbContext) : base(dbContext) { }

        public IQueryable<EmployeeRecord> GetEmployeeRecordsByEmployeeName(string EmployeeName)
            => _dbContext.EmployeeRecord.Where(x => (x.EmployeeProfile.FirstName + x.EmployeeProfile.LastName).Contains(EmployeeName));

        public async Task<List<EmployeeRecord>> GetRecordByEmployeeId(int EmployeeId)
            => await _dbContext.EmployeeRecord.Where(u => u.EmployeeProfileID == EmployeeId).ToListAsync();
        public IQueryable<EmployeeRecord> GetEmployeeRecordsAsQuarable()
            => _dbContext.EmployeeRecord
            .Include(x=> x.EmployeeProfile);

        public IQueryable<EmployeeRecord> GetEmployeeRecordsById(string EmployeeId) 
            => _dbContext.EmployeeRecord.Where(x => x.EmployeeProfileID.ToString().Contains(EmployeeId));

        IQueryable<Reason> IEmployeeRecordRepository.GetEmployeeMedicalRecordReasonList()
        {
            return _dbContext.Reasons;
        }

        async Task<Reason> IEmployeeRecordRepository.GetEmployeeMedicalReasonRecord(int reasonId)
        {
            return await _dbContext.Reasons.Where(u => u.ReasonID == reasonId).FirstOrDefaultAsync();
        }
    }
}

using PMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Interfaces;
using UnitOfWorkDemo.Core.Models;
using UnitOfWorkDemo.Services.Interfaces;

namespace UnitOfWorkDemo.Services
{
    public class EmployeeRecordService : IEmployeeRecordService
    {
        public IUnitOfWork _unitOfWork;

        public EmployeeRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateEmployeeRecord(EmployeeRecord EmployeeRecordDetails)
        {
            if (EmployeeRecordDetails != null)
            {
                await _unitOfWork.EmployeeRecord.Add(EmployeeRecordDetails);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteEmployeeRecord(int EmployeeRecordId)
        {
            if (EmployeeRecordId > 0)
            {
                var EmployeeDetails = await _unitOfWork.EmployeeRecord.GetById(EmployeeRecordId);
                if (EmployeeDetails != null)
                {
                    _unitOfWork.EmployeeRecord.Delete(EmployeeDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<EmployeeRecord>> GetAllEmployeeRecords()
        {
            var EmployeeRecordList = await _unitOfWork.EmployeeRecord.GetAll();
            return EmployeeRecordList;
        }

        public async Task<EmployeeRecord> GetEmployeeRecordById(int EmployeeRecordId)
        {
            if (EmployeeRecordId > 0)
            {
                var EmployeeRecordDetails = await _unitOfWork.EmployeeRecord.GetById(EmployeeRecordId);
                if (EmployeeRecordDetails != null)
                {
                    return EmployeeRecordDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateEmployeeRecord(EmployeeRecord EmployeeRecordDetails)
        {
            if (EmployeeRecordDetails != null)
            {
                var EmployeeRecord = await _unitOfWork.EmployeeRecord.GetById(EmployeeRecordDetails.EmployeeMedicalRecordID);
                if(EmployeeRecord != null)
                {
                    var type = typeof(EmployeeRecord);
                    var properties = type.GetProperties();

                    foreach (var property in properties)
                    {
                        var originalValue = property.GetValue(EmployeeRecord);
                        var newValue = property.GetValue(EmployeeRecordDetails);

                        if ((originalValue == null && newValue != null) ||
                            (originalValue != null && !originalValue.Equals(newValue)))
                        {
                            // Update the property if it has changed
                            property.SetValue(EmployeeRecord, newValue);
                        }
                    }

                    // Mark the entity as modified
                    _unitOfWork.EmployeeRecord.Update(EmployeeRecord);

                    var result = _unitOfWork.Save();// Assuming SaveAsync is asynchronous

                    return result > 0;
                }
            }
            return false;
        }

        public Task<List<EmployeeRecord>> GetRecordByEmployeeId(int EmployeeRecordId)
        {
            if (EmployeeRecordId > 0)
            {
                var EmployeeRecordDetails = _unitOfWork.EmployeeRecord.GetRecordByEmployeeId(EmployeeRecordId);
                if (EmployeeRecordDetails != null)
                {
                    return EmployeeRecordDetails;
                }
            }
            return null;
        }
        public Task<Reason> GetEmployeeMedicalReasonRecord(int reasonId)
        {
            if (reasonId > 0)
            {
                var EmployeeRecordDetails = _unitOfWork.EmployeeRecord.GetEmployeeMedicalReasonRecord(reasonId);
                if (EmployeeRecordDetails != null)
                {
                    return EmployeeRecordDetails;
                }
            }
            return null;
        }

        public IQueryable<EmployeeRecord> GetEmployeeRecordsByEmployeeName(string EmployeeName)
            => _unitOfWork.EmployeeRecord.GetEmployeeRecordsByEmployeeName(EmployeeName);

        public IQueryable<EmployeeRecord> GetEmployeeRecordsAsQuarable()
            => _unitOfWork.EmployeeRecord.GetEmployeeRecordsAsQuarable();
        public IQueryable<Reason> GetEmployeeMedicalRecordReasonList()
            => _unitOfWork.EmployeeRecord.GetEmployeeMedicalRecordReasonList();

        public IQueryable<EmployeeRecord> GetEmployeeRecordsById(string EmployeeId)
            => _unitOfWork.EmployeeRecord.GetEmployeeRecordsById(EmployeeId);
    }
}

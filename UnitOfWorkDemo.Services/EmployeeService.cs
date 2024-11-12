using AutoMapper;
using PMS.Core.Models.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Interfaces;
using UnitOfWorkDemo.Core.Models;
using UnitOfWorkDemo.Services.Interfaces;

namespace UnitOfWorkDemo.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> CreateEmployee(EmployeeDTO EmployeeDetails)
        {
            if (EmployeeDetails != null)
            {
                try
                {
                    var newEmployee = new Employee()
                    {
                        Address = EmployeeDetails.Address,
                        LastName = EmployeeDetails.LastName,
                        Allergic = EmployeeDetails.Allergic,
                        BloodGroup = EmployeeDetails.BloodGroup,
                        ContactNumber = EmployeeDetails.ContactNumber,
                        DateOfBirth = EmployeeDetails.DateOfBirth,
                        EmergencyContactNo = EmployeeDetails.EmergencyContactNo,
                        FirstName = EmployeeDetails.FirstName,
                        Gender = EmployeeDetails.Gender,
                      //  NIC = EmployeeDetails.NIC, removed for now
                        MedicalHistory = EmployeeDetails.MedicalHistory,
                        insuranceInfomation = EmployeeDetails.insuranceInfomation,
                        isActive = EmployeeDetails.isActive,
                        RegisteredDate = System.DateTime.Now

                    };

                    await _unitOfWork.Employee.Add(newEmployee);
                    int result = _unitOfWork.Save();

                    if (result > 0)
                    {
                        // Mapping the newly created Employee to a DTO and returning it
                        return (newEmployee);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions here
                    Console.WriteLine(ex.Message);
                    return new Employee();
                }
            }

            return new Employee();
        }
            public async Task<bool> DeleteEmployee(int EmployeeId)
        {
            if (EmployeeId > 0)
            {
                try
                {
                    Employee EmployeeDetails = await _unitOfWork.Employee.GetById(EmployeeId);
                    if (EmployeeDetails != null)
                    {
                        _unitOfWork.Employee.Delete(EmployeeDetails);
                        var result = _unitOfWork.Save();

                        if (result > 0)
                            return true;
                        else
                            return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            return false;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var EmployeeList = await _unitOfWork.Employee.GetAll();
            return EmployeeList;
        }

        public async Task<Employee> GetEmployeeById(int EmployeeId)
        {
            if (EmployeeId > 0)
            {
                var EmployeeDetails = await _unitOfWork.Employee.GetById(EmployeeId);
                if (EmployeeDetails != null)
                {
                    return EmployeeDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateEmployee(Employee EmployeeDetails)
        {
            if (EmployeeDetails != null)
            {
                var Employee = await _unitOfWork.Employee.GetById(EmployeeDetails.Id);
                if(Employee != null)
                {
                    Employee.FirstName = EmployeeDetails.FirstName;
                    Employee.LastName = EmployeeDetails.LastName;
                    Employee.DateOfBirth = EmployeeDetails.DateOfBirth;
                    Employee.Gender = EmployeeDetails.Gender;
                    Employee.ContactNumber = EmployeeDetails.ContactNumber;
                    Employee.Address = EmployeeDetails.Address;
                    Employee.EmergencyContactNo = EmployeeDetails.EmergencyContactNo;
                    Employee.BloodGroup = EmployeeDetails.BloodGroup;
                    Employee.MedicalHistory = EmployeeDetails.MedicalHistory;
                    Employee.Allergic = EmployeeDetails.Allergic;
                   // Employee.NIC = EmployeeDetails.NIC;  removed  for now 
                    Employee.insuranceInfomation = EmployeeDetails.insuranceInfomation;
                    Employee.isActive = EmployeeDetails.isActive;
                    _unitOfWork.Employee.Update(Employee);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public IQueryable<GetEmployeeStatisticsDto> GetEmployeeStats()
        {
            return _unitOfWork.Employee.GetEmployeeStats();
        }

        public IQueryable<Employee> GetEmployeeRecordsByEmployeeName(string EmployeeName)
         => _unitOfWork.Employee.GetEmployeeByEmployeeName(EmployeeName);

        public IQueryable<Employee> GetEmployeeRecordsAsQuarable()
            => _unitOfWork.Employee.GetEmployeeAsQuarable();

        public IQueryable<Employee> GetEmployeeRecordsById(int EmployeeId)
            => _unitOfWork.Employee.GetEmployeeById(EmployeeId);
    }
}

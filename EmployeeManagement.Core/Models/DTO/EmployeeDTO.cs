﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [AllowNull]
        public string? FirstName { get; set; }

        [AllowNull]
        public string? LastName { get; set; }

        [AllowNull]
        public DateTime? DateOfBirth { get; set; }

        [AllowNull]
        public string? Gender { get; set; }

        [AllowNull]
        public string? ContactNumber { get; set; }

        [AllowNull]
        public string? Address { get; set; }

        [AllowNull]
        public string? EmergencyContactNo { get; set; }

        [AllowNull]
        public string? BloodGroup { get; set; }

        [AllowNull]
        public string? MedicalHistory { get; set; }

        [AllowNull]
        public string? Allergic { get; set; }

        [AllowNull]
        public string? insuranceInfomation { get; set; }

        [AllowNull]
        public string? NIC { get; set; }

        [AllowNull]
        public bool? isActive { get; set; }

        [AllowNull]
        public DateTime? RegisteredDate { get; set; }

        public ICollection<EmployeeRecord>? EmployeeMedicalRecordDetails { get; set; }
    }
}

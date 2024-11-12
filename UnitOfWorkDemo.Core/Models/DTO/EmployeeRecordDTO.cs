using PMS.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Models.DTO
{
    public class EmployeeRecordDTO
    {

        public int EmployeeProfileID { get; set; }
        [AllowNull]
        public string? WardNumber { get; set; }
        [AllowNull]
        public string? Background { get; set; }
        [AllowNull]
        public string? Investigations { get; set; }
        [AllowNull]
        public string? Treatments { get; set; }
        [AllowNull]
        public string? DailyStatus { get; set; }
        [AllowNull]
        public string? Plan { get; set; }
        [AllowNull]
        public string? SpecialRemarks { get; set; }
        [AllowNull]
        public DateTime? AdmittedDate { get; set; }
        [AllowNull]
        public string? Fiepath { get; set; }
        [AllowNull]
        public string? FieName { get; set; }
        [AllowNull]
        public string? Diagnosis { get; set; }
        [AllowNull]
        public DateTime? CreatedDate { get; set; }


    }
}

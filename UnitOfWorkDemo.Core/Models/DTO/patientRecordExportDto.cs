using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkDemo.Core.Models;

namespace PMS.Core.Models.DTO
{
    public class EmployeeRecordExportDto
    {
           
        public virtual Employee? EmployeeProfile { get; set; }


        public string? BHTNumber { get; set; }

        public string? WardNumber { get; set; }
        public string? Background { get; set; }
        public string? Investigations { get; set; }
        public string? Treatments { get; set; }
        public string? DailyStatus { get; set; }
        public string? Plan { get; set; }
        public string? SpecialRemarks { get; set; }
        public DateTime? AdmittedDate { get; set; }
        public string? Fiepath { get; set; }
        public string? FieName { get; set; }
        public string? Diagnosis { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

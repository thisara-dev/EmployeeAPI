using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Models.DTO
{
    public class GetEmployeeStatisticsDto
    {
        public int TotalEmployees { get; set; }
        public int NewEmployeesToday { get; set; }
        public int NewEmployeesThisWeek { get; set; }
        public int ActiveEmployees { get; set; }
    }
}

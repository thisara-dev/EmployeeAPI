using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Models.DTO
{
    public class RecordSearchParamsDto

    {

      public string? searchstring { get; set; }
      public string? reason { get; set; }
      public string? EmployeeType { get; set; }
    }
}

using AutoMapper;
using PMS.Core.Models;
using PMS.Core.Models.DTO;
using UnitOfWorkDemo.Core.Models;

namespace PMS.Endpoints.Mappings
{
    public class AutoMapperProfiles  :Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();

            CreateMap<EmployeeRecord, EmployeeRecordDTO>().ReverseMap();          
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<EmployeeRecord, EmployeeRecordExportDto>().ReverseMap();

        }

    }
}

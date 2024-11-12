using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PMS.Core.Models;
using PMS.Core.Models.DTO;
using PMS.Core.Models.Enum;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Helpers;
using UnitOfWorkDemo.Services;
using UnitOfWorkDemo.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PMS.Endpoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRecordController : ControllerBase
    {
        public readonly IEmployeeRecordService _EmployeeRecordService;
        public IMapper Mapper { get; }

        public EmployeeRecordController(IEmployeeRecordService EmployeeRecordService , IMapper mapper)
        {
            this._EmployeeRecordService = EmployeeRecordService;
            Mapper = mapper;
        }

        [HttpGet("GetEmployeeRecordList")]
        public async Task<IActionResult> GetEmployeeRecordList()
        {
            var results = await _EmployeeRecordService.GetEmployeeRecordsAsQuarable().ToListAsync();
            if (results == null)
            {
                return NotFound();
            }
            // Use JsonSerializerOptions with ReferenceHandler.Preserve
            var jsonOptions = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            // Serialize the results to JSON
            var json = JsonConvert.SerializeObject(results, jsonOptions);

            return Ok(JsonConvert.DeserializeObject<List<EmployeeRecord>>(json));
        }

        [HttpGet("GetEmployeeRecordsBySearchString/{searchstring}")]
        public async Task<IActionResult> GetEmployeeRecordsBySearchString(string searchstring)
        {
            string[] searchstrings = searchstring.Split(',','/','|');

            var EmployeeRecords = _EmployeeRecordService.GetEmployeeRecordsAsQuarable();
             
            if (searchstrings.Count()>= 1 && !string.IsNullOrWhiteSpace(searchstrings[0]))
            {
                var name = searchstrings[0];
                EmployeeRecords = EmployeeRecords.Where(x => (x.EmployeeProfile.FirstName + x.EmployeeProfile.LastName).Contains(name));
            }
             
            if (searchstrings.Count() >= 2 && !string.IsNullOrWhiteSpace(searchstrings[1]))
            {
                var userId = searchstrings[1];
                EmployeeRecords = EmployeeRecords.Where(x => x.EmployeeProfileID.ToString().Contains(userId));
            }
             
            if (searchstrings.Count() >= 3 && !string.IsNullOrWhiteSpace(searchstrings[2]))
            {
                EmployeeRecords = EmployeeRecords.Where(x => x.EmployeeProfile.NIC.Contains(searchstrings[2], StringComparison.OrdinalIgnoreCase));
            }

            var results = EmployeeRecords.ToList();
            if (results == null)
            {
                return NotFound();
            }
            // Use JsonSerializerOptions with ReferenceHandler.Preserve
            var jsonOptions = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            // Serialize the results to JSON
            var json = JsonConvert.SerializeObject(results, jsonOptions);

            return Ok(JsonConvert.DeserializeObject<List<EmployeeRecord>>(json));
        }


        [HttpGet("GetEmployeeRecordsByReason/{reason}")]
        public async Task<IActionResult> GetEmployeeRecordsByReason(string reason)
        {
            var results = await _EmployeeRecordService.GetEmployeeRecordsAsQuarable().Where(x=> x.Reason.ReasonDescription.ToLower().Contains(reason.ToLower())).ToListAsync();

            if (results != null)
            {
                // Use JsonSerializerOptions with ReferenceHandler.Preserve
                var jsonOptions = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                // Serialize the results to JSON
                var json = JsonConvert.SerializeObject(results, jsonOptions);

                return Ok(JsonConvert.DeserializeObject<List<EmployeeRecord>>(json));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetEmployeeRecordsByDate/")]
        public async Task<IActionResult> GetEmployeeRecordsByDate(DateTime fromdate, DateTime? todate)
        {
            var results = await _EmployeeRecordService.GetEmployeeRecordsAsQuarable().Where(x => x.CreatedDate>= fromdate && x.CreatedDate<=todate).ToListAsync();

            if (results != null)
            {
                // Use JsonSerializerOptions with ReferenceHandler.Preserve
                var jsonOptions = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                // Serialize the results to JSON
                var json = JsonConvert.SerializeObject(results, jsonOptions);

                return Ok(JsonConvert.DeserializeObject<List<EmployeeRecord>>(json));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetEmployeeRecordById/{EmployeeRecordId}")]
        public async Task<IActionResult> GetEmployeeRecordById(int EmployeeId)
        {
            var EmployeeDetails = await _EmployeeRecordService.GetEmployeeRecordById(EmployeeId);

            if (EmployeeDetails != null)
            {
                return Ok(EmployeeDetails);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost("AddEmployeeRecord")]
        public async Task<IActionResult> AddEmployeeRecord(EmployeeRecord EmployeeDetails)
        {
            var isEmployeeCreated = await _EmployeeRecordService.CreateEmployeeRecord(EmployeeDetails);

            if (isEmployeeCreated)
            {
                return Ok(isEmployeeCreated);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("UpdateEmployeeRecord")]
        public async Task<IActionResult> UpdateEmployeeRecord(EmployeeRecord EmployeeDetails)
        {
            if (EmployeeDetails != null)
            {
                var isEmployeeUpdated = await _EmployeeRecordService.UpdateEmployeeRecord(EmployeeDetails);
                if (isEmployeeUpdated)
                {
                    return Ok(isEmployeeUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("DeleteEmployeeRecord/{EmployeeRecordId}")]
        public async Task<IActionResult> DeleteEmployeeRecord(int EmployeeRecordId)
        {
            var isEmployeeRecordCreated = await _EmployeeRecordService.DeleteEmployeeRecord(EmployeeRecordId);

            if (isEmployeeRecordCreated)
            {
                return Ok(isEmployeeRecordCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetRecordByEmployeeId/{EmployeeRecordId}")]
        public async Task<IActionResult> GetRecordByEmployeeId(int EmployeeRecordId)
        {
            var EmployeeDetails = await _EmployeeRecordService.GetRecordByEmployeeId(EmployeeRecordId);

            if (EmployeeDetails != null)
            {
                return Ok(EmployeeDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetEmployeeMedicalRecordReason/{reasonId}")]
        public async Task<IActionResult> GetEmployeeMedicalRecordReason(int reasonId)
        {
            var EmployeeRecordReasons = await _EmployeeRecordService.GetEmployeeMedicalReasonRecord(reasonId);

            if (EmployeeRecordReasons != null)
            {
                return Ok(EmployeeRecordReasons);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetEmployeeMedicalRecordReasonList")]
        public async Task<IActionResult> GetEmployeeMedicalRecordReasonList()
        {
            var EmployeeRecordReasons = await _EmployeeRecordService.GetEmployeeMedicalRecordReasonList().ToListAsync();
            if (EmployeeRecordReasons == null)
            {
                return NotFound();
            }
            // Use JsonSerializerOptions with ReferenceHandler.Preserve
            var jsonOptions = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            // Serialize the results to JSON
            var json = JsonConvert.SerializeObject(EmployeeRecordReasons, jsonOptions);

            return Ok(JsonConvert.DeserializeObject<List<Reason>>(json));
        }

        [HttpPost("GetEmployeeRecordsAsCSV/")]
        public async Task<IActionResult> GetEmployeeRecordsAsCSV(RecordSearchParamsDto? recordSearchParams)
        {
            var EmployeeRecords = _EmployeeRecordService.GetEmployeeRecordsAsQuarable();

            string[] searchstrings = recordSearchParams.searchstring != null ? recordSearchParams.searchstring.Split(',', '/', '|') : new string[] { };

            // var EmployeeRecords = _EmployeeRecordService.GetEmployeeRecordsAsQuarable().Where(x => x.EmployeeTypeID == EmployeeCategory);

            if (searchstrings.Count() >= 1 && !string.IsNullOrWhiteSpace(searchstrings[0]))
            {
                var name = searchstrings[0];
                EmployeeRecords = EmployeeRecords.Where(x => (x.EmployeeProfile.FirstName + x.EmployeeProfile.LastName).Contains(name));
            }

            if (searchstrings.Count() >= 2 && !string.IsNullOrWhiteSpace(searchstrings[1]))
            {
                var userId = searchstrings[1];
                EmployeeRecords = EmployeeRecords.Where(x => x.EmployeeProfileID.ToString().Contains(userId));
            }

            if (searchstrings.Count() >= 3 && !string.IsNullOrWhiteSpace(searchstrings[2]))
            {
                EmployeeRecords = EmployeeRecords.Where(x => x.EmployeeProfile.NIC.Contains(searchstrings[2], StringComparison.OrdinalIgnoreCase));
            }



            var exportData = Mapper.Map<List<EmployeeRecordExportDto>>(EmployeeRecords.OrderByDescending(x => x.CreatedDate));


            if (exportData == null)
            {
                return NotFound();
            }


            //this is for testing the data

            //var EmployeeRecord = new EmployeeMedicalRecordDetails() { EmployeeMedicalRecordID = 1, EmployeeProfileID = 2, BHTNumber = "12" };
            //List<EmployeeMedicalRecordDetails> EmployeeMedicalRecordDetails = new List<EmployeeMedicalRecordDetails>();
            //EmployeeMedicalRecordDetails.Add(EmployeeRecord);

            byte[] bin;
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (TextWriter textWriter = new StreamWriter(memoryStream))
                    using (var csvWriter = new CsvWriter(textWriter, CultureInfo.InvariantCulture))
                    {
                        //var ExportModel = _mapper.Map <List<EmployeeRecordExportDTO>>(results);
                        csvWriter.WriteRecords(exportData);


                        //Uncomment below and 136-138 lines, commment above for testings
                        //csvWriter.WriteRecords(EmployeeMedicalRecordDetails);
                    }

                    bin = memoryStream.ToArray();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }

            return File(bin, "application/csv", $"Employee-Records{DateTime.UtcNow}.csv");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PMS.Core.Models.DTO;
using UnitOfWorkDemo.Core.Models;
using UnitOfWorkDemo.Services.Interfaces;

namespace UnitOfWorkDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IEmployeeService _EmployeeService;
        public IMapper Mapper { get; }

        public EmployeeController(IEmployeeService EmployeeService,IMapper mapper)
        {
            _EmployeeService = EmployeeService;
            Mapper = mapper;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEmployeeList")]
        public async Task<IActionResult> GetEmployeeList()
        {
            var productDetailsList = await _EmployeeService.GetAllEmployees();
            if(productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [HttpGet("GetEmployeeById/{EmployeeId}")]
        public async Task<IActionResult> GetEmployeeById(int EmployeeId)
        {
            var EmployeeDetails = await _EmployeeService.GetEmployeeById(EmployeeId);

            if (EmployeeDetails != null)
            {
                return Ok(EmployeeDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="EmployeeDetails"></param>
        /// <returns></returns>
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDTO EmployeeDetails)
        {
            var isEmployeeCreated = await _EmployeeService.CreateEmployee(EmployeeDetails);

            if (isEmployeeCreated!=null)
            {
                return Ok(isEmployeeCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="EmployeeDetails"></param>
        /// <returns></returns>
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO EmployeeDetails)
        {
            if (EmployeeDetails != null)
            {
                var mappedEmployee = Mapper.Map<Employee>(EmployeeDetails);
                var isEmployeeUpdated = await _EmployeeService.UpdateEmployee(mappedEmployee);
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

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteEmployee/{EmployeeId}")]
        public async Task<IActionResult> DeleteProduct(int EmployeeId)
        {
            var isProductCreated = await _EmployeeService.DeleteEmployee(EmployeeId);

            if (isProductCreated)
            {
                return Ok(isProductCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetEmployeeStats")]
        public IActionResult GetEmployeeStats()
        {
            try
            {
                var EmployeeStats = _EmployeeService.GetEmployeeStats();

                if (EmployeeStats != null)
                {
                    return Ok(EmployeeStats);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetEmployeeBySearchString/{searchstring}/{searchId}")]
        public async Task<IActionResult> GetEmployeeBySearchString(string searchstring, int searchId)
        {
            string[] searchstrings = searchstring.Split(',', '/', '|');

            var EmployeeRecords = _EmployeeService.GetEmployeeRecordsAsQuarable();

            switch (searchId)
            {
                case 0:
                    EmployeeRecords = EmployeeRecords.Where(x => (x.FirstName + x.LastName).Contains(searchstring));
                    break;
                case 1:
                    EmployeeRecords = EmployeeRecords.Where(x => x.ContactNumber.Contains(searchstring));
                    break;
                case 2:
                    EmployeeRecords = EmployeeRecords.Where(x => x.NIC.Contains(searchstring));
                    break;

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

            return Ok(JsonConvert.DeserializeObject<List<Employee>>(json));
        }
    }
}

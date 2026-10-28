using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Dto;
using MyFirstApi.GenericResponse;
using MyFirstApi.IService;

namespace MyFirstApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await employeeService.GetAllEmployeeAsync();

            if (!result.Item2.Any())
            {
                return Ok(ResponseResult<List<EmployeeDto>>.Failure(null, "No Employees Found"));
            }

            return Ok(ResponseResult<List<EmployeeDto>>.Success(result.Item2, "Employees Found"));
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var result = await employeeService.CreateEmployee(employeeDto);

            if (result.Item1 == 0)
            {
                return Ok(ResponseResult<string>.Failure(null, result.Item2));
            }

            return Ok(ResponseResult<string>.Success(null, result.Item2));
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var result = await employeeService.UpdateEmployee(employeeDto);

            if (result.Item1 == 0)
            {
                return Ok(ResponseResult<string>.Failure(null, result.Item2));
            }

            return Ok(ResponseResult<string>.Success(null, result.Item2));
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await employeeService.DeleteEmployee(id);

            if (result.Item1 == 0)
            {
                return Ok(ResponseResult<string>.Failure(null, result.Item2));
            }

            return Ok(ResponseResult<string>.Success(null, result.Item2));
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            var result = await employeeService.GetEmployeeById(id);

            if (result.Item1 == 0)
            {
                return Ok(ResponseResult<string>.Failure(null, "Employee Not Found"));
            }

            return Ok(ResponseResult<EmployeeDto>.Success(result.Item2, "Employee Found"));
        }
    }
}
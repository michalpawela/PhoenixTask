using Common.Dtos;
using BoardGame_REST_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame_REST_API.Controllers
{
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] EmployeeDto employeeDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updated = await _employeeService.UpdateAsync(id, employeeDto);

                if (updated == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                var deleted = await _employeeService.DeleteAsync(id);

                if (deleted == null)
                {
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateAsync([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var created = await _employeeService.CreateAsync(employeeDto);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllAsync()
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetAsync([FromRoute] int id)
        {
            var employee = await _employeeService.GetByIDAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}

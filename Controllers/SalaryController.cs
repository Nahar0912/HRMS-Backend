using HRMS.Backend.DTOs;
using HRMS.Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var salaries = await _salaryService.GetAllAsync();
                return Ok(salaries);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving salary records", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var salary = await _salaryService.GetByIdAsync(id);
                if (salary == null)
                    return NotFound(new { message = "Salary record not found" });

                return Ok(salary);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving salary record", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] SalaryCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdSalary = await _salaryService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdSalary.Id },createdSalary);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] SalaryUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedSalary = await _salaryService.UpdateAsync(id, dto);
                if (updatedSalary == null)
                    return NotFound(new { message = "Salary record not found" });

                return Ok(updatedSalary);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _salaryService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Salary record not found" });

                return Ok(new { message = "Salary record deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting salary record", error = ex.Message });
            }
        }
    }
}
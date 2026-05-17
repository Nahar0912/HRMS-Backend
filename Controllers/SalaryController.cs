using AutoMapper;
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
        private readonly IMapper _mapper;

        public SalaryController(ISalaryService salaryService, IMapper mapper)
        {
            _salaryService = salaryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var salaries = await _salaryService.GetAllAsync();
            return Ok(salaries);
        }

        [HttpGet("{id}")]
        [Authorize]    
        public async Task<IActionResult> GetById(int id)
        {
            var salary = await _salaryService.GetByIdAsync(id);

            if (salary == null)
                return NotFound(new { message = "Salary record not found" });

            return Ok(salary);
        }

        [HttpPost]
        [Authorize]    
        public async Task<IActionResult> Create([FromBody] SalaryCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSalary = await _salaryService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = createdSalary.Id },
                createdSalary);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SalaryUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedSalary = await _salaryService.UpdateAsync(id, dto);

            if (updatedSalary == null)
                return NotFound(new { message = "Salary record not found" });

            return Ok(updatedSalary);
        }

        [HttpDelete("{id}")]
        [Authorize]    
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _salaryService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Salary record not found" });

            return Ok(new { message = "Salary record deleted successfully" });
        }
    }
}

using AutoMapper;
using HRMS.Backend.DTOs;
using HRMS.Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly IMapper _mapper;

        public PayrollController(IPayrollService payrollService, IMapper mapper)
        {
            _payrollService = payrollService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payrolls = await _payrollService.GetAllAsync();
            return Ok(payrolls);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var payroll = await _payrollService.GetByIdAsync(id);
            if (payroll == null)
                return NotFound(new { message = "Payroll record not found" });

            return Ok(payroll);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PayrollCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdPayroll = await _payrollService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdPayroll.Id }, createdPayroll);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] PayrollUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedPayroll = await _payrollService.UpdateAsync(id, dto);
                if (updatedPayroll == null)
                    return NotFound(new { message = "Payroll record not found" });

                return Ok(updatedPayroll);
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
            var deleted = await _payrollService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Payroll record not found" });

            return Ok(new { message = "Payroll record deleted successfully" });
        }

        [HttpPost("generate")]
        [Authorize]
        public async Task<IActionResult> GeneratePayrolls([FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                var payrollMonth = new DateTime(year, month, 1);
                var payrolls = await _payrollService.GenerateMonthlyPayrollsAsync(payrollMonth);

                return Ok(new
                {
                    message = $"Payrolls generated for {month}/{year}",
                    data = payrolls
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

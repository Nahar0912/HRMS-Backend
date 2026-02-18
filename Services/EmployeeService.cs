using AutoMapper;
using HRMS.Backend.DTOs;
using HRMS.Backend.Interfaces;
using HRMS.Backend.Models;

namespace HRMS.Backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            var created = await _repository.AddAsync(employee);
            return _mapper.Map<EmployeeDTO>(created);
        }

        public async Task<EmployeeDTO?> UpdateAsync(int id, EmployeeUpdateDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            var updated = await _repository.UpdateAsync(existing);

            return _mapper.Map<EmployeeDTO>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<EmployeeDTO?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return employee == null ? null : _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync(); // fetch all

            return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
        }

    }
}

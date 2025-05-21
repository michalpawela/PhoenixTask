using AutoMapper;
using DbManagement.Entities;
using DbManagement;
using Common.Dtos;
using BoardGame_REST_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoardGame_REST_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly PhoenixDbContext _dbContext;
        private readonly IMapper _mapper;

        public EmployeeService(PhoenixDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> UpdateAsync(int id, EmployeeDto employeeDto)
        {
            var employee = await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
            var updated = _mapper.Map<Employee>(employeeDto);

            employee = updated;

            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
            return employeeDto;
        }

        public async Task<EmployeeDto> DeleteAsync(int id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> GetByIDAsync(int id)
        {
            var employee = await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _dbContext.Employees
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employeeDto;
        }
    }
}

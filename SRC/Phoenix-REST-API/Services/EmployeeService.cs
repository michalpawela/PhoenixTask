using AutoMapper;
using DbManagement.Entities;
using DbManagement;
using Common.Dtos;
using Phoenix_REST_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Phoenix_REST_API.Services
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
            var employee = await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Lp == id);
            var updated = _mapper.Map<Employee>(employeeDto);

            employee = updated;

            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
            return employeeDto;
        }

        public async Task<EmployeeDto> DeleteAsync(int id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Lp == id);
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> GetByIDAsync(int id)
        {
            var employee = await _dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Lp == id);

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

        public async Task<IEnumerable<EmployeeDto>> ParseCsvAsync(IFormFile csvFile)
        {
            var result = new List<EmployeeDto>();
            using (var stream = csvFile.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                string? line;
                bool isFirstLine = true;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue; // skip header
                    }
                    var parts = line.Split(';');
                    if (parts.Length < 6)
                        continue; // skip invalid lines

                    result.Add(new EmployeeDto
                    {
                        Lp = int.Parse(parts[0]),
                        Imie = parts[1],
                        Nazwisko = parts[2],
                        Zarobki = decimal.Parse(parts[3]),
                        PoziomStanowiska = parts[4],
                        MiejsceZamieszkania = parts[5]
                    });
                }

                var employees = _mapper.Map<List<Employee>>(result);
                await _dbContext.Employees.AddRangeAsync(employees);
                await _dbContext.SaveChangesAsync();


            }
            return result;
        }

        public async Task<List<EmployeeDto>> GetHighestEarnerPerCityAsync()
        {
            return await _dbContext.Employees
                .GroupBy(e => e.MiejsceZamieszkania)
                .Select(g => g.OrderByDescending(e => e.Zarobki).First())
                .Select(e => _mapper.Map<EmployeeDto>(e)) // Fix: Map Employee to EmployeeDto
                .ToListAsync();
        }

        public async Task<List<EmployeeDto>> GetHighestEarnerPerJobLevelAsync()
        {
            var employees = await _dbContext.Employees
                .FromSqlRaw("EXEC GetHighestEarnerPerJobLevel")
                .ToListAsync();

            return _mapper.Map<List<EmployeeDto>>(employees); // Fix: Map List<Employee> to List<EmployeeDto>
        }

        public async Task<List<EmployeeDto>> GetLowestEarnerPerCityAfterTaxAsync()
        {
            var employees = await _dbContext.Employees
                .FromSqlRaw("EXEC GetLowestEarnerPerCityAfterTax")
                .ToListAsync();

            return _mapper.Map<List<EmployeeDto>>(employees); // Fix: Map List<Employee> to List<EmployeeDto>
        }
    }
}

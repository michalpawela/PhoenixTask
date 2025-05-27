using Common.Dtos;


namespace Phoenix_REST_API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> UpdateAsync(int id, EmployeeDto employeeDto);
        Task<EmployeeDto> DeleteAsync(int id);
        Task<EmployeeDto> GetByIDAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto);
        Task<IEnumerable<EmployeeDto>> ParseCsvAsync(IFormFile csvPath);
        Task<List<EmployeeDto>> GetHighestEarnerPerCityAsync();
        Task<List<EmployeeDto>> GetHighestEarnerPerJobLevelAsync();
        Task<List<EmployeeDto>> GetLowestEarnerPerCityAfterTaxAsync();
    }
}

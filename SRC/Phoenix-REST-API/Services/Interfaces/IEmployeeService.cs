using Common.Dtos;

namespace BoardGame_REST_API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> UpdateAsync(int id, EmployeeDto employeeDto);
        Task<EmployeeDto> DeleteAsync(int id);
        Task<EmployeeDto> GetByIDAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto);
    }
}

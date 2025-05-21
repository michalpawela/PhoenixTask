using AutoMapper;
using Common.Dtos;
using DbManagement.Entities;

namespace Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}

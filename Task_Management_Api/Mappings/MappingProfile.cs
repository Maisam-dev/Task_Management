using AutoMapper;
using Task_Management_Api.DTOs;
using Task_Management_Api.Models;

namespace Task_Management_Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<TaskItem, TaskDto>();
        }
    }
}
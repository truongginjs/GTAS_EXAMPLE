using AutoMapper;
using BaseService.DTOs;
using BaseService.Models;

namespace BaseService
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Test,TestResponseDTO>();
            CreateMap<TestRequestDTO,Test>();
        }
    }
}
using AutoMapper;
using Contoso.API.DTO;
using Contoso.Model;

namespace Contoso.API.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(config =>
                                  config.CreateMap<Department, DepartmentDTO>()
                             );
        }
    }
}
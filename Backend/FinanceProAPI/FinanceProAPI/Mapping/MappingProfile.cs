using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using data = DAL.DO.Objects;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<data.Gastos, DataModels.Gastos>().ReverseMap();
            CreateMap<data.Ingresos, DataModels.Ingresos>().ReverseMap();
            CreateMap<data.Categorias, DataModels.Categorias>().ReverseMap();
        }
    }
}

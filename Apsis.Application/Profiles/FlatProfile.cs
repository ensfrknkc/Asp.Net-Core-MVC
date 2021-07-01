using Apsis.Application.Dto;
using Apsis.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsis.Application.Profiles
{
    public class FlatProfile : Profile
    {
        public FlatProfile()
        {
            CreateMap<FlatViewDto, Flat>().ReverseMap();
            CreateMap<Flat, FlatViewDto>().ReverseMap();
            
        }
    }
}

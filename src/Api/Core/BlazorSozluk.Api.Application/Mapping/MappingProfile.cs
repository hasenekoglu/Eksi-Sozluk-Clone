using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserComand, User>();
        }
    }
}

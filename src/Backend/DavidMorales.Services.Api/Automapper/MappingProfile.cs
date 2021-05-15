using AutoMapper;

using DavidMorales.Domain.Entities;
using DavidMorales.Services.Api.ViewModels;

namespace DavidMorales.Services.Api.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyViewModel, Company>().ReverseMap();
            CreateMap<CompanyUpdateViewModel, Company>().ReverseMap();
            CreateMap<CompanyCreateViewModel, Company>().ReverseMap();

            CreateMap<PersonCreateViewModel, Person>().ReverseMap();
            CreateMap<PersonUpdateViewModel, Person>().ReverseMap();

            CreateMap<UserCreateViewModel, AppUser>().ReverseMap();
            CreateMap<UserUpdateViewModel, AppUser>().ReverseMap();

            CreateMap<FileViewModel, Document>().ReverseMap();
        }
    }
}

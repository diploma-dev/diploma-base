using AutoMapper;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.AuthHelpers;
using DiplomaProject.Models.DTO;
using DiplomaProject.Models.RequestModels;

namespace DiplomaProject.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestModel, UserDTO>()
                .ForMember(x => x.Firstname, opt => opt.MapFrom(x => x.Firstname))
                .ForMember(x => x.Lastname, opt => opt.MapFrom(x => x.Lastname))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email));

            CreateMap<UserEntity, UserDTO>()
                .ForMember(x => x.Firstname, opt => opt.MapFrom(x => x.Firstname))
                .ForMember(x => x.Lastname, opt => opt.MapFrom(x => x.Lastname))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.PasswordHash, opt => opt.MapFrom(x => x.PasswordHash))
                .ForMember(x => x.PasswordSalt, opt => opt.MapFrom(x => x.PasswordSalt))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));

            CreateMap<RefreshTokenEntity, RefreshTokenModel>();
        }
    }
}

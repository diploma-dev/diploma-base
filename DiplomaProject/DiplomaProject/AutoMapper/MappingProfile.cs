using AutoMapper;
using DiplomaProject.EntityModels;
using DiplomaProject.Models.AuthHelpers;
using DiplomaProject.Models.DTO;
using DiplomaProject.Models.RequestModels;
using DiplomaProject.Models.ResponseModels;
using DiplomaProject.Models.ResponseModels.ProfilePhoto;

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
            CreateMap<UserDTO, ProfileResponseModel>();
            CreateMap<ProfilePhotoEntity, ProfilePhotoDTO>();
            CreateMap<ProfilePhotoDTO, ProfilePhotoResponseModel>()
                .ForMember(x => x.PhotoPath, opt => opt.MapFrom(x => x.PhotoFullPath))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(x => x.Id));

            CreateMap<HealthParametrEntity, HealthParametrDTO>();
            CreateMap<HealthParametrDTO, HealthParametrEntity>();
            CreateMap<HealthParametrDTO, HealthParametrsResponseModel>();
            CreateMap<UpdateHealthParametrRequestModel, HealthParametrDTO>();
            CreateMap<UploadHealthParametrRequestModel, HealthParametrDTO>();
            CreateMap<BMIHistoryEntity, BMIHistoryDTO>();
            CreateMap<BMIHistoryDTO, BMIResponseModel>();
        }
    }
}

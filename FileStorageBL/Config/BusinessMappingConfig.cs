using AutoMapper;
using FileStorageBL.DTOs;
using FileStorageDAL.Models;
using System.Linq;

namespace FileStorageBL.Config
{
    public class BusinessMappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<File, FileDto>().ReverseMap();
            config.CreateMap<FileVersion, FileVersionDto>().ReverseMap();
            config.CreateMap<Folder, FolderShowDto>().ReverseMap();
            config.CreateMap<Log, LogDto>().ReverseMap();
            config.CreateMap<Role, RoleDto>().ReverseMap();

            config.CreateMap<VersionedFile, VersionedFileCreateDto>().ReverseMap();
            config.CreateMap<VersionedFile, VersionedFileUpdateDto>().ReverseMap();
            config.CreateMap<VersionedFile, VersionedFileShowDto>().ReverseMap();


            config.CreateMap<Folder, FolderCreateDto>().ReverseMap();
            config.CreateMap<Folder, FolderUpdateDto>().ReverseMap();

            config.CreateMap<User, UserLoginDto>().ReverseMap();
            config.CreateMap<User, UserCreateDto>().ReverseMap();
            config.CreateMap<User, UserUpdateDto>().ReverseMap();
            config.CreateMap<User, UserShowDto>()
                .ForMember(x => x.Roles, x => x.MapFrom(y => y.Roles.Select(z => z.Name)))
                .ReverseMap();
        }
    }
}

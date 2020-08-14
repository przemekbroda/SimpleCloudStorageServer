using AutoMapper;
using SimpleCloudStorageServer.Dto;
using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap(typeof(PagedList<>), typeof(PagedListDto<>));

            CreateMap<User, UserForDetailsDto>();

            CreateMap<User, UserForDeleteDto>();

            CreateMap<User, UserForRegisterResultDto>();

            CreateMap<File, FileForUploadDto>();

            CreateMap<FileForUpdateDto, File>();

        }
    }
}

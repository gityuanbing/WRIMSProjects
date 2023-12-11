using System;
using System.Linq;
using AutoMapper;
using WRIMS.Model.Models.Common;
using WRIMS.Model.TransformModels.FileTransfer;

namespace WRIMS.Extensions.AutoMapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileDto>().ForMember(dest => dest.RuleGuid,
                opt =>
                    opt.MapFrom(src => src.Map.LastOrDefault() != null ? (Guid?)src.Map.LastOrDefault().TypeGuid : null));


            CreateMap<File, ImageFileDto>().ForMember(dest => dest.RuleGuid,
               opt =>
                   opt.MapFrom(src => src.Map.LastOrDefault() != null ? (Guid?)src.Map.LastOrDefault().TypeGuid : null));
        }
    }

}
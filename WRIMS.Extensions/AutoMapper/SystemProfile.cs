using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using WRIMS.Model;
using WRIMS.Model.Models.System;
using WRIMS.Model.Models.SystemManagement;
using WRIMS.Model.TransformModels.Common;
using WRIMS.Model.TransformModels.System;
using WRIMS.Model.TransformModels.SystemManagement;

namespace WRIMS.Extensions.AutoMapper
{
    public class SystemProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public SystemProfile()
        {
            CreateMap<Module,ModuleInfo_Dto>();

            CreateMap(typeof(PageModel<>), typeof(PageModel<>));

            CreateMap<UserInfo, UserInfo_Dto>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => src.UserRoles.Where(x => x.IsDeleted != true && x.Role.IsDeleted != true).Select(x => x.Role).ToList()))
                .ForMember(dest => dest.Agency,
                    opt => opt.MapFrom(
                        src => src.Agency != null ? src.Agency.Name : null));

            CreateMap<UserInfo_CreateDto, UserInfo>();

            CreateMap<UserInfo_UpdateDto, UserInfo>();

            CreateMap<Role, Role_Dto>()
                .ForMember(dest => dest.UserCount,
                    opt => opt.MapFrom(
                        src => src.UserRoles.Count(x => x.User.IsEnabled == true && x.User.IsDeleted != true)));

            CreateMap<Role_Create_Dto, Role>();

            CreateMap<Agency_CreateDto, Agency>();

            CreateMap<Agency, Agency_Dto>()
                .ForMember(dest => dest.ContacterName,
                    opt => opt.MapFrom(src =>
                        src.Contacter != null ? src.Contacter.RealName : null))
                .ForMember(dest => dest.SubAgency,
                    opt => opt.Ignore());

            CreateMap<Agency_UpdateDto, Agency>();
            CreateMap<Menu, Menu_Dto>().ForMember(dest => dest.SubMenu, opt => opt.Ignore());

            CreateMap<MessageCreate_Dto, Message>();
            CreateMap<MessageUpdate_Dto, Message>();
            CreateMap<Message, MessageAbstract_Dto>()
                .AfterMap((src, dest) =>
                {
                    dest.CreatorName = src?.Creator?.RealName;
                    var list = src?.FileMap.Select(x => x.File.FullName);
                    dest.FileListDesc = string.Join(",", list);
                    //if (src.State == Model.Enum.MessageStateEnum.置顶)
                    //{
                    //    dest.Title = "[置顶]" + dest.Title;
                    //}
                });

            CreateMap<Message, MessageDetail_Dto>().ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.FileMap.Select(x => x.File))).AfterMap((src, dest) => {
                dest.CreatorName = src?.Creator?.RealName;
            });

            
        }
    }
}

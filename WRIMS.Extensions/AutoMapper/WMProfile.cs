using AutoMapper;
using System.Text.RegularExpressions;
using WRIMS.Model.Models.WM;
using WRIMS.Model.TransformModels.WM;

namespace WRIMS.Extensions.AutoMapper
{
    public class WMProfile : Profile
    {
        public WMProfile()
        {
            CreateMap<WM_DuplexFrequencyPlanning, WM_DuplexFrequencyPlanning_Dto>();
            CreateMap<WM_DuplexFrequencyPlanning_CreateDto, WM_DuplexFrequencyPlanning>();
            CreateMap<WM_DuplexFrequencyPlanning_UpdateDto, WM_DuplexFrequencyPlanning>();

            CreateMap<WM_SimplexFrequencyPlanning, WM_SimplexFrequencyPlanning_Dto>();
            CreateMap<WM_SimplexFrequencyPlanning_CreateDto, WM_SimplexFrequencyPlanning>();
            CreateMap<WM_SimplexFrequencyPlanning_UpdateDto, WM_SimplexFrequencyPlanning>();

            CreateMap<WM_PortableRadioGroup_CreateDto, WM_PortableRadioGroup>();
            CreateMap<WM_PortableRadioGroup, WM_PortableRadioGroup_Dto>();

            CreateMap<WM_PortableRadioGroup, WM_PortableRadioGroupDetail_Dto>().AfterMap((src, dest) =>
            {
                dest.GroupId = src.Id;
            });

            CreateMap<WM_PortableRadioConfig, WM_PortableRadioConfigDto>();
            CreateMap<WM_PortableRadioConfigCreateDto, WM_PortableRadioConfig>();
            CreateMap<WM_PortableRadioConfigUpdateDto, WM_PortableRadioConfig>();
            CreateMap<WM_UltrashortConfigGroup, WM_UltrashortConfigGroupDto>()
                .ForMember(x => x.Config,
                    opt => opt.Ignore());
            CreateMap<WM_PortableRadioConfig, SubPortableGroup>();

            CreateMap<WM_UltrashortConfig, WM_UltrashortConfigDto>();
            CreateMap<WM_UltrashortConfigCreateDto, WM_UltrashortConfig>();
            CreateMap<WM_UltrashortBuType, WM_UltrashortBuTypeDto>();
            CreateMap<WM_UltrashortBuTypeCreateDto, WM_UltrashortBuType>();
            CreateMap<WM_UltrashortBuTypeUpdate_Dto, WM_UltrashortBuType>();
            CreateMap<WM_UltrashortConfigGroupUpdateDto, WM_UltrashortConfigGroup>()
                .ForMember(dest => dest.Configs, opt => opt.Ignore());
            CreateMap<WM_UltrashortConfigGroupCreateDto, WM_UltrashortConfigGroup>()
                .ForMember(dest => dest.Configs, opt => opt.Ignore());


            CreateMap<WM_UltrashortBuType, WM_UltrashortBuTypeDetail_Dto>().AfterMap((src, dest) =>
            {
                dest.buTypeId = src.Id;
            });



            CreateMap<WM_ModeTransitionPoint_CreateDto, WM_ModeTransitionPoint>()
                 .AfterMap(
                    (src, dest) =>
                    {

                        //前端 纬度的度分秒已传，直接拼接
                        dest.Latitude = $"{src.latdeg ?? 0}°{src.latmin ?? 0}′{src.latsec ?? 0}″";

                        //前端 经度的度分秒已传，直接拼接
                        dest.Longitude = $"{src.lngdeg ?? 0}°{src.lngmin ?? 0}′{src.lngsec ?? 0}″";
                    });
            CreateMap<WM_ModeTransitionPoint, WM_ModeTransitionPointDto>().AfterMap(
                (src, dest) =>
                {

                    Regex regx2 = new Regex(@"(?<deg>\d+)°(?<min>\d+)′(?<sec>\d+)″");
                    var latMatch = regx2.Match(src.Latitude);
                    var lngMatch = regx2.Match(src.Longitude);

                    int.TryParse(latMatch.Groups["deg"].Value, out int latdeg);
                    dest.latdeg = latdeg;
                    int.TryParse(latMatch.Groups["min"].Value, out int latmin);
                    dest.latmin = latmin;
                    int.TryParse(latMatch.Groups["sec"].Value, out int latsec);
                    dest.latsec = latsec;

                    int.TryParse(lngMatch.Groups["deg"].Value, out int lngdeg);
                    dest.lngdeg = lngdeg;
                    int.TryParse(lngMatch.Groups["min"].Value, out int lngmin);
                    dest.lngmin = lngmin;
                    int.TryParse(lngMatch.Groups["sec"].Value, out int lngsec);
                    dest.lngsec = lngsec;
                }); ;
            CreateMap<WM_ModeTransitionPoint_UpdateDto, WM_ModeTransitionPoint>().AfterMap(
                    (src, dest) =>
                    {

                        //前端 纬度的度分秒已传，直接拼接
                        dest.Latitude = $"{src.latdeg ?? 0}°{src.latmin ?? 0}′{src.latsec ?? 0}″";

                        //前端 经度的度分秒已传，直接拼接
                        dest.Longitude = $"{src.lngdeg ?? 0}°{src.lngmin ?? 0}′{src.lngsec ?? 0}″";
                    }); ;

            CreateMap<WM_RouteSelectionArea_PutDto, WM_RouteSelectionArea>();
            CreateMap<WM_RouteSelectionArea, WM_RouteSelectionArea_Dto>();
        }
    }
}

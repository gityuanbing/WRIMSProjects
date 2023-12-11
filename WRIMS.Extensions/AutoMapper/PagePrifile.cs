using System;
using System.Linq;
using AutoMapper;
//using MoreLinq;
using WRIMS.Model.Enum;
using WRIMS.Model.Models.WorkFlow;
using WRIMS.Model.TransformModels.Common;

namespace WRIMS.Extensions.AutoMapper
{
    public class PagePrifile : Profile
    {
        public PagePrifile()
        {
            CreateMap<Flow, FlowSimpleDto>().
                ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.RealName))
                .ForMember(dest => dest.CreateAgency, opt => opt.MapFrom(src => src.CreatorAgency.Name))
                .AfterMap((src, dest) =>
                {
                    //var currentStep = src.Steps.Where(x => x.State == StepStateEnum.处理中).MaxBy(x => x.SortIndex).LastOrDefault();//NET CORE 3.1 使用MoreLinq语法
                    var currentStep = src.Steps.Where(x => x.State == StepStateEnum.处理中).MaxBy(x => x.SortIndex);//NET6 LINQ 支持MinBy
                    dest.StepName = currentStep?.StepName ?? src.State.ToString();
                    dest.CurrentProcessor = currentStep?.Processor?.RealName;
                    dest.CurrentProcessorInfo = currentStep?.Processor?.Info;


                    //var previousStep = src.Steps.Where(x => new[] { StepStateEnum.完成, StepStateEnum.关闭 }.Contains(x.State)).MaxBy(x => x.SortIndex).LastOrDefault();//NET CORE 3.1 使用MoreLinq语法
                    var previousStep = src.Steps.Where(x => new[] { StepStateEnum.完成, StepStateEnum.关闭 }.Contains(x.State)).MaxBy(x => x.SortIndex);//NET6 LINQ 支持MinBy
                    dest.PreviousProcessor = previousStep?.Processor?.RealName;
                    dest.PreviousProcessorInfo = previousStep?.Processor?.Info;
                    //TODO 需要完善Type和Description


                    //switch (src.Type)
                    //{
                    //    case FlowTypeEnum.机车电台执照办理:
                    //        dest.Type = "机车电台执照办理";
                    //        dest.Description = $"{src.RsFlowInfo.Model} {src.RsFlowInfo.Number}";
                    //        break;
                    //    case FlowTypeEnum.GSM_R系统设台手续办理:
                    //        dest.Type = "GSM-R系统设台手续办理";
                    //        dest.Description = $"{src.GSMInfo.ProjectName} {src.GSMInfo.Frequency}";
                    //        break;
                    //    case FlowTypeEnum.无线干扰申诉:
                    //        break;
                    //    case FlowTypeEnum._400M系统设台手续办理:
                    //        dest.Type = "400M频段设台手续办理";
                    //        dest.Description = $"{src.FRInfo.Name}";
                    //        break;
                    //    case FlowTypeEnum._400M频率使用许可办理:
                    //        dest.Type = "400M频段频率许可办理";
                    //        dest.Description = $"{src.FFInfo.Name}";
                    //        break;
                    //    case FlowTypeEnum.通用:
                    //        break;
                    //    default:
                    //        throw new ArgumentOutOfRangeException();
                    //}
                });

        }
    }
}

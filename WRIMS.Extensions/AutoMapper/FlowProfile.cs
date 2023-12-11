using AutoMapper;
using WRIMS.Model.Models.WorkFlow;
using WRIMS.Model.TransformModels.SystemManagement;
using WRIMS.Model.TransformModels.WF;

namespace WRIMS.Extensions.AutoMapper
{
    public class FlowProfile : Profile
    {
        public FlowProfile()
        {
            CreateMap<Step, Step_Dto>()
                .ForMember(dest => dest.Processor,
                    opt => opt.MapFrom(
                        src => src.Processor != null ? src.Processor.RealName : string.Empty)
                )
                .ForMember(dest => dest.AgencyName,
                    opt => opt.MapFrom(src =>
                        src.Processor.Agency != null ? src.Processor.Agency.Name : null
                    ))
                .ForMember(dest => dest.AgencyName,
                    opt => opt.MapFrom(src =>
                        src.Processor.Agency != null ? src.Processor.Agency.Name : string.Empty));


            CreateMap<Form, Form_Dto>()
                .ForMember(dest => dest.DownLoadList, opt => opt.Ignore())
                .ForMember(dest => dest.UploadList, opt => opt.Ignore())
                .ForMember(dest => dest.DataFields, opt => opt.Ignore());

            CreateMap<Revision, Revision_Dto>();
            CreateMap<Revision_Create_Dto, Revision>();
            CreateMap<WrokFlowConfig, WrokFlowConfigDto>();
        }
    }
}
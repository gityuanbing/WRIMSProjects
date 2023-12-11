using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WRIMS.Model.Models.WorkFlow;
using WRIMS.Model.TransformModels.SystemManagement;
using WRIMS.Model.TransformModels.WF;

namespace WRIMS.Extensions.AutoMapper
{
    /// <summary>
    /// 工作流配置
    /// </summary>
    public class WrokFlowConfigProfile: Profile
    {

        public WrokFlowConfigProfile()
        {
            CreateMap<WrokFlowConfigFileTemplate, FileTemplateDto>();

            CreateMap<WrokFlowConfig_CreateDto, WrokFlowConfig>();

            CreateMap<WrokFlowConfig, WrokFlowConfig_Detail_Dto>();

            CreateMap<WrokFlowConfigFileTemplate, WrokFlowConfigFileTemplate_Dto>();

            CreateMap<WrokFlowConfigFile, ReplenishFile_Detail_Dto>();
            
        }
    }
}

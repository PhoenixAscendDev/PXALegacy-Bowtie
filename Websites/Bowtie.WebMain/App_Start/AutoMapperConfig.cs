using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bowtie.WebMain
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<JB2.Bowtie.IApplication, JB2.Bowtie.Web.Models.ApplicationViewModel>()
                                                        .ForMember(dest => dest.CompanyID,opts => opts.MapFrom(src => src.Company.ID))
                                                        .ForMember(dest => dest.CompanyName, opts => opts.MapFrom(src => src.Company.Name))
                                                        .ForMember(dest => dest.jBeanKey,opts => opts.MapFrom(src => src.GetTreasuryRequestKey("jBean").Key)));

            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<JB2.Bowtie.IDewdrop, JB2.Bowtie.Web.Models.DewdropViewModel>()
                                                        .ForMember(dest => dest.jBeanCost, opts => opts.MapFrom(src => src.GetjBeanCost()))
                                                        .ForMember(dest => dest.GraphID, opts => opts.MapFrom(src => src.GetGraphID()))
                                                        .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.GetDescription())));
        }
    }
}
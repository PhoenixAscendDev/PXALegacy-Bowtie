using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JB2.Bowtie.Web;

namespace Bowtie.WebMain
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {


            
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<JB2.Bowtie.IApplication, JB2.Bowtie.Web.Models.ApplicationViewModel>()
                        .ForMember(dest => dest.CompanyID, opts => opts.MapFrom(src => src.Company.ID))
                        .ForMember(dest => dest.CompanyName, opts => opts.MapFrom(src => src.Company.Name))
                        .ForMember(dest => dest.jBeanKey, opts => opts.MapFrom(src => src.GetTreasuryRequestKey("jBean").Key));
                cfg.CreateMap<JB2.Bowtie.IDewdrop, JB2.Bowtie.Web.Models.DewdropViewModel>()
                        .ForMember(dest => dest.jBeanCost, opts => opts.MapFrom(src => src.GetjBeanCost()))
                        .ForMember(dest => dest.GraphID, opts => opts.MapFrom(src => src.GetGraphID()))
                        .ForMember(dest => dest.ApplicationID, opts => opts.MapFrom(src => src.GetApplicationID()))
                        .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.GetDescription()));
                cfg.CreateMap<JB2.Bowtie.IGraphElement, JB2.Bowtie.Web.Models.GraphElementViewModel>()
                        .ForMember(dest => dest.ElementType, opts => opts.MapFrom(src => src.ElementType.ToString()));
                cfg.CreateMap<JB2.Bowtie.GraphObject, JB2.Bowtie.Web.Models.GraphObjectViewModel>()
                        .ForMember(dest  => dest.ElementType, opts => opts.MapFrom(src => src.ElementType.ToString()))
                        .ForMember(dest => dest.Properties, opts => opts.MapFrom(src => src.GetProperties().ToSelectItems()));
                cfg.CreateMap<JB2.Bowtie.GraphProperty, JB2.Bowtie.Web.Models.GraphPropertyViewModel>()
                        .ForMember(dest => dest.ElementType, opts => opts.MapFrom(src => src.ElementType.ToString()))
                        .ForMember(dest => dest.GraphPropertyType, opts => opts.MapFrom(src => src.GraphPropertyType.ToString()));
                cfg.CreateMap<JB2.Bowtie.GraphAction, JB2.Bowtie.Web.Models.GraphActionViewModel>()
                       .ForMember(dest => dest.ElementType, opts => opts.MapFrom(src => src.ElementType.ToString()))
                       .ForMember(dest => dest.Properties, opts => opts.MapFrom(src => src.GetProperties().ToSelectItems()))
                       .ForMember(dest => dest.Objects, opts => opts.MapFrom(src => src.GetAssociatedObjects().ToSelectItems()));
            });
        }
    }
}
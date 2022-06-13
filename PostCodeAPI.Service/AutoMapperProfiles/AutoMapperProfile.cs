using AutoMapper;
using PostCodeAPI.DataTransferModel;
using PostCodeAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostCodeAPI.Service.AutoMapperProfiles
{
    public class AutoMapperProfile : Profile  
    {
        public AutoMapperProfile()
        {
            CreateMap<PostCodeAutoComplete, PostCodeAutoCompleteDataTransferModel>();

            CreateMap<PostCodeLookUp, PostCodeLookUpDataTransferModel>()
                .ForMember(x => x.area, y => y.MapFrom(z => z.result.area))
                .ForMember(x => x.country, y => y.MapFrom(z => z.result.country))
                .ForMember(x => x.region, y => y.MapFrom(z => z.result.region))
                .ForMember(x=>x.admin_district,y=>y.MapFrom(z=>z.result.codes.admin_district))
                .ForMember(x => x.parliamentary_constituency, y => y.MapFrom(z => z.result.codes.parliamentary_constituency));
        }
    }
}


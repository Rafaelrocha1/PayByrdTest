using AutoMapper;
using paybyrd.Entities;
using paybyrd.Entities.Request;
using paybyrd.Entities.Response;

namespace paybyrd.AutoMapper
{
    public class ConfigurationMapping : Profile
    {

        public  ConfigurationMapping()
        {
            CreateMap <DiffRequest, Diff >().ReverseMap();
            CreateMap <DiffResponse, Diff >().ReverseMap();
        }
    }
}

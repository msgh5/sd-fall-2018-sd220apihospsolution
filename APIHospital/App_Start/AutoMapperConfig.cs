using APIHospital.Models;
using APIHospital.Models.Domain;
using AutoMapper;

namespace APIHospital.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(p =>
            {
                p.CreateMap<Patient, PatientBindingModel>().ReverseMap();
                p.CreateMap<Patient, PatientViewModel>().ReverseMap();
                p.CreateMap<Visit, VisitViewModel>().ReverseMap();
                p.CreateMap<Visit, VisitBindingModel>().ReverseMap();
            });
        }
    }
}
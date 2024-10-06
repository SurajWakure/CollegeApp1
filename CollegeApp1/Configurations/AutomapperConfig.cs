using AutoMapper;
using Collegeapp1.Data;
using Microsoft.IdentityModel.Tokens;

namespace Collegeapp1.Configurations
{
    public class AutomapperConfig:Profile
    {
        public AutomapperConfig()
        {
            /*CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();*/

            CreateMap<StudentDTO, Student>().ReverseMap()
                .ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address) ? "no data found" : n.Address));
                
        }
    }
}

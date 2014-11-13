namespace TeamUp.Web.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class GridUserViewModel : IMapFrom<TeamUpUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public Occupation Occupation { get; set; }

        public ICollection<string> ProgrammingCategories { get; set; }

        public ICollection<string> Skills { get; set; }

        public ICollection<string> Projects { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<TeamUpUser, GridUserViewModel>()
                        .ForMember(t => t.Skills,
                            o => o.MapFrom(b => b.Skills.Select(s => s.Name).ToList())
                        )
                        .ForMember(t => t.ProgrammingCategories,
                            o => o.MapFrom(b => b.ProgrammingCategories.Select(p => p.Name).ToList())
                        )
                        .ForMember(t => t.Projects,
                            o => o.MapFrom(b => b.Projects.Select(p => p.Name).ToList())
                        );
        }
    }
}
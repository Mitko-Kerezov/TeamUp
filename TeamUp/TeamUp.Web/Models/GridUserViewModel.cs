namespace TeamUp.Web.Models
{
    using System.Collections.Generic;
    using AutoMapper;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class GridUserViewModel : IMapFrom<TeamUpUser>, IHaveCustomMappings
    {
        public string Email { get; set; }

        public ICollection<ProgrammingCategory> ProgrammingCategories { get; set; }

        public string[] Skills { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<TeamUpUser, GridUserViewModel>()
                        .ForMember(t => t.Skills,
                        o => o.ResolveUsing(b =>
                            {
                                
                                return b.Value;
                            })
                        );
        }
    }
}
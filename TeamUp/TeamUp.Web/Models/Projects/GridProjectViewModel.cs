namespace TeamUp.Web.Models.Projects
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class GridProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasEnded { get; set; }

        public DateTime DateCreated { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Project, GridProjectViewModel>()
                        .ForMember(t => t.Id,
                            o => o.MapFrom(b => b.Id.ToString())
                        );
        }
    }
}
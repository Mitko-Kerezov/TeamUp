namespace TeamUp.Web.Models.Invitations
{
    using System;

    using TeamUp.Models;
    using TeamUp.Web.Infrastructure.Mapping;

    public class ShowInvitationViewModel : IMapFrom<Invitation>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public TeamUpUser Author { get; set; }

        public Project Project { get; set; }

        public DateTime DateSent { get; set; }

        public bool ShouldShowButton { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Invitation, ShowInvitationViewModel>()
                        .ForMember(t => t.ShouldShowButton,
                            o => o.MapFrom(b => b.InvitationResponse == InvitationResponseType.NotRead)
                        );
        }
    }
}
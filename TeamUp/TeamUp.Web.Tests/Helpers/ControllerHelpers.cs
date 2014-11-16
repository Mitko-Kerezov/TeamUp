namespace TeamUp.Web.Tests.Helpers
{
    using System;

    using Telerik.JustMock;

    using TeamUp.Data;
    using TeamUp.Web.Controllers;
    using TeamUp.Web.Infrastructure.Mapping;
    using TeamUp.Models;

    public static class ControllerHelpers
    {
        public static T GetSetuppedController<T>(ITeamUpData data) where T : BaseController
        {
            var controller = Mock.Create<T>(data);
            AutoMapperConfig.Execute();
            Mock.Arrange(() => controller.CurrentUser)
                .Returns(() => new TeamUpUser()
                {
                    Id = "0",
                    Email = "pesho@pesho.com",
                    UserName = "pesho@pesho.com",
                });

            return controller;
        }
    }
}

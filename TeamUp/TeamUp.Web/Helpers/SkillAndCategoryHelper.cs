namespace TeamUp.Web.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using TeamUp.Data;
    using TeamUp.Web.Models;

    public static class SkillAndCategoryHelper
    {
        public static IList<CheckBoxItem> GetSkillsFromDbAsCheckBoxes(ITeamUpData data)
        {
            return data.Skills
                        .All()
                        .Select(s => new CheckBoxItem()
                        {
                            Text = s.Name,
                            Value = s.Name
                        })
                        .ToList();
        }

        public static IList<CheckBoxItem> GetCategoriesFromDbAsCheckBoxes(ITeamUpData data)
        {
            return data.ProgrammingCategories
                        .All()
                        .Select(s => new CheckBoxItem()
                        {
                            Text = s.Name,
                            Value = s.Name
                        })
                        .ToList();
        }
    }
}
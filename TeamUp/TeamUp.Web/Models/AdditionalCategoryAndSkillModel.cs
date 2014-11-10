namespace TeamUp.Web.Models
{
    using System.Collections.Generic;

    public class AdditionalCategoryAndSkillModel
    {
        public AdditionalCategoryAndSkillModel(IList<CheckBoxItem> categories, IList<CheckBoxItem> skills)
        {
            this.Categories = categories;
            this.Skills = skills;
        }

        public IList<CheckBoxItem> Categories { get; set; }

        public IList<CheckBoxItem> Skills { get; set; }
    }
}
namespace TeamUp.Web.Models
{
    using System.Collections.Generic;

    public class ChangeSkillsModel
    {
        public ChangeSkillsModel(IList<CheckBoxItem> skills)
        {
            this.Skills = skills;
        }

        public IList<CheckBoxItem> Skills { get; set; }
    }

    public class ChangeSkillsPostModel
    {
        public string[] Skills { get; set; }
    }
}
namespace TeamUp.Web.Models
{
    using System.Collections.Generic;

    public class ChangeCategoriesModel
    {
        public ChangeCategoriesModel(IList<CheckBoxItem> categories)
        {
            this.ProgrammingCategories = categories;
        }

        public IList<CheckBoxItem> ProgrammingCategories { get; set; }
    }

    public class ChangeCategoriesPostModel
    {
        public string[] ProgrammingCategories { get; set; }
    }
}
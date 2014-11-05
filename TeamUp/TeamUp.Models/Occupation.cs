namespace TeamUp.Models
{
    using System.ComponentModel;

    public enum Occupation
    {
        [Description("Developer")]
        Developer,
        [Description("Front-end")]
        FrontEnd,
        [Description("Quality Assurance")]
        QA
    }
}

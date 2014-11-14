namespace TeamUp.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AdministrationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
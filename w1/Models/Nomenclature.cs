using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace w1.Models
{
    public class Nomenclature
    {
        [DisplayName("Артикул")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать наименование товара")]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Единица измерения")]
        public Units Unit { get; set; }

        public enum Units
        {
            [Display(Name = "шт.")]
            piece,
            [Display(Name = "ящ.")]
            box,
            [Display(Name = "плт.")]
            pallet
        }
    }
}
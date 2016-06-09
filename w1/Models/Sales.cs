using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace w1.Models
{
    public class Sales
    {
        [DisplayName("Номер")]
        public int Id { get; set; }     
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [DisplayName("Товар")]
        public int NomenclatureId { get; set; }

        public virtual Nomenclature Nomenclature { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }
        [DisplayName("Сумма")]
        public decimal Amount { get; set; }
    }
}
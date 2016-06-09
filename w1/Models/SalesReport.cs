using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace w1.Models
{
    public class SalesReport
    {
        [DisplayName("Товар")]
        public virtual Nomenclature Nomenclature { get; set; }

        [DisplayName("В количестве")]
        public int Quantity { get; set; }

        [DisplayName("Общая сумма")]
        public decimal Amount { get; set; }

        [DisplayName("Всего заказов")]
        public int Orders { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace w1.Models
{
    public class Order
    {
        [DisplayName("Номер")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату заказа")]
        [DisplayName("Дата")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Необходимо указать дату доставки")]
        [DisplayName("Дата доставки")]
        public DateTime DeliveryDate { get; set; }

        [Range(typeof(decimal), "0,01", "999999999999,00", ErrorMessage = "Неправильная цена")]
        [Required(ErrorMessage = "Необходимо ввести цену")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [Range(typeof(int), "1", "9999999", ErrorMessage = "Необходимо ввести количество")]
        [Required(ErrorMessage = "Необходимо ввести количество")]
        [DisplayName("Количество")]
        public int Quantity { get; set; }

        [DisplayName("Комментарий")]
        public string Сomment { get; set; }

        [DisplayName("Закрыт")]
        public bool IsClose { get; set; }

        public byte[] Xml { get; set; }

        
        [Required(ErrorMessage = "Необходимо указать товар")]
        public int NomenclatureId { get; set; }

        [DisplayName("Номенклатура")]
        public virtual Nomenclature Nomenclature { get; set; }

        public IEnumerable<SelectListItem> GoodsList
        {
            get { return new SelectList((new DefaultContext()).Goods, "Id", "Name"); }
        }

    }
}
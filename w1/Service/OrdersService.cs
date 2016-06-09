using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using w1.Models;

namespace w1.Service
{
    public class OrdersService
    {
        DefaultContext db;
        public OrdersService(DefaultContext database)
        {
            db = database;
        }

        public Order GetOrderById(int? id)
        {
            Order order = db.Orders.Find(id);
            return order;
        }

        public Order CreateOrder()
        {
            Order newOrder = new Order();
            newOrder.OrderDate = DateTime.Now;
            newOrder.DeliveryDate = DateTime.Now.AddDays(2);
            return newOrder;
        }

        public void DeleteOrder(Order order)
        {
            var sale = db.Sales.FirstOrDefault(x => x.OrderId == order.Id);
            if (sale != null)
            {
                db.Sales.Remove(sale);
            }
            db.Orders.Remove(order);
            try
            {
                db.SaveChanges(); ;
            }
            catch { }
        }

        public void AddSaveOrder(Order order)
        {
            if (order.Id == 0)
            {
                db.Orders.Add(order);
            }
            else
            {
                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            }
            
            try
            {
                db.SaveChanges();
                AddSaveSale(order);
                if (order.IsClose)
                {
                    GenerateXml(order);
                }
                db.SaveChanges();
            }
            catch (Exception ex){ }
        }

        private void AddSaveSale(Order order)
        {
            var sale = db.Sales.FirstOrDefault(x => x.OrderId == order.Id);
            if (sale == null)
            {
                sale = db.Sales.Create();
                sale.OrderId = order.Id;
            }
            sale.NomenclatureId = order.NomenclatureId;
            sale.Quantity = order.Quantity;
            sale.Amount = order.Quantity*order.Price;
            if(sale.Id == 0){
                db.Sales.Add(sale);
            }               
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders.ToList();
        }

        private void GenerateXml(Order order)
        {
            XDocument doc = new XDocument(
                new XElement("Order",
                    new XElement("Id", SetXmlValue(order.Id)),
                    new XElement("OrderDate", SetXmlValue(order.OrderDate)),
                    new XElement("DeliveryDate", SetXmlValue(order.DeliveryDate)),
                    new XElement("ProductArticle", SetXmlValue(order.NomenclatureId)),
                    new XElement("Price", SetXmlValue(order.Price)),
                    new XElement("Quantity", SetXmlValue(order.Quantity)),
                    new XElement("Comment", SetXmlValue(order.Quantity))));


            Encoding enc = Encoding.Unicode;
            order.Xml = enc.GetBytes(doc.ToString());
        }

        private string SetXmlValue(object value, string defValue = "")
        {
            if (value == null)
            {
                return defValue;
            }
            return value.ToString();
        }

        public Dictionary<string, object> GetXmlDictById(int? id)
        {
            var result = new Dictionary<string, object>();

            Order order = db.Orders.Find(id);
            result.Add("fileBytes", order.Xml);
            result.Add("fileName", string.Format("Order_{0}.xml", order.Id));
            return result;
        }

        public IEnumerable<SalesReport> GenerateSalesReport()
        {
            var sales = db.Sales.GroupBy(x => x.NomenclatureId).Select(x => new SalesReport() { Nomenclature = x.FirstOrDefault().Nomenclature, Quantity = x.Sum(dt => dt.Quantity), Amount = x.Sum(dt => dt.Amount) }).OrderByDescending(x => x.Amount);
            return sales;
        }
    }
}
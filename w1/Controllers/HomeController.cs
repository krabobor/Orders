using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using w1.Models;
using w1.Service;

namespace Orders.Controllers
{
    public class HomeController : Controller
    {
        private readonly OrdersService _ordersService;
        private readonly GoodsService _goodsService;

        public HomeController()
        {
            DefaultContext db = DBInitialization.InitializeDB();
            _ordersService = new OrdersService(db);
            _goodsService = new GoodsService(db);
        }

        #region order
        // GET: Home
        public ActionResult Index()
        {
            var ordersList = _ordersService.GetAllOrders();
            return View(ordersList);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return View(_ordersService.CreateOrder());
            }
            Order order = _ordersService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.IsClose)
            {
                return RedirectToAction("Details", order);
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _ordersService.AddSaveOrder(order);
                return RedirectToAction("Index");
            }
            return View(order);

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = _ordersService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = _ordersService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Order order = _ordersService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            _ordersService.DeleteOrder(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DownloadXml(int? id)
        {
            var xmlDict = _ordersService.GetXmlDictById(id);
            if (xmlDict["fileBytes"] == null)
            {
                return HttpNotFound();
            }
            return File((byte[])xmlDict["fileBytes"], "xml", (string)xmlDict["fileName"]);
        }
        #endregion

        #region goods
        public ActionResult Goods()
        {
            var goodsList = _goodsService.GetAllGoods();
            return View(goodsList);
        }

        [HttpGet]
        public ActionResult EditNomenclature(int? id)
        {
            if (id == null)
            {
                return View(_goodsService.CreateNomenclature());
            }
            Nomenclature nom = _goodsService.GetNomenclatureById(id);
            if (nom == null)
            {
                return HttpNotFound();
            }
            return View(nom);
        }

        [HttpPost]
        public ActionResult EditNomenclature(Nomenclature nom)
        {
            if (ModelState.IsValid)
            {
                _goodsService.AddSaveNomenclature(nom);
                return RedirectToAction("Goods");
            }
            return View(nom);
        }

        #endregion

        #region report
        [HttpGet]
        public ActionResult Report(Nomenclature nom)
        {
            var sales = _ordersService.GenerateSalesReport();
            return View(sales);
        }
        #endregion
    }
}
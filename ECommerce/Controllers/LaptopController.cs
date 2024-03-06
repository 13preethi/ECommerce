using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using LaptopLibrary;

namespace ECommerce.Controllers
{
    public class LaptopController : Controller
    {
        // GET: Laptop
        public ActionResult Index()
        {
            LaptopDAL dal = new LaptopDAL();
            List<Laptop> list = dal.GetLaptopList();
            List<LaptopModel> laptopModels = new List<LaptopModel>();
            foreach (Laptop laptop in list)
            {
                laptopModels.Add(new LaptopModel { Id = laptop.Id, Brand = laptop.Brand, Processor=laptop.Processor,Operating_System=laptop.Operating_System,Price=laptop.Price });

            }
            return View(laptopModels);
        }

        // GET: Laptop/Details/5
        public ActionResult Details(int id)
        {
            int LaptopId = id;

            LaptopDAL dal = new LaptopDAL();
            Laptop laptop = new Laptop();
            laptop = dal.FindLaptop(LaptopId);
            LaptopModel model = new LaptopModel();
            model.Id = laptop.Id;
            model.Brand = laptop.Brand;
            model.Processor = laptop.Processor;
            model.Operating_System = laptop.Operating_System;
            model.Price = laptop.Price;
            return View(model);
        }

        // GET: Laptop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Laptop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                LaptopDAL dal = new LaptopDAL();


                Laptop laptop = new Laptop();
                laptop.Brand = collection["Brand"];
                laptop.Processor = collection["Processor"];
                laptop.Operating_System = collection["OperatingSystem"];
               
                laptop.Price = Convert.ToDouble(collection["Price"]);

                dal.AddLaptop(laptop);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }

        // GET: Laptop/Edit/5
        public ActionResult Edit(int id)
        {
            int LaptopId = id;

            LaptopDAL dal = new LaptopDAL();
            Laptop laptop = new Laptop();
            laptop = dal.FindLaptop(LaptopId);
            LaptopModel model = new LaptopModel();
            model.Id = laptop.Id;
            model.Brand = laptop.Brand;
            model.Processor = laptop.Processor;
            model.Operating_System = laptop.Operating_System;
            model.Price = laptop.Price;
            return View(model);
        }

        // POST: Laptop/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            bool completed = false;
            try
            {
                LaptopDAL dal = new LaptopDAL();
                Laptop laptop = new Laptop();
                laptop.Id = id;
                laptop.Brand = collection["Brand"];
                laptop.Processor = collection["Processor"];
                laptop.Operating_System = collection["OperatingSystem"];

                laptop.Price = Convert.ToDouble(collection["Price"]);
                completed = dal.EditLaptop(laptop,id);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
            if (completed)
                return RedirectToAction("Index");
            else

                return View();
        }

        // GET: Laptop/Delete/5
        public ActionResult Delete(int id)
        {
            int LaptopId = id;

            LaptopDAL dal = new LaptopDAL();
            Laptop laptop = new Laptop();
            laptop = dal.FindLaptop(LaptopId);
            LaptopModel model = new LaptopModel();
            model.Id = laptop.Id;
            model.Brand = laptop.Brand;
            model.Processor = laptop.Processor;
            model.Operating_System = laptop.Operating_System;
            model.Price = laptop.Price;
            return View(model);
        }

        // POST: Laptop/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            bool completed = false;
            try
            {
                // TODO: Add delete logic here
                LaptopDAL dal = new LaptopDAL();
                int laptopid = id;
                completed = dal.RemoveLaptop(laptopid);
                if (completed)
                {
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
            return View();
        }
        public ActionResult AddToCart()
        {
            LaptopDAL dal = new LaptopDAL();
            List<Laptop> list = dal.GetLaptopList();
            List<LaptopModel> laptopModels = new List<LaptopModel>();
            foreach (Laptop laptop in list)
            {
                laptopModels.Add(new LaptopModel { Id = laptop.Id, Brand = laptop.Brand, Processor = laptop.Processor, Operating_System = laptop.Operating_System, Price = laptop.Price });

            }
            return View(laptopModels);
        }
        public ActionResult SelectedLaptop(int id)
        {
            int LaptopId = id;

            LaptopDAL dal = new LaptopDAL();
            Laptop laptop = new Laptop();
            laptop = dal.FindLaptop(LaptopId);
            LaptopModel model = new LaptopModel();
            model.Id = laptop.Id;
            model.Brand = laptop.Brand;
            model.Processor = laptop.Processor;
            model.Operating_System = laptop.Operating_System;
            model.Price = laptop.Price;
            TempData["Price"] = model.Price;
            TempData["Brand"] = model.Brand;
            TempData.Keep();
            return View(model);
        }

        [HttpPost]
        public ActionResult SelectedLaptop(int qty, int id)
        {
            if (TempData["Brand"] != null && TempData["Price"] != null)
            {
                string brand = TempData["Brand"].ToString();
                float price = Convert.ToSingle(TempData["Price"]);
                float Total_Amt = price * qty;
                TempData["Total_amt"] = Total_Amt;

                TempData.Keep();
                return RedirectToAction("Payment");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Payment()
        {
            ViewBag.TotalAmount = Convert.ToSingle(TempData["Total_amt"]);

            return View();
        }
    }
}

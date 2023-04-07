using Pcompany_task.Data_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pcompany_task.Controllers
{
    public class Product_MasterController : Controller
    {
        // GET: Product_Master
        public ActionResult Index()
        {
            ViewBag.State = Category_Data();
            return View();
        }
        public ActionResult SaveOrUpdate(Product_table model)
        {

            using (Pcompany_taskEntities db = new Pcompany_taskEntities())
            {
                if (model.Product_id == 0)
                {
                    Product_table obj = new Product_table()
                    {
                        Category_id = model.Category_id,
                        Product_name = model.Product_name,
                        Price = model.Price,
                    };
                    db.Entry(obj).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                }
                else
                {
                    Product_table obj = new Product_table()
                    {
                        Product_id = model.Product_id,
                        Category_id = model.Category_id,
                        Product_name = model.Product_name,
                        Price = model.Price,
                    };
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }

        }

        public static List<SelectListItem> Category_Data()
        {
            var _selectList = new List<SelectListItem>();
            _selectList.Add(new SelectListItem { Value = "0", Text = "--Select--" });
            var get_State = (dynamic)null;
            try
            {
                using (Pcompany_taskEntities _Db = new Pcompany_taskEntities())
                {
                    get_State = (from x in _Db.Categories
                                 select new
                                 {
                                     x.Id,
                                     x.Category1
                                 });
                    foreach (var item in get_State)
                    {
                        _selectList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Category1.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (get_State != null)
                    get_State = null;
            }
            return _selectList;
        }
        public ActionResult Report()
        {
            using (Pcompany_taskEntities db = new Pcompany_taskEntities())
            {
                ViewBag.State = Category_Data();
                return View(db.sp_product_report().ToList());
            }
        }
        public ActionResult Edit(int Id)
        {
            try
            {
                Product_table obj;
                using (Pcompany_taskEntities db = new Pcompany_taskEntities())
                {
                    var Getdata = new Product_table();
                    Getdata = db.Product_table.Where(x => x.Product_id == Id).FirstOrDefault();
                    obj = new Product_table()
                    {
                        Product_id = Getdata.Product_id,
                        Category_id = Getdata.Category_id,
                        Product_name = Getdata.Product_name,
                        Price = Getdata.Price,
                    };

                }
                ViewBag.State = Category_Data();
                return View("Index", obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DELETE(int id)
        {
            using (Pcompany_taskEntities db = new Pcompany_taskEntities())
            {
                var Get_Data = new Product_table();
                Get_Data = db.Product_table.Where(x => x.Product_id == id).FirstOrDefault();
                db.Product_table.Remove(Get_Data);
                db.SaveChanges();
                var lis = db.Product_table.ToList();
                return RedirectToAction("Index", lis);

            }
        }
    }
}
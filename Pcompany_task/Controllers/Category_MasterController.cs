using Pcompany_task.Data_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pcompany_task.Controllers
{
    public class Category_MasterController : Controller
    {
        // GET: Category_Master
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult Save(Category ctg)
        {

            using (Pcompany_taskEntities db = new Pcompany_taskEntities())
            {

                if (ctg.Id == 0)
                {
                    var obj = new Category();
                    obj = db.Categories.Where(x => x.Category1 == ctg.Category1).FirstOrDefault();
                    if (obj == null)
                    {
                        Category _catg = new Category()
                        {
                            Category1 = ctg.Category1,
                        };
                        db.Entry(ctg).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                    }
                    else
                    {
                        TempData["Error"] = "Oops Something wrong !!!";
                    };
                }
                else
                {
                    Category _catg = new Category()
                    {
                        Id = ctg.Id,
                        Category1 = ctg.Category1,
                    };
                    db.Entry(ctg).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult Report()
        {
            using (Pcompany_taskEntities db = new Pcompany_taskEntities())
            {
                return View(db.sp_category_report().ToList());
            }
        }
        public ActionResult Edit(int Id)
        {
            try
            {
                Category obj;
                using(Pcompany_taskEntities db = new Pcompany_taskEntities())
                {
                    var Getdata = new Category();
                    Getdata = db.Categories.Where(x => x.Id == Id).FirstOrDefault();
                    obj = new Category()
                    {
                        Id = Getdata.Id,
                        Category1= Getdata.Category1,
                    };

                }
                return View("Index", obj);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DELETE(int id)
        {
          
            using(Pcompany_taskEntities db = new Pcompany_taskEntities())
            {
                var Get_Data = new Category();
                Get_Data = db.Categories.Where(x => x.Id == id).FirstOrDefault();
                db.Categories.Remove(Get_Data);
                db.SaveChanges();
                var lis = db.Categories.ToList();
                return RedirectToAction("Index", lis);
               
            }
        }
        
    }
}
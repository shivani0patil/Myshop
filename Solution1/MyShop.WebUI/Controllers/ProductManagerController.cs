using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemeory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemeoryRepository<product> context;
        InMemeoryRepository<ProductCategory> productCategories;
       

        public ProductManagerController()
        {
            context = new InMemeoryRepository<product>();
            productCategories = new InMemeoryRepository<ProductCategory>();
        }
        // GET: ProductManager


        public ActionResult Index()
        {
            List<product> pro = context.Collection().ToList();
            return View(pro);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.pr = new product();
            viewModel.ProductCategories = productCategories.Collection();
           
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(product p)
        {
            if(!ModelState.IsValid)
            {
                return View(p);
            }
            else
            {
                context.Insert(p);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            product p = context.Find(Id);
            if(p==null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.pr = p;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(product p,string Id)
        {
            product productEdit = context.Find(Id);
            
            if (productEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
               if(!ModelState.IsValid)
                {
                    return View(p);
                }

                productEdit.Category = p.Category;
                productEdit.Description = p.Description;
                productEdit.Image = p.Image;
                productEdit.Name = p.Name;
                productEdit.price = p.price;

                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            product productDelete = context.Find(Id);
            if (productDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            product productDelete = context.Find(Id);
            if (productDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                return RedirectToAction("Index");
            }
        }

    }
}
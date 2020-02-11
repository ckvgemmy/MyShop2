using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using MyShop.DataAccess.InMemory;

/// <summary>
/// 1.controller for products. define the productrepository which is located in inmemory folder.
/// 2.initialise the product repository
/// </summary>

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productCategories;

        public ProductManagerController()               // Initialise the ProductRepository and ProductManagerRepository
        {
            context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }



        /// <summary>
        /// 1.this is the default action, index. usually list all products
        /// 2.here we get the list of products from repository then equate it to our context of type productrepository
        /// 3. then we list all the products
        /// </summary>


        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }


        /// <summary>
        /// We want to create a drop down category list when we want to create or edit a product
        /// viewmodel - is a view where we can control what the user sees. Viewmodel is similiar to a model where you define the 
        /// variables
        /// </summary>
       
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();          // An empty Product
            viewModel.ProductCategories = productCategories.Collection();           // Product Category from the database
            return View(viewModel);                                                 // Return viewmodel instead of the product
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {

            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);

            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                //productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Product product, string Id)
        {
            Product productTodelete = context.Find(Id);

            if (productTodelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productTodelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productTodelete = context.Find(Id);

            if (productTodelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}
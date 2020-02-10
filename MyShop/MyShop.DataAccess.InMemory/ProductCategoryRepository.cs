using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;


        /// <summary>
        /// 1.Gotta becareful of the naming convention. ProductCategory in a list is called productCategories. 
        /// 2.Product in a list is called products
        /// </summary>

        public ProductCategoryRepository()
        {
            productCategories = cache["products"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }



        //1.in the productrepository you must add the methode to manipulate the data to the database
        //2.add, delete , edit, find

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }


        /// <summary>
        /// 1.Update product category - create object type Product Category called productCategoryToUpdate and productCategory.
        /// 2.Then we need to find the product based on the id. Find the productCategory from looking at the productcategories(list)
        /// 3.if everything alright, then only assign the value of productCategoryToUpdate to productCategory
        /// </summary>

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }

        }


        /// <summary>
        /// 1.This one , only 1 argument string Id, so find only id.
        /// 2.Maybe because you dont need to edit it only find
        /// </summary>

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }


        //1.iqueryable is suitable for querying data from off memory(database)
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();

        }


        //1. Delete - no need to define another object because no need to assign new value. Just delete it.

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }
    }
}

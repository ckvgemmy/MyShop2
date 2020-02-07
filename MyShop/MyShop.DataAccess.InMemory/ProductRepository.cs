using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{

    /// <summary>
    /// 1.product repository is the place where you store the data in the model
    /// 2.next time when you create alot of products, this is where you store them
    /// 3.caching is a way to store the data in memory for quick retrival, you dont need to ask the database to search for the item each time
    /// </summary>

    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();


        /// <summary>
        /// 1.now you link the list of products to the cache in memory
        /// 2.make sure there is always a productrepository ready on standby, if dont have make a new one, but make sure only ever got one.
        /// </summary>

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        
        
        //1.in the productrepository you must add the methode to manipulate the data to the database
        //2.add, delete , edit, find

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p =>p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            } else
            {
                throw new Exception("Product not found" );
            }

        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }


        //1.iqueryable is suitable for querying data from off memory(database)
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();

        }

        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

    };

    
}

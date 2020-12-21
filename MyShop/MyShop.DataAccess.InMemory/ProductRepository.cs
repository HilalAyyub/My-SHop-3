using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        //creating Objecvt Cash

        ObjectCache cache = MemoryCache.Default;

        // Internal List of Products 

        List<Product> products;


        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            // checking if there is alerady a lsit stored in cacahe 
            if (products == null)
            {
                products = new List<Product>();

            }//end if 
        }

        // when people add products to reposatory it will get added to cache, current list of product  
        public void Commit()
        {
            cache["products"] = products;
        }

        // indivisual endpoins to inser, delete, find and return entire foleder

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            //find product we want to update

            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product no found");
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
                throw new Exception("Product no found");
            }
        }

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
                throw new Exception("Product no found");
            }
        }


    }
}

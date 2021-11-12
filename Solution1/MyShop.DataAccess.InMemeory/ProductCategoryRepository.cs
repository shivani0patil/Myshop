using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemeory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcategory;

        public ProductCategoryRepository()
        {
            productcategory = cache["productcategory"] as List<ProductCategory>;
            if (productcategory == null)
            {
                productcategory = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productcategory"] = productcategory;
        }

        public void Insert(ProductCategory p)
        {
            productcategory.Add(p);
        }

        public void Update(ProductCategory p2)
        {
            ProductCategory productcategoryupdate = productcategory.Find(p => p.Id == p2.Id);
            if (productcategoryupdate != null)
            {
                productcategoryupdate = p2;
            }
            else
            {
                throw new Exception("product no found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productcategoryfind = productcategory.Find(p => p.Id == Id);
            if (productcategoryfind != null)
            {
                return productcategoryfind;
            }
            else
            {
                throw new Exception("product no found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productcategory.AsQueryable();
        }


        public void Delete(string Id)
        {
            ProductCategory productcategoryDelete = productcategory.Find(p => p.Id == Id);
            if (productcategoryDelete != null)
            {
                productcategory.Remove(productcategoryDelete);
            }
            else
            {
                throw new Exception("product no found");
            }
        }
    }
}

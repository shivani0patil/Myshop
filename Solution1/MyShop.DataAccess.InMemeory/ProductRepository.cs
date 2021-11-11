using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemeory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<product> pro;

        public ProductRepository()
        {
            pro = cache["product"] as List<product>;
            if(pro==null)
            {
                pro = new List<product>();           
            }
        }

        public void Commit()
        {
            cache["product"] = pro;
        }

        public void Insert(product p)
        {
            pro.Add(p);
        }

        public void Update(product p2)
        {
            product productupdate = pro.Find(p => p.Id == p2.Id);
            if(productupdate!=null)
            {
                productupdate = p2;
            }
            else
            {
                throw new Exception("product no found");
            }
        }

        public product Find(string Id)
        {
            product productfind = pro.Find(p => p.Id == Id);
            if (productfind!= null)
            {
                return productfind;
            }
            else
            {
                throw new Exception("product no found");
            }
        }

        public IQueryable<product>Collection()
        {
            return pro.AsQueryable();
        }
       

        public void Delete(string Id)
        {
            product productDelete = pro.Find(p => p.Id == Id);
            if (productDelete != null)
            {
                pro.Remove(productDelete);
            }
            else
            {
                throw new Exception("product no found");
            }
        }
    }
}

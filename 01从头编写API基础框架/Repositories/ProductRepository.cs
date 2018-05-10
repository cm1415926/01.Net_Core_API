﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net_Core_API.Entities;

namespace Net_Core_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext _myContext;

        public ProductRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _myContext.Products.OrderBy(x => x.Name).ToList();
        }

        public Product GetProduct(int ProductId, bool includeMaterials)
        {
            if (includeMaterials)
            {
                //这里的  include方法 没有 怎么实现？ 查询有问题
                List<Material> materials = _myContext.Materials.Where(x => x.ProductId == ProductId).ToList();
                if (materials!=null)
                {
                    return _myContext.Products.ToList().Find(x => x.Id == ProductId);
                }
            }
            return _myContext.Products.Find(ProductId);
        }

        public IEnumerable<Material> GetMaterials(int productId)
        {
            //return _myContext.Products.Find(productId).Materials;
            return _myContext.Materials.Where(x => x.ProductId == productId).ToList();
        }

        public Material GetMaterial(int productId, int materialId)
        {
            return _myContext.Materials.FirstOrDefault(x => x.ProductId == productId && x.Id == materialId);
        }

    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    //Paginacion conteo
    public class ProductWithFiltersForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productsParam)
             : base(x =>
         //(!brandId.HasValue || x.ProductBrandId==brandId)&&
         //(!typeId.HasValue || x.ProductTypeId==typeId)//filtra productos por tipo y clase
         (string.IsNullOrEmpty(productsParam.Search) || x.Name.ToLower()
         .Contains(productsParam.Search)) &&
         (!productsParam.BrandId.HasValue || x.ProductBrandId == productsParam.BrandId) &&
         (!productsParam.TypeId.HasValue || x.ProductTypeId == productsParam.TypeId)//filtra productos por tipo y clase
        )
        {

        }
    }
}

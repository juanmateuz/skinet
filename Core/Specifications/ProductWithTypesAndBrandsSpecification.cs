using Core.Entities;

namespace Core.Specifications
{   

    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>  //Core/Specification
    {
        //especificacion de lo que voy a incluir
        //public ProductWithTypesAndBrandsSpecification(string sort,int? brandId,int? typeId)
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams  productsParam)
        :base(x=>
        //(!brandId.HasValue || x.ProductBrandId==brandId)&&
        //(!typeId.HasValue || x.ProductTypeId==typeId)//filtra productos por tipo y clase
        (string.IsNullOrEmpty(productsParam.Search)||x.Name.ToLower()
            .Contains(productsParam.Search)) &&
        (!productsParam.BrandId.HasValue || x.ProductBrandId == productsParam.BrandId) &&
        (!productsParam.TypeId.HasValue || x.ProductTypeId == productsParam.TypeId)//filtra productos por tipo y clase
        )
        {
            AddInclude(x=> x.ProductType);//Metodo en base specification incluir tipo producto de producto
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productsParam.PageSize*(productsParam.PageIndex-1),
             productsParam.PageSize);

            if (!string.IsNullOrEmpty(productsParam.Sort))
            {
                switch (productsParam.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p=>p.Name);
                        break;
                }
            }
        }

        public ProductWithTypesAndBrandsSpecification(int id) 
            : base(x=>x.Id==id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}

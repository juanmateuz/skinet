using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class ProductsController : ControllerBase
    public class ProductsController : BaseApiController // Errores de la api http BaseApiController
    {
        private readonly IMapper _mapper;
        public IGenericRepository<Product> _productRepo { get; }
        public IGenericRepository<ProductBrand> _productBrand { get; }
        public IGenericRepository<ProductType> _productType { get; }

        //private readonly IproductRepository _repo;
        //Llama a la interfaz /Core/Interfaces/IproductRepository        
        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> ProductBrand,//IproductRepository repo
            IGenericRepository<ProductType> productType,IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrand = ProductBrand;
            _productType = productType;
            _mapper = mapper;
            // _repo = repo;
        }

        [HttpGet]
        //Task<ActionResult<List<ProductToReturnDto cambio el list para que coincida con el metodo
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            //var products = await _productRepo.ListAllAsync();
            var spec = new ProductWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            // return Ok(products);
            //return products.Select(products => new ProductToReturnDto
            //{
            //    Id = products.Id,
            //    Name = products.Name,
            //    Description = products.Name,
            //    PictureUrl = products.PictureUrl,
            //    Price = products.Price,
            //    ProductBrand = products.ProductBrand.Name,
            //    ProductType = products.ProductType.Name
            //}).ToList();
            //return Automapping
            return Ok(_mapper
                .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]//especificos en swagger conlos errores
       [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            //var products = await _productRepo.getByIdAsync(id);
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            //return Ok(products);
            //return await _productRepo.GetEntityWithSpec(spec);
            var producto = await _productRepo.GetEntityWithSpec(spec);
            //return new ProductToReturnDto
            //{
            //    Id=producto.Id,
            //    Name=producto.Name,
            //    Description = producto.Name,
            //    PictureUrl = producto.PictureUrl,
            //    Price=producto.Price,
            //    ProductBrand=producto.ProductBrand.Name,
            //    ProductType=producto.ProductType.Name
            //};

            //Si no encontramos productos 404= error notfound

            if (producto == null) return NotFound(new ApiResponse(404));
    
            return _mapper.Map<Product, ProductToReturnDto>(producto);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrand.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productType.ListAllAsync());
        }
    }
}

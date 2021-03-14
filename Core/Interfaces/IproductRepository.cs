using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IproductRepository
    {
        //Interface implementada en Infraestructura/Data/ProductRepository 
        //Interface es un contrato que obliga a la clase que implementa la interfaz
        //a definir todos los metodos que se encuentran en el cuerpo de la Interface
        //La clase tiene q cumplir el contrato
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

    }
}

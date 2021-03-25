using Core.Entities;
using Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{  
    //GenericRepository de Infraestructure/Data
    public interface IGenericRepository<T> where T:BaseEntity //restringimos repositorio a entidades que derivan baseEntity
    {

        Task<T> getByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();//llamado desde el controlador
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T>spec);//Usando metodo especificacion Video39
        Task<int> CountAsync(ISpecification<T> spec);//Contador paginación
    }
}

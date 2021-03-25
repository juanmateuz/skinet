using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    //Repositorio generico |Conexion a la BD
    //Ayuda a minimizar los queries
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity  //restringimos repositorio a entidades que derivan baseEntity
    {
        private readonly StoreContext _context;//contexto conexion a BD

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<T> getByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);//set T envio clase 
        }       

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        //////////////////////////////////////Especificacion include
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync(); 
        }

        //paginacion
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        
    }
}

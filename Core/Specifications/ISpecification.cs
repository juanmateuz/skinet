using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        //expresion tiene una funcion y retorna un booleano
        Expression<Func<T,bool>>Criteria { get; }
        //expresion tiene una funcion y retorna un objeto
        List<Expression<Func<T, object>>> Includes { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>//En Core => BaseSpecification
    {
        //expresion tiene una funcion y retorna un booleano
        Expression<Func<T,bool>>Criteria { get; }
        //expresion tiene una funcion y retorna un objeto
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}

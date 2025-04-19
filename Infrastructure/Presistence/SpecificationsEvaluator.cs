using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery,ISpecifications<TEntity,TKey> specifications) where TEntity:BaseEntity<TKey> 
        {
            var Query = InputQuery ;

            if(specifications.Criteria is not null)
            {
                Query=Query.Where(specifications.Criteria) ;
            }
            if (specifications.OrderBy is not null)

                Query = Query.OrderBy(specifications.OrderBy);

            if (specifications.OrderByDesc is not null)

                Query = Query.OrderByDescending(specifications.OrderByDesc);

            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                Query = specifications.IncludeExpressions.Aggregate(Query, (CurrentQuery, IncExp) => CurrentQuery.Include(IncExp));
            }
            return Query ;

        }
    }
}
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string,object> repositories = [];
        IGenericRepository<TEntity, TKey> IUnitOfWork.GetRepository<TEntity, TKey>()
        {
            var typeName= typeof(TEntity).Name;
            if (repositories.TryGetValue(typeName,out object ?value))
            {
                return (IGenericRepository<TEntity,TKey>)value;
            }
            else
            {
                var Repo= new GenericRepository<TEntity, TKey>(_dbContext);
                //repositories.Add(typeName, Repo);
                repositories["typeName"] =Repo;
                return Repo;

            }

        }

        async Task<int> IUnitOfWork.SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

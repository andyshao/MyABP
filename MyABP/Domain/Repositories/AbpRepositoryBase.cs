using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyABP.Domain.Entities;

namespace MyABP.Domain.Repositories
{
    public abstract class AbpRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region 查询

        public virtual TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                string errMsg = $"There is no such an entity with given primary key: {typeof(TEntity).FullName}, primary key: {id}";
                throw new Exception(errMsg);
            }
            return entity;
        }

        public virtual Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                string errMsg = $"There is no such an entity with given primary key: {typeof(TEntity).FullName}, primary key: {id}";
                throw new Exception(errMsg);
            }
            return entity;
        }

        public abstract IQueryable<TEntity> GetAll();

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            //因为id是泛型，所以不能简单使用这种相等条件进行筛选
            //需要构造Lambda表达式进行查询
            //return GetAll().FirstOrDefault(a => a.Id == id);
            Expression<Func<TEntity, bool>> predicate = CreateEqualityExpressionForId(id);
            return GetAll().FirstOrDefault(predicate);

        }

        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        #endregion

        #region 更新

        public virtual TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 新增

        public abstract TEntity Insert(TEntity entity);

        public virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public virtual TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient() ? Insert(entity) : Update(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }

        public virtual TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        public virtual Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }

        #endregion


        #region 删除

        public abstract void Delete(TPrimaryKey id);


        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(entity);
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 计数

        public virtual int Count()
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual long LongCount()
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> LongCountAsync()
        {
            throw new NotImplementedException();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 构建Lambda表达式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey)));
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}

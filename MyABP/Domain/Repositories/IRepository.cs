using MyABP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Domain.Repositories
{
    /// <summary>
    /// 仓储接口，供所有的仓储实现。通过约定标识仓储。可以使用该接口的泛型版本
    /// </summary>
    public interface IRepository
    {
    }

    /// <summary>
    /// 仓储接口的默认实现，因为默认实体的主键类型是Int
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class,IEntity<int>
    {

    }

    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体主键类型</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class,IEntity<TPrimaryKey>
    {
        #region 查询方法

        /// <summary>
        /// 根据实体类型Id查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体类型</returns>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// 根据实体类型Id查询的异步版本
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体类型</returns>
        Task<TEntity> GetAsync(TPrimaryKey id);

        /// <summary>
        /// 获得该实体类型的IQueryable集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 获取该实体类型的List集合
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 获取该实体类型List集合的异步版本
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// 根据传入的Lambda表达式查询实体类型List集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据传入的Lambda表达式查询实体类型List集合的异步版本
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据id查询实体，若不存在则返回默认值NULL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// 根据id查询实体，若不存在则返回默认值NULL --异步
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        /// <summary>
        /// 根据传入的Lambda表达式查询实体，若为空返回默认值
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据传入的Lambda表达式查询实体，若为空返回默认值 --异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据传入的Lambda表达式获取确切的一个实体，若不存在或多于一个，则抛出异常
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据传入的Lambda表达式获取确切的一个实体，若不存在或多于一个，则抛出异常 --异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 加载指定Id的该实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Load(TPrimaryKey id);

        /// <summary>
        /// 在整个集合中执行一个查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        #endregion

        #region 更新方法

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新实体 -- 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新一个存在的实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <param name="updateAction">更新实体的Action</param>
        /// <returns>需要被更新的实体（不是更新后的实体）</returns>
        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);

        /// <summary>
        /// 更新一个存在的实体 --异步
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <param name="updateAction">更新实体的Action</param>
        /// <returns>需要被更新的实体（不是更新后的实体）</returns>
        Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction);

        #endregion

        #region 插入
        /// <summary>
        /// 插入一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入一个实体 --异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 插入一个实体并返回它的Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>实体Id</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);

        /// <summary>
        /// 插入一个实体并返回它的Id -- 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>实体Id</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        /// <summary>
        /// 新增或更新一个实体。（若为Transient实体则新增，否则更新）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity InsertOrUpdate(TEntity entity);

        /// <summary>
        /// 新增或更新一个实体。（若不存在则新增，存在则更新）--异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        /// <summary>
        /// 新增或更新一个实体并返回实体Id。（若不存在则新增，存在则更新）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);

        /// <summary>
        /// 新增或更新一个实体并返回实体Id。（若不存在则新增，存在则更新）--异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity);

        #endregion

        #region 删除

        /// <summary>
        /// 删除指定Id的实体
        /// </summary>
        /// <param name="id"></param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 删除指定Id的实体 -- 异步
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除一个实体 -- 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 通过Lambda表达式删除满足条件的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过Lambda表达式删除满足条件的实体 --异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region 聚合
        /// <summary>
        /// 统计实体个数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 统计实体个数 -- 异步
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 统计满足Lambda表达式的实体个数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 统计满足Lambda表达式的实体个数 --异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 统计实体个数（若实体个数可能超过int.MaxValue）
        /// </summary>
        /// <returns></returns>
        long LongCount();

        /// <summary>
        /// 统计实体个数（若实体个数可能超过int.MaxValue）--异步
        /// </summary>
        /// <returns></returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 统计满足Lambda表达式的实体个数（若实体个数可能超过int.MaxValue）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 统计满足Lambda表达式的实体个数（若实体个数可能超过int.MaxValue）--异步
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion
    }
}

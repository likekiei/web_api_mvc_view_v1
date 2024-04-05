using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Main_EF.Interface
{
    /// <summary>
    /// 【Main】【interface】通用 Repository
    /// </summary>
    /// <typeparam name="TEntity">Table</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        #region == 檢查相關 ==
        /// <summary>
        /// 檢查是否有任意資料達成條件
        /// </summary>
        /// <param name="predicate">處理式</param>
        /// <returns>「true，任意資料符合」</returns>
        bool Any(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region == 取資料相關 ==
        /// <summary>
        /// 依Table主key取Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(long id);

        /// <summary>
        /// 取Table單筆Data
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 取Table全部Data的Queryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 取Table全部Data過濾後的Queryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAlls(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region == 更新資料相關 ==
        /// <summary>
        /// 添加Data至DbContext (尚未實際寫入DB)
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// 移除DbContext的Data (尚未實際寫入DB)
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// 修改DbContext的Data (尚未實際寫入DB)
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        #endregion

        #region == 其他 ==
        // ...
        #endregion
    }
}

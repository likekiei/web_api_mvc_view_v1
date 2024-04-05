using ERP_EF.Interface;
using ERP_EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_EF.Repository
{
    public class C_ERP_Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private DB_T014Context _context
        {
            get;
            set;
        }

        public C_ERP_Repository()
            : this(new DB_T014Context())
        {
        }

        public C_ERP_Repository(DB_T014Context context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        //public C_ERP_Repository(ObjectContext context)
        //{
        //    if (context == null)
        //    {
        //        throw new ArgumentNullException("context");
        //    }
        //    this._context = new DbContext(context, true);
        //}

        /// <summary>
        /// Creates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }
        }

        //public void Create(List<TEntity> instance)
        //{
        //    if (instance == null)
        //    {
        //        throw new ArgumentNullException("instance");
        //    }
        //    else
        //    {
        //        this._context.Set<TEntity>().AddRange(instance);
        //        this.SaveChanges();
        //    }
        //}

        /// <summary>
        /// Deletes the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                //this._context.Entry(instance).State = EntityState.Deleted;
                var data = this._context.Set<TEntity>().Remove(instance);
                this.SaveChanges();
            }
        }

        ////public void DeleteRange(List<TEntity> instance)
        ////{
        ////    if (instance.Count() == 0)
        ////    {
        ////        throw new ArgumentNullException("instance");
        ////    }
        ////    else
        ////    {
        ////        var data = this._context.Set<TEntity>().RemoveRange(instance);
        ////        //foreach (var item in instance)
        ////        //{
        ////        //    this._context.Entry(item).State = EntityState.Deleted;
        ////        //}                
        ////        this.SaveChanges();
        ////    }

        ////}

        /// <summary>
        /// Get then Delete
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var data = this._context.Set<TEntity>().FirstOrDefault(predicate);
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            else
            {
                this._context.Entry(data).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        ////public void Delete(Expression<Func<TEntity, bool>> predicate, string TableNname)
        ////{
        ////    var data = this._context.Set<TEntity>().Include(TableNname).FirstOrDefault(predicate);
        ////    if (data == null)
        ////    {
        ////        throw new ArgumentNullException("data");
        ////    }
        ////    else
        ////    {
        ////        this._context.Entry(data).State = EntityState.Deleted;
        ////        this.SaveChanges();
        ////    }
        ////}

        ///// <summary>
        ///// Get then Delete
        ///// </summary>
        ///// <param name="predicate">The predicate</param>
        ///// <exception cref="System.NotImplementedException"></exception>
        //public void Deletes(Expression<Func<TEntity, bool>> predicate)
        //{
        //    var data = this._context.Set<TEntity>().Where(predicate);
        //    if (!data.Any())
        //    {
        //        throw new ArgumentNullException("data");
        //    }
        //    else
        //    {
        //        //this._context.Entry(data).State = EntityState.Deleted;
        //        this._context.Set<TEntity>().RemoveRange(data);
        //        this.SaveChanges();
        //    }
        //}

        ////public void Deletes(IEnumerable<TEntity> predicate)
        ////{
        ////    var data = this._context.Set<TEntity>().RemoveRange(predicate);
        ////    if (data == null)
        ////    {
        ////        throw new ArgumentNullException("data");
        ////    }
        ////    else
        ////    {
        ////        //this._context.Entry(data).State = EntityState.Deleted;
        ////        this.SaveChanges();
        ////    }
        ////}

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Gets the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, string tableName)
        {
            return this._context.Set<TEntity>().Include(tableName).FirstOrDefault(predicate);
        }

        //public IQueryable<TEntity> Gets(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return this._context.Set<TEntity>().Where(predicate).AsQueryable();
        //}

        public IQueryable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetAlls(Expression<Func<TEntity, bool>> predicate)
        {
            return this._context.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> GetAlls(Expression<Func<TEntity, bool>> predicate, string tableName)
        {
            return this._context.Set<TEntity>().Include(tableName).Where(predicate).AsQueryable();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }


    }
}

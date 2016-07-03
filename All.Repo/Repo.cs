using All.Core;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace All.Repo
{
    public class Repo<T> where T : class,IBaseData, new()
    {
        public MyDbContext dbContext;

        public User User;

        internal Repo()
        {

        }

        public void Save()
        {
            foreach (var item in dbContext.Set<T>().Local)
            {
                if (IsModified(item))
                {
                    if (item is IChangeTime)
                    {
                        var now = DateTime.Now;
                        (item as IChangeTime).ModifiedOn = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                    }
                    CheckChangeUser(item);
                }

            }
            dbContext.SaveChanges();
        }

        public T Insert(T o, bool saveNow = false)
        {
            var t = o;
            if (t is IChangeTime)
            {
                var now = DateTime.Now;
                (t as IChangeTime).CreatedOn = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                (t as IChangeTime).ModifiedOn = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            }
            CheckChangeUser(t);

            dbContext.Set<T>().Add(t);
            if (saveNow)
            {
                dbContext.SaveChanges();
            }
            return t;
        }

        public virtual void Delete(T o, bool saveNow = false)
        {
            if (o is IDeleted)
            {
                if (IsDetached(o))
                {
                    dbContext.Set<T>().Attach(o);
                }
                (o as IDeleted).Deleted = true;

            }
            else
            {
                if (IsDetached(o))
                {
                    Attach(o);
                }
                dbContext.Set<T>().Remove(o);
            }
            if (saveNow)
            {
                dbContext.SaveChanges();
            }
        }

        public virtual void Delete(IEnumerable<T> list, bool saveNow = false)
        {
            if (typeof(IDeleted).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in list)
                {
                    if (IsDetached(item))
                    {
                        dbContext.Set<T>().Attach(item);
                    }
                    (item as IDeleted).Deleted = true;
                }
            }
            else
            {
                foreach (var item in list)
                {
                    if (IsDetached(item))
                    {
                        Attach(item);
                    }
                }
                dbContext.Set<T>().RemoveRange(list);
            }
            if (saveNow)
            {
                dbContext.SaveChanges();
            }
        }

        public T Get(int id)
        {
            return dbContext.Set<T>().Find(id);
        }
        /// <summary>
        /// 从逻辑删除状态还原为正常状态
        /// </summary>
        /// <param name="o"></param>
        public void Restore(T o, bool saveNow = false)
        {
            if (o is IDeleted)
            {
                if (IsDetached(o))
                {
                    dbContext.Set<T>().Attach(o);
                }
                (o as IDeleted).Deleted = false;
            }
            if (saveNow)
            {
                dbContext.SaveChanges();
            }
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbContext.Set<T>();
        }


        public System.Data.Entity.IDbSet<T> Entites
        {
            get { return dbContext.Set<T>(); }
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="o"></param>
        public void Destroy(T o, bool saveNow = false)
        {
            if (IsDetached(o))
            {
                Attach(o);
            }
            dbContext.Set<T>().Remove(o);
            if (saveNow)
            {
                dbContext.SaveChanges();
            }
        }


        public bool IsAdded(T o)
        {
            return dbContext.Entry(o).State == System.Data.Entity.EntityState.Added;
        }

        public bool IsDetached(T o)
        {
            return dbContext.Entry(o).State == System.Data.Entity.EntityState.Detached;
        }

        public bool IsUnchanged(T o)
        {
            return dbContext.Entry<T>(o).State == System.Data.Entity.EntityState.Unchanged;
        }

        public bool IsDeleted(T o)
        {
            return dbContext.Entry<T>(o).State == System.Data.Entity.EntityState.Deleted;
        }

        public bool IsModified(T o)
        {
            return dbContext.Entry<T>(o).State == System.Data.Entity.EntityState.Modified;
        }

        public void SetPropertyIsModified<TProperty>(T o, Expression<Func<T, TProperty>> property, bool modified)
        {
            dbContext.Entry<T>(o).Property(property).IsModified = modified;
        }

        /// <summary>
        /// 附加
        /// </summary>
        /// <param name="o"></param>
        public void Attach(T o)
        {
            //没在里面才附加
            if (IsDetached(o))
            {
                dbContext.Set<T>().Attach(o);
            }

        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity t) where TEntity : class
        {
            return dbContext.Entry(t);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        public ICollection<T> SqlQuery(string sql, params object[] parameters)
        {
            return dbContext.Database.SqlQuery<T>(sql, parameters).ToList();
        }


        public void InsertRange(IEnumerable<T> items, bool saveNow = false)
        {
            foreach (var item in items)
            {
                var t = item;
                if (t is IChangeTime)
                {
                    var now = DateTime.Now;
                    (t as IChangeTime).CreatedOn = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                    (t as IChangeTime).ModifiedOn = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                }
                CheckChangeUser(t);

            }
            dbContext.Set<T>().AddRange(items);

            if (saveNow)
            {
                dbContext.SaveChanges();
            }
        }

        private void CheckChangeUser(T t)
        {
            if (t is IChangeUser)
            {
                var obj = (t as IChangeUser);
                if (obj.CreatedBy == null || obj.CreatedBy == "")
                {
                    if (User != null)
                    {
                        obj.CreatedBy = User.UserCode;
                    }
                    else
                    {
                        obj.CreatedBy = "system";
                    }
                }

                if (obj.ModifiedBy == null || obj.ModifiedBy == "")
                {
                    if (User != null)
                    {
                        obj.ModifiedBy = User.UserCode;
                    }
                    else
                    {
                        obj.ModifiedBy = "system";
                    }
                }
            }
        }

    }
}

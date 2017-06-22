﻿using Db.EF.DbModel;
using Quick.Framework.EFData;
using Quick.Framework.Tool;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace All.Repo
{
    /// <summary>
    ///     EntityFramework仓储操作基类
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    /// <typeparam name="TKeyType">实体主键类型</typeparam>
    public abstract class EFRepositoryBase<TEntity, TKeyType> where TEntity : EntityBase<TKeyType>
    {
        #region 属性

        /// <summary>
        ///     获取 仓储上下文的实例
        /// </summary>
        [Import]
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     获取 EntityFramework的数据仓储上下文
        /// </summary>
        public virtual UnitOfWorkContextBase EFContext
        {
            get
            {
                if (UnitOfWork is UnitOfWorkContextBase)
                {
                    return UnitOfWork as UnitOfWorkContextBase;
                }
                throw new DataAccessException(string.Format("数据仓储上下文对象类型不正确，应为UnitOfWorkContextBase，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity, TKeyType>(); }
        }

        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="saveNow"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(TEntity entity, bool saveNow = true, bool validateOnSaveEnabled = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterNew<TEntity, TKeyType>(entity);
            return saveNow ? EFContext.Commit(validateOnSaveEnabled) : 0;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="saveNow"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool saveNow = true, bool validateOnSaveEnabled = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            EFContext.RegisterNew<TEntity, TKeyType>(entities);
            return saveNow ? EFContext.Commit(validateOnSaveEnabled) : 0;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="saveNow"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TKeyType id, bool saveNow = true, bool validateOnSaveEnabled = false)
        {
            PublicHelper.CheckArgument(id, "id");
            TEntity entity = EFContext.Set<TEntity, TKeyType>().Find(id);
            return entity != null ? Delete(entity, saveNow, validateOnSaveEnabled) : 0;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="saveNow"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(TEntity entity, bool saveNow = true, bool validateOnSaveEnabled = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterDeleted<TEntity, TKeyType>(entity);
            return saveNow ? EFContext.Commit() : 0;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="saveNow"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool saveNow = true, bool validateOnSaveEnabled = true)
        {
            PublicHelper.CheckArgument(entities, "entities");
            EFContext.RegisterDeleted<TEntity, TKeyType>(entities);
            return saveNow ? EFContext.Commit(validateOnSaveEnabled) : 0;
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="saveNow"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool saveNow = true, bool validateOnSaveEnabled = false)
        {
            PublicHelper.CheckArgument(predicate, "predicate");
            List<TEntity> entities = EFContext.Set<TEntity, TKeyType>().Where(predicate).ToList();
            return entities.Count > 0 ? Delete(entities, saveNow, validateOnSaveEnabled) : 0;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="o"></param>
        public virtual int Destroy(TKeyType key, bool saveNow = true, bool validateOnSaveEnabled = true)
        {
            TEntity o = GetByKey(key);

            EFContext.DbContext.Set<TEntity>().Remove(o);

            return saveNow ? EFContext.Commit(validateOnSaveEnabled) : 0;
        }
        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="saveNow"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Update(TEntity entity, bool saveNow = true, bool validateOnSaveEnabled = true)
        {
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterModified<TEntity, TKeyType>(entity);
            return saveNow ? EFContext.Commit(validateOnSaveEnabled) : 0;
        }

        /// <summary>
        /// 使用附带新值的实体信息更新指定实体属性的值
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="saveNow">是否执行保存</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        /// <returns>操作影响的行数</returns>
        public int Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, bool saveNow = true, bool validateOnSaveEnabled = false)
        {
            //throw new NotSupportedException("上下文公用，不支持按需更新功能。");
            PublicHelper.CheckArgument(propertyExpression, "propertyExpression");
            PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterModified<TEntity, TKeyType>(propertyExpression, entity);
            if (saveNow)
            {
                var dbSet = EFContext.Set<TEntity, TKeyType>();
                dbSet.Local.Clear();
                var entry = EFContext.DbContext.Entry(entity);
                return EFContext.Commit(validateOnSaveEnabled);
            }
            return 0;
        }

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(TKeyType key)
        {
            PublicHelper.CheckArgument(key, "key");
            return EFContext.Set<TEntity, TKeyType>().Find(key);
        }


        #endregion
    }

}
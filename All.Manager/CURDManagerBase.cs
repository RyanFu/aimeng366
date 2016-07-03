using Db.DbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Omu.ValueInjecter;
using Db.EF.DbModel;
using Db.EF;
using All.Manager;

namespace All.Core
{
    /// <summary>
    /// 普通增删改查基类
    /// </summary>
    public class CURDManagerBase<T> : ManagerBase<T>
        where T : class,IBaseData, new()
    {
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="item"></param>
        /// <param name="saveNow"></param>
        /// <returns></returns>
        public virtual ValidationResult Create(T item, bool saveNow = true)
        {
            var result = CheckRepeat(item, true);

            if (result != null)
            {
                return result;
            }
            Repo.Insert(item);
            if (saveNow)
            {
                try
                {
                    Repo.Save();
                }
                catch (Exception e)
                {
                    return new ValidationResult(e.Message);
                }
            }
            return result;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual ValidationResult Save()
        {
            try
            {
                Repo.Save();
            }
            catch (Exception e)
            {
                return new ValidationResult(e.Message);
            }
            return null;
        }
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            return Repo.Get(id);
        }
        /// <summary>
        /// 获取所有  
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return Repo.GetAll();
        }
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> func)
        {
            return Repo.Where(func);
        }
        /// <summary>
        /// 更新  只能更新简单类型和string  不能更新引用类型
        /// </summary>
        /// <param name="t"></param>
        /// <param name="SaveNow"></param>
        /// <returns></returns>
        public virtual ValidationResult Update(T t, bool SaveNow = true)
        {
            var result = CheckRepeat(t, false);
            if (result != null)
            {
                return result;
            }
            Repo.Get(t.Id).InjectFrom(t);
            if (SaveNow)
            {
                Repo.Save();
            }
            return result;
        }
        /// <summary>
        /// 销货（彻底删除） 需呀带id   或者这个对象受ef管理的对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveNow"></param>
        public virtual ValidationResult Destroy(T t, bool saveNow = true)
        {
            Repo.Destroy(t);
            if (saveNow)
            {
                try
                {
                    Repo.Save();
                }
                catch (Exception e)
                {
                    return new ValidationResult(e.Message);
                }
            }
            return null;

        }
        /// <summary>
        /// 简单获取分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetPage(int pageIndex, int pageSize)
        {
            return GetAll().Skip(pageIndex * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 添加和修改的时候检查
        /// </summary>
        /// <param name="t"></param>
        /// <param name="IsNew"></param>
        /// <returns></returns>
        public virtual ValidationResult CheckRepeat(T t, bool IsNew)
        {
            return null;
        }

    }
}

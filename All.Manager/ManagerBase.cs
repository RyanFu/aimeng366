using All.Core;
using All.Repo;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace All.Manager
{
    public class ManagerBase<T>
        where T : class,IBaseData,new ()
    {
        Repo<T> repo = WebRepoFactory.CreateRepo<T>(new MyDbContext());

        public Repo<T> Repo
        {
            get
            {
                return repo;
            }
        }
    }


}

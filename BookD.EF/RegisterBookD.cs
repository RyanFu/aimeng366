using Db.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookD.EF
{
    /// <summary>
    /// 注册本模块 模型
    /// </summary>
    public class RegisterBookD
    {
        public RegisterBookD()
        {
            ModelMapRegister.RegisterModelAction += RegisterBookDModel;
        }

        private void RegisterBookDModel(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            
        }
    }
}

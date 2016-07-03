using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQ.EF
{
    /// <summary>
    /// 注册qq实体
    /// </summary>
    public class RegisterQQ
    {
        public RegisterQQ()
        {
            Db.EF.ModelMapRegister.RegisterModelAction += RegisterBookDModel;
        }

        private void RegisterBookDModel(System.Data.Entity.DbModelBuilder modelBuilder)
        {
           
        }
    }
}

using All.Core;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace All.Manager
{
    public class UserManager : CURDManagerBase<User>
    {
        public override ValidationResult CheckRepeat(User t, bool IsNew)
        {
            if (IsNew)
            {
                if (Repo.GetAll().Any(r => r.UserCode == t.UserCode))
                {
                    return new ValidationResult("帐号已注册!");
                }
            }

            return base.CheckRepeat(t, IsNew);
        }
    }
}

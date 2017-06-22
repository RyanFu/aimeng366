using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.EF.DbModel
{
    public abstract class KeyModel<TKeyType>
    {
        public virtual TKeyType Id { get; set; }
    }
}

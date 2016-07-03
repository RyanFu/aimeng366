using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.EF.DbModel
{
    public class ResourceBase
    {
        public string ResourceId { get; set; }
        public ResourceFromSite ResourceFromSite { get; set; }
    }
}

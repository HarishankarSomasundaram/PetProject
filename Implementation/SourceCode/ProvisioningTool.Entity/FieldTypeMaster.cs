using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class FieldTypeMaster  : Audit
    {
        public FieldTypeMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int FieldTypeMasterID { get; set; }
        public string FieldTypeName { get; set; }
    }
}

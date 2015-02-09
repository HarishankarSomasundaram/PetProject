
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class Role : Audit
    {
        public Role()
        {
            //
            // TODO: Role Add constructor logic here
            //
        }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public string oper { get; set; }
        public int id { get; set; }
    }
}

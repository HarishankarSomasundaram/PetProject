using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class Documents: Audit
    {
        public Documents()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int DocumentID { get; set; }
        public string Type { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public int ReferenceID { get; set; }
    }
}

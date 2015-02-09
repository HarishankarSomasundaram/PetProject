using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class Mail : Audit
    {

        public Mail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Replyto { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyEncoding { get; set; }
        public string IsBodyHtml { get; set; }
        public string BodyContent { get; set; }
        public string PageName { get; set; }
        public int PageIdentity { get; set; }
    }
}

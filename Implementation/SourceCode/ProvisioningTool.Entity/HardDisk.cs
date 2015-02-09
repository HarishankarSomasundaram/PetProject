using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class HardDisk : Audit
    {

        public HardDisk()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int SystemHardDiskID { get; set; }
        public string HardDiskName { get; set; }
        public int Size { get; set; }
        public string SizeUnit { get; set; }

        public List<HardDiskDrive> HardDiskDrive { get; set; }
    }
}

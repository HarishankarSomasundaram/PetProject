using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemHardDrive
    {
        public SystemHardDrive()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemHardDriveID { get; set; }
        
        public int SystemID { get; set;}
        public string HardDriveDetails { get; set; }
        public int Size {  get; set;}
        public string SizeUnit { get; set; }
        public HardDisk HardDrive { get; set; }
        public List<HardDisk> DiskDetails { get; set; }
        public int Quantity { get; set; }
    }
}

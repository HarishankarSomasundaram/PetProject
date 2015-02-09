using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class SystemDisk : Audit
    {
        public SystemDisk()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int SystemHardDiskDriveID { get; set; }
        public int SystemHardDiskID { get; set; }
        public string DriveCharacter { get; set; }
        public int Size { get; set; }
        public string SizeUnit { get; set; }
    }
}

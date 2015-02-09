using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class HardDiskDrive : Audit
    {

        public HardDiskDrive()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string oper { get; set; }
        public int id { get; set; }

        public int SystemHardDiskDriveID { get; set; }
        public int SystemHardDiskID { get; set; }
        public string DriveCharacter { get; set; }
        public int Size { get; set; }
        public string SizeUnit { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class ServerHardware : Audit
    {
        public ServerHardware()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string oper { get; set; }
        public int id { get; set; }

        public int ServerHardwareID { get; set; }
        public string ModelName { get; set; }
        public string Manufacturer { get; set; }
        public int Core { get; set; }

        public string HostName { get; set; }
        public string SerialNumber { get; set; }

        public GlobalMasterDetail CPU { get; set; }
        public int CPUID { get; set; }
        public string CPUName { get; set; }

        public List<SystemMemory> Memorys { get; set; }
        public GlobalMasterDetail Memory { get; set; }
        public int MemoryID { get; set; }
        public string MemoryName { get; set; }
        public string MemoryIDs { get; set; }

        public GlobalMasterDetail MotherBoard { get; set; }
        public int MotherBoardID { get; set; }
        public string MotherBoardName { get; set; }

        public List<SystemHardDrive> HardDrives { get; set; }
        public GlobalMasterDetail HardDrive { get; set; }
        public int HardDriveID { get; set; }
        public string HardDriveName { get; set; }
        public string HardDriveIDs { get; set; }

        public GlobalMasterDetail Chipset { get; set; }
        public int ChipsetID { get; set; }
        public string ChipsetName { get; set; }

        public List<SystemVideoCard> VideoCards { get; set; }
        public GlobalMasterDetail VideoCard { get; set; }
        public int VideoCardID { get; set; }
        public string VideoCardName { get; set; }
        public string VideoCardIDs { get; set; }

        public List<SystemDisplay> Displays { get; set; }
        public GlobalMasterDetail Display { get; set; }
        public int DisplayID { get; set; }
        public string DisplayName { get; set; }
        public string DisplayIDs { get; set; }

        public List<SystemMultimedia> Multimedias { get; set; }
        public GlobalMasterDetail Multimedia { get; set; }
        public int MultimediaID { get; set; }
        public string MultimediaIDs { get; set; }
        public string MultimediaIDName { get; set; }

        public List<SystemPort> Ports { get; set; }
        public GlobalMasterDetail Port { get; set; }
        public int PortID { get; set; }
        public string PortIDs { get; set; }
        public string PortName { get; set; }

        public List<SystemSlot> Slots { get; set; }
        public GlobalMasterDetail Slot { get; set; }
        public int SlotID { get; set; }
        public string SlotIDs { get; set; }
        public string SlotName { get; set; }

        public GlobalMasterDetail Chassis { get; set; }
        public int ChassisID { get; set; }
        public string ChassisName { get; set; }

        public List<SystemPower> Powers { get; set; }
        public GlobalMasterDetail Power { get; set; }
        public int PowerID { get; set; }
        public string PowerIDs { get; set; }
        public string PowerName { get; set; }


        public NotesMaster Notes { get; set; }
        public string FullNotes { get; set; }
        public int SiteID { get; set; }

        public string View { get; set; }

    }
}

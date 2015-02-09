using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningTool.Entity
{
    public class User : Audit
    {
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public GlobalMasterDetail Title { get; set; }
        public int TitleID { get; set; }
        public string TitleName { get; set; }

        public GlobalMasterDetail Department { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        
        //GlobalMasterDetail
        public List<UserApp> UserAppsList { get; set; }
        public string SelectedApps { get; set; }
        public List<UserSecurityGroup> UserSecurityGroupList { get; set; }
        public string SelectedSecurityGroup { get; set; }
        public List<UserTablet> UserTabletList { get; set; }
        public string SelectedTablet { get; set; }
        public List<UserRemoteAccess> UserRemoteAccessList { get; set; }
        public string SelectedRemoteAccess { get; set; }


        
        public List<WorkStationInfo> UserComputerList { get; set; }
        public string SelectedComputer { get; set; }

        public List<MobileDevice> UserMobilePhoneList { get; set; }
        public string SelectedMobilePhone { get; set; }

        public List<Printer> UserPrinterList { get; set; }
        public string SelectedPrinter { get; set; }

        public List<LaptopInfo> UserLaptopList { get; set; }
        public string SelectedLaptop { get; set; }

        public List<NetworkShare> UserNetworkSharesList { get; set; }
        public string SelectedNetworkShares { get; set; }
        
        public List<ServerInfo> UserServersList { get; set; }
        public string SelectedServers { get; set; }
        

       
        public string View { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Notes { get; set; }
        public string Template { get; set; }
        
        public string oper { get; set; }
        public int id { get; set; }

        public bool IsAutoTask { get; set; }
        public string MappingID { get; set; }
    }
}

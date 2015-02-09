
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProvisioningTool.Entity
{

    public class HistoryTracker 
    {
        public HistoryTracker()
        {
            //
            // TODO: HistoryTracker Add constructor logic here
            //
        }
        
        public int HistoryTrackerID { get; set; }
        public string TrackerValue { get; set; }
        public int HistoryMasterID { get; set; }
        public string HistoryMasterName { get; set; }
        public string HistoryFieldName { get; set; }
        public int HistoryFieldID { get; set; }
        public int HistoryMasterFieldMappingID { get; set; }
        public bool ISForward { get; set; }
        public string HistoryHtml { get; set; }
        public string prevhref { get; set; }
        public string nexthref { get; set; }
        public int TableID { get; set; }
        public bool IsGlobalMaster { get; set; }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
namespace ProvisioningTool.Entity
{
    public class NotesMaster : Audit
    {
        public NotesMaster()
        {
            //
            // TODO: NotesMaster Add constructor logic here
            //
        }
        public int NotesMasterID { get; set; }
        public string NotesMasterName { get; set; }
        public List<NotesDetail> NotesDetailList { get; set; }
        public NotesDetail NotesDetails { get; set; }
        
    }
}

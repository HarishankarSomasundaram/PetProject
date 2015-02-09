using System;
namespace ProvisioningTool.Entity
{
    public class NotesDetail : Audit
    {
        public NotesDetail()
        {
            // TODO: NotesDetail Add constructor logic here
        }
        public int NotesDetailID { get; set; }
        public int NotesMasterID { get; set; }
        public int NotesClientID { get; set; }
        public string Notes { get; set; }
        public int isFromIOS { get; set; }

    }
}

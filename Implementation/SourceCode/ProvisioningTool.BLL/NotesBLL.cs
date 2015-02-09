using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;
using ProvisioningTool.Entity;

namespace ProvisioningTool.BLL
{
    public class NotesBLL
    {
        #region [ Variable Declarations ]
        PTResponse response;
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        bool isUpdated;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public NotesBLL()
        {
            dataAdapter = new DalAdapter();
            response = new PTResponse();
            this.rowsAffected = 0;
            this.isDuplicate = false;
            this.isUpdated = false;
        }
        #endregion [ Constructor ]


        #region [ Update Notes ]
        public PTResponse UpdateNotes(PTRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException("Invalid Request Recieved or Request is null");
                if (request.CurrentAction == ActionType.Edit)
                {
                    try
                    {
                        if (CheckAttributes(request))
                        {
                            if (request.CurrentAction == ActionType.Edit)
                            {
                                dataAdapter.ModifyNotes(request.NotesMaster, out isUpdated, out rowsAffected);

                                if (rowsAffected == 1)
                                {
                                    response.isSuccess = true;
                                    response.Message = "Feedback has been updated successfully.";
                                }
                                else if (rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while updating Feedback.";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return response;
            }
            finally { }
        }
        #endregion [ Update Notes ]

        private bool CheckAttributes(PTRequest request)
        {
            return true;
        }
    }
}

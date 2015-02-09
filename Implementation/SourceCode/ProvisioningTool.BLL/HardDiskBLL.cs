using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvisioningTool.DAL;

namespace ProvisioningTool.BLL
{
    public class HardDiskBLL
    {
        #region [ Variable Declarations ]
        DalAdapter dataAdapter;
        int rowsAffected;
        bool isDuplicate;
        int HardDiskID;
        PTResponse response;
        #endregion [ Variable Declarations ]

        #region [ Constructor ]
        public HardDiskBLL()
        {
            dataAdapter = new DalAdapter();
            this.rowsAffected = 0;
            this.HardDiskID = 0;
            this.isDuplicate = false;
            response = new PTResponse();
        }
        #endregion [ Constructor ]

        #region [ GetAllHardDisks ]
        public PTResponse GetAllHardDisks()
        {
            try
            {
                response.HardDiskList = dataAdapter.GetAllHardDisks();
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
        #endregion [ GetAllHardDisks ]

        #region [ Get Hard Drive by Hard Drive ID ]
        public PTResponse GetHardDiskByHardDiskID(int HardDiskID)
        {
            try
            {
                response.HardDisk = dataAdapter.GetHardDiskByHardDiskID(HardDiskID);
                if (response.HardDisk == null)
                    response.isSuccess = false;
                else
                    response.isSuccess = true;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion [ Get Hard Drive by Hard Drive ID ]

        #region [Delete  Hard Drive  ]
        public bool DeleteHardDiskByHardDiskID(int HardDiskID)
        {

            try
            {
                return dataAdapter.DeleteHardDiskByHardDiskID(HardDiskID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion  [Delete Hard Drive ]

        #region [ Save Hard Drive ]
        public PTResponse SaveHardDisk(PTRequest request)
        {
            try
            {
                if (request == null) throw new ArgumentNullException("Invalid Request Recieved or Request is null");
                if (request.CurrentAction == ActionType.Add || request.CurrentAction == ActionType.Edit)
                {
                    try
                    {
                        if (CheckAttributes(request))
                        {
                            if (request.CurrentAction == ActionType.Add)
                            {
                                dataAdapter.AddHardDisk(request, out isDuplicate, out rowsAffected, out HardDiskID);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    List<HardDiskDrive> hdDriveList = request.HardDisk.HardDiskDrive;
                                    request.HardDiskDrive = new HardDiskDrive();
                                    if (hdDriveList != null && hdDriveList.Count > 0)
                                    {
                                        foreach (HardDiskDrive hdd in hdDriveList)
                                        {
                                            request.HardDiskDrive.DriveCharacter = hdd.DriveCharacter;
                                            request.HardDiskDrive.Size = hdd.Size;
                                            request.HardDiskDrive.SizeUnit = hdd.SizeUnit;
                                            request.HardDiskDrive.StatusID = 1;
                                            request.HardDiskDrive.CreatedBy = request.HardDisk.CreatedBy;
                                            request.HardDiskDrive.ModifiedBy = request.HardDisk.ModifiedBy;
                                            request.HardDiskDrive.CreatedOn = request.HardDisk.CreatedOn;
                                            request.HardDiskDrive.ModifiedOn = request.HardDisk.ModifiedOn;
                                            request.HardDiskDrive.SystemHardDiskID = HardDiskID;

                                            dataAdapter.AddHardDiskDrive(request, out isDuplicate, out rowsAffected);
                                        }
                                    }
                                    response.isSuccess = true;
                                    response.Message = "Hard Drive has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Hard Drive already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Hard Drive...";
                                }
                            }
                            else
                            {
                                dataAdapter.ModifyHardDisk(request, out isDuplicate, out rowsAffected);

                                if (!isDuplicate && rowsAffected == 1)
                                {
                                    List<HardDiskDrive> hdDriveList = request.HardDisk.HardDiskDrive;
                                    request.HardDiskDrive = new HardDiskDrive();
                                    if (hdDriveList != null && hdDriveList.Count > 0)
                                    {
                                        foreach (HardDiskDrive hdd in hdDriveList)
                                        {
                                            request.HardDiskDrive.DriveCharacter = hdd.DriveCharacter;
                                            request.HardDiskDrive.Size = hdd.Size;
                                            request.HardDiskDrive.SizeUnit = hdd.SizeUnit;
                                            request.HardDiskDrive.StatusID = 1;
                                            request.HardDiskDrive.CreatedBy = request.HardDisk.CreatedBy;
                                            request.HardDiskDrive.ModifiedBy = request.HardDisk.ModifiedBy;
                                            request.HardDiskDrive.CreatedOn = request.HardDisk.CreatedOn;
                                            request.HardDiskDrive.ModifiedOn = request.HardDisk.ModifiedOn;
                                            request.HardDiskDrive.SystemHardDiskID = request.HardDisk.SystemHardDiskID;

                                            dataAdapter.AddHardDiskDrive(request, out isDuplicate, out rowsAffected);
                                        }
                                    }
                                    response.isSuccess = true;
                                    response.Message = "Hard Drive has been saved successfully.";
                                }
                                else if (isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Hard Drive already exist(s).";
                                }
                                else if (!isDuplicate && rowsAffected == 0)
                                {
                                    response.isSuccess = false;
                                    response.Message = "Error while saving Hard Drive.";
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
        #endregion [ Save Hard Drive ]

        #region [ Private Function ]
        private bool CheckAttributes(PTRequest request)
        {
            return true;
        }
        #endregion [ Private Function ]
    }
}

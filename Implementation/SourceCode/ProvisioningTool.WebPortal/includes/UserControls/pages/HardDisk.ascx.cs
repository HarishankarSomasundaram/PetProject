using Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Newtonsoft.Json;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System.IO;

public partial class UserControlsHardDisk : UCController
{
    #region [ Variable Declarations ]
    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;
    string baseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    string masterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    #endregion [ Variable Declarations ]

    #region [ Page Load Event ]

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DetermineAction();
                if (CurrentAction != ActionType.MoreView)
                    Page.Validate();

                if (Request.QueryString["isColorBox"] != null)
                {
                    btnBack.Style.Add("display", "none");
                    //PageBody.Attributes.Add("class", "colorbox-parent");
                }
            }
            if (Request.QueryString["do"] != null )
            {
                CrudHardDisk.Visible = true;
                grdHardDisks.Visible = false;
            }
            else {
                CrudHardDisk.Visible = false;
                grdHardDisks.Visible = true;
            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    #endregion [ Page Load Event ]

    #region [ Private Funtion ]

    private void DetermineAction()
    {
        try
        {
            //InetilizeIframe(CrudHardDisk, grdHardDisks);

            if (CurrentAction == ActionType.Add)
            {
                btnSubmit.Visible = true;
                CrudHardDisk.Visible = true;
                grdHardDisks.Visible = false;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                btnSubmit.Visible = true;
                CrudHardDisk.Visible = true;
                grdHardDisks.Visible = false;
                ModifyHardDisk();
            }
            else{
                btnSubmit.Visible = false;
                CrudHardDisk.Visible = false;
                grdHardDisks.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    private void ModifyHardDisk()
    {
        try
        {

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETHARDDISK";
                request.HardDisk = new ProvisioningTool.Entity.HardDisk();
                request.HardDisk.SystemHardDiskID = ConvertHelper.ConvertToInteger(base.Id);
                request.URL = serviceURL;
            }

            webServiceHelper = new WebServiceHelper();
            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.HardDisk != null)
            {
                txtHardDiskName.Text = response.HardDisk.HardDiskName;
                txtSize.Text = ConvertHelper.ConvertToString(response.HardDisk.Size, "");
                ddlSizeUnit.SelectedValue = ddlSizeUnit.Items.FindByText(response.HardDisk.SizeUnit).Value;
                List<HardDiskDrive> hdDriveList = response.HardDisk.HardDiskDrive;
                if (hdDriveList != null && hdDriveList.Count > 0)
                {
                    foreach (HardDiskDrive hdd in hdDriveList)
                    {
                        if (txtDriveDetail.Text == "")
                            txtDriveDetail.Text = hdd.DriveCharacter + " Drive : " + ConvertHelper.ConvertToString(hdd.Size, "0") + " " + hdd.SizeUnit + ",";
                        else
                            txtDriveDetail.Text = txtDriveDetail.Text + "," + hdd.DriveCharacter + " Drive : " + ConvertHelper.ConvertToString(hdd.Size, "0") + " " + hdd.SizeUnit;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void ClearALL()
    {
        try
        {
            txtDrive.Text = string.Empty;
            txtDriveDetail.Text = string.Empty;
            txtHDSize.Text = string.Empty;
            txtHDSize.Text = string.Empty;
            txtSize.Text = string.Empty;
            ddlHDSizeUnit.SelectedIndex = 0;
            ddlSizeUnit.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //Check Duplicate Hard Drive Name 
    private bool CheckDuplication()
    {
        try
        {
            PTRequest checkrequest = new PTRequest();
            PTResponse checkresponse = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string url = string.Empty;

            //Framing the URL
            url = string.Format(PostServiceURL + "{0}", "GETHARDDISKLIST");
            checkrequest.URL = url;

            checkrequest.HardDisk = new ProvisioningTool.Entity.HardDisk();
            checkresponse = webServiceHelper.PostRequest<PTResponse>(checkrequest);
            if (checkresponse != null)
            {
                List<ProvisioningTool.Entity.HardDisk> hardDiskList = checkresponse.HardDiskList;
                foreach (ProvisioningTool.Entity.HardDisk harddisk in hardDiskList)
                {
                    if (CurrentAction == ActionType.Add)
                    {
                        if (harddisk.HardDiskName.Trim() == txtHardDiskName.Text.Trim())
                            return false;
                    }
                    if (CurrentAction == ActionType.Edit)
                    {
                        if (harddisk.HardDiskName.Trim() == txtHardDiskName.Text.Trim() && harddisk.SystemHardDiskID != ConvertHelper.ConvertToInteger(base.Id))
                            return false;
                    }
                }

            }
            return true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }

    //Check Hard Drive and Hard Drive Capacity
    private bool CheckHardDiskDetails(List<HardDiskDrive> hddList)
    {
        try
        {
            int hardDiskSize, hardDriveSize, iKey;
            hardDiskSize = hardDriveSize = iKey = 0;
            Hashtable drive = new Hashtable();
            hardDiskSize = SizeCalculator(ConvertHelper.ConvertToInteger(txtSize.Text), ConvertHelper.ConvertToString(ddlSizeUnit.SelectedItem.Value, ""));

            if (hardDriveSize > hardDiskSize)
            {
                ShowMessage("Hard Drive does not have required size", false);
                return false;
            }
            else
                return true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }



    //Size Calculator
    private int SizeCalculator(int Size, string sSizeUnit)
    {
        int iSize = 0;
        if (sSizeUnit == "GB")
            iSize = Size;
        else if (sSizeUnit == "TB")
            iSize = Size * 1000;
        return iSize;
    }

    #endregion [ Private Funtion ]

    #region [ Control Event ]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string url = string.Empty;

            //Framing the URL
            url = string.Format(PostServiceURL + "{0}", "SAVEHARDDISK");
            request.URL = url;

            request.HardDisk = new ProvisioningTool.Entity.HardDisk();


            request.HardDisk.Size = ConvertHelper.ConvertToInteger(txtSize.Text, 0);
            request.HardDisk.HardDiskName = ConvertHelper.ConvertToString(txtHardDiskName.Text, "");
            request.HardDisk.SizeUnit = ConvertHelper.ConvertToString(ddlSizeUnit.SelectedItem.Value, "");

            if (txtDriveDetail.Text != "")
            {
                List<HardDiskDrive> hddList = new List<HardDiskDrive>();
                HardDiskDrive hdd = new HardDiskDrive();

                string hddListFull = txtDriveDetail.Text;
                string[] hddLists = hddListFull.Split(',');

                for (int iLoop = 0; iLoop < hddLists.Length-1; iLoop++)
                {
                    hdd = new HardDiskDrive();
                    hdd.DriveCharacter = hddLists[iLoop].Substring(0, 1);
                    string[] temp = hddLists[iLoop].Split(':');
                    if (temp[1] != null)
                    {
                        hdd.SizeUnit = temp[1].TrimEnd().Substring(temp[1].Length - 2, 2);
                        temp[1] = temp[1].Trim().Substring(0, temp[1].Length - 3);
                        hdd.Size = ConvertHelper.ConvertToInteger(temp[1], 0);
                    }
                    hdd.StatusID = 1;
                    hdd.CreatedBy = currentUser.ApplicationUserID;
                    hdd.ModifiedBy = currentUser.ApplicationUserID;
                    hddList.Add(hdd);
                }


                request.HardDisk.HardDiskDrive = hddList;
            }
            else
                request.HardDisk.HardDiskDrive = null;

            request.HardDisk.StatusID = 1;
            request.HardDisk.CreatedBy = currentUser.ApplicationUserID;
            request.HardDisk.ModifiedBy = currentUser.ApplicationUserID;
            request.CurrentAction = CurrentAction;

            if (CurrentAction == ActionType.Edit)
            {
                request.HardDisk.SystemHardDiskID = ConvertHelper.ConvertToInteger(base.Id);
            }
            response = new PTResponse();
            if (CheckDuplication())
            {
                if (CheckHardDiskDetails(request.HardDisk.HardDiskDrive))
                {
                    response = webServiceHelper.PostRequest<PTResponse>(request);
                    if (response != null)
                    {
                        ShowMessage(response.Message, response.isSuccess);
                        if (response.isSuccess)
                        {
                            ClearALL();
                            CrudHardDisk.Visible = false;
                            grdHardDisks.Visible = true;
                        }
                    }
                    else
                    {
                        ShowMessage("Error While Adding the Hard Drive", false);
                    }
                }
            }
            else
                ShowMessage("Hard Drive Name already available.", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        grdHardDisks.Visible = true;
        CrudHardDisk.Visible = false;
    }
    #endregion [ Control Event ]
}
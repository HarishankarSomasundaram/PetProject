using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library;
using ProvisioningTool.Entity;

public partial class UserControlsNetworkShare : UCController
{
    #region [ Declartion ]

    int iControlCount;
    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;

    #endregion [ Declartion ]

    #region [ Page Load Event ]

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            divMessage.Attributes["style"] = "display:block";
            DetermineAction();
            if (!Page.IsPostBack && CurrentAction != ActionType.MoreView) { Page.Validate(); }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    #endregion [ Page Load Event ]

    #region [Determine Action]

    private void DetermineAction()
    {
        InitializeIframe(divCrudNetworkShare, divGrdNetworkShare);

        if (CurrentAction == ActionType.Add)
        {
            PopulateControls();
            btnSSubmit.Visible = true;
            divCrudNetworkShare.Visible = true;
            divGrdNetworkShare.Visible = false;
        }
        else if (CurrentAction == ActionType.Edit)
        {
            PopulateControls();
            ModifyNetworkShareDetails();
            netTabContent.Attributes.Add("class", netTabContent.Attributes["class"] + " netShareTab");
            btnSSubmit.Visible = true;
            divCrudNetworkShare.Visible = true;
            divGrdNetworkShare.Visible = false;
        }
        else if (CurrentAction == ActionType.View)
        {
            divCrudNetworkShare.Visible = false;
            divGrdNetworkShare.Visible = true;
        }
        else if (CurrentAction == ActionType.MoreView)
        {
            PopulateControls();
            ModifyNetworkShareDetails();
            DisableControls(divNetworkShareDetail);
            divNetworkShareDetail.Attributes.Add("class", divNetworkShareDetail.Attributes["class"] + " viewPage");

            btnSSubmit.Visible = false;
            divCrudNetworkShare.Visible = true;
            divGrdNetworkShare.Visible = false;
            btnSBack.Visible = true;
            btnSBack.Enabled = true;
        }
    }

    #endregion[Determine Action]

    #region [ Controls Events ]

    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        SetDropDownValue();
        AddControlUsingLinkButton();
    }

    private void lnkAddNew_Click(object sender, EventArgs e)
    {
        SetDropDownValue();
        AddControlUsingLinkButton();
    }

    private void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            RemoveControls(ConvertHelper.ConvertToInteger(hidControlsCount.Text));
            hidControlsCount.Text = ConvertHelper.ConvertToString(ConvertHelper.ConvertToInteger(hidControlsCount.Text) - 1);
            if (hidControlsCount.Text == "0")
                lnkAddMore.Visible = true;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnSSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();

            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string statusMessage = string.Empty;
            serviceURL = PostServiceURL;
            serviceName = "SAVENETWORKSHARE";

            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;

            request.NetworkShare = new NetworkShare();
            request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.NetworkShare.NetworkShareName = ConvertHelper.ConvertToString(txtNetworkShareName.Text, "");
            request.NetworkShare.NetworkShareDetail = ConvertValueToNSDetailList();

            request.NetworkShare.StatusID = 1;
            request.NetworkShare.CreatedBy = currentUser.ApplicationUserID;
            request.NetworkShare.ModifiedBy = currentUser.ApplicationUserID;
            request.NetworkShare.CreatedOn = DateTime.Now;
            request.NetworkShare.ModifiedOn = DateTime.Now;

            request.NetworkShare.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);

            request.CurrentAction = CurrentAction;
            if (CurrentAction == ActionType.Edit)
            {
                request.NetworkShareDetail = new NetworkShareDetail();
                request.NetworkShareDetail.NetworkShareDetailID = ConvertHelper.ConvertToInteger(base.Id);
                request.NetworkShareDetail.Path = ConvertHelper.ConvertToString(txtPath.Text);
                request.NetworkShareDetail.Mapped = ConvertHelper.ConvertToString(txtMapped.Text);
                request.NetworkShareDetail.NetworkShareDescription = ConvertHelper.ConvertToString(txtDescription.Text);
                request.NetworkShareDetail.NetworkShareAssignedUserIDs = ConvertHelper.ConvertToString(hidmulddlAUsers.Text);
            }
            response = new PTResponse();
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
                if (response.isSuccess)
                {
                    ClearAll();
                    divCrudNetworkShare.Visible = false;
                    //netTabContent.Attributes.Remove("class");
                    divGrdNetworkShare.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnSBack_Click(object sender, EventArgs e)
    {
        ClearAll();
        ShowMessage("", false);
        divCrudNetworkShare.Visible = false;
        //netTabContent.Attributes.Remove("class");
        divGrdNetworkShare.Visible = true;

    }

    #endregion [ Controls Events ]

    #region [ Private Methods ]

    #region [ Populate Controls ]

    private void PopulateControls()
    {
        try
        {
            BindAssignedUserDropdown(ddlAUsers);
            if (hidControlsCount.Text == "")
                iControlCount = 0;
            else
            {
                iControlCount = ConvertHelper.ConvertToInteger(hidControlsCount.Text, 0);
                for (int i = 1; i <= iControlCount; i++)
                {
                    AddingControls(i, true);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Populate Controls ]

    #region [ Adding Controls in Dynamically ]

    //Adding Controls Dynamically
    private void AddingControls(int iNumber, bool ISLoad)
    {
        try
        {
            if (iNumber != 0)
            {
                //Creating Common Div Tag for the Controls
                HtmlGenericControl div1 = new HtmlGenericControl("div");
                div1.ID = "div" + iNumber;

                #region [ Mapped ]

                //Creating First internal div 
                HtmlGenericControl divCommon1 = new HtmlGenericControl("div");
                divCommon1.Attributes.Add("class", "inlineProperty");

                ////Creating Label in the First Part of the Controls
                HtmlGenericControl labelMap = new HtmlGenericControl("Label");
                labelMap.InnerText = "Mapped";

                //Adding Label
                divCommon1.Controls.Add(labelMap);

                //Creating Text box in the First Part of the Controls
                TextBox txtMapped1 = new TextBox();
                txtMapped1.ID = "txtMapped" + iNumber;
                txtMapped1.Attributes.Add("class", "watermark");
                txtMapped1.Attributes.Add("placeholder", "Mapped");
                txtMapped1.Attributes.Add("ClientIDMode", "Static");
                txtMapped1.Attributes.Add("MaxLength", "1");



                //Adding Text Box
                divCommon1.Controls.Add(txtMapped1);



                //Creating Required Field Validator in the First Part of the Controls
                RequiredFieldValidator rfvMapped1 = new RequiredFieldValidator();
                rfvMapped1.ID = "rfvMapped" + iNumber;
                rfvMapped1.ControlToValidate = txtMapped1.ID;
                rfvMapped1.Display = ValidatorDisplay.Dynamic;
                rfvMapped1.ValidationGroup = "SReq";
                rfvMapped1.ErrorMessage = "*";

                //Adding Validator
                labelMap.Controls.Add(rfvMapped1);

                #endregion [ Mapped ]

                #region [ Path ]

                //Creating Second internal div 
                HtmlGenericControl divCommon2 = new HtmlGenericControl("div");
                divCommon2.Attributes.Add("class", "inlineProperty");

                //Creating Label in the Second Part of the Controls
                HtmlGenericControl labelPath = new HtmlGenericControl("Label");
                labelPath.InnerText = "Path";

                //Adding Label
                divCommon2.Controls.Add(labelPath);

                //Creating Text box in the Second Part of the Controls
                TextBox txtPath1 = new TextBox();
                txtPath1.ID = "txtPath" + iNumber;
                txtPath1.Attributes.Add("class", "watermark");
                txtPath1.Attributes.Add("placeholder", "Path");
                txtPath1.Attributes.Add("ClientIDMode", "Static");
                txtPath1.Attributes.Add("MaxLength", "100");

                //Adding Text Box
                divCommon2.Controls.Add(txtPath1);


                //Creating Required Field Validator in the Second Part of the Controls
                RequiredFieldValidator rfvPath1 = new RequiredFieldValidator();
                rfvPath1.ID = "rfvPath" + iNumber;
                rfvPath1.ControlToValidate = txtPath1.ID;
                rfvPath1.Display = ValidatorDisplay.Dynamic;
                rfvPath1.ValidationGroup = "SReq";
                rfvPath1.ErrorMessage = "*";

                //Adding Validator
                labelPath.Controls.Add(rfvPath1);

                #endregion [ Path ]

                #region [ Description ]
                //Creating Thrid internal div 
                HtmlGenericControl divCommon3 = new HtmlGenericControl("div");
                divCommon3.Attributes.Add("class", "inlineProperty");

                //Creating Label in the Thrid Part of the Controls
                HtmlGenericControl labelDescription = new HtmlGenericControl("Label");
                labelDescription.InnerText = "Description";

                //Adding Label
                divCommon3.Controls.Add(labelDescription);

                //Creating Text box in the Thrid Part of the Controls
                TextBox txtDescription1 = new TextBox();
                txtDescription1.ID = "txtDescription" + iNumber;
                txtDescription1.Attributes.Add("class", "watermark");
                txtDescription1.Attributes.Add("placeholder", "Description");
                txtDescription1.Attributes.Add("ClientIDMode", "Static");
                txtDescription1.Attributes.Add("MaxLength", "100");


                //Adding Text Box
                divCommon3.Controls.Add(txtDescription1);

                //Creating Required Field Validator in the Thrid Part of the Controls
                RequiredFieldValidator rfvDescription1 = new RequiredFieldValidator();
                rfvDescription1.ID = "rfvDescription" + iNumber;
                rfvDescription1.ControlToValidate = txtDescription1.ID;
                rfvDescription1.Display = ValidatorDisplay.Dynamic;
                rfvDescription1.ValidationGroup = "SReq";
                rfvDescription1.ErrorMessage = "*";

                //Adding Validator
                labelDescription.Controls.Add(rfvDescription1);

                #endregion [ Description ]

                #region [ Clear ]

                HtmlGenericControl divClear1 = new HtmlGenericControl("div");
                divClear1.Attributes.Add("class", "clear");

                HtmlGenericControl divClear2 = new HtmlGenericControl("div");
                divClear2.Attributes.Add("class", "clear");
                #endregion [ Clear ]

                #region [ Assigned User ]

                //Creating Thrid internal div 
                HtmlGenericControl divCommon4 = new HtmlGenericControl("div");
                divCommon4.Attributes.Add("class", "inlineProperty firstColumn");

                //Creating Label in the Thrid Part of the Controls
                HtmlGenericControl labelAssigned = new HtmlGenericControl("Label");
                labelAssigned.InnerText = "Assigned User";

                //Adding Label
                divCommon4.Controls.Add(labelAssigned);

                DropDownList ddlAUser = new DropDownList();
                ddlAUser.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                ddlAUser.ID = "ddlAUser" + iNumber;
                ddlAUser.Attributes.Add("class", "chosen-select-width AUser" + iNumber);
                ddlAUser.Attributes.Add("multiple", "multiple");

                //Adding DropDownList
                divCommon4.Controls.Add(ddlAUser);

                BindAssignedUserDropdown(ddlAUser);                

                HiddenField hidmulddlAusers1 = new HiddenField();
                hidmulddlAusers1.ID = "hidmulddlAUsers" + iNumber;
                hidmulddlAusers1.ClientIDMode = System.Web.UI.ClientIDMode.Static;

                //Adding DropDownList
                divCommon4.Controls.Add(hidmulddlAusers1);

                #endregion [ Assigned User ]

                #region [ Add and Delete Link Button ]

                HtmlGenericControl divCommon5 = new HtmlGenericControl("div");
                divCommon5.Attributes.Add("class", "inlineProperty secondColumn");

                LinkButton lnkAddNew = new LinkButton();
                lnkAddNew.ID = "lnkAddNew" + ConvertHelper.ConvertToString(iNumber, "");
                lnkAddNew.Text = "Add More";
                lnkAddNew.CssClass = "myLinkButton";
                lnkAddNew.Attributes.Add("Style", "margin-top: 25px;min-width:13px !important");
                lnkAddNew.Visible = true;
                lnkAddNew.Click += lnkAddNew_Click;

                LinkButton lnkDelete = new LinkButton();
                lnkDelete.ID = "lnkDelete" + ConvertHelper.ConvertToString(iNumber, "");
                lnkDelete.Text = "Delete";
                lnkDelete.Visible = true;
                lnkDelete.CssClass = "myLinkButton";
                lnkDelete.Attributes.Add("Style", "margin-top: 25px;min-width:13px !important");
                lnkDelete.Click += lnkDelete_Click;



                if (lnkAddMore.Visible == true)
                    lnkAddMore.Visible = false;
                else
                {
                    LinkButton lnkAdd = (LinkButton)this.FindControl("lnkAddNew" + ConvertHelper.ConvertToString(iNumber - 1, ""));
                    if (lnkAdd != null)
                        lnkAdd.Visible = false;
                    LinkButton lnkDel = (LinkButton)this.FindControl("lnkDelete" + ConvertHelper.ConvertToString(iNumber - 1, ""));
                    if (lnkDel != null)
                        lnkDel.Visible = false;
                }

                //Adding Add More Link Button to Div Tag
                divCommon5.Controls.Add(lnkAddNew);

                //Adding Delete Link Button to Div Tag
                divCommon5.Controls.Add(lnkDelete);

                #endregion [ Add and Delete Link Button ]

                #region [ Adding Div in Full Div ]
                //Adding First internal div 
                div1.Controls.Add(divCommon1);

                //Adding Second internal div 
                div1.Controls.Add(divCommon2);

                //Adding Thrid internal div 
                div1.Controls.Add(divCommon3);

                //Adding Clear div 
                div1.Controls.Add(divClear1);

                //Adding Forth internal div 
                div1.Controls.Add(divCommon4);

                //Adding Fifth internal div 
                div1.Controls.Add(divCommon5);

                //Adding Clear div 
                div1.Controls.Add(divClear2);
                #endregion [ Adding Div in Full Div ]

                //Adding Common Div Tag for the Controls
                phControl.Controls.Add(div1);
                div1.Visible = true;
                if (!ISLoad)
                {
                    txtMapped1.Text = string.Empty;
                    txtPath1.Text = string.Empty;
                    txtDescription1.Text = string.Empty;
                    ddlAUser.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    //Removing Controls Dynamically
    private void RemoveControls(int iNumber)
    {
        HtmlGenericControl div1 = (HtmlGenericControl)this.FindControl("div" + ConvertHelper.ConvertToString(iNumber, ""));
        if (div1 != null)        
            div1.Visible = false; 

        LinkButton lnkAdd = (LinkButton)this.FindControl("lnkAddNew" + ConvertHelper.ConvertToString(iNumber - 1, ""));
        if (lnkAdd != null)
            lnkAdd.Visible = true;       

        LinkButton lnkDel = (LinkButton)this.FindControl("lnkDelete" + ConvertHelper.ConvertToString(iNumber - 1, ""));
        if (lnkDel != null)
            lnkDel.Visible = true;

    }

    #endregion [ Adding Controls in Dynamically ]

    #region [  Add Controls Using Link Button Event ]

    private void AddControlUsingLinkButton()
    {
        try
        {
            int iCount;
            iCount = 0;
            //Increament Count of Control Avaiable
            if (hidControlsCount.Text == "")
            {
                hidControlsCount.Text = "1";
                iCount = 1;
            }
            else
            {
                iCount = ConvertHelper.ConvertToInteger(hidControlsCount.Text, 0);
                iCount = iCount + 1;
                hidControlsCount.Text = ConvertHelper.ConvertToString(iCount);
            }
            if (iCount != 0)
                AddingControls(iCount, false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [  Add Controls Using Link Button Event ]

    #region [ Drop Down Bind Event ]

    private void BindAssignedUserDropdown(DropDownList ddl)
    {
        try
        {
            #region [GE ALL USERS AND POPULATE]
            response = new PTResponse();
            request = new PTRequest();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            serviceURL = PostServiceURL + "POPULATEALLUSERS";
            request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.UserList != null && response.UserList.Count > 0)
            {
                PopulateUserDropDownList(ddl, response.UserList, true);
            }
            #endregion
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Drop Down Bind Event ]

    #region [ Convert the Value to Network Share Details List ]

    private List<NetworkShareDetail> ConvertValueToNSDetailList()
    {
        List<NetworkShareDetail> networkShareDetailList = new List<NetworkShareDetail>();

        try
        {
            NetworkShareDetail networkShareDetail = new NetworkShareDetail();

            networkShareDetail.Path = ConvertHelper.ConvertToString(txtPath.Text);
            networkShareDetail.Mapped = ConvertHelper.ConvertToString(txtMapped.Text);
            networkShareDetail.NetworkShareDescription = ConvertHelper.ConvertToString(txtDescription.Text);
            networkShareDetail.NetworkShareAssignedUserIDs = ConvertHelper.ConvertToString(hidmulddlAUsers.Text);

            networkShareDetail.StatusID = 1;
            networkShareDetail.CreatedBy = currentUser.ApplicationUserID;
            networkShareDetail.ModifiedBy = currentUser.ApplicationUserID;
            networkShareDetail.CreatedOn = DateTime.Now;
            networkShareDetail.ModifiedOn = DateTime.Now;

            networkShareDetailList.Add(networkShareDetail);

            if (hidControlsCount.Text != "")
            {
                iControlCount = ConvertHelper.ConvertToInteger(hidControlsCount.Text, 0);
                for (int i = 1; i <= iControlCount; i++)
                {
                    networkShareDetail = new NetworkShareDetail();

                    TextBox txtPaths = (TextBox)this.FindControl("txtPath" + ConvertHelper.ConvertToString(i, ""));
                    if (txtPaths != null)
                        networkShareDetail.Path = ConvertHelper.ConvertToString(txtPaths.Text);

                    TextBox txtMappeds = (TextBox)this.FindControl("txtMapped" + ConvertHelper.ConvertToString(i, ""));
                    if (txtMappeds != null)
                        networkShareDetail.Mapped = ConvertHelper.ConvertToString(txtMappeds.Text);

                    TextBox txtDescriptions = (TextBox)this.FindControl("txtDescription" + ConvertHelper.ConvertToString(i, ""));
                    if (txtDescriptions != null)
                        networkShareDetail.NetworkShareDescription = ConvertHelper.ConvertToString(txtDescriptions.Text);

                    HiddenField hidmulddlAUserss = (HiddenField)this.FindControl("hidmulddlAUsers" + ConvertHelper.ConvertToString(i, ""));
                    if (hidmulddlAUserss != null)
                        networkShareDetail.NetworkShareAssignedUserIDs = ConvertHelper.ConvertToString(hidmulddlAUserss.Value);

                    networkShareDetail.StatusID = 1;
                    networkShareDetail.CreatedBy = currentUser.ApplicationUserID;
                    networkShareDetail.ModifiedBy = currentUser.ApplicationUserID;
                    networkShareDetail.CreatedOn = DateTime.Now;
                    networkShareDetail.ModifiedOn = DateTime.Now;

                    networkShareDetailList.Add(networkShareDetail);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

        return networkShareDetailList;

    }

    #endregion [ Convert the Value to Network Share Details List ]

    #region [ Clear all Controls ]

    private void ClearAll()
    {
        txtDescription.Text = string.Empty;
        txtMapped.Text = string.Empty;
        txtNetworkShareName.Text = string.Empty;
        txtPath.Text = string.Empty;
        txtNetworkShareName.ReadOnly = false;
        lnkAddMore.Visible = true;
    }

    #endregion [ Clear all Controls ]

    #region [ Modify Network Share Details ]

    private void ModifyNetworkShareDetails()
    {
        try
        {
            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETNETWORKSHAREDETAILBYNETWORKSHAREDETAILID";
                request.NetworkShareDetail = new NetworkShareDetail();
                request.NetworkShareDetail.NetworkShareDetailID = ConvertHelper.ConvertToInteger(base.Id);
                hidEditID.Value = ConvertHelper.ConvertToString(base.Id);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.NetworkShareDetail != null)
            {
                txtNetworkShareName.Text = response.NetworkShareDetail.NetworkShareName;
                txtNetworkShareName.ReadOnly = true;
                txtPath.Text = response.NetworkShareDetail.Path;
                txtDescription.Text = response.NetworkShareDetail.NetworkShareDescription;
                txtPath.ToolTip = response.NetworkShareDetail.Path;
                txtDescription.ToolTip = response.NetworkShareDetail.NetworkShareDescription;
                

                if (response.NetworkShareDetail.NetworkShareAssignedUserIDs != null)
                    BindMultiSelectDropDownWithSelectedValues(ddlAUsers, hidmulddlAUsers, response.NetworkShareDetail.NetworkShareAssignedUserIDs.Replace('|', ','));
                lnkAddMore.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion  [ Modify Network Share Details ]

    #region [ Set Drop down Value ]

    private void SetDropDownValue()
    {
        if (hidmulddlAUsers.Text != "")
            SetMultiSelectDropDown(ddlAUsers, hidmulddlAUsers.Text);
        if (hidControlsCount.Text == "")
            iControlCount = 0;
        else
        {
            iControlCount = ConvertHelper.ConvertToInteger(hidControlsCount.Text, 0);
            for (int i = 1; i <= iControlCount; i++)
            {
                HiddenField hid = (HiddenField)this.FindControl("hidmulddlAUsers" + i);
                if (hid != null)
                {
                    DropDownList ddl = (DropDownList)this.FindControl("ddlAUser" + i);
                    if (ddl != null)
                    {
                        if (hid.Value != null)
                            SetMultiSelectDropDown(ddl, hid.Value);
                    }
                }
            }
        }
    }
    #endregion [ Set Drop down Value ]

    public void BindMultiSelectDropDownWithSelectedValues(DropDownList ddlMulSelectAttribute, TextBox hidMulSelectDdl, string multipleVal)
    {

        if (multipleVal != null && multipleVal != "")
        {
            string sMemory = multipleVal;
            ArrayList arrMemory = new ArrayList(); ;
            arrMemory.AddRange(sMemory.Split(new char[] { ',' }));
            foreach (string s in arrMemory)
            {
                ddlMulSelectAttribute.Items.FindByValue(s).Attributes.Add("selected", "selected");
                if (hidMulSelectDdl.Text != "")
                    hidMulSelectDdl.Text = ConvertHelper.ConvertToString(s);
                else
                    hidMulSelectDdl.Text = ConvertHelper.ConvertToString(hidMulSelectDdl.Text) + "," + ConvertHelper.ConvertToString(s);
            }
        }


    }

    #endregion [ Private Methods ]
}
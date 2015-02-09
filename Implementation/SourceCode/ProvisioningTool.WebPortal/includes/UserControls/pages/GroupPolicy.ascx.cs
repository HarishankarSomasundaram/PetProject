using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library;
using ProvisioningTool.Entity;

public partial class UserControlsGroupPolicy : UCController
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
            PopulateControls();
            FillValues();
            if (!Page.IsPostBack && CurrentAction != ActionType.MoreView) { Page.Validate(); }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    protected void Page_Init()
    {
        PopulateControls();
        FillValues();
    }

    #endregion [ Page Load Event ]

    #region [Populate Controls]

    private void PopulateControls()
    {
        try
        {
            response = new PTResponse();
            request = new PTRequest();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            serviceURL = PostServiceURL + "GETGROUPPOLICYSETUPLIST";
            request.GroupPolicySetup = new GroupPolicySetup();
            request.GroupPolicySetup.SiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.GroupPolicySetupList != null && response.GroupPolicySetupList.Count > 0)
            {
                lblNoGroupPolicy.Text = string.Empty;
                List<GroupPolicySetup> groupPolicySetupList = response.GroupPolicySetupList;

                //Creating Common Div Tag for the Controls
                HtmlGenericControl div1 = new HtmlGenericControl("div");
                div1.ID = "div";

                //Adding Common Div Tag for the Controls
                phControl.Controls.Add(div1);
                div1.Visible = true;

                CreateControls(groupPolicySetupList, div1);

            }
            else
            {
                btnSSubmit.Visible = false;
                lblNoGroupPolicy.Text = "No Group Policy Setup for this Site.";
            }
        }
        catch (Exception ex)
        {
            ShowMessage( ex.Message, false);
        }
    }

    #endregion[Populate Controls]

    #region [ Fill the Control Value ]

    private void FillValues()
    {
        try
        {
            response = new PTResponse();
            request = new PTRequest();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            serviceURL = PostServiceURL + "GETGROUPPOLICYLIST";
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);

            if (response != null && response.GroupPolicyList != null && response.GroupPolicyList.Count > 0)
            {
                List<GroupPolicy> groupPolicy = response.GroupPolicyList;
                foreach (GroupPolicy gp in groupPolicy)
                {
                    Label lbl = (Label)this.FindControl(ConvertHelper.ConvertToString(gp.GroupPolicySetupID));

                    if (lbl != null)
                    {
                        TextBox txt = (TextBox)this.FindControl(lbl.Text);
                        if (txt != null)
                            txt.Text = gp.GroupPolicyFieldValue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage( ex.Message, false);
        }
    }

    #endregion [ Fill the Control Value ]

    #region [ Button Events ]
    protected void btnSSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (DeleteAllGroupPolicy())
                AddAllGroupPolicy(phControl);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    protected void btnSBack_Click(object sender, EventArgs e)
    {
        ShowMessage("", false);
    }
    #endregion [ Button Events ]

    #region [ Private Methods ]

    #region [ Create Controls ]

    private void CreateControls(List<GroupPolicySetup> groupPolicySetupList, HtmlGenericControl div1)
    {
        try
        {
            List<GroupPolicySetup> grpHeading, grpControl;
            grpHeading = groupPolicySetupList;
            grpControl = groupPolicySetupList;
            var groupHeading = grpHeading.Where(gps => gps.FieldType == 1)
                .OrderBy(gps => gps.HeadingCount)
                .ToList<GroupPolicySetup>();

            foreach (GroupPolicySetup gps in groupHeading)
            {
                CreateHeading(gps.HeadingCount, gps.FieldName, div1);
            }

            var groupControl = grpControl.Where(gps => gps.FieldType != 1)
                .OrderBy(gps => gps.FieldCount)
                .ToList<GroupPolicySetup>();

            string sDiv = "";
            HtmlGenericControl divCommon1;
            foreach (GroupPolicySetup gps in groupControl)
            {
                if (!sDiv.Contains("div" + ConvertHelper.ConvertToString(gps.HeadingCount) + "div" + ConvertHelper.ConvertToString(gps.FieldCount)))
                {
                    divCommon1 = new HtmlGenericControl("div");
                    divCommon1.ID = "div" + ConvertHelper.ConvertToString(gps.HeadingCount) + "div" + ConvertHelper.ConvertToString(gps.FieldCount);
                    divCommon1.Attributes.Add("class", "inlineProperty");
                    sDiv = sDiv + "div" + ConvertHelper.ConvertToString(gps.HeadingCount) + "div" + ConvertHelper.ConvertToString(gps.FieldCount);
                    div1.Controls.Add(divCommon1);

                }
                else
                {
                    divCommon1 = (HtmlGenericControl)div1.FindControl("div" + ConvertHelper.ConvertToString(gps.HeadingCount) + "div" + ConvertHelper.ConvertToString(gps.FieldCount));
                }
                if (gps.FieldType == 2)
                    CreateLabel(gps.FieldCount, gps.FieldName, div1, divCommon1, gps.HeadingCount);
                if (gps.FieldType == 3)
                    CreateText(gps.FieldCount, gps.FieldName, div1, divCommon1, gps.GroupPolicySetupID, gps.IsRequired, gps.HeadingCount);
            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void CreateHeading(int fieldCount, string Name, HtmlGenericControl div1)
    {
        try
        {
            //Creating Common Div Tag for the Controls
            HtmlGenericControl divHeading = new HtmlGenericControl("div");
            divHeading.ID = "divHead" + ConvertHelper.ConvertToString(fieldCount);

            HtmlGenericControl h1 = new HtmlGenericControl("h1");
            h1.ID = "h1" + ConvertHelper.ConvertToString(fieldCount);
            h1.InnerHtml = Name;

            divHeading.Controls.Add(h1);

            HtmlGenericControl divClear1 = new HtmlGenericControl("div");
            divClear1.Attributes.Add("class", "clear");

            div1.Controls.Add(divHeading);
            div1.Controls.Add(divClear1);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void CreateLabel(int fieldCount, string Name, HtmlGenericControl div1, HtmlGenericControl divCommon1, int HeadingCount)
    {
        try
        {
            //Creating Label in the First Part of the Controls
            HtmlGenericControl labelMap = new HtmlGenericControl("Label");
            labelMap.ID = "lbl" + HeadingCount.ToString() + fieldCount.ToString();
            labelMap.InnerText = Name;

            //Adding Label
            divCommon1.Controls.Add(labelMap);

            HtmlGenericControl divHeading = (HtmlGenericControl)div1.FindControl("divHead" + HeadingCount);
            divHeading.Controls.Add(divCommon1);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void CreateText(int fieldCount, string Name, HtmlGenericControl div1, HtmlGenericControl divCommon1, int iGroupPolicySetupID, bool ISrequired, int HeadingCount)
    {

        try
        {
            //<asp:RequiredFieldValidator ID="rfgHostName" runat="server"
            //        ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
            //        ValidationGroup="SReq">*</asp:RequiredFieldValidator></label>

            //Creating Text box in the First Part of the Controls
            TextBox txtbox = new TextBox();
            txtbox.ID = Name;
            txtbox.MaxLength = 255;
            txtbox.Attributes.Add("class", "watermark");
            //txtMapped1.Attributes.Add("placeholder", "Mapped");
            txtbox.Attributes.Add("ClientIDMode", "Static");

            Label lbl = new Label();
            lbl.ID = ConvertHelper.ConvertToString(iGroupPolicySetupID);
            lbl.Text = Name;
            lbl.Visible = false;

            Label lbl1 = new Label();
            lbl1.ID = "lblgps" + Name;
            lbl1.Text = ConvertHelper.ConvertToString(iGroupPolicySetupID); ;
            lbl1.Visible = false;

            RequiredFieldValidator rfv = new RequiredFieldValidator();
            rfv.ID = "rfv" + Name;
            rfv.ControlToValidate = txtbox.ID;
            rfv.ErrorMessage = "*";
            
            rfv.ValidationGroup = "GroupPolicy";

            //Adding Controls
            divCommon1.Controls.Add(txtbox);
            divCommon1.Controls.Add(lbl);
            divCommon1.Controls.Add(lbl1);
            if (ISrequired)
            {
                HtmlGenericControl lblShow = (HtmlGenericControl)div1.FindControl("lbl"+ HeadingCount.ToString() + fieldCount.ToString());
                if (lblShow != null)
                    lblShow.Controls.Add(rfv);
            }

            HtmlGenericControl divHeading = (HtmlGenericControl)div1.FindControl("divHead" + HeadingCount);
            divHeading.Controls.Add(divCommon1);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion [ Control Controls ]

    #region [ Add Group Policy ]

    private void AddAllGroupPolicy(Control ctl)
    {
        try
        {
            foreach (Control cntl in ctl.Controls)
            {
                if (cntl is TextBox)
                {
                    Label cntlLabel = (Label)this.FindControl("lblgps" + cntl.ID);
                    TextBox cntlTextBox = (TextBox)cntl;
                    if (cntlLabel != null)
                    {
                        AddGroupPolicy(ConvertHelper.ConvertToInteger(cntlLabel.Text), cntlTextBox.Text);
                    }
                }
                else if (cntl is HtmlGenericControl)
                {
                    AddAllGroupPolicy(cntl);
                }
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    private void AddGroupPolicy(int iGroupPolicySetup, string sGroupPolicyValue)
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
            serviceName = "SAVEGROUPPOLICY";

            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;

            request.GroupPolicy = new GroupPolicy();

            request.GroupPolicy.GroupPolicySetupID = ConvertHelper.ConvertToInteger(iGroupPolicySetup, 0);
            request.GroupPolicy.GroupPolicyFieldValue = ConvertHelper.ConvertToString(sGroupPolicyValue, "");

            request.GroupPolicy.StatusID = 1;
            request.GroupPolicy.CreatedBy = currentUser.ApplicationUserID;
            request.GroupPolicy.ModifiedBy = currentUser.ApplicationUserID;
            request.GroupPolicy.CreatedOn = DateTime.Now;
            request.GroupPolicy.ModifiedOn = DateTime.Now;

            request.CurrentAction = ActionType.Add;

            response = new PTResponse();
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null)
            {
                ShowMessage(response.Message, response.isSuccess);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }
    }

    #endregion

    #region [ Delete All Group Policy ]

    private bool DeleteAllGroupPolicy()
    {
        try
        {
            response = new PTResponse();
            request = new PTRequest();
            webServiceHelper = new WebServiceHelper();
            string serviceURL = string.Empty;
            string url = string.Empty;
            string serviceName = string.Empty;
            string serviceResponseString = string.Empty;

            serviceURL = PostServiceURL + "DELETEGROUPPOLICY";
            request.URL = serviceURL;

            response = webServiceHelper.PostRequest<PTResponse>(request);
            return true;

        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
            return false;
        }
    }
    #endregion [ Delete All Group Policy ]

    #endregion [ Private Methods ]
}
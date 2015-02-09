using Library;
using Newtonsoft.Json;
using ProvisioningTool.BLL;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.Services;
using System.Globalization;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using iTextSharp.text.html;
using System.IO;
using System.Text;

public partial class UserControlsProvisioningCheckList : UCController
{
    #region [ Variable Declarations ]

    PTResponse response;
    PTRequest request;
    WebServiceHelper webServiceHelper;
    string baseServiceURL = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["BaseServiceURL"], "");
    string masterServiceName = ConvertHelper.ConvertToString(ConfigurationManager.AppSettings["MasterServiceName"], "");
    string serviceURL = string.Empty;
    string url = string.Empty;
    string serviceName = string.Empty;
    string serviceResponseString = string.Empty;
    #endregion [ Variable Declarations ]

    protected void Page_Load(object sender, EventArgs e)
    {
        divMessage.Attributes["style"] = "display:block";
        DetermineAction();
        if (!Page.IsPostBack && CurrentAction != ActionType.MoreView) { Page.Validate(); }
    }

    #region [Determine Action]
    private void DetermineAction()
    {
        try
        {
            if (CurrentAction == ActionType.Add)
            {
                divGrdProvisioningCheckListInfo.Visible = false;
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                //btnPrint.Visible = false;
            }
            else if (CurrentAction == ActionType.Edit)
            {
                ModifyCheckList();
                btnSubmit.Visible = true;
                //btnPrint.Visible = false;
                btnBack.Visible = true;
            }
            else if (CurrentAction == ActionType.MoreView)//To view the page without edit
            {
                DisableControls(divProvisioningCheckListDetail);
                ModifyCheckList();
                btnSubmit.Visible = false;
                //btnPrint.Visible = true;
                //btnPrint.Enabled = true;
                btnBack.Visible = true;
                btnBack.Enabled = true;
            }
            else
            {
                CrudProvisioningCheckList.Visible = false;
                divProvisioningCheckListDetail.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Add Checklist]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceURL = PostServiceURL;

            Checklist checklist = new Checklist();
            checklist.User = new User();
            checklist.User.UserID = ConvertHelper.ConvertToInteger(base.Id);
            checklist.UserAccountCreation = ConvertHelper.ConvertToBoolean(hidUserName.Value);
            checklist.AddUserToDepartment = ConvertHelper.ConvertToBoolean(hidDepartment.Value);
            checklist.AddUserToSecurityGroup = ConvertHelper.ConvertToBoolean(hidSecurityGroups.Value);
            checklist.AddLoginScript = ConvertHelper.ConvertToBoolean(hidNetworkShares.Value);
            checklist.CreateEmailAccount = ConvertHelper.ConvertToBoolean(hidchkVerifyEmail.Value);
            checklist.EmailAddress = ConvertHelper.ConvertToBoolean(hidEmail.Value);
            checklist.AddUserToEmailDistributions = ConvertHelper.ConvertToBoolean(hidAddUserEmailDistributions.Value);
            checklist.HostedAntispam = ConvertHelper.ConvertToBoolean(hidHosted_ServerAntispam.Value);
            checklist.AssignedPrinters = ConvertHelper.ConvertToBoolean(hidAssignedPrinters.Value);
            checklist.PerformTest = ConvertHelper.ConvertToBoolean(hidPerformTest.Value);
            checklist.CustomerLANdiagram = ConvertHelper.ConvertToBoolean(hidUpdateCustomerLanDiagram.Value);
            checklist.ThanktheCustomer = ConvertHelper.ConvertToBoolean(hidThanksCustomer.Value);
            checklist.AllTaskCompleted = ConvertHelper.ConvertToBoolean(hidTaskCompleted.Value);
            checklist.Notes = "";
            checklist.StatusID = 1;
            request.Checklist = checklist;
            request.Checklist.CreatedBy = currentUser.ApplicationUserID;
            request.Checklist.ModifiedBy = currentUser.ApplicationUserID;
            request.CurrentAction = CurrentAction;
            if (CurrentAction == ActionType.Edit)
            {
                serviceName = "SAVECHECKLIST";
            }
            else
            {
                request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                request.Checklist.StatusID = 1;
                serviceName = "SAVECHECKLIST";

            }
            //Framing the URL
            url = string.Format(serviceURL + "{0}", serviceName);
            request.URL = url;
            response = new PTResponse();
            //            response = webServiceHelper.PostRequest(request);
            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.isSuccess == true)
            {
                //ShowMessage(response.Message, true);
                ShowMessage(response.Message, true);
                CrudProvisioningCheckList.Visible = false;
                divGrdProvisioningCheckListInfo.Visible = true;
                Response.Redirect("CustomerInfo.aspx?nav=Provisioning%20Check%20List&#hTab-2", false);
            }
            else
            {
                ShowMessage(response.Message, false);
                CrudProvisioningCheckList.Visible = true;
                divGrdProvisioningCheckListInfo.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Back to Grid View Mode of Corresponding Wireless Grid]
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            ShowMessage("", true);
            CrudProvisioningCheckList.Visible = false;
            divGrdProvisioningCheckListInfo.Visible = true;
            Response.Redirect("CustomerInfo.aspx?do=v&nav=Provisioning%20Check%20List&#hTab-2", false);
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }

    #endregion

    #region [Get CheckList Info and Bind the Controls For Edit And View]
    private void ModifyCheckList()
    {
        try
        {
            CrudProvisioningCheckList.Visible = true;
            divGrdProvisioningCheckListInfo.Visible = false;

            request = new PTRequest();
            response = new PTResponse();
            webServiceHelper = new WebServiceHelper();
            serviceURL = string.Empty;
            url = string.Empty;
            serviceName = string.Empty;
            serviceResponseString = string.Empty;

            if (ConvertHelper.ConvertToString(base.Id) != null)
            {
                serviceURL = PostServiceURL + "GETCHECKLISTANDCHECKLISTDETAILSBYUSERID";
                request.User = new User();
                request.sessionSiteID = ConvertHelper.ConvertToInteger(base.sessionSiteId);
                request.User.UserID = ConvertHelper.ConvertToInteger(base.Id);
                request.URL = serviceURL;
            }

            response = webServiceHelper.PostRequest<PTResponse>(request);
            if (response != null && response.Checklist != null)
            {
                Checklist checklist = new Checklist();
                checklist = response.Checklist;
                if (checklist.User != null)
                {
                    lblUserName.Text = ConvertHelper.ConvertToString(checklist.User.UserName, "");
                    lblDepartment.Text = ConvertHelper.ConvertToString(checklist.User.DepartmentName, "");
                    lblSecurityGroup.Text = ConvertHelper.ConvertToString(checklist.User.SelectedSecurityGroup, "");
                    lblEmail.Text = ConvertHelper.ConvertToString(checklist.User.Email, "");
                    lblAssignedPrinters.Text = ConvertHelper.ConvertToString(checklist.User.SelectedPrinter, "");
                    lblNetworkShares.Text = ConvertHelper.ConvertToString(checklist.User.SelectedNetworkShares, "");

                    chkUserName.Checked = ConvertHelper.ConvertToBoolean(checklist.UserAccountCreation, false);
                    chkDepartment.Checked = ConvertHelper.ConvertToBoolean(checklist.AddUserToDepartment, false);
                    chkSecurityGroups.Checked = ConvertHelper.ConvertToBoolean(checklist.AddUserToSecurityGroup, false);
                    chkNetworkShares.Checked = ConvertHelper.ConvertToBoolean(checklist.AddLoginScript, false);
                    chkVerifyEmail.Checked = ConvertHelper.ConvertToBoolean(checklist.CreateEmailAccount, false);
                    chkEmail.Checked = ConvertHelper.ConvertToBoolean(checklist.EmailAddress, false);
                    chkAddUserEmailDistributions.Checked = ConvertHelper.ConvertToBoolean(checklist.AddUserToEmailDistributions, false);
                    chkHosted_ServerAntispam.Checked = ConvertHelper.ConvertToBoolean(checklist.HostedAntispam, false);
                    chkAssignedPrinters.Checked = ConvertHelper.ConvertToBoolean(checklist.AssignedPrinters, false);
                    chkPerformTest.Checked = ConvertHelper.ConvertToBoolean(checklist.PerformTest, false);
                    chkUpdateCustomerLanDiagram.Checked = ConvertHelper.ConvertToBoolean(checklist.CustomerLANdiagram, false);
                    chkThanksCustomer.Checked = ConvertHelper.ConvertToBoolean(checklist.ThanktheCustomer, false);
                    chkTaskCompleted.Checked = ConvertHelper.ConvertToBoolean(checklist.AllTaskCompleted, false);

                    hidUserName.Value = ConvertHelper.ConvertToString(checklist.UserAccountCreation, "false");
                    hidDepartment.Value = ConvertHelper.ConvertToString(checklist.AddUserToDepartment, "false");
                    hidSecurityGroups.Value = ConvertHelper.ConvertToString(checklist.AddUserToSecurityGroup, "false");
                    hidNetworkShares.Value = ConvertHelper.ConvertToString(checklist.AddLoginScript, "false");
                    hidchkVerifyEmail.Value = ConvertHelper.ConvertToString(checklist.CreateEmailAccount, "false");
                    hidEmail.Value = ConvertHelper.ConvertToString(checklist.EmailAddress, "false");
                    hidAddUserEmailDistributions.Value = ConvertHelper.ConvertToString(checklist.AddUserToEmailDistributions, "false");
                    hidHosted_ServerAntispam.Value = ConvertHelper.ConvertToString(checklist.HostedAntispam, "false");
                    hidAssignedPrinters.Value = ConvertHelper.ConvertToString(checklist.AssignedPrinters, "false");
                    hidPerformTest.Value = ConvertHelper.ConvertToString(checklist.PerformTest, "false");
                    hidUpdateCustomerLanDiagram.Value = ConvertHelper.ConvertToString(checklist.CustomerLANdiagram, "false");
                    hidThanksCustomer.Value = ConvertHelper.ConvertToString(checklist.ThanktheCustomer, "false");
                    hidTaskCompleted.Value = ConvertHelper.ConvertToString(checklist.AllTaskCompleted, "false");

                    Image1.Visible = false;
                    Image2.Visible = false;
                    Image3.Visible = false;
                    Image4.Visible = false;
                    Image5.Visible = false;
                    Image6.Visible = false;
                    Image7.Visible = false;
                    Image8.Visible = false;
                    Image9.Visible = false;
                    Image10.Visible = false;
                    Image11.Visible = false;
                    Image12.Visible = false;
                    Image13.Visible = false;

                    if (CurrentAction == ActionType.MoreView)
                    {
                        chkUserName.Visible = false;
                        chkDepartment.Visible = false;
                        chkSecurityGroups.Visible = false;
                        chkNetworkShares.Visible = false;
                        chkVerifyEmail.Visible = false;
                        chkEmail.Visible = false;
                        chkAddUserEmailDistributions.Visible = false;
                        chkHosted_ServerAntispam.Visible = false;
                        chkAssignedPrinters.Visible = false;
                        chkPerformTest.Visible = false;
                        chkUpdateCustomerLanDiagram.Visible = false;
                        chkThanksCustomer.Visible = false;
                        chkTaskCompleted.Visible = false;

                        Image1.Visible = true;
                        Image2.Visible = true;
                        Image3.Visible = true;
                        Image4.Visible = true;
                        Image5.Visible = true;
                        Image6.Visible = true;
                        Image7.Visible = true;
                        Image8.Visible = true;
                        Image9.Visible = true;
                        Image10.Visible = true;
                        Image11.Visible = true;
                        Image12.Visible = true;
                        Image13.Visible = true;

                        var tick = "http://" + Request.Url.Host + "/includes/UI/images/tick.png";
                        var cross = "http://" + Request.Url.Host + "/includes/UI/images/cross.png";
                        var line = "http://" + Request.Url.Host + "/includes/UI/images/line.png";

                        if (checklist.UserAccountCreation)
                            Image1.ImageUrl = tick;
                        else
                            Image1.ImageUrl = cross;

                        if (checklist.AddUserToDepartment)
                            Image2.ImageUrl = tick;
                        else
                            Image2.ImageUrl = cross;

                        if (checklist.AddUserToSecurityGroup)
                            Image3.ImageUrl = tick;
                        else
                            Image3.ImageUrl = cross;

                        if (checklist.AddLoginScript)
                            Image4.ImageUrl = tick;
                        else
                            Image4.ImageUrl = cross;

                        if (checklist.CreateEmailAccount)
                            Image5.ImageUrl = tick;
                        else
                            Image5.ImageUrl = cross;

                        if (checklist.EmailAddress)
                            Image6.ImageUrl = tick;
                        else
                            Image6.ImageUrl = cross;

                        if (checklist.AddUserToEmailDistributions)
                            Image7.ImageUrl = tick;
                        else
                            Image7.ImageUrl = cross;

                        if (checklist.HostedAntispam)
                            Image8.ImageUrl = tick;
                        else
                            Image8.ImageUrl = cross;

                        if (checklist.AssignedPrinters)
                            Image9.ImageUrl = tick;
                        else
                            Image9.ImageUrl = cross;

                        if (checklist.PerformTest)
                            Image10.ImageUrl = tick;
                        else
                            Image10.ImageUrl = cross;

                        if (checklist.CustomerLANdiagram)
                            Image11.ImageUrl = tick;
                        else
                            Image11.ImageUrl = cross;


                        if (checklist.ThanktheCustomer)
                            Image12.ImageUrl = tick;
                        else
                            Image12.ImageUrl = cross;

                        if (checklist.AllTaskCompleted)
                            Image13.ImageUrl = tick;
                        else
                            Image13.ImageUrl = cross;


                        Image14.ImageUrl = line;
                        Image15.ImageUrl = line;
                        Image16.ImageUrl = line;
                        Image17.ImageUrl = line;
                        Image18.ImageUrl = line;
                        Image19.ImageUrl = line;
                        Image20.ImageUrl = line;
                        Image21.ImageUrl = line;
                        Image22.ImageUrl = line;
                        Image23.ImageUrl = line;
                        Image24.ImageUrl = line;
                        Image25.ImageUrl = line;
                    }

                }
            }
            else
            {
                ShowMessage("Invalid record could not be found ", false);
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message, false);
        }

    }
    #endregion

    #region [Bind the information of user for provisioning checklist]
    /// <summary>
    /// On selecting the  user the checklist items information will be displayed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAssignedUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModifyCheckList();
    }
    #endregion


    protected void btnExport2PDF_Click(object sender, EventArgs e)
    {

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Provisioningchecklist.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        Panel1.RenderControl(hw); //CONVERT THE PANEL TO PDF

        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 50, 50, 110, 25);

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfWriter.PageEvent = new ITextEvents();

        pdfDoc.Open();

        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }


    private static StyleSheet GenerateStyleSheet()
    {

        FontFactory.Register(@"c:\windows\fonts\gara.ttf", "Garamond");
        FontFactory.Register(@"c:\windows\fonts\garabd.ttf");
        FontFactory.Register(@"c:\windows\fonts\garait.ttf");

        StyleSheet css = new StyleSheet();

        css.LoadTagStyle("body", "face", "Garamond");
        css.LoadTagStyle("body", "encoding", "Identity-H");
        css.LoadTagStyle("body", "size", "13pt");
        css.LoadTagStyle("h1", "size", "30pt");
        css.LoadTagStyle("h1", "style", "line-height:30pt;font-weight:bold;");
        css.LoadTagStyle("h2", "size", "18pt");
        css.LoadTagStyle("h2", "style", "line-height:30pt;font-weight:bold;margin-top:5pt;margin-bottom:12pt;");
        css.LoadTagStyle("h3", "size", "15pt");
        css.LoadTagStyle("h3", "style", "line-height:25pt;font-weight:bold;margin-top:1pt;margin-bottom:15pt;");
        css.LoadTagStyle("h4", "size", "13pt");
        css.LoadTagStyle("h4", "style", "line-height:23pt;margin-top:1pt;margin-bottom:15pt;");
        css.LoadTagStyle("hr", "width", "100%");
        css.LoadTagStyle("a", "style", "text-decoration:underline;");
        return css;
    }



    public class ITextEvents : PdfPageEventHelper
    {

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {

            }
            catch (System.IO.IOException ioe)
            {

            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            Phrase p1Header = new Phrase("Provisioning Check List", baseFontNormal);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            PdfPCell pdfCell1 = new PdfPCell();
            PdfPCell pdfCell2 = new PdfPCell(p1Header);
            PdfPCell pdfCell3 = new PdfPCell();
            String text = "Page " + writer.PageNumber + " of ";


            ////Add paging to header
            //{
            //    cb.BeginText();
            //    cb.SetFontAndSize(bf, 12);
            //    cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
            //    cb.ShowText(text);
            //    cb.EndText();
            //    float len = bf.GetWidthPoint(text, 12);
            //    //Adds "12" in Page 1 of 12
            //    cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
            //}

            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(100), document.PageSize.GetBottom(30));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(100) + len, document.PageSize.GetBottom(30));
            }
            //Row 2
            PdfPCell pdfCell4 = new PdfPCell(new Phrase("", baseFontNormal));
            //Row 3


            PdfPCell pdfCell5 = new PdfPCell();
            PdfPCell pdfCell6 = new PdfPCell();
            //PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));

            PdfPCell pdfCell7 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString(), baseFontBig));
            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell7.HorizontalAlignment = Element.ALIGN_RIGHT;


            pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;


            pdfCell4.Colspan = 3;



            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;


            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);
            pdfTab.AddCell(pdfCell4);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell6);
            pdfTab.AddCell(pdfCell7);

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;


            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.Height - 100);
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();


        }
    }

}
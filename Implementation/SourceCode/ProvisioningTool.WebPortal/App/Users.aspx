<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="ManageSystemEngineer" %>

<%@ Register Src="~/includes/UserControls/common/Master.ascx" TagName="Master" TagPrefix="ProvisioningTool" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="headManageSystemEngineer" runat="server">
    <title>Users</title>
    <ProvisioningTool:Master ID="Master" runat="server" />

    <script type="text/javascript">

        var siteID = $.cookie("siteID");
        if (siteID == "" || siteID == null) { siteID = 0 }

        var gridWidth = "";
        var gridName = "#grdManageSystemEngineerInfo";
        var gridPager = "#grdManageSystemEngineerInfopager";
        var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
        var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
        var getService = '<%=ConfigurationManager.AppSettings["GetService"].ToString() %>';
        var postService = '<%=ConfigurationManager.AppSettings["PostService"].ToString() %>';
        var gridHeight = "220"
        var gridSortOrder = "asc"
        var gridpageSize = "10";
        var gridListName = "ApplicationUserList"
        var pageSizeOption = ["10", "20", "30"];

        function InitializeGrid(caption, SearchUrl) {
            //To define the Grid for the page on the design time
            var colname = ["ApplicationUserID", "User Name", "Role", "Status"];

            var colmodel = [
                               { name: 'ApplicationUserID', key: true, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: false, hidedlg: true },
                               { name: 'ApplicationUsername', sortable: true, align: "left", hidden: false, editable: true, search: false },
                               { name: 'Role.RoleName', sortable: true, align: "center", hidden: false, editable: false, search: false },
                               { name: 'Status', sortable: true, align: "center", hidden: false, editable: true, search: false }
            ];
            //Default SortColumn
            var sortName = "ApplicationUsername";
            var gridCaption = caption;
            var getWebServiceURL = "";

            if (SearchUrl != "")
                getWebServiceURL = baseServiceURL + masterServiceName + SearchUrl;
            else
                getWebServiceURL = baseServiceURL + masterServiceName + getService + "GETALLAPPLICATIONUSER/MasterName/0/0";


            var deleteWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEAPPLICATIONUSERBYAPPLICATIONUSERID";
            var crudWebServiceURL = baseServiceURL + masterServiceName + postService + "DELETEAPPLICATIONUSERBYAPPLICATIONUSERID";
            //Calling the webservices and the desgining the Grid at Runtime 
            var objGridList = new oData(
                                            gridName, // Table or Grid name in the page
                                            getWebServiceURL,//Web Service URL
                                            "json", // Default dont change
                                            "GET", // Webservice Mode
                                            gridCaption, //Grid Caption
                                            gridpageSize, //  Number of records in each page
                                            pageSizeOption, //Page Size Option 10 20 30[this will be available in the Grid Dropdown]
                                            sortName, //Default Sortname
                                            gridSortOrder, //Sort Type - desc or asc
                                            gridWidth, // Grid width
                                            gridHeight, // Grid height
                                            crudWebServiceURL, // Add
                                            gridPager, //div name in the page (gridpager1)
                                            false, // is Edit Button visiable
                                            false, // is Add Button visiable
                                            false, // is Delete Button visiable
                                            false, // is Search Button visiable
                                            false, // is Refresh Button visiable
                                            false, // is Search TextBox Visiable/Enabled
                                            false, //
                                            colname, // Grid Column names
                                            colmodel, // Grid Column names and properties
                                            false, // Select All options on the header and this will give checkbox for each row
                                            false, //
                                            false, //
                                            false, //
                                            false, //
                                            false, //
                                            0, //
                                            gridListName, //Result Set name which is availabe in the Service
                                            true, //is New page required for Add
                                            "Users.aspx?search=1&do=a&nav=AppplicationUser", // Add URL
                                            true, //is New page required for Edit
                                            "Users.aspx?search=1&mode=v&do=e&nav=AppplicationUser&id=", //Edit URL
                                            deleteWebServiceURL
                                            );
            return objGridList;
        };

        $(document).ready(function () {

            if (getQueryStringByName("search") == 1) {
                jqGridGenerator(InitializeGrid("Users", ""));
            } else {
                $('#customSearch').hide();
            }

            if (getQueryStringByName("do") == "a") {
                $("#lblHeader").html("Create Users");
                $.validate({
                    form: '#frmManageSystemEngineer',
                    modules: 'sweden,security',
                    language: myLanguage
                });
            }
            else if (getQueryStringByName("do") == "e")
                $("#lblHeader").html("Modify Users");

            $('#contentWrap').css({
                'min-height': ($(window).height() - $('header').height() - $('footer').height() + 18) + 'px'
            });

            //Search button 
            $("#btnSearchSubmit").click(function () {

                $('#grdManageSystemEngineerInfo').jqGrid('GridUnload');

                var txtSearch = $("#txtSearch").val();

                //Check if the value is empty if so all the data should be fetched
                var gridSearchURL = "";

                if (txtSearch == "") {
                    gridSearchURL = "SEARCHUSERSBYKEY/Users/all";
                }
                else {
                    gridSearchURL = "SEARCHUSERSBYKEY/Users/" + txtSearch;
                }

                jqGridGenerator(InitializeGrid("Users", gridSearchURL));

                hideGridfooter();

                return false;
            });

            $("#btnSearchClear").click(function () {
                $("#txtSearch").val('');
                $("#btnSearchSubmit").click();
                return false;
            });

            hideGridfooter();
        });

        function DisplayDialog() {
            $(function () {
                $("#dialog-confirm").show();
                $("#dialog-confirm").dialog({
                    modal: true,
                    buttons: {
                        Ok: function () {
                            $("[id*=btnSubmit]").click();
                        },
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });
                $(".ui-dialog-title").html('Information !');
                return true;
            });
        };
    </script>

</head>
<body>
    <form id="frmManageSystemEngineer" runat="server">
        <div id="divMessage" class="divMessage" runat="server">
            <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
        </div>
         <div id="dialog-message" title="Warning">
            <div>Please, select row</div>
        </div>
         <div id="dialog-confirm" title="Warning">
            <div>Do you want to Submit</div>
        </div>
        <div class="fullWidthGrid">
            <div class="leftMenuWrapper">
                <ul>
                    <li>
                        <div class="largeNav" id="iSearchUsers">
                            <img src="../../includes/UI/images/searchUserLargeIcon.png" />
                            <h3>Search</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iCreateUsers">
                            <img src="../../includes/UI/images/createUserLargeIcon.png" />
                            <h3>Create</h3>
                        </div>
                    </li>
                    <li>
                        <div class="largeNav" id="iModifyUsers">
                            <img src="../../includes/UI/images/modifyUserLargeIcon.png" />
                            <h3>Modify</h3>
                        </div>
                    </li>
                </ul>
            </div>

            <div class="rightContent">

                <div id="CrudManageSystemEngineer" runat="server" class="siteDetail clearedContent">
                    <div id="divManageSystemEngineerDetail" runat="server" class="contentDetail scrollabow" name="top" style="height: 400px;">
                        <h1 id="lblHeader">Users </h1>
                        <div class="inlineProperty ">
                            <label>
                                User Name
                                              <asp:RequiredFieldValidator ID="rfgApplicationUserName" runat="server"
                                                  ControlToValidate="txtApplicationUserName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                                  ValidationGroup="Req">*</asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox Text="" ID="txtApplicationUserName" class="watermark Username custclass"  data-validation="alphanumeric" data-validation-allowing="-+()_.," data-validation-optional="true" placeholder="User Name"
                                runat="server" MaxLength="20"></asp:TextBox>

                        </div>
                        <div class="inlineProperty ">
                            <label>
                                Password 
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox Text="" ID="txtPassword" class="watermark custclass" placeholder="Password" TextMode="Password" data-validation="alphanumeric" data-validation-allowing="-+()_.," data-validation-optional="true" runat="server" MaxLength="20"></asp:TextBox>

                        </div>
                        <div class="inlineProperty ">
                            <label>
                                Confirm Password 
                                                        <asp:CompareValidator ID="cvtxtConfirmPassword" runat="server"
                                                            ControlToValidate="txtPassword"
                                                            ControlToCompare="txtConfirmPassword"
                                                            ErrorMessage="No Match"
                                                            ValidationGroup="Req"
                                                            ToolTip="No Match">Does Not Match</asp:CompareValidator>
                            </label>
                            <asp:TextBox Text="" ID="txtConfirmPassword" class="watermark" placeholder="Confirm Password" TextMode="Password" runat="server" MaxLength="20"></asp:TextBox>

                        </div>
                        <div class="inlineProperty ">
                            <label>
                                Email 

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                                        ValidationGroup="Req">*</asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtEmail" runat="server" class="watermark custclass" data-validation="email" data-validation-optional="true" placeholder="Email" TextMode="Email" MaxLength="256"></asp:TextBox>

                        </div>
                        <div class="inlineProperty">
                            <label>
                                Application User Role
                                                          <asp:RequiredFieldValidator ID="rfvRole" runat="server"
                                                              ControlToValidate="ddlRole" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                                                              ValidationGroup="Req">*</asp:RequiredFieldValidator>
                            </label>
                            <asp:DropDownList ID="ddlRole" runat="server" class="selector">
                                <asp:ListItem Text="System Engineer" Value="2" />
                                <asp:ListItem Text="Administrator" Value="1" />
                            </asp:DropDownList>
                        </div>
                        <div class="clear"></div>
                        <div class="inlineProperty radioCotent" style="min-width: 122px!important;">
                            <label>
                                Status

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="rbtStatus" Display="Dynamic" ErrorMessage="*" InitialValue=""
                                                        ValidationGroup="Req">*</asp:RequiredFieldValidator>

                            </label>
                            <%--<asp:CheckBox Text="" ID="chkPOE" runat="server" />--%>
                            <asp:RadioButtonList runat="server" ID="rbtStatus" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Text="Active" />
                                <asp:ListItem Value="2" Text="Inactive" />
                            </asp:RadioButtonList>

                        </div>
                        <div class="clear"></div>

                      <%--  <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="Req" OnClick="btnSubmit_Click" href="#top" />--%>
                         <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit"  ValidationGroup="Req" OnClick="btnPopupSubmit_Click" /> <%--OnClientClick="return ConfirmRemoveDialog();"  --%>
                              <asp:Button ID="btnSubmithid" CssClass="actionBtn" runat="server" Text="Submit"  Style="display: none" ValidationGroup="Req" OnClick="btnSubmit_Click" />     
                        <asp:Button ID="btnBack" CssClass="actionBtn" runat="server" Text="Back" OnClick="btnBack_Click" />
                    </div>
                </div>

                <div id="customSearch" runat="server">
                    <div class="inlineProperty">
                        <asp:TextBox Text="" ID="txtSearch" placeholder="Search"
                            runat="server" MaxLength="50"></asp:TextBox>
                    </div>

                    <div class="inlineProperty">
                        <asp:Button ID="btnSearchSubmit" CssClass="actionBtn" runat="server" Text="Search" />
                        <asp:Button ID="btnSearchClear" CssClass="actionBtn" runat="server" Text="Clear" />
                    </div>

                </div>

                <div id="divGrdManageSystemEngineerInfo" runat="server">
                    <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 100%;">
                        <div id="grdManageSystemEngineerInfopager"></div>
                        <table id="grdManageSystemEngineerInfo"></table>
                    </div>
                </div>

            </div>

        </div>
        <div id="pdfContainer" style="display: block;"></div>
        <div id="dialog" title="Warning" class="dialog">
            <div>Please, select row</div>
        </div>

        <div class="clear"></div>
        <footer>
            <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
        </footer>
    </form>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServerHardware.ascx.cs" Inherits="UserControlServerHardware" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<script type="text/javascript">
    var siteID = $.cookie("siteID");
    if (siteID == "" || siteID == null) { siteID = 0 }

    var gridWidth = "";
    var gridName = "#grdServerHardwareInfo";
    var gridPager = "#grdServerHardwareInfopager";
    var baseServiceURL = '<%=ConfigurationManager.AppSettings["BaseServiceURL"].ToString() %>';
    var masterServiceName = '<%=ConfigurationManager.AppSettings["MasterServiceName"].ToString() %>';
    var gridHeight = "220"
    var gridSortOrder = "asc"
    var gridpageSize = "10";
    var gridListName = "ServerList"
    var pageSizeOption = ["10", "20", "30"];

    function InitializeGrid1(caption) {

        //To define the Grid for the page on the design time
        var colname = ["ServerHardwareID", "Host Name", "Model", "Processor", "MotherBoard", "HardDrive", "Chipset", "VideoCard", "Display", "Multimedia", "Port", "Slot", "Chassis", "Power", ""];

        var colmodel = [
                           { name: 'ServerHardwareID', key: true, width: 100, align: "center", hidedlg: true, hidden: true, searchtype: "integer", editable: true, hidedlg: true },
                           { name: 'HostName', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Model', width: 100, sortable: true, hidedlg: true, hidden: true, editable: true, search: true },
                           { name: 'Processor', width: 150, sortable: true, align: "center", hidden: false, editable: false, search: true },
                           { name: 'MotherBoard', width: 100, sortable: false, hidden: false, align: "center", editable: false, search: false },
                           { name: 'HardDrive', width: 100, sortable: false, align: "left", hidden: false, editable: false, search: false },
                           { name: 'Chipset', width: 150, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'VideoCard', width: 100, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Display', width: 100, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Multimedia', width: 100, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Port', width: 100, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Slot', width: 100, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Chassis', width: 100, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'Power', width: 100, sortable: true, align: "left", hidden: false, editable: true },
                           { name: 'View', width: 40, sortable: true, align: "left", hidden: false, editable: true, search: false },

        ];
        //Default SortColumn
        var sortName = "HostName";
        var gridCaption = caption;
        var getWebServiceURL = baseServiceURL + masterServiceName + "GetAllServerHardware";

        //alert(getWebServiceURL);
        var crudWebServiceURL = baseServiceURL + masterServiceName + "GlobalMasterCrud/" + caption + "/" + siteID;
        //Calling the webservices and the desgining the Grid at Runtime 
        var objGridList = new oData(
                                        gridSHName, // Table or Grid name in the page
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
                                        gridSHPager, //div name in the page (gridpager1)
                                        false, // is Edit Button visiable
                                        false, // is Add Button visiable
                                        true, // is Delete Button visiable
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
                                        "CustomerInfo.aspx?do=a&nav=servershardware", // Add URL
                                        true, //is New page required for Edit
                                        "CustomerInfo.aspx?do=e&nav=servershardware&id=" //Edit URL
                                     );
        return objGridList;
    };

    $(document).ready(function () {
        $('#grdServerHardwareInfo').jqGrid('GridUnload');
        jqGridGenerator(InitializeGrid("Server Hardware Info"));

        if (getQueryStringByName("do") != "m") {
            //VALIDATION CONTROL jquery.form-validator
            $.validate({
                form: '#main',
                modules: 'sweden,security',
                language: myLanguage
            });
        }
    });

</script>

<div id="CrudServerHardware" runat="server">
    <div class="innerTabContent">
        <div class="siteDetail" style="padding-top: 25px; margin-left: 10px">

            <div class="inlineProperty">
                <label>Host Name</label>
                <asp:TextBox Text="" ID="txtHostName" class="watermark" placeholder="Host Name"
                    runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfgHostName" runat="server"
                    ControlToValidate="txtHostName" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="inlineProperty">
                <label>Model</label>
                <asp:DropDownList ID="ddlModel" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvModel" runat="server"
                    ControlToValidate="ddlModel" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="inlineProperty">
                <label>Serial No / Service Tag</label>
                <asp:TextBox Text="" ID="txtSerialNo" class="watermark" placeholder="Serial No / Service Tag"
                    runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server"
                    ControlToValidate="txtSerialNo" Display="Dynamic" ErrorMessage="*" InitialValue=""
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="inlineProperty">
                <label>CPU</label>
                <asp:DropDownList ID="ddlCPU" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCPU" runat="server"
                    ControlToValidate="ddlCPU" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="clear"></div>


            <div class="inlineProperty">
                <label>Memory</label>
                <asp:DropDownList ID="ddlMemory" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvMemory" runat="server"
                    ControlToValidate="ddlMemory" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>


            <div class="inlineProperty">
                <label>Motherboard</label>
                <asp:DropDownList ID="ddlMotherboard" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvMotherboard" runat="server"
                    ControlToValidate="ddlMotherboard" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>


            <div class="inlineProperty">
                <label>Hard drive</label>
                <asp:DropDownList ID="ddlHardDrive" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvHardDrive" runat="server"
                    ControlToValidate="ddlHardDrive" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="inlineProperty">
                <label>Chipset</label>
                <asp:DropDownList ID="ddlChipset" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvChipset" runat="server"
                    ControlToValidate="ddlChipset" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="clear"></div>


            <div class="inlineProperty">
                <label>Video Card</label>
                <asp:DropDownList ID="ddlVideoCard" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvVideoCard" runat="server"
                    ControlToValidate="ddlVideoCard" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>


            <div class="inlineProperty">
                <label>Display 1</label>
                <asp:DropDownList ID="ddlDisplay" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDisplay1" runat="server"
                    ControlToValidate="ddlDisplay1" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>


            <div class="inlineProperty">
                <label>Display 2</label>
                <asp:DropDownList ID="ddlDisplay2" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvDisplay2" runat="server"
                    ControlToValidate="ddlDisplay2" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="inlineProperty">
                <label>Multimedia</label>
                <asp:DropDownList ID="ddlMultimedia" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvMultimedia" runat="server"
                    ControlToValidate="ddlMultimedia" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="clear"></div>

            <div class="inlineProperty">
                <label>Ports</label>
                <asp:DropDownList ID="ddlPorts" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPorts" runat="server"
                    ControlToValidate="ddlPorts" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>


            <div class="inlineProperty">
                <label>Slots</label>
                <asp:DropDownList ID="ddlSlots" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSlots" runat="server"
                    ControlToValidate="ddlSlots" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>


            <div class="inlineProperty">
                <label>Chasis</label>
                <asp:DropDownList ID="ddlChasis" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvChasis" runat="server"
                    ControlToValidate="ddlChasis" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="inlineProperty">
                <label>Power</label>
                <asp:DropDownList ID="ddlPower" runat="server" class="chosen-select"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvPower" runat="server"
                    ControlToValidate="ddlPower" Display="Dynamic" ErrorMessage="*" InitialValue="0"
                    ValidationGroup="Req">*</asp:RequiredFieldValidator>
            </div>

            <div class="clear"></div>
            <div class="inlineProperty">
                <label>Notes</label>
                <asp:TextBox Text="" ID="txtNotes" MaxLength="1000" TextMode="MultiLine"
                    runat="server"></asp:TextBox>
            </div>

            <div class="clear"></div>
            <asp:Button ID="btnSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="Req" />
        </div>
    </div>
</div>


<div id="divGrdServerHardwareInfo" runat="server">
    <div style="padding-top: 5px; padding-left: 0px; text-align: center; width: 800px">
        <table border="0">
            <tr>
                <td id="mainTable">
                    <table id="grdServerHardwareInfo"></table>
                    <div id="grdServerHardwareInfopager"></div>
                </td>
            </tr>
        </table>
    </div>
</div>

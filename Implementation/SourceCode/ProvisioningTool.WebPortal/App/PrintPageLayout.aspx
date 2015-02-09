<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPageLayout.aspx.cs" Inherits="App_PrintPageLayout" ValidateRequest="false" ViewStateMode="Enabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us">
<head runat="server" id="printHead">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- jspdf generator -->
    <script type="text/javascript" src="../../includes/common/jspdfPlugins/dist/jspdf.debug.js"></script>
    <!-- calling above html to pdf conversion method and bind in jqGrid.gen.js file download pdf option. -->
    <script type="text/javascript" src="../../includes/common/jspdfPlugins/basic.js"></script>
    
    <script type="text/javascript" src="../../includes/common/jquery.min.js"></script>

    <link rel="stylesheet" type="text/css" href="../../includes/UI/css/jquery-ui-framework.css" />
    <link rel="stylesheet" type="text/css" href="../../includes/UI/css/style.css" />
    <link rel="stylesheet" type="text/css" href="../../includes/UI/css/admin.css" />
    <link rel="stylesheet" type="text/css" href="../../includes/UI/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../../includes/UI/fonts/fonts.css" />
    <link rel="stylesheet" type="text/css" href="../../includes/UI/css/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="../../includes/UI/css/chosen.css" />
    <link rel="stylesheet" type="text/css" href="../../includes/UI/css/checkble.css" />
    <link rel="Stylesheet" type="text/css" media="print" href="../../includes/UI/css/print.css" />

    <script type="text/ecmascript">
        $(document).ready(function () {
            var htmlContent = $(".scrollabow", opener.document).html();

            //htmlContent = htmlContent.replace("class=\"clearfix\"", "style=\"clear: both !important;\"");
            //htmlContent = htmlContent.replace("class=\"line\"", "style=\"position: relative;top: 0px;left: 10px !important;height: 1px !important;width: 87% !important;background: #ccc !important;\"");
            //htmlContent = htmlContent.replace("class=\"watermark\"", "style=\"float: right !important;\"");
            //htmlContent = htmlContent.replace("class=\"heading\"", "style=\"font-size: 20px !important;line-height: 30px !important;text-align: left !important;margin: 0 0 0 10px !important;font-family: 'source_sans_probold' !important;margin-bottom: 15px !important;\"");
            //htmlContent = htmlContent.replace("class=\"check\"", "style=\"line-height: 2 !important;\"");
            //htmlContent = htmlContent.replace("class=\"check checkUserName\"", "style=\"line-height: 2 !important;\"");

            $("#printContent").val(htmlContent);
            $("#printContent").html(htmlContent);

            $("#hidPrintContent").val(htmlContent);

            $("#hidPrintContent").text(htmlContent);

            $("#btnClose").live("click", function () {
                window.close();
            });

            $("#btnPrint").live("click", function () {
                window.print();
            });

            $("#btnExport2PDF").live("click", function () {
                //var printContent = $("#printContent").val();
                //printContent = printContent.replace(/<img/gi, '<label>✔</label><img ');
                //$("#printContent").val(printContent);
                //$("#printContent").html(printContent);
                demoFromHTMLProvisioning("Provisioning Check List.pdf", "a4", "p", "printContent");
            });
            
            $(".mainPage").hide();
        });

    </script>

    <style scoped="scoped">
        ul.star-shaped-bullet {
            margin: 0;
            list-style: none;
            text-align: left;
        }

            ul.star-shaped-bullet li:before {
                content: "&#10003;";
            }
    </style>

</head>
<body>
    <header>
        <div class="container">
            <aside class="logo">
                <img src="../../includes/UI/images/logo.png" alt="" />
            </aside>
        </div>
    </header>
    <form id="form1" runat="server">
        <div class="clear"></div>
        <br />
        <br />
        <div id="printContentHead" runat="server">
            <table id="printContent" runat="server"></table>
        </div>

        <%-- <asp:Label ID="hidPrintContent" runat="server" />--%>
        <div class="classbtnSubmit no-print">
            <asp:Button ID="btnPrint" CssClass="actionBtn" ClientIDMode="Static" runat="server" Text="Print" />
            <asp:Button ID="btnExport2PDF" CssClass="actionBtn" ClientIDMode="Static" runat="server" Text="Export to PDF" />
            <asp:Button ID="btnClose" CssClass="actionBtn" ClientIDMode="Static" runat="server" Text="Close" />
        </div>
        <asp:Label ID="hidPrintContent" ViewStateMode="Enabled" runat="server" Style="top: -1000px; left: -1000px" Visible="false" ClientIDMode="Static"></asp:Label>
    </form>
</body>
</html>

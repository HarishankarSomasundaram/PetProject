<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Master.ascx.cs" Inherits="includes_UserControls_common_Master" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>

<ProvisioningTool:Includes ID="Includes" runat="server" />
<ProvisioningTool:Header ID="Header" runat="server" />
<ProvisioningTool:Footer ID="Footer" runat="server" />

<div id="htmlTable" style="display: block;"></div>
<div id="pdfContainer" style="display: block;"></div>
<div style="display: none;"><a id="exportCsv" href="#" style="color: #0026ff"><span></span></a></div>

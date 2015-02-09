<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageUnderConstruction.ascx.cs" Inherits="includes_UserControls_pages_PageUnderConstruction" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>





<div class="innerTabContent">
    <p class="divMessage">   <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label></p>
    <div id="divGrdPageUnderConstruction" runat="server">
        <div style="padding-top: 5px; padding-left: 0px; text-align: center; width:auto;">
            Page Under Construction !
        </div>
    </div>
</div>

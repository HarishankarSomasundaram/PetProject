<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="App_ChangePassword" %>

<%@ Register Src="~/includes/UserControls/common/Header.ascx" TagName="Header" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Footer.ascx" TagName="Footer" TagPrefix="ProvisioningTool" %>
<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>

<!DOCTYPE html>
<head runat="server">
    <title>Change Password</title>
    <ProvisioningTool:Includes ID="Includes" runat="server" />
    <ProvisioningTool:Header ID="Header" runat="server" />
    <ProvisioningTool:Footer ID="Footer" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="contentWrapText noMrginLeft">
            <section id="contentWrap">
                <div class="container doubleColumn">
                    <div class="hideContent">
                        <div id="widgetContentWrap">
                            <article class="widget" style="width: 100% !important;">
                                <div class="headerSection">
                                    <h1>Change Password</h1>
                                </div>
                                <div>
                                    <div class="clear"></div>
                                        <div class="formWrap changePswdContent">
                                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            <div class="inputPassWordWrap">
                                                <asp:TextBox ID="txtOldPassword" runat="server" class="watermark" placeholder="Old Password" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator  runat="server" ID="rfvOldPassword" ControlToValidate="txtOldPassword" ErrorMessage="*" ValidationGroup="Pass"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="inputPassWordWrap">
                                                <asp:TextBox ID="txtNewPassword" runat="server" class="watermark" placeholder="New Password" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator  runat="server" ID="rfvNewPassword" ControlToValidate="txtNewPassword" ErrorMessage="*" ValidationGroup="Pass"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="inputPassWordWrap">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" class="watermark" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
                                            </div>
                                            <div class="submitWrap">
                                                <asp:Button ID="btnChangePassword" runat="server" class="actionBtn" Text="Submit"  OnClick="btnChangePassword_Click" ValidationGroup="Pass" />
                                                <asp:Button ID="btnCancel" runat="server" class="actionBtn" Text="Cancel"  OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                </div>
                            </article>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
            </section>
        </div>

        <div class="clear"></div>
        <footer>
            <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
        </footer>

    </form>
</body>

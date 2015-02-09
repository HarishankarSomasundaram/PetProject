<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupPolicy.ascx.cs" Inherits="UserControlsGroupPolicy" %>

<script type="text/javascript">

    $(document).ready(function () {
        //Fade out the message after 10 sec
        $('#lblMessage1').fadeIn().delay(2000).fadeOut(function () {
            //This is for URL rewrite with out post back 
            var stateObj = {};
            History.pushState(stateObj, "", "CustomerInfo.aspx");
            return false;
        });
    });

</script>
<div class="innerTabContent">
    <p class="divMessage" style="text-align: center; margin: 0" runat="server" id="divMessage">
        <asp:Label ID="lblErrorMessage" runat="server" ClientIDMode="Static"></asp:Label>
    </p>

    <div class="siteDetail contentDetail" style="padding-top: 25px; margin-left: 10px">
        <asp:PlaceHolder ID="phControl" runat="server" />
        <div class="clear"></div>
        <asp:Button ID="btnSSubmit" CssClass="actionBtn" runat="server" Text="Submit" ValidationGroup="GroupPolicy" OnClick="btnSSubmit_Click" />
        <%--    <asp:Button ID="btnSBack" CssClass="actionBtn" runat="server" Text="Back" OnClick="btnSBack_Click" />--%>
    </div>
    <div>
        <h1 style="text-align:center">
            <asp:Label runat="server" ID="lblNoGroupPolicy"></asp:Label>
        </h1>
    </div>
</div>

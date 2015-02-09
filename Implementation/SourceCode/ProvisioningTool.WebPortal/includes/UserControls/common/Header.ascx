<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="includes_UserControls_Header" %>
<header id="headderMain" runat="server">
    <div class="container" id="containerMain" runat="server">

        <aside class="logo">
            <a href="../../../App/Main.aspx" id="logoLink" runat="server">
                <img src="../../includes/UI/images/logo.png" alt="" /></a>
        </aside>

        <ul class="nav no-print" id="headderlinks" runat="server">
           <%-- <a id="headerHome" class="adminMenu" visible="false" runat="server" href="../../../App/Main.aspx">Home</a>--%>
            <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
        </ul>
         
        <aside class="profileAction no-print">
          
            <a id="headerMaster" class="adminMenu" runat="server" visible="true" href="../../../App/Search.aspx">Customer Site</a>
            <span class="guestName">Welcome
            <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></span>
            <div id="headderlogout" class="setting" runat="server">
                <a href="javascript:void(0)"></a>
                <ul id="Ul1">
					<li><a href="../../../App/ChangePassword.aspx"><i class="icon-pass"></i>Change Password</a></li>
					<li><a href="../../../App/logout.aspx"><i class="icon-logout"></i>Log out</a></li>
                </ul>
             </div>
        </aside>

    </div>
</header>
<div class="clear"></div>

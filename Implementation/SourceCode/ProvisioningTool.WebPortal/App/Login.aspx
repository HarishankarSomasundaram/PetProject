<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/includes/UserControls/common/Includes.ascx" TagName="Includes" TagPrefix="ProvisioningTool" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <ProvisioningTool:Includes ID="Includes" runat="server" />

    <%--<%: System.Web.Optimization.Scripts.Render("~/bundles/login") %>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            //Script to Load ColorBox Frame on the UI
            $.cookie('SearchUser', null, { expires: 1 });
            $.cookie('siteID', null, { expires: 1 });
            $.cookie('SlideHeader', null, { expires: 1 });
            $.cookie('lefttab', null, { expires: 1 });
            $.cookie('SettingsTab', null, { expires: 1 });
            $.cookie('UsersTab', null, { expires: 1 });
            $.cookie('CustomerTab', null, { expires: 1 });
            
            setTimeout(parent.$.colorbox.close, 0);

            ////Clears all the cookies
            //var cookies = $.cookie();
            //for (var cookie in cookies) {
            //    $.removeCookie(cookie);
            //}

            //Load a Color box of page div item
            $(".inline").colorbox({
                onOpen: function () {
                    $('#lblForgotErrroMessage').text("");
                    var username = $('#txtUsername').val();
                    $('#txtforgotUsername').val(username);
                    $('#txtforgotUsername').hide();
                },
                rel: 'inline',
                inline: true,
                href: $(this).attr('href'),
                width: '330px',
                height: '195px',
                transition: 'none'
            });


            $("#btnSubmitForgotPassword").click(function () {

                var forgotusername = $('#txtforgotUsername').val();
                var email = $('#txtEmail').val();
                if (email != '') {
                    //__doPostBack('btnSubmitForgotPassword', '');
                    if (!validateEmail(email)) {
                        $('#lblForgotErrroMessage').css('color', 'red');
                        $('#lblForgotErrroMessage').text("Invalid email");
                        return false;
                    }
                    

                    //Ajax call to webmethod
                    var status;

                    $.ajax({
                        type: 'POST',
                        url: "../App/Login.aspx/EmailForForgotPassword",
                        data: '{"forgotusername":"' + forgotusername + '","email":"' + email + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        beforeSend: function () {
                            //alert("Sending Mail Please Wait...");
                           // $("#mailStatus").show();
                        },
                        success: function (data) {
                            status = data.d;
                            if (status == 'true') {
                                $('#lblMessage').css('color', 'green');
                                $('#lblMessage').text("Password has been sent to your email");
                                $.colorbox.close();
                                return true;
                            }
                            else {
                                $('#lblForgotErrroMessage').css('color', 'red');
                                $('#lblForgotErrroMessage').text("Email cannot be sent");
                                return false;
                            }
                        }
                    });
                }
                else {
                    $('#lblForgotErrroMessage').css('color', 'red');
                    $('#lblForgotErrroMessage').text("Please enter Email");
                    return false;
                }
            });

            function sendConfirm() {

                var forgotusername = $('#txtforgotUsername').val();
                var email = $('#txtEmail').val();
                if (email != '') {
                    //__doPostBack('btnSubmitForgotPassword', '');
                    if (!validateEmail(email)) {
                        $('#lblForgotErrroMessage').css('color', 'red');
                        $('#lblForgotErrroMessage').text("Invalid email");
                        return false;
                    }
                    
                    sendEmail(forgotusername, email);
                }
                else {
                    $('#lblForgotErrroMessage').css('color', 'red');
                    $('#lblForgotErrroMessage').text("Please enter Email");
                    return false;
                }
            }

            function validateEmail($email) {
                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
                if (!emailReg.test($email)) {
                    return false;
                } else {
                    return true;
                }
            }

            function sendEmail(forgotusername, email) {
                //Ajax call to webmethod
                var status;
                
                $.ajax({
                    type: 'POST',
                    url: "../App/Login.aspx/EmailForForgotPassword",
                    data: '{"forgotusername":"' + forgotusername + '","email":"' + email + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    beforeSend: function () {
                        //alert("Sending Mail Please Wait...");
                    },
                    success: function (data) {
                        status = data.d;
                        if (status == 'true') {
                            $('#lblMessage').css('color', 'green');
                            $('#lblMessage').text("Password has been sent to your email");
                            $.colorbox.close();
                            return true;
                        }
                        else {
                            $('#lblForgotErrroMessage').css('color', 'red');
                            $('#lblForgotErrroMessage').text("Email cannot be sent");
                            return false;
                        }
                    }
                });
            }
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header>
                <div class="container">
                    <aside class="logo">
                        <a href="javascript:void(0)">
                            <img src="../../includes/UI/images/logo.png" alt="" /></a>
                    </aside>
                </div>
            </header>
            <section id="contentWrap" class="login">
                <div class="loginWrap">
                    <h1>Login To Your Account</h1>
                    <div class="formWrap">
                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" CssClass="signInError"></asp:Label>
                        <div class="inputUserWrap">
                            <asp:TextBox ID="txtUsername" runat="server" class="watermark" placeholder="Username"></asp:TextBox>
                        </div>
                        <div class="inputPassWordWrap">
                            <asp:TextBox ID="txtPassword" runat="server" class="watermark" placeholder="Password" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="submitWrap">
                            <asp:Button ID="btnLogin" runat="server" class="actionBtn" Text="Login" OnClick="btnLogin_Click" />
                            <asp:LinkButton ID="lbtnforgotPassword" runat="server" href="#divForgotpassword" CssClass="forgotPass inline">Forgot Password?</asp:LinkButton>
                        </div>

                    </div>
                </div>

            </section>
            <div id="iframeloading" style="display: none; position: absolute; top: 100px; left: -20px; width: 100%; height: 100%; z-index:9999999;">
                <img src="../../includes/UI/images/progress.gif" alt="loading" style="top: 50%; position: relative; left: 50%;" />
            </div>
            <div class="clear"></div>
            <div style="display: none;">
                <div class="loginWrap" id="divForgotpassword" style="padding: 10px;">
                    <h1>Forgot Password</h1>
                    <div class="formWrap">
                        <asp:Label ID="lblForgotErrroMessage" runat="server" Text="" ForeColor="Red" CssClass="signInError"></asp:Label>
                        <div class="inputUserWrap">
                            <asp:TextBox ID="txtforgotUsername" runat="server" class="watermark" placeholder="Username"></asp:TextBox>

                        </div>
                        <asp:Label ID="lblemail" runat="server" Text="" ForeColor="Red" CssClass="signInError">
                            <asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" CssClass="requiredField"
                                ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="*" InitialValue="" SetFocusOnError="true"
                                ValidationGroup="Req">*</asp:RequiredFieldValidator></asp:Label>
                        <div class="inputUserWrap">
                            <asp:TextBox ID="txtEmail" runat="server" class="watermark" placeholder="Email" TextMode="Email"></asp:TextBox>

                        </div>

                        <div class="submitWrap">
                            <asp:Button ID="btnSubmitForgotPassword" runat="server" class="actionBtn" Text="Send" />
                        </div>
                        
                    </div>
                    <!--span-- id="mailStatus" style="display:none;">Please wait while we are sending mail to you...</span-->
                </div>
            </div>
            <footer>
                <p>© 2014 - 2015 - intelligIS - All Rights Reserved</p>
            </footer>

        </div>
    </form>
</body>
</html>

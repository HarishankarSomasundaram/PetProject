// JavaScript Document

$(document).ready(function () {


    $('#iCustomer').click(function () {
        window.location = '/App/Masters/CustomerInfo.aspx';
    });

    $('#iUsers').click(function () {
        window.location = '/App/Users.aspx';
    });
    $('#iSettings').click(function () {
        window.location = '/App/Settings.aspx';
    });
   
    //$('#iCustomerSites').click(function () {
   
    //});
  


    $('#iSearchCustomer').click(function () {
        window.location = '/App/Masters/CustomerInfo.aspx?mode=s&search=1';
    });
    $('#iCreateCustomer').click(function () {
        window.location = '/App/Masters/CustomerInfo.aspx?mode=a&do=a';
    });
    $('#iModifyCustomer').click(function () {
        window.location = '/App/Masters/CustomerInfo.aspx?mode=v&search=1';
    });



    $('#iSearchUsers').click(function () {
        window.location = '/App/Users.aspx?mode=s&search=1';
    });
    $('#iCreateUsers').click(function () {
        window.location = '/App/Users.aspx?mode=a&do=a';
    });
    $('#iModifyUsers').click(function () {
        window.location = '/App/Users.aspx?mode=v&search=1';
    });


    $('#iSysLocale').click(function () {
        window.location = '/App/Controls.aspx?menu=SystemLocale';
    });
    $('#iSysUser').click(function () {
        window.location = '/App/Controls.aspx?menu=SystemUser';
    });
    $('#iHardware').click(function () {
        window.location = '/App/Controls.aspx?menu=Hardware';
    });
    $('#iSoftware').click(function () {
        window.location = '/App/Controls.aspx?menu=Software';
    });
    $('#iServiceProvider').click(function () {
        window.location = '/App/Controls.aspx?menu=ServiceProvider';
    });



    $('#iSearchCustomerSites').click(function () {
        window.location = '/App/CustomerSites.aspx?mode=s&search=1';
    });

    $('#iCreateCustomerSites').click(function () {
        window.location = '/App/CustomerSites.aspx?mode=a&do=a';
    });

    $('#iModifyCustomerSites').click(function () {
        window.location = '/App/CustomerSites.aspx?mode=v&search=1';
    });
    
});
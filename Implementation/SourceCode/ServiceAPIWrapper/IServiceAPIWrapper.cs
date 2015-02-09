using System.Collections.Generic;
using ServiceAPIWrapper.AutotaskWSDL;

namespace ServiceAPIWrapper
{
    interface IServiceAPIWrapper
    {
        void Connect(string Userid, string Password);
        List<Account> QueryAccount(string SearchField, string SearchValue);
        List<Contact> QueryContact(string SearchField, string SearchValue);
        //Entity[] Query(string Entity, string SearchField, string SearchValue);
    }
}

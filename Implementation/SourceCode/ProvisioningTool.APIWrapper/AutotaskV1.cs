using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using ServiceAPIWrapper.AutotaskWSDL;

namespace ServiceAPIWrapper
{
    public class AutotaskV1 : IServiceAPIWrapper
    {
        private ATWSZoneInfo zoneInfo = null;
        public ATWSSoapClient client = null;

        public void Connect(string Userid, string Password)
        {
            client = new ATWSSoapClient();
            zoneInfo = client.getZoneInfo(Userid);

            // Create the binding.
            // must use BasicHttpBinding instead of WSHttpBinding
            // otherwise a "SOAP header Action was not understood." is thrown.
            BasicHttpBinding myBinding = new BasicHttpBinding();
            myBinding.Security.Mode = BasicHttpSecurityMode.Transport;
            myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

            // Must set the size otherwise
            // The maximum message size quota for incoming messages (65536) has been exceeded. To increase the quota, use the MaxReceivedMessageSize property on the appropriate binding element.
            myBinding.MaxReceivedMessageSize = 2147483647;

            // Create the endpoint address.
            EndpointAddress ea = new EndpointAddress(zoneInfo.URL);

            client = new ATWSSoapClient(myBinding, ea);
            client.ClientCredentials.UserName.UserName = Userid;
            client.ClientCredentials.UserName.Password = Password;
        }

        public List<Account> QueryAccount(string SearchField, string SearchValue)
        {
            bool hasMoreResult = false;
            return QueryAccount(SearchField, SearchValue, out hasMoreResult);
        }

        public List<Account> QueryAccount(string SearchField, string SearchValue, out bool hasMoreResult)
        {
            List<Account> AccountResults = null;
            Entity[] EntityResults = Query(Constant.Account, SearchField, SearchValue, out hasMoreResult);
            if (EntityResults != null && EntityResults.Length > 0)
            {
                AccountResults = new List<Account>();
                foreach (var item in EntityResults)
                {
                    Account acct = (Account)item;
                    AccountResults.Add(acct);
                }
            }
            return AccountResults;
        }

        public List<Contact> QueryContact(string SearchField, string SearchValue)
        {
            bool hasMoreResult = false;
            return QueryContact(SearchField, SearchValue, out hasMoreResult);
        }

        public List<Contact> QueryContact(string SearchField, string SearchValue, out bool hasMoreResult)
        {
            List<Contact> ContactResults = null;
            Entity[] EntityResults = Query(Constant.Contact, SearchField, SearchValue, out hasMoreResult);
            if (EntityResults != null && EntityResults.Length > 0)
            {
                ContactResults = new List<Contact>();
                foreach (var item in EntityResults)
                {
                    Contact acct = (Contact)item;
                    ContactResults.Add(acct);
                }
            }
            return ContactResults;
        }

        private Entity[] Query(string Entity, string SearchField, string SearchValue, out bool hasMoreResult)
        {
            //by default assume the result has less than 500 results
            hasMoreResult = false;
            // query for any account. This should return all accounts since we are retreiving anything greater than 0.
            StringBuilder sb = new StringBuilder();
            /*
            sb.Append(string.Format("<queryxml><entity>{0}</entity>", Entity)).Append(System.Environment.NewLine);
            sb.Append(string.Format("<query><field>{0}<expression op=\"Contains\">{1}</expression></field></query>", SearchField, SearchValue)).Append(System.Environment.NewLine);
            sb.Append("</queryxml>").Append(System.Environment.NewLine);
            */
            sb.Append(string.Format(Constant.SingleConditionQuery, Entity, SearchField, SearchValue)).Append(System.Environment.NewLine);

            AutotaskIntegrations at_integrations = new AutotaskIntegrations();

            // this will not handle the 500 results limitation.
            // Autotask only returns up to 500 results in a response. if there are more you must query again for the next 500.
            var r = client.query(at_integrations, sb.ToString());
            if (r.ReturnCode == 1)
            {
                if (r.EntityResults.Length > 0)
                {
                    if (r.EntityResults.Length > 500)
                        hasMoreResult = true;
                    return r.EntityResults;
                }
            }
            return null;
        }
    }
}

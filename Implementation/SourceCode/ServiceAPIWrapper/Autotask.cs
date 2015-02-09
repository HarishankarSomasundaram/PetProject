using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using ServiceAPIWrapper.AutotaskWSDL;

namespace ServiceAPIWrapper
{
    public class Autotask
    {
        private ATWSZoneInfo zoneInfo = null;
        public ATWSSoapClient client = null;
        public bool IsConnected = false;

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
            IsConnected = true;
        }

        public List<object> Query(EntityEnum Entity, string SearchField, string SearchValue)
        {
            bool hasMoreResult = false;
            return Query(Entity, SearchField, SearchValue, out hasMoreResult);
        }

        private List<object> Query(EntityEnum Entity, string SearchField, string SearchValue, out bool hasMoreResult)
        {
            string sEntity = (Entity == EntityEnum.Customer) ? Constant.Account : (Entity == EntityEnum.User) ? Constant.Contact : "";
            if (string.IsNullOrEmpty(sEntity))
                throw new ArgumentException("Invalid entity details", "Entity", null);

            //by default assume the result has less than 500 results
            hasMoreResult = false;
            // query for any account. This should return all accounts since we are retreiving anything greater than 0.
            AutotaskIntegrations at_integrations = new AutotaskIntegrations();
            List<object> Results = null;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(Constant.SingleConditionQuery, sEntity, SearchField, SearchValue)).Append(System.Environment.NewLine);

            // this will not handle the 500 results limitation.
            // Autotask only returns up to 500 results in a response. if there are more you must query again for the next 500.
            var r = client.query(at_integrations, sb.ToString());
            if (r.ReturnCode == 1)
            {
                if (r.EntityResults.Length > 0)
                {
                    if (r.EntityResults.Length > 500)
                    {
                        hasMoreResult = true;
                    }
                    if (r.EntityResults != null && r.EntityResults.Length > 0)
                    {
                        Results = new List<object>();
                        foreach (var item in r.EntityResults)
                        {
                            Results.Add(item);
                        }
                    }
                    return Results;
                }
            }

            return null;
        }
    }
}

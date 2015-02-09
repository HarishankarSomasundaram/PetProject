using System.Collections.Generic;
using ServiceAPIWrapper;
using ServiceAPIWrapper.AutotaskWSDL;

namespace APIWrapper
{
    public class ConnectWise : IServiceAPIWrapper
    {
        private ATWSZoneInfo zoneInfo = null;
        public ATWSSoapClient client = null;

        public void Connect(string Userid, string Password)
        {
        }

        public List<Account> QueryAccount(string SearchField, string SearchValue)
        {
            return null;
        }

        public List<Contact> QueryContact(string SearchField, string SearchValue)
        {
            return null;
        }

        private Entity[] Query(string Entity, string SearchField, string SearchValue)
        {
            return null;
        }
    }
}
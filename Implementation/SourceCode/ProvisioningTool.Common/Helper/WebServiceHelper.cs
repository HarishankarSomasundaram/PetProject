using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace ProvisioningTool.Common
{
    public class WebServiceHelper
    {
        #region [Post Request]
        public bool PostRequest(string URL)
        {
            bool isPosted = false;
            HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(URL);
            postRequest.Method = "POST";
            postRequest.KeepAlive = false;
            postRequest.Timeout = 5000;
            // Get the request stream
            Stream postStream = postRequest.GetRequestStream();
            //Get response from server
            HttpWebResponse postResponse = (HttpWebResponse)postRequest.GetResponse();
            if (postResponse.StatusCode == HttpStatusCode.OK)
                isPosted = true;
            postStream.Close();
            postResponse.Close();
            return isPosted;
        }
        #endregion [Post Request]

        #region [Get Request]
        public object GetRequest(string URL)
        {
            Stream GETResponseStream = null;
            StreamReader sReader = null;
            string jsonResponse = string.Empty;
            try
            {
                //Generate get request
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(URL);
                GETRequest.Method = "GET";
                HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();
                GETResponseStream = GETResponse.GetResponseStream();
                sReader = new StreamReader(GETResponseStream, encode);
                jsonResponse = sReader.ReadToEnd().Trim();

                ////If the response is in XML then use the below code
                //DataSet ds = new DataSet();
                //using (StringReader stringReader = new StringReader(jsonResponse))
                //{
                //    ds = new DataSet();
                //    ds.ReadXml(stringReader);
                //}
                //DataTable dt = ds.Tables[0];

                DataTable dt2 = new DataTable();
                dt2 = ConvertToStreamObject(jsonResponse);
                return jsonResponse;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sReader != null) sReader.Close();
                if (GETResponseStream != null) GETResponseStream.Close();

            }
        }
        #endregion [Get Request]


        public static DataTable ConvertToStreamObject(string jsonResponseString)
        {
            try
            {
                DataTable dt1 = new DataTable();
                dt1 = JsonConvert.DeserializeObject<DataTable>(jsonResponseString);

                return dt1;
            }
            catch
            {
                return null;
            }
        }
    }
}
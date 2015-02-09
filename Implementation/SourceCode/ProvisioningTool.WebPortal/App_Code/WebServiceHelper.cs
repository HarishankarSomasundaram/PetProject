using Library;
using Newtonsoft.Json;
using ProvisioningTool.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for WebServiceHelper
/// </summary>
public class WebServiceHelper
{
    public WebServiceHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region [Post Request]
    public T PostRequest<T>(PTRequest request)
    {
        PTResponse response = new PTResponse();
        response.isPosted = false;
        Stream postStream = null;
        HttpWebResponse postResponse = null;

        try
        {
            Uri uri = new Uri(ConvertHelper.ConvertToString(request.URL, ""));
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json; charset=utf-8";
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string serOut = jsonSerializer.Serialize(request);
            using (StreamWriter writer = new StreamWriter(webRequest.GetRequestStream()))
            {
                writer.Write(serOut);
            }

            WebResponse webResponce = webRequest.GetResponse();
            Stream reader = webResponce.GetResponseStream();

            StreamReader sReader = new StreamReader(reader);
            string outResult = sReader.ReadToEnd();
            sReader.Close();
            return ConvertToObject<T>(outResult); 
        }
        catch (Exception ex)
        {
            return default(T);
        }
        finally
        {
            if (postStream != null) postStream.Close();
            if (postResponse != null) postResponse.Close();

        }

    }
    #endregion [Post Request]
    


    #region [Get Request]
    public string GetRequest(string url)
    {
        PTResponse response = new PTResponse();
        Stream GETResponseStream = null;
        StreamReader sReader = null;
        string jsonResponse = string.Empty;

        try
        {
            //Generate get request
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            HttpWebRequest GETRequest = (HttpWebRequest)WebRequest.Create(ConvertHelper.ConvertToString(url, ""));
            GETRequest.Method = "GET";

            HttpWebResponse GETResponse = (HttpWebResponse)GETRequest.GetResponse();
            GETResponseStream = GETResponse.GetResponseStream();
            sReader = new StreamReader(GETResponseStream, encode);
            jsonResponse = sReader.ReadToEnd().Trim();
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

    #region [ConvertToGlobalMasterDetailObject]

    public List<T> ConvertToObjectList<T>(string jsonResponseString)
    {
        try
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonResponseString);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    public T ConvertToObject<T>(string jsonResponseString)
    {
        return JsonConvert.DeserializeObject<T>(jsonResponseString);
    }

    #endregion [ConvertToGlobalMasterDetailObject]
}
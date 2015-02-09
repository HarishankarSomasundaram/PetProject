using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using Library;
using iTextSharp.tool.xml;

public partial class App_PrintPageLayout : FormController
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sPrintContent = "";
        sPrintContent = hidPrintContent.Text;
        /*
            .clearfix {*zoom: 1;}
            .clearfix:before,.clearfix:after {content: '';display: table;}
            .clearfix:after {clear: both;}
            .heading {font-size: 20px;line-height: 30px;text-align: left;margin: 0 0 0 10px;font-family: 'source_sans_probold';margin-bottom: 15px;}
            .line {position: relative;top: 0px;left: 10px;height: 1px;width: 87%;background: #ccc;}
            .check {line-height: 2;}
            .watermark {float: right;}
         */
        /*sPrintContent = sPrintContent.Replace("class=\"line\"", "style=\"position: relative;top: 0px;left: 10px;height: 1px;width: 87%;background: #ccc;\"");
        sPrintContent = sPrintContent.Replace("class=\"watermark\"", "style=\"float: right;\"");
        sPrintContent = sPrintContent.Replace("class=\"heading\"", "style=\"font-size: 20px;line-height: 30px;text-align: left;margin: 0 0 0 10px;font-family: 'source_sans_probold';margin-bottom: 15px;\"");
        sPrintContent = sPrintContent.Replace("class=\"check\"", "style=\"line-height: 2;\"");
        printContent.InnerHtml = sPrintContent;*/
    }

    //protected void btnExport2PDF_Click(object sender, EventArgs e)
    //{
    //    string sPrintContent = "";
    //    sPrintContent = hidPrintContent.Text;
    //    ExporttoPDF(sPrintContent);
    //}

    //private void ExporttoPDF(string html)
    //{
    //    try
    //    {
    //        if (!string.IsNullOrEmpty(html))
    //        {
    //            var by = System.Text.Encoding.UTF8.GetBytes(html);
    //            using (var input = new MemoryStream(by))
    //            {
    //                MemoryStream output = new MemoryStream(); // this MemoryStream is closed by FileStreamResult

    //                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 50, 50, 50, 50);
    //                PdfWriter writer = PdfWriter.GetInstance(document, output);
    //                writer.CloseStream = false;
    //                document.Open();

    //                XMLWorkerHelper xmlW = XMLWorkerHelper.GetInstance();
    //                xmlW.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
    //                document.Close();
    //                output.Position = 0;

    //                byte[] bytes = output.GetBuffer();
    //                //byte[] bytes = output.ToArray();
    //                output.Close();

    //                Response.Buffer = true;
    //                Response.Clear();
    //                Response.ContentType = "application/pdf";
    //                Response.AddHeader("content-disposition", "attachment; filename=report.pdf");
    //                Response.BinaryWrite(bytes);
    //                Response.Flush();
    //                //Response.End();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public string HttpContent(string url)
    //{
    //    string result = "";
    //    System.Net.WebRequest objRequest = System.Net.HttpWebRequest.Create(url.Trim());
    //    StreamReader sr = new StreamReader(objRequest.GetResponse().GetResponseStream());
    //    result = sr.ReadToEnd();
    //    sr.Close();


    //    return result;
    //}


}

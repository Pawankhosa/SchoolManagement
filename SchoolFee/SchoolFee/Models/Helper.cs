using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

using Admin2.Models;
using System.Security.Cryptography;
using System.Data;
using System.Data.OleDb;


namespace SchoolManagement.Controllers
{
    public class Helper
    {
        dbcontext db = new dbcontext();
        public string _FileName;
        const string passphrase = "password";
        string _path,path;
        public string uploadfile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);

                    _FileName = Guid.NewGuid().ToString().Substring(0, 4) + _FileName;
                    string _path = (HttpContext.Current.Server.MapPath("/UploadedFiles/" + _FileName));

                    file.SaveAs(_path);
                }

                return _FileName;
            }
            catch (Exception e)
            {
                return "False";
            }
        }
        public void excel(HttpPostedFileBase Data)
        {
            try
            {
                if (Data.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(Data.FileName);
                    path = "../UploadedFiles/" + _FileName;
                    _FileName = Guid.NewGuid().ToString().Substring(0, 4) + _FileName;
                     _path = (HttpContext.Current.Server.MapPath("/UploadedFiles/" + _FileName));

                    Data.SaveAs(_path);
                }

                
            }
            catch { }
            
            DataSet ds = new DataSet();

            OleDbCommand excelCommand = new OleDbCommand(); OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter();

            string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + HttpContext.Current.Server.MapPath(path) + ";Extended Properties=Excel 12.0;";

            OleDbConnection excelConn = new OleDbConnection(excelConnStr);

            excelConn.Open();

            DataTable dtPatterns = new DataTable();
            excelCommand = new OleDbCommand("select * from [sheet1$]", excelConn);

            excelDataAdapter.SelectCommand = excelCommand;

            excelDataAdapter.Fill(dtPatterns);



            ds.Tables.Add(dtPatterns);
            //Grd.DataSource = dtPatterns;
            //Grd.DataBind();

            
        }

        public string EncryptData(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();

                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public string DecryptData(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        public string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            
            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }
        //public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        //{
        //    dbcontext db = new dbcontext();
        //    var office = db.officedetails.ToList();

        //    using (MailMessage mailMessage = new MailMessage(office[0].Emailid, recepientEmail))
        //    {

        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        mailMessage.IsBodyHtml = true;

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(office[0].Emailid, office[0].Password);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mailMessage);
        //    }
        //}
        public string SendSMS(string User, string sender, string to, string message, string type,string api)
        {
            string stringpost = "username=" + User + "&message=" + message + "&sendername=" + sender + "&smstype=" + type + "&numbers=" + to + "&apikey="+api+"";
            //Response.Write(stringpost)
            string functionReturnValue = null;
            functionReturnValue = "";

            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;

            try
            {
                string stringResult = null;

                objWebRequest = (HttpWebRequest)WebRequest.Create("http://sms.officialsolutions.in/sendSMS");
                //domain name: Domain name Replace With Your Domain  
                objWebRequest.Method = "Post";

                // Response.Write(objWebRequest)

                // Use below code if you want to SETUP PROXY.
                //Parameters to pass: 1. ProxyAddress 2. Port
                //You can find both the parameters in Connection settings of your internet explorer.


                // If You are In the proxy Then You Uncomment the below lines and Enter IP And Port Number


                //System.Net.WebProxy myProxy = new System.Net.WebProxy("192.168.1.108", 6666);
                //myProxy.BypassProxyOnLocal = true;
                //objWebRequest.Proxy = myProxy;

                objWebRequest.ContentType = "application/x-www-form-urlencoded";

                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();

                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();


                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();

                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                return (stringResult);
            }
            catch (Exception ex)
            {
                return (ex.ToString());

            }
            finally
            {
                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;

            }
        }

        //public string smssetting(string mobile, string Message)
        //{
        //    var setting = db.EmailSettings.ToList();
        //    if (setting != null)
        //    {
                
        //            SendSMS(setting[0].SmsUser, setting[0].password, mobile, Message, "Trans", setting[0].Api);
                
        //        return ("Done");
        //    }
        //    return ("Done");

        //}
        public string PopulateBody(string userName, string title, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/Template/ConfirmEmail.txt")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Description}", description);
            return body;
        }
        //public void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        //{
        //    dbcontext db = new dbcontext();
        //    var office = db.EmailSettings.ToList();

        //    using (MailMessage mailMessage = new MailMessage(office[0].Email, recepientEmail))
        //    {

        //        mailMessage.Subject = subject;
        //        mailMessage.Body = body;
        //        mailMessage.IsBodyHtml = true;

        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;
        //        NetworkCredential NetworkCred = new NetworkCredential(office[0].Email, office[0].Password);
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mailMessage);
        //    }
        //}
    }
    public enum NotificationEnumeration
    {
        Success,
        Error,
        Warning
    }


}

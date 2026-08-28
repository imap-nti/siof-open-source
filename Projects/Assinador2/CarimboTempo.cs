using Disig.TimeStampClient;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Assinador2
{
    public class CarimboTempo
    {
        #region Current
        private static string uid => Environment.GetEnvironmentVariable("SIOF_TSA_UID");
        private static string key => Environment.GetEnvironmentVariable("SIOF_TSA_KEY");
        private static string uri => Environment.GetEnvironmentVariable("SIOF_TSA_URI") ?? "https://act.bry.com.br";

        public static byte[] Carimbar(byte[] @in)
        {
            var oid = Oid.SHA256;

            var digest = DigestUtils.ComputeDigest(@in, oid);

            var auth = new NetworkCredential { UserName = uid, Password = key };

            var credentials = new UserCredentials(auth);

            var request = new Request(digest, oid, null, string.Empty, true);

            var token = TimeStampClient.RequestTimeStampToken(uri, request, credentials);

            return token.ToByteArray();
        }

        #endregion

        #region Old
        System.Collections.Specialized.NameValueCollection nvc = new System.Collections.Specialized.NameValueCollection();
        //System.IO.Stream retorno;

        public CarimboTempo(String hash_code)
        {
            nvc.Add("cmd", "doLWDirectTimeStamping");
            //nvc.Add("user_id", "37"); usuario de teste
            nvc.Add("user_id", "45");
            nvc.Add("user_key", "Imap_ashjdasgfajsfas");
            nvc.Add("transaction_id", "1");
            nvc.Add("return_email", "sistemas@portalimap.org.br.com");
            nvc.Add("return_errors", "sistemas@portalimap.org.br.com");//thiago@portalimap.org.br
            nvc.Add("hash_code", hash_code); //"aIt1B55zY8rXbXMXPwtUew==");
            nvc.Add("algoritm", "SHA1");

        }

        public byte[] Carimbar()
        {
            //GetPage("http://www.selopostal.com/comprova/servlet/timestamper", nvc);
            //return GetPage("https://www.comprova.com/servlet/timestamper", nvc);
            //return GetPage("https://uat13.selopostal.com/VAComprova3/VAService", nvc);
            return GetPage("https://www.selopostal.com.br/servlet/timestamper", nvc);
        }

        public Byte[] GetContent()
        {
            return null;// ReadFully(retorno);
        }

        private byte[] GetPage(String url, System.Collections.Specialized.NameValueCollection nvc)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            HttpWebRequest requisicao = (HttpWebRequest)WebRequest.Create(url);
            requisicao.Method = "POST";
            requisicao.Accept = "text/html, application/xhtml+xml, */*";
            requisicao.Headers.Add("Accept-Language", "pt-BR");
            requisicao.AllowAutoRedirect = true;
            requisicao.KeepAlive = true;
            requisicao.Headers.Add("Cache-Control", "no-cache");
            requisicao.Headers.Add("Accept-Encoding", "gzip, deflate");
            requisicao.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; MANM; rv:11.0) like Gecko";
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            requisicao.ContentType = "multipart/form-data; boundary=" + boundary;
            requisicao.ProtocolVersion = HttpVersion.Version11;
            StringBuilder sbPostData = new StringBuilder();

            bool primeiraVez = true;

            if (nvc != null)
            {
                foreach (string key in nvc.AllKeys)
                {
                    string[] values = nvc.GetValues(key);
                    if (values != null)
                    {
                        foreach (string value in values)
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                if (primeiraVez)
                                {
                                    sbPostData.Append("--" + boundary + "\r\n");
                                    primeiraVez = false;
                                }
                                else
                                {
                                    sbPostData.Append("\r\n" + "--" + boundary + "\r\n");
                                }

                                sbPostData.Append("Content-Disposition: form-data; name=\"" + HttpUtility.UrlEncode(key) + "\"" + "\r\n");
                                sbPostData.Append("\r\n");
                                sbPostData.Append(value);

                            }

                        }
                    }
                }
            }

            sbPostData.Append("\r\n" + "--" + boundary + "--" + "\r\n");

            var parameterString = Encoding.UTF8.GetBytes(sbPostData.ToString());
            //requisicao.Referer = "http://localhost:2817/Default.aspx";
            requisicao.ContentLength = sbPostData.ToString().Length;

            byte[] ret;
            using (Stream requestStream = requisicao.GetRequestStream())
            {
                requestStream.Write(parameterString, 0, parameterString.Length);
                requestStream.Close();
                try
                {

                    //nothig is received in cookies. Status of response 200 (OK), but on the web page is error that Session is Time OUT. Please Login again
                    using (HttpWebResponse response = requisicao.GetResponse() as HttpWebResponse)
                    {
                        ret = ReadFully(response.GetResponseStream());
                        //using (StreamReader stIn = new System.IO.StreamReader(response.GetResponseStream()))
                        //{

                        //    //here I receive session Time Out. Please login again
                        //    string s = stIn.ReadToEnd();
                        //}
                    }
                }
                catch (WebException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw e;
                }
                //requestStream.Close();
                return ret;
            }
        }

        public byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #endregion
    }
}
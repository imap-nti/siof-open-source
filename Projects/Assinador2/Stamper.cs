using System;
using System.IO;
using System.Xml;
using System.Net;

namespace Assinador2
{
    public class Stamper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">Arquivo qual deseja-se obter hash</param>
        /// <returns>Hash do arquivo de entrada</returns>
        public string GetBase64SHA1HashFromFile(byte[] file)
        {
            using (MemoryStream fs = new MemoryStream(file))
            using (System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                return Convert.ToBase64String(sha1.ComputeHash(fs));
            }
        }

        /// <summary>
        /// Método utilizado para gerar arquivo XML a partir de Hash de um arquivo para verificação de integridade em duas fases.
        /// </summary>
        /// <param name="hashFile">Hash gerado de um arquivo qualquer</param>
        /// <returns>Documento XML</returns>
        public XmlDocument GerarXMLCarimboTempo(string hashFile)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-16", null));
            XmlElement Signed = xml.CreateElement("signed");
            Signed.SetAttribute("type", "TimeStamp");
            XmlElement File = xml.CreateElement("File");
            XmlElement Hash = xml.CreateElement("Hash");
            Hash.InnerXml = hashFile;
            File.AppendChild(Hash);
            Signed.AppendChild(File);
            xml.AppendChild(Signed);

            XmlElement TimeStamp = xml.CreateElement("TimeStamp");
            Signed.AppendChild(TimeStamp);

            XmlElement DateTime = xml.CreateElement("DateTime");
            string data = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            DateTime.InnerXml = data;
            TimeStamp.AppendChild(DateTime);

            XmlElement TimeStampHash = xml.CreateElement("Hash");
            TimeStampHash.InnerXml = getAsciiEncodedBase64SHA1FromString(data);
            TimeStamp.AppendChild(TimeStampHash);

            XmlElement Signature = xml.CreateElement("Signature");
            Signature.InnerXml = getAsciiEncodedBase64SHA1FromString("TimeStamp" + xml.ChildNodes[1].InnerXml);
            Signed.AppendChild(Signature);


            return xml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str">String qual deseja-se obter hash</param>
        /// <returns>Hash da string de entrada</returns>
        public string getAsciiEncodedBase64SHA1FromString(string str)
        {
            string rethash = "";
            try
            {
                System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
                System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
                byte[] combined = encoder.GetBytes(str);
                hash.ComputeHash(combined);
                rethash = Convert.ToBase64String(hash.Hash);
            }
            catch (Exception ex)
            {
                string strerr = "Error in HashCode : " + ex.Message;
            }
            return rethash;
        }

        public byte[] getFileStamp(HttpWebRequest request)
        {
            try
            {
                request.Headers.Add("", "");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                byte[] buffer = new byte[2048];

                MemoryStream file = new MemoryStream();

                int readCount = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                while (readCount > 0)
                {
                    file.Write(buffer, 0, readCount);
                    readCount = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                }

                return file.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void decryptContainer(string dlc_content)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://dcrypt.it/decrypt/paste");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            byte[] _byteVersion = System.Text.Encoding.ASCII.GetBytes(string.Concat("content=", dlc_content));

            request.ContentLength = _byteVersion.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
    }
}

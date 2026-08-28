using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace DsAdmin
{
    public class FTPManager
    {
        public bool CreateDirectoryInFTP(string directory, string uid, string pwd)
        {
            try
            {
                FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(new Uri(directory));
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(uid, pwd);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                ftpStream.Close();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return false;
                }
                else
                {
                    response.Close();
                    throw ex;
                }
            }
        }

        public byte[] DownloadFileFromFTP(string ftpfilepath, string user, string pass)
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpfilepath);

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential(user, pass);
            
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            byte[] buffer = new byte[2048];

            MemoryStream newFile = new MemoryStream();

            int readCount = responseStream.Read(buffer, 0, buffer.Length);
            while (readCount > 0)
            {
                newFile.Write(buffer, 0, readCount);
                readCount = responseStream.Read(buffer, 0, buffer.Length);
            }
            newFile.Close();
            responseStream.Close();
            response.Close();
            return newFile.ToArray();
        }

        public bool UploadFileToFTP(byte[] file, string ftphost, string ftpfilepath, string filename, string user, string pass)
        {
            try
            {
                string ftpfullpath = "ftp://" + ftphost + ftpfilepath + filename;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpfullpath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(user, pass);
                byte[] bytes = file;
                request.ContentLength = bytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFileOnFtp(string deleteFile, string user, string pass)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://" + deleteFile);
                ftpRequest.Credentials = new NetworkCredential(user, pass);

                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;

                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

                ftpResponse.Close();
                ftpRequest = null;
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using Ionic.Zip;


namespace Assinador2
{
    public class Zipper
    {
        public Byte[] ZipFiles(Dictionary<String, Byte[]> arquivos)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    foreach (String filename in arquivos.Keys)
                    {
                        zip.AddEntry(filename, arquivos[filename]);
                    }

                    zip.Comment = String.Format("Este arquivo zip foi criado pela aplicacao 'SiofSuporte Web' - '{0}'",
                       System.Net.Dns.GetHostName());

                    MemoryStream ms = new MemoryStream();
                    zip.Save(ms);

                    return ms.ToArray();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<String, Byte[]> UnZipFiles(Byte[] zipfile)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(new MemoryStream(zipfile)))
                {
                    Dictionary<String, Byte[]> files = new Dictionary<String, Byte[]>();

                    foreach (ZipEntry entry in zip.Entries)
                    {
                        MemoryStream ms = new MemoryStream();
                        entry.Extract(ms);

                        files.Add(entry.FileName, ms.ToArray());
                    }
                    return files;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

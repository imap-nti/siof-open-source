using System;
using System.Xml;
using System.IO;

namespace Assinador2
{
    public class Signer
    {
        private string certPath = Environment.GetEnvironmentVariable("SIOF_CERT_PATH") ?? @"C:/certificado/imap.pfx";
        private string certPwd = Environment.GetEnvironmentVariable("SIOF_CERT_PASSWORD");
        private string tempDir = Environment.GetEnvironmentVariable("SIOF_TEMP_DIR") ?? @"X:/suporte_app";
        private string licenca = "I48cdAeQI2C6Nr11wIRdjymp2hCRumkl44C3+5XZ+RP27+giIQrcOs8CaUDBVuwIueoIZXhXmG1T3wKfUtWbyiYjeN+CR9OrQpfnKL2PCmBetzHBRKeLuRfiNDIu/CIrMUR2Pt0AI2PyxnLloe19ai+XEsusO8j1JHlmO9OAh1qOY2taY4xWO4SpRpnZbHZsYLiluR5msQVUaaLL9lcuD58oHNy9xwANJi9+qJEjUyjdJ5g7sXMqDXPnEPjUL8zDAF9+nMgS79vWCSUxa+ZBmi1Kr/ofEvo3nyLD2Ef50UAlTYQDX1i92PNRAGhRYb2f+K8BqHHe40rq3k7nidrkXlti3hC84WglGdQ/xbRYUZAWFQwaubB/u4rMmoyHAT3yhxLf5MOe6dplOK9uLb71VQ0NkbENwf6iPg83sF18QMmtceQJg0u1JszHDpms4lYjC8AKIh8lgFNA/JUu7BYf1PS/aK5PyZ7eQjy3ziBidBTeEWP8FhI6WwJJhFpN2YY7rAEguJtO7LolTd2KuL7jaeb7PII5+Fzceikyujs/hkpDaFSLz6XxrIBX7+XgnKPZkTq+6aHSgnN4Nv2F8/KEIa25n6E1bD541mt26EP7JOovAY4chMc1hWajhMGKryWyVmVn/UxOBhu3lDNHI6bY/Xg3SeSiKjXSj57ddzzrt8rfx6cnZwf0pcKg2Pe+0Te820I3NXNYbey2YALQBeRkA5GMJcd3NVWdHyEY5SRWsdp0cTtn/cp4plLbn+kzfF3P1VQnTzFjECdwTrpQQjfJ2Lsc33Ny4P4OPI2V9KWOlDYtzfBNwrIZ4GpZ6H2ldrmdQqGxT+uQLIwFPbMyjMtZXy0v6/yD+y1qv8meshL650PBTMFtkxmKEATDhVcrwWX9+dt0AHfsR4cEosM8P+Q2qvV8ZHIcaBKRW6z3GY33ok9KBJWZoraS04FxprctyTE5Sq2c4RD2ShiBE/ko7X6+K vutNGS10NlDt+jlyxURYQNvEv6+tRc0KsBjo9+AsFrg+JFv11a5IYIUBR8MlAEPOpvMKdFpxhgfUnFCLP/mZSMqpB5ZQWPfdzAuw6mAvarDPHXZE5y8GR234sdarMuCOhSzU3ljfnB7";
        private string IdCertif = "";//"5D8278BCCF42ECF6600EF38795D41AC573610167"
        private string descricao = "Garantir autenticidade e integridade dos dados, através de assinatura digital ICP-Brasil gerada pelo software de propriedade do IMAP - Instituto Municipal de Administração Pública, sendo vedada a utilização por terceiros sem sua autorização, conforme Lei nº 9.609/98.";

        public Signer()
        {
            DeskSignerSDKPDF.clsCertificados c = new DeskSignerSDKPDF.clsCertificados();
            c.Licenca = licenca;

            //XmlDocument xml = new XmlDocument();
            //xml.LoadXml(c.ListarCertificadosPessoais());
            //throw new Exception("Certificados: " + c.ListarCertificadosPessoais());
            //foreach (XmlNode raiz in xml.DocumentElement.ChildNodes[1].ChildNodes)
            //{
            //    if (raiz.SelectSingleNode("Valido").InnerText.Equals("S")
            //        && Convert.ToDateTime(raiz.SelectSingleNode("dtFinalValidade").InnerText) >= DateTime.Today
            //        && raiz.SelectSingleNode("Nome").InnerXml.Contains("INSTITUTO MUNICIPAL DE ADMINISTRACAO PUBLICA"))
            //    {
            //        IdCertif = raiz.InnerXml;
            //    }
            //}
        }

        /// <summary>
        /// Método para assinatura digital de documento PDF.
        /// </summary>
        /// <param name="filePaht">Caminho utilizado para busca do arquivo em disco.</param>
        public byte[] AssinarDocumento(byte[] fileToSign)
        {
            DeskSignerSDKPDF.clsPDF clsPDF = new DeskSignerSDKPDF.clsPDF();
            clsPDF.Licenca = licenca;
            string fileTempPath = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff");
            byte[] fileToReturn;
            string tempDir = Environment.GetEnvironmentVariable("SIOF_TEMP_DIR") ?? @"X:\suporte_app";
            Directory.CreateDirectory(tempDir);
            //try
            //{
            FileStream fs = new FileStream(tempDir + "/" + fileTempPath + ".pdf", FileMode.CreateNew);
            fs.Write(fileToSign, 0, fileToSign.Length);
            clsPDF.Assinar(CarregarXML(tempDir + "/" + fileTempPath + ".pdf", descricao, IdCertif).InnerXml);
            
            fileToReturn = File.ReadAllBytes(tempDir + "/" + fileTempPath + "_assinado.pdf");
            fs.Close();

            File.Delete(tempDir + "/" + fileTempPath + ".pdf");
            File.Delete(tempDir + "/" + fileTempPath + "_assinado.pdf");
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return fileToReturn;
        }

        /// <summary>
        /// Método para assinatura digital de documento PDF.
        /// </summary>
        /// <param name="filePaht">Caminho utilizado para busca do arquivo em disco.</param>
        /// <param name="descricao">Descrição agregada ao arquivo durante sua assinatura.</param>
        public byte[] AssinarDocumento(byte[] fileToSign, string descricao)
        {
            DeskSignerSDKPDF.clsPDF clsPDF = new DeskSignerSDKPDF.clsPDF();
            clsPDF.Licenca = licenca;
            string fileTempPath = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ff");
            byte[] fileToReturn;
            Directory.CreateDirectory(tempDir);
            try
            {
                FileStream fs = new FileStream(tempDir + "/" + fileTempPath + ".pdf", FileMode.CreateNew);
                fs.Write(fileToSign, 0, fileToSign.Length);
                clsPDF.Assinar(CarregarXML(tempDir + "/" + fileTempPath + ".pdf", descricao, IdCertif).InnerXml);
                fileToReturn = File.ReadAllBytes(tempDir + "/" + fileTempPath + "_assinado.pdf");
                fs.Close();

                File.Delete(tempDir + "/" + fileTempPath + ".pdf");
                File.Delete(tempDir + "/" + fileTempPath + "_assinado.pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileToReturn;
        }

        /// <summary>
        /// Método utilizado para construir o XML necessário a assinatura digital.
        /// </summary>
        /// <param name="pathArquivo">Caminho onde é localizado o arquivo a ser assinado</param>
        /// <param name="descricao">Descrição agregada ao arquivo durante sua assinatura.</param>
        /// <param name="IdCertif">Idêntificador do certificado instalado no servidor</param>
        /// <returns></returns>
        public XmlDocument CarregarXML(string pathArquivo, string descricao, string IdCertif)
        {
            XmlDocument xml = new XmlDocument();
            XmlNode declaracao = xml.CreateXmlDeclaration("1.0", null, null);
            xml.AppendChild(declaracao);

            //Raiz
            XmlElement raiz = xml.CreateElement("DeskSignerSDKPDF");
            xml.AppendChild(raiz);

            //Caminho
            XmlElement caminho = xml.CreateElement("Caminho", "Caminho", null);
            raiz.AppendChild(caminho);
            caminho.InnerXml = pathArquivo;

            //Certificado
            XmlElement certificado = xml.CreateElement("Certificado", "Certificado", null);
            raiz.AppendChild(certificado);

            XmlElement IdCertificado = xml.CreateElement("IdCertif", "IdCertif", null);
            IdCertificado.InnerXml = IdCertif;
            certificado.AppendChild(IdCertificado);

            XmlElement CaminhoPKCS12 = xml.CreateElement("CaminhoPKCS12", "CaminhoPKCS12", null);
            CaminhoPKCS12.InnerXml = certPath;
            certificado.AppendChild(CaminhoPKCS12);

            XmlElement SenhaPKCS12 = xml.CreateElement("SenhaPKCS12", "SenhaPKCS12", null);
            SenhaPKCS12.InnerXml = certPwd;
            certificado.AppendChild(SenhaPKCS12);

            //CaminhoSaida
            XmlElement CaminhoSaida = xml.CreateElement("CaminhoSaida", "CaminhoSaida", null);
            CaminhoSaida.InnerXml = pathArquivo.Replace(".pdf", "_assinado.pdf");
            raiz.AppendChild(CaminhoSaida);

            //bPrimeiraPagina
            XmlElement bPrimeiraPagina = xml.CreateElement("bPrimeiraPagina", "bPrimeiraPagina", null);
            bPrimeiraPagina.InnerXml = "S";
            raiz.AppendChild(bPrimeiraPagina);

            //bUltimaPagina
            XmlElement bUltimaPagina = xml.CreateElement("bUltimaPagina", "bUltimaPagina", null);
            bUltimaPagina.InnerXml = "N";
            raiz.AppendChild(bUltimaPagina);

            //bTodasPaginas
            XmlElement bTodasPaginas = xml.CreateElement("bTodasPaginas", "bTodasPaginas", null);
            bTodasPaginas.InnerXml = "N";
            raiz.AppendChild(bTodasPaginas);

            //Descricao
            XmlElement Descricao = xml.CreateElement("Descricao", "Descricao", null);
            Descricao.InnerXml = descricao;
            raiz.AppendChild(Descricao);

            //Pagina
            XmlElement Pagina = xml.CreateElement("Pagina", "Pagina", null);
            Pagina.InnerXml = "0";
            raiz.AppendChild(Pagina);

            //bVisivel
            XmlElement bVisivel = xml.CreateElement("bVisivel", "bVisivel", null);
            bVisivel.InnerXml = "N";
            raiz.AppendChild(bVisivel);

            //bImagem
            XmlElement bImagem = xml.CreateElement("bImagem", "bImagem", null);
            bImagem.InnerXml = "N";
            bVisivel.AppendChild(bImagem);

            XmlElement CaminhoImagem = xml.CreateElement("CaminhoImagem", "CaminhoImagem", null);
            CaminhoImagem.InnerXml = tempDir;
            bImagem.AppendChild(CaminhoImagem);

            XmlElement TamImagem = xml.CreateElement("TamImagem", "TamImagem", null);
            TamImagem.InnerXml = "21";
            bImagem.AppendChild(TamImagem);

            XmlElement ImagemAltura = xml.CreateElement("ImagemAltura", "ImagemAltura", null);
            ImagemAltura.InnerXml = "57";
            bImagem.AppendChild(ImagemAltura);

            XmlElement ImagemLargura = xml.CreateElement("ImagemLargura", "ImagemLargura", null);
            ImagemLargura.InnerXml = "198";
            bImagem.AppendChild(ImagemLargura);

            //bTexto
            XmlElement bTexto = xml.CreateElement("bTexto", "bTexto", null);
            bTexto.InnerXml = "N";
            bVisivel.AppendChild(bTexto);

            XmlElement bTextoCompleto = xml.CreateElement("bTextoCompleto", "bTextoCompleto", null);
            bTextoCompleto.InnerXml = "N";
            bTexto.AppendChild(bTextoCompleto);

            XmlElement TamFonteTitulo = xml.CreateElement("TamFonteTitulo", "TamFonteTitulo", null);
            TamFonteTitulo.InnerXml = "";
            bTexto.AppendChild(TamFonteTitulo);

            XmlElement TamFonteTexto = xml.CreateElement("TamFonteTexto", "TamFonteTexto", null);
            TamFonteTexto.InnerXml = "";
            bTexto.AppendChild(TamFonteTexto);

            //AssinaturaPosX
            XmlElement AssinaturaPosX = xml.CreateElement("AssinaturaPosX", "AssinaturaPosX", null);
            AssinaturaPosX.InnerXml = "20";
            bVisivel.AppendChild(AssinaturaPosX);

            //AssinaturaPosY
            XmlElement AssinaturaPosY = xml.CreateElement("AssinaturaPosY", "AssinaturaPosY", null);
            AssinaturaPosY.InnerXml = "20";
            bVisivel.AppendChild(AssinaturaPosY);

            //AssinaturaPosY
            XmlElement Detached = xml.CreateElement("Detached", "Detached", null);
            Detached.InnerXml = "";
            raiz.AppendChild(Detached);

            return xml;
        }

        public byte[] AssinarXMLDsign(string hash_tst)
        {
            Assinador.Certificado certificado = new Assinador.Certificado();
            certificado.licensa = "695dfw683451s6d8f4$$@!3468t1vb844536454g1f#$%&¨¨#$8f+9sdf?lrjÈÌîlpfhjÙ.Rz)/)QzJ:meÒdÒ-d¥¼ÌÅýlpHpÌ`/(";

            certificado.pedirSenhaApenasUmaVez = true;
            certificado.dataAtual = DateTime.Now;


            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));
            XmlElement raiz = xml.CreateElement("Signature");
            raiz.InnerText = hash_tst;
            xml.AppendChild(raiz);
            xml.Save(tempDir + "/assinatura.xml");

            //List<System.Security.Cryptography.X509Certificates.X509Certificate2> lista = new List<System.Security.Cryptography.X509Certificates.X509Certificate2>();
            //lista.Add(certificado.RetornaCertificado());

            certificado.CarregaCertificado();
            try
            {
                if (certificado.ValidarCertificado())
                {
                    if (certificado.isDigitalSignature()
                        && certificado.isNonRepudiation()
                        && certificado.isKeyEncipherment())
                    {
                        xml.PreserveWhitespace = true;
                        certificado.XmlDsign_SignXml(ref xml);
                        XmlNodeList nodeList = xml.GetElementsByTagName("Signature");
                        if (nodeList.Count <= 0)
                        {
                            return null;
                        }
                        else
                        {
                            XmlDocument retorno = new XmlDocument();
                            retorno.InnerXml = xml.InnerXml;
                            File.Delete(tempDir + "/assinatura.xml");
                            return System.Text.Encoding.Default.GetBytes(retorno.InnerXml);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("KeyManager") > -1)
                {
                    // 'Return False
                    //'Return True
                    // 'Exit Function
                }
                //MessageBox.Show(ex.Message, "Erro");
                throw ex;
            }
        }
    }

}
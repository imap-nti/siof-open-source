using System;
using System.Linq;
using WebSupergoo.ABCpdf7;
using System.IO;
using System.Globalization;
using DsAdmin;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
namespace Assinador2
{
    public class PDFHandler
    {
#if false
        /// <summary>
        /// Host destino para onde são enviados os arquivos.
        /// </summary>
        private string ftpHost = Environment.GetEnvironmentVariable("SIOF_FTP_HOMOLOGA_HOST") ?? "ftp://net.servidor";

        /// <summary>
        /// Privilégios de login ao repositório FTP.
        /// </summary>
        private String ftpUid = Environment.GetEnvironmentVariable("SIOF_FTP_HOMOLOGA_UID") ?? "ftp_homologa", ftpPwd = Environment.GetEnvironmentVariable("SIOF_FTP_HOMOLOGA_PASSWORD");
#else
        /// <summary>
        /// Host destino para onde são enviados os arquivos.
        /// </summary>
        private String ftpHost = Environment.GetEnvironmentVariable("SIOF_FTP_HOST") ?? "storage.io.org.br/documentosSiof/";

        private String ftpModel = Environment.GetEnvironmentVariable("SIOF_FTP_MODEL_HOST") ?? "storage.io.org.br/documentosSEP/modelo/";

        /// <summary>
        /// Privilégios de login ao repositório FTP.
        /// </summary>
        private String ftpUid = Environment.GetEnvironmentVariable("SIOF_FTP_UID") ?? "documentos", ftpPwd = Environment.GetEnvironmentVariable("SIOF_FTP_PASSWORD");
#endif

        /// <summary>
        /// Constante utilizada na conversão de points para centimetro
        /// </summary>
        private object constante; //= 28.346456693;

        /// <summary>
        /// Dados utilzados ao gerar a edição.
        /// </summary>
        private DateTime data = DateTime.Now;
        private int doc_codigo;
        private string des_certificacao, anoedicao;

        public byte[] GerarEdicao(IMAPModeloEdicao modelo, byte[] edicaoDiagramador, string des_certificacao, string des_anoedicao, int doc_codigo, DataSet ds, DateTime data)
        {
            this.data = data;
            this.anoedicao = des_anoedicao;
            this.doc_codigo = doc_codigo;
            this.des_certificacao = des_certificacao;

            var model = new Doc();

            //Modelo com a moldura a ser inserida sobre a edição do diagramador .
            model.Read(modelo.Modelo);
            model.TopDown = true;
            model.Units = "Mm";

            //Edição envidada pelo diagramador.
            var edicao = new Doc();
            edicao.Read(edicaoDiagramador);
            edicao.TopDown = true;
            edicao.Units = "Mm";

            #region Inclusão da moldura via merge de páginas
            for (int i = 1; i <= edicao.PageCount; i++)
            {
                edicao.PageNumber = i;
                if (i == 1)
                {
                    edicao.AddImageDoc(model, 1, null);
                }
                else
                {
                    edicao.AddImageDoc(model, 2, null);
                }
            }
            #endregion

            #region Inclusão de campos do modelo
            for (int pagina = 1; pagina <= edicao.PageCount; pagina++)
            {

                modelo.Campos.ForEach(s =>
                {
                    IMAPCampoModelo campo = new IMAPCampoModelo();
                    campo.cod_pagina_modelo_pam = s.cod_pagina_modelo_pam;
                    campo.cod_campo_modelo_cam = s.cod_campo_modelo_cam;
                    campo.num_height_cam = s.num_height_cam;
                    campo.num_width_cam = s.num_width_cam;
                    campo.num_top_cam = s.num_top_cam;
                    campo.num_left_cam = s.num_left_cam;
                    campo.cod_modelo_edicao_moe = s.cod_modelo_edicao_moe;
                    campo.des_nome_cam = s.des_nome_cam;
                    campo.des_regra_cam = s.des_regra_cam;

                    if (s.num_tamanho_fonte_cam != 0)
                    {
                        campo.num_tamanho_fonte_cam = s.num_tamanho_fonte_cam;

                        campo.cod_fonte_modelo_fom = s.cod_fonte_modelo_fom;
                        campo.tip_alinhamento_fonte_cam = s.tip_alinhamento_fonte_cam;
                        campo.tip_negrito_fonte_cam = s.tip_negrito_fonte_cam;
                        campo.tip_sublinhado_fonte_cam = s.tip_sublinhado_fonte_cam;
                        campo.tip_italico_fonte_cam = s.tip_italico_fonte_cam;
                        campo.des_fonte_modelo_fom = s.des_fonte_modelo_fom;
                    }
                    else
                    {
                        campo.arq_imagem_cam = s.arq_imagem_cam;
                    }

                    edicao = IncluirCampo(campo, pagina, edicao, ds);
                });
            }
            #endregion

            MemoryStream ms = new MemoryStream();
            edicao.Save(ms);

            return ms.ToArray();
        }

        public DateTime Data
        {
            get { return data; }
        }

        public string AnoEdicao
        {
            get { return this.anoedicao; }

            set { this.anoedicao = value; }
        }

        public int DocCodigo
        {
            get { return this.doc_codigo; }

            set { this.doc_codigo = value; }
        }

        public string DesCertificacao
        {
            get { return this.des_certificacao; }

            set { this.des_certificacao = value; }
        }

        public byte[] GerarEdicao(IMAPModeloEdicao modelo, byte[] edicao_diagramador, DataSet dataset, int cod_cliente)
        {
            this.anoedicao = dataset.Tables[0].Rows[0]["DES_ANOEDICAO"].ToString();
            this.doc_codigo = Convert.ToInt32(dataset.Tables[0].Rows[0]["COD_DOCUMENTO"]);
            this.des_certificacao = pegaHash(data.Day.ToString(), data.ToString("MMMM"), data.Year.ToString(), cod_cliente, doc_codigo.ToString());

            var model = new Doc();

            model.Read(modelo.Modelo);
            model.TopDown = true;
            model.Units = "Mm";

            var edicao = new Doc();
            edicao.Read(edicao_diagramador);
            edicao.AddPage(0);
            edicao.TopDown = true;
            edicao.Units = "Mm";

            for (int i = 1; i <= edicao.PageCount; i++)
            {
                edicao.PageNumber = i;

                if (i == 1)
                    edicao.AddImageDoc(model, 1, null);

                else
                    edicao.AddImageDoc(model, 2, null);
            }

            var firsts = modelo.Campos.Where(element => element.cod_pagina_modelo_pam == 1);

            var lasts = modelo.Campos.Where(element => element.cod_pagina_modelo_pam == 2);


            for (int page = 1; page <= edicao.PageCount; page++)
            {
                if (page == 1)
                    foreach (var element in firsts)
                        edicao = _IncluirCampo(element, page, edicao, dataset);
                else
                    foreach (var element in lasts)
                        edicao = _IncluirCampo(element, page, edicao, dataset);
            }

            var ms = new MemoryStream();
            edicao.Save(ms);

            return ms.ToArray();
        }

        public String ToRomano(int anoInteiro)
        {
            int[] aInteiros = new int[13] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            string[] aRomanos = new string[13] { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            string resultado = "";

            for (int i = 12; i >= 0; i += -1)
            {
                while (anoInteiro >= aInteiros[i])
                {
                    anoInteiro = anoInteiro - aInteiros[i];
                    resultado = resultado + aRomanos[i];
                }
            }
            return resultado;
        }

        public byte[] pegaBrasao(int cod_cliente)
        {
            return (byte[])new DsAdmin.Cliente().ConsultarBrasao(cod_cliente).Table.Rows[0][0];
        }

        private Doc _IncluirCampo(IMAPCampoModelo campo, int pagina, Doc model, DataSet ds)
        {
            var result = ResolveRegra(campo, ds);

            model = IncluirElemento(campo, result, pagina, model);

            return model;
        }

        private Doc IncluirCampo(IMAPCampoModelo campo, int pagina, Doc model, DataSet ds)
        {
            constante = ResolveRegra(campo, ds);

            switch (campo.cod_pagina_modelo_pam)
            {
                case 1:
                    if (pagina == 1)
                    {
                        //campos para a primeira página
                        if (constante is String)
                        {
                            model = IncluirTexto(
                                campo.num_top_cam,
                                campo.num_left_cam,
                                campo.num_height_cam,
                                campo.num_width_cam,
                                campo.des_fonte_modelo_fom,
                                campo.num_tamanho_fonte_cam,
                                campo.tip_negrito_fonte_cam, //== "S" ? true : false,
                                campo.tip_italico_fonte_cam, //== "S" ? true : false,
                                campo.tip_sublinhado_fonte_cam, //== "S" ? true : false,
                                campo.tip_alinhamento_fonte_cam,
                                (string)constante,//(string)ResolveRegra(campo, ds),
                                pagina,
                                model);//, ref objid)
                        }
                        else
                        {
                            model = IncluirImagem(
                            campo.num_top_cam,
                            campo.num_left_cam,
                            campo.num_height_cam,
                            campo.num_width_cam,
                            pagina,
                            (byte[])constante,//ResolveRegra(campo, ds),
                            model);
                        }
                    }
                    break;
                case 2:
                    // campos para as páginas restantes
                    if (pagina % 2 == 0 && pagina != 1)
                    { //Páginas pares
                        model = constante is String ?
                                IncluirTexto(
                            campo.num_top_cam,
                            campo.num_left_cam,
                            campo.num_height_cam,
                            campo.num_width_cam,
                            campo.des_fonte_modelo_fom,
                            campo.num_tamanho_fonte_cam,
                            campo.tip_negrito_fonte_cam, //== "S" ? true : false,
                            campo.tip_italico_fonte_cam, //== "S" ? true : false,
                            campo.tip_sublinhado_fonte_cam, //== "S" ? true : false,
                            campo.tip_alinhamento_fonte_cam,
                            (string)constante,//(string)ResolveRegra(campo, ds),
                            pagina,
                            model)//, ref objid)
                                :
                                IncluirImagem(
                                campo.num_top_cam,
                                campo.num_left_cam,
                                campo.num_height_cam,
                                campo.num_width_cam,
                                pagina,
                                (byte[])constante,//ResolveRegra(campo, ds),
                                model);
                    }
                    else if (pagina % 2 != 0 && pagina != 1)
                    { //Páginas impares
                        model = constante is String ?
                            IncluirTexto(
                            campo.num_top_cam,
                            campo.num_left_cam = (campo.num_left_cam >= 25 && campo.num_left_cam <= 150 ? campo.num_left_cam : (campo.num_left_cam <= 25 ? campo.num_left_cam + 142 : campo.num_left_cam - 135)),
                            campo.num_height_cam,
                            campo.num_width_cam,
                            campo.des_fonte_modelo_fom,
                            campo.num_tamanho_fonte_cam,
                            campo.tip_negrito_fonte_cam,
                            campo.tip_italico_fonte_cam,
                            campo.tip_sublinhado_fonte_cam,
                            (campo.num_left_cam >= 25 && campo.num_left_cam <= 150 ? campo.tip_alinhamento_fonte_cam : (campo.num_left_cam <= 25 ? "E" : "D")),
                            (string)constante,//ResolveRegra(campo, ds),
                            pagina,
                            model)
                                :
                                IncluirImagem(
                                campo.num_top_cam,
                                (campo.num_left_cam >= 25 && campo.num_left_cam <= 150 ? campo.num_left_cam : (campo.num_left_cam <= 25 ? campo.num_left_cam + 142 : campo.num_left_cam - 135)),
                                campo.num_height_cam,
                                campo.num_width_cam,
                                pagina,
                                (byte[])constante,//ResolveRegra(campo, ds),
                                model);
                    }
                    break;
            }
            return model;
        }

        public Doc IncluirImagem(decimal topo, decimal esquerda, decimal altura, decimal largura, int pagina, byte[] imagem, Doc pdf)
        {
            pdf.PageNumber = pagina;
            XImage img = new XImage();
            img.SetData(imagem);

            pdf.Pos.X = (double)esquerda;
            pdf.Pos.Y = (double)topo;
            // Topo - Esquerda
            // 842 unidade máxima de afastamento da base
            //pdf.Rect.Bottom = 0;
            //pdf.Rect.Left = 0;
            pdf.Rect.Bottom = ((double)topo);// * constante);
            pdf.Rect.Left = ((double)esquerda);// * constante);

            // Altura - Largura
            //pdf.Rect.Height = 0;
            //pdf.Rect.Width = 0;
            pdf.Rect.Height = ((double)altura);// * constante);
            pdf.Rect.Width = ((double)largura);// * constante);

            pdf.AddImageObject(img, true);

            return pdf;
        }

        public Doc IncluirElemento(IMAPCampoModelo campo, object element, int pagina, Doc pdf)
        {
            pdf.PageNumber = pagina;

            decimal left = 0;

            if (pagina % 2 != 0 && pagina != 1)
            {
                if (campo.num_left_cam >= 25 && campo.num_left_cam <= 150)
                {
                    left = campo.num_left_cam;
                }
                else if (campo.num_left_cam <= 25)
                {
                    left = campo.num_left_cam + 140;
                }
                else
                {
                    left = campo.num_left_cam - 140;
                }
            }
            else
            {
                left = campo.num_left_cam;
            }

            var align = pagina % 2 != 0 && pagina != 1 ? (campo.num_left_cam >= 25 && campo.num_left_cam <= 150 ? "E" : "D") : campo.tip_alinhamento_fonte_cam;

            if (element is string)
                pdf = IncluirTexto(campo.num_top_cam,
                    left,
                    campo.num_height_cam, campo.num_width_cam, campo.des_fonte_modelo_fom,
                    campo.num_tamanho_fonte_cam, campo.tip_negrito_fonte_cam, campo.tip_italico_fonte_cam, campo.tip_sublinhado_fonte_cam,
                    align,
                    element.ToString(), pagina, pdf);
            else
                pdf = IncluirImagem(
                    campo.num_top_cam,
                    left,
                    campo.num_height_cam, campo.num_width_cam, pagina, (byte[])element, pdf);

            return pdf;
        }


        public Doc IncluirImagem(decimal topo, decimal esquerda, int pagina, byte[] imagem, Doc pdf, ref int objId)
        {
            pdf.PageNumber = pagina;
            XImage img = new XImage();
            img.SetData(imagem);

            pdf.Pos.X = (double)esquerda;
            pdf.Pos.Y = (double)topo;

            pdf.AddImageObject(img, true);

            return pdf;
        }

        public Doc IncluirTexto(decimal topo, decimal esquerda, decimal altura, decimal largura, string fonte, decimal tamFonte, string negrito, string italico, string sublinhado, string alinhamento, string texto, int pagina, Doc pdf)
        {
            //Pagina
            pdf.PageNumber = pagina;

            pdf.Pos.X = (double)esquerda;
            pdf.Pos.Y = (double)topo;

            //pdf.Rect.Top = ((double)topo);
            //pdf.Rect.Left = ((double)esquerda);

            //pdf.Rect.Height = ((double)altura);
            //pdf.Rect.Width = ((double)largura);

            pdf.Rect.SetRect((double)esquerda, (double)topo, (double)largura, (double)altura);

            pdf.Font = pdf.AddFont(fonte);
            pdf.FontSize = Convert.ToInt32(tamFonte);

            pdf.TextStyle.Bold = negrito == "S" ? true : false; //Negrito
            pdf.TextStyle.Italic = italico == "S" ? true : false; //Itálico
            pdf.TextStyle.Underline = sublinhado == "S" ? true : false; //Sublinhado

            #region Alinhamento
            switch (alinhamento) //Alinhamento do texto
            {
                case "E":
                    pdf.HPos = 0; //Alinhar à Esquerda
                    break;
                case "C":
                    pdf.HPos = 0.5; //Centralizado
                    break;
                case "D":
                    pdf.HPos = 1; //Alinhar à Direita
                    break;
                case "J":
                    pdf.TextStyle.Justification = 1; //Justificado
                    break;
                default:
                    break;
            }
            #endregion

            pdf.AddText(texto);

            return pdf;
        }

        public Doc IncluirTexto(decimal topo, decimal esquerda, int tamFonte, bool negrito, bool italico, bool sublinhado, string alinhamento, string texto, int pagina, Doc pdf, ref int objId)
        {
            //Pagina
            pdf.PageNumber = pagina;

            //Posição
            pdf.Pos.X = (double)esquerda;
            pdf.Pos.Y = (double)topo;

            //Fonte
            pdf.FontSize = tamFonte;
            pdf.TextStyle.Bold = negrito;
            pdf.TextStyle.Italic = italico;
            pdf.TextStyle.Underline = sublinhado;

            switch (alinhamento) //Alinhamento do texto
            {
                case "E":
                    pdf.HPos = 0; //Alinhar à Esquerda
                    break;
                case "C":
                    pdf.HPos = 0.5; //Centralizado
                    break;
                case "D":
                    pdf.HPos = 1; //Alinhar à Direita
                    break;
                case "J":
                    pdf.TextStyle.Justification = 1; //Justificado
                    break;
                default:
                    break;
            }

            objId = pdf.AddText(texto);

            return pdf;
        }

        public Doc IncluirHtml(decimal topo, decimal esquerda, int tamFonte, bool negrito, bool italico, bool sublinhado, string alinhamento, string html, int pagina, Doc pdf, ref int objId)
        {
            //Pagina
            pdf.PageNumber = pagina;

            //Posição
            pdf.Pos.X = (double)esquerda;
            pdf.Pos.Y = (double)topo;

            //Fonte
            pdf.FontSize = tamFonte;
            pdf.TextStyle.Bold = negrito;
            pdf.TextStyle.Italic = italico;
            pdf.TextStyle.Underline = sublinhado;

            switch (alinhamento) //Alinhamento do texto
            {
                case "E":
                    pdf.HPos = 0; //Alinhar à Esquerda
                    break;
                case "C":
                    pdf.HPos = 0.5; //Centralizado
                    break;
                case "D":
                    pdf.HPos = 1; //Alinhar à Direita
                    break;
                case "J":
                    pdf.TextStyle.Justification = 1; //Justificado
                    break;
                default:
                    break;
            }

            objId = pdf.AddHtml(html);

            return pdf;
        }

        /// <summary>
        /// Médodo utilizado para resolver umaa regra passada a este e devolver o objeto corespondente a ela.
        /// </summary>
        /// <param name="regra">Regra a ser resolvida pelo método</param>
        /// <returns>Retorna objeto ( string || byte[] ) a ser inserido no campo</returns>
        public object ResolveRegra(IMAPCampoModelo campo, DataSet ds)
        {
            //Dados do Cliente
            int cod_cliente = Convert.ToInt32(ds.Tables[0].Rows[0]["cod_cliente"]);

            //Dados do momento de geração.
            string dia = "";
            string mes = "";
            string ano = "";
            string diasemana;
            string dataExt;
            string[] tipo_cliente;

            var culture = new CultureInfo("pt-BR");
            var dtfi = culture.DateTimeFormat;

            //Objeto de retorno ( byte[] ou string )
            object retorno = null;

            Int32 retornoRegra = retornaNumeroRegra(campo.des_regra_cam);

            if (retornoRegra == -1)
            {
                retorno = campo.des_regra_cam;
            }

            while (retornoRegra > -1)
            {

                switch (retornoRegra)
                {
                    case 1:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Brasão }", "");

                        retorno = pegaBrasao(cod_cliente);

                        break;
                    case 2:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Cliente }", new Cliente().Consultar(cod_cliente).Table.Rows[0]["des_cidade"].ToString());

                        retorno = campo.des_regra_cam;

                        break;
                    case 3:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Tipo Cliente }", "");

                        retorno = new FTPManager().DownloadFileFromFTP(campo.arq_imagem_cam, ftpUid, ftpPwd);

                        break;
                    case 4:
                        dia = data.Day.ToString();
                        ano = data.Year.ToString();
                        mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(data.Month));
                        diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(data.DayOfWeek));
                        dataExt = diasemana + "\n" + dia + " de " + mes + " de " + ano + "\nAno " + anoedicao + " ° Nº" + doc_codigo;

                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Dia por Extenso }", dataExt);

                        retorno = campo.des_regra_cam;

                        break;
                    case 5:
                        dia = data.Day.ToString();
                        mes = data.Month.ToString();
                        ano = data.Year.ToString();
                        string edicao = doc_codigo.ToString();

                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Hash }", "CERTIFICAÇÃO DIGITAL: " + pegaHash(dia, mes, ano, cod_cliente, edicao));
                        retorno = campo.des_regra_cam;
                        break;
                    case 6:
                        string tipoCliente, municipio;
                        tipoCliente = ds.Tables[0].Rows[0]["DES_NOME"].ToString();
                        municipio = ds.Tables[0].Rows[0]["DES_CIDADE"].ToString();

                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Tipo Cliente / Municipio }", tipoCliente + " Municipal de " + municipio);
                        retorno = campo.des_regra_cam;
                        break;
                    case 7:
                        dia = data.Day.ToString();
                        ano = data.Year.ToString();
                        mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(data.Month));
                        diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(data.DayOfWeek));

                        dataExt = diasemana + " ° " + dia + " de " + mes + " de " + ano + " ° Ano " + anoedicao + " ° Nº" + doc_codigo;

                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Dia ° Dia Ext ° Ano ° Nº }", dataExt);
                        retorno = campo.des_regra_cam;
                        break;
                    case 8:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Ano Romano }", ToRomano(int.Parse(ano)));

                        retorno = campo.des_regra_cam;

                        break;
                    case 9:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Site }",
                            "Esta edição encontra-se no site www." + ds.Tables[0].Rows[0]["des_login"].ToString() + ".io.org.br em servidor certificado ICP-BRASIL"
                            + (ds.Tables[0].Rows[0]["cli_dominio_gov"].ToString() != "" ? " e no site www." + ds.Tables[0].Rows[0]["cli_dominio_gov"].ToString() : ""));

                        retorno = campo.des_regra_cam;

                        break;
                    case 10:
                        tipo_cliente = campo.des_regra_cam.Trim().Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("Img", "").Split(';');

                        string path = "";

                        foreach (string pair in tipo_cliente)
                        {
                            string[] values = pair.Trim().Split(',');
                            if (ds.Tables[0].Rows[0]["COD_TIPOCLIENTE"].ToString() == values[0])
                            {
                                path = values[1];
                            }
                        }

                        if (path != "")
                        {
                            var des_path_imagem_imm = new DsAdmin.ModeloImagem().Consultar(Convert.ToInt32(path)).Table.Rows[0]["des_path_imagem_imm"].ToString();

                            var join = ("ftp://" + ftpHost + des_path_imagem_imm).Replace("documentosSiof/", "");

                            return retorno = new FTPManager().DownloadFileFromFTP(join, ftpUid, ftpPwd);
                        }

                        //campo.des_regra_cam = "";

                        break;
                    case 11:
                        tipo_cliente = campo.des_regra_cam.Trim().Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Replace("Txt", "").Split(';');

                        string txt = "";

                        foreach (string pair in tipo_cliente)
                        {
                            string[] values = pair.Trim().Split(',');
                            if (ds.Tables[0].Rows[0]["COD_TIPOCLIENTE"].ToString() == values[0])
                            {
                                txt = values[0];
                            }
                        }
                        if (txt != "")
                        {
                            retorno = txt;
                        }

                        campo.des_regra_cam = "";

                        break;
                    case 12:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Nome Gestor }", ds.Tables[0].Rows[0]["DES_AUTORIDADE"].ToString());

                        retorno = campo.des_regra_cam;

                        break;
                    case 13:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Expediente }", ds.Tables[0].Rows[0]["DES_ENDERECO"].ToString());

                        retorno = campo.des_regra_cam;

                        break;
                    case 14:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Secretário }", ds.Tables[0].Rows[0]["NOM_SECRETARIO"].ToString());

                        retorno = campo.des_regra_cam;

                        break;
                    case 15:
                        campo.des_regra_cam = campo.des_regra_cam.Replace("{ Editor }", ds.Tables[0].Rows[0]["NOM_EDITOR"].ToString());

                        retorno = campo.des_regra_cam;

                        break;
                    default:
                        break;
                }

                retornoRegra = retornaNumeroRegra(campo.des_regra_cam);
            }

            return retorno;
        }

        /// <summary>
        ///  Desenha marca d´agua no documento
        /// </summary>
        /// <param name="theDoc">Documento</param>
        /// <param name="modelo">Modelo com a marca d´agua</param>
        public void DrawWaterMark(Doc theDoc, IMAPModeloEdicao modelo)
        {
            int theID = 0;
            for (int i = 1; i <= theDoc.PageCount; i++)
            {
                theDoc.PageNumber = i;
                theDoc.Layer = theDoc.LayerCount + 1;
                if (i == 1)
                {
                    //string thePath = modelo.Capa; --Refatorar
                    //theDoc.AddImageFile(thePath, 1);
                }
                else if (i != 1)
                {
                    //string thePath = modelo.ContraCapa; --Refatorar
                    //theID = theDoc.AddImageFile(thePath, 1);
                }
                else
                {
                    theDoc.AddImageCopy(theID);
                }
            }
        }

        private Int32 retornaNumeroRegra(String texto)
        {
            if (texto.IndexOf("{ Brasão }") > -1)
            {
                return 1;
            }
            else if (texto.IndexOf("{ Cliente }") > -1)
            {
                return 2;
            }
            else if (texto.IndexOf("{ Tipo Cliente }") > -1)
            {
                return 3;
            }
            else if (texto.IndexOf("{ Dia por Extenso }") > -1)
            {
                return 4;
            }
            else if (texto.IndexOf("{ Hash }") > -1)
            {
                return 5;
            }
            else if (texto.IndexOf("{ Tipo Cliente / Municipio }") > -1)
            {
                return 6;
            }
            else if (texto.IndexOf("{ Dia ° Dia Ext ° Ano ° Nº }") > -1)
            {
                return 7;
            }
            else if (texto.IndexOf("{ Ano Romano }") > -1)
            {
                return 8;
            }
            else if (texto.IndexOf("{ Site }") > -1)
            {
                return 9;
            }
            else if (texto.IndexOf("{ Img") > -1)
            {
                return 10;
            }
            else if (texto.IndexOf("{ Txt") > -1)
            {
                return 11;
            }
            else if (texto.IndexOf("{ Nome Gestor") > -1)
            {
                return 12;
            }
            else if (texto.IndexOf("{ Expediente") > -1)
            {
                return 13;
            }
            else if (texto.IndexOf("{ Secretário") > -1)
            {
                return 14;
            }
            else if (texto.IndexOf("{ Editor") > -1)
            {
                return 15;
            }

            return -1;
        }

        public static int getPages(byte[] pdf)
        {
            var file = new Doc();
            file.Read(pdf);

            return file.PageCount;
        }

        public static String pegaHash(string dia, string mes, string ano, int codigoCliente, string edicao)
        {
            var Ue = new UnicodeEncoding();

            var bytesourcetext = Ue.GetBytes(ano + mes + dia + codigoCliente.ToString() + edicao.ToString());
            var md51 = new MD5CryptoServiceProvider();
            byte[] hashedBytes = md51.ComputeHash(bytesourcetext);

            string retorno = Convert.ToBase64String(hashedBytes);

            return retorno.Substring(0, 22).ToUpper();
        }

        public static String retornaMes(int numero)
        {
            switch (numero)
            {
                case 1:
                    return "Janeiro";
                case 2:
                    return "Fevereiro";
                case 3:
                    return "Março";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Junho";
                case 7:
                    return "Julho";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Dezembro";
                default:
                    return null;
            }
        }

        public int getPageCount(byte[] pdf)
        {
            var d = new Doc();
            d.Read(pdf);
            return d.PageCount;
        }

        public byte[] ArquivoEmBranco(int qtdPaginas)
        {
            var d = new Doc();

            for (int x = 0; x < qtdPaginas; x++)
            {
                d.Page = d.AddPage();
            }

            var ms = new MemoryStream();

            d.Save(ms);
            return ms.ToArray();
        }

        public byte[] ConcatenarArquivosPdf(List<byte[]> arquivos)
        {
            var retorno = new Doc();

            foreach (var pdf in arquivos)
            {
                var d = new Doc();
                d.Read(pdf);

                retorno.Append(d);
            }

            var ms = new MemoryStream();
            retorno.Save(ms);

            return ms.ToArray();
        }

        public byte[] ConcatenarArquivosPdfPorCategoria(List<byte[]> arquivos, DataView categorias)
        {
            var retorno = new Doc();

            #region Configuração do documento
            retorno.MediaBox.String = "A4";
            retorno.Units = "Mm";
            retorno.TopDown = true;
            retorno.Font = retorno.AddFont("Arial");
            #endregion

            string categoria = "";
            decimal contador = 0;

            var vw = new DsAdmin.Cliente().Consultar(Convert.ToInt32(categorias[0]["cod_cliente"]));

            #region Titulo
            decimal tx = 25;
            decimal ty = 80;

            decimal th = 25;
            decimal tw = 159;

            XRect rect = new XRect(retorno.Rect.String);

            retorno.Rect.Top = (int)ty;
            retorno.Rect.Left = (int)tx;
            retorno.Rect.Width = (int)tw;
            retorno.Rect.Height = (int)th;

            int objId = 0;
            string texto = vw.Table.Rows[0]["des_descricao"].ToString() + " publica:";

            retorno = IncluirTexto(ty, tx, (texto.Length > 36 ? 10 : 12), true, false, true, "C", texto, 1, retorno, ref objId);
            contador += 13 * Convert.ToInt32(retorno.GetInfo(objId, "Lines"));

            retorno.Rect.Top = (int)ty;
            retorno.Rect.Left = (int)tx;
            #endregion

            #region 1º Página Aux
            decimal x = 25;
            decimal y = 80;

            decimal h = 160;
            int auxiliar = 0, espacamento = 0;
            //decimal w = 155;

            bool titulo = false;

            System.Collections.Generic.List<int> banners = new System.Collections.Generic.List<int>();
            #endregion

            #region Cadernos
            foreach (System.Data.DataRowView row in categorias)
            {
                if (row["des_caderno_modelo_cam"].ToString() != categoria)
                {
                    retorno = IncluirTexto(y + contador, x, 5, true, false, false, "E", row["des_caderno_modelo_cam"].ToString(), retorno.Page, retorno, ref objId);
                    objId = retorno.AddLine(25, Convert.ToDouble((y + contador)) + 5, 185, Convert.ToDouble((y + contador)) + 5);
                    contador += Convert.ToInt32(retorno.GetInfo(objId, "Width"));
                    objId = retorno.AddLine(25, Convert.ToDouble((y + contador)) + 5, 185, Convert.ToDouble((y + contador)) + 5);
                    contador += 7 * Convert.ToInt32(retorno.GetInfo(objId, "Width"));
                    titulo = true;
                }
                if (titulo && row["des_caderno_modelo_cam"].ToString() != categoria)
                {
                    retorno = IncluirHtml(y + contador, x, 4, false, false, false, "J", "<li> " + row["des_resumo_edt"].ToString() + " </li>", retorno.Page, retorno, ref objId);
                    categoria = row["des_caderno_modelo_cam"].ToString();
                    titulo = false;
                }
                else
                {
                    retorno = IncluirHtml(y + contador, x, 4, false, false, false, "J", "<li> " + row["des_resumo_edt"].ToString() + " </li>", retorno.Page, retorno, ref objId);
                }

                if (auxiliar == 0)
                {
                    y += 4 * Convert.ToInt32(retorno.GetInfo(objId, "Lines"));

                    espacamento = (5 * Convert.ToInt32(retorno.GetInfo(objId, "Lines"))) - (4 * Convert.ToInt32(retorno.GetInfo(objId, "Lines")));

                    auxiliar++;

                    y += espacamento;
                }
                else
                {
                    y += (4 * Convert.ToInt32(retorno.GetInfo(objId, "Lines")));
                    y += espacamento;
                }

                if (titulo && y + contador > 269)
                {
                    retorno.Delete(objId);
                    retorno.Delete(objId - 1);
                    retorno.Delete(objId - 2);
                    retorno.Delete(objId - 3);

                    //retorno.Page = 
                    retorno.AddPage();
                    retorno.PageNumber = retorno.PageCount;

                    y = 30;
                    contador = 0;

                    retorno = IncluirTexto(y + contador, x, 5, true, false, false, "E", categoria, retorno.Page, retorno, ref objId);
                    objId = retorno.AddLine(25, Convert.ToDouble((y + contador)) + 5, 185, Convert.ToDouble((y + contador)) + 5);
                    contador += Convert.ToInt32(retorno.GetInfo(objId, "Width"));
                    objId = retorno.AddLine(25, Convert.ToDouble((y + contador)) + 5, 185, Convert.ToDouble((y + contador)) + 5);
                    contador += 7 * Convert.ToInt32(retorno.GetInfo(objId, "Width"));

                    retorno = IncluirHtml(y + contador, x, 4, false, false, false, "J", "<li> " + row["des_resumo_edt"].ToString() + " </li>", retorno.Page, retorno, ref objId);
                    y += 4 * Convert.ToInt32(retorno.GetInfo(objId, "Lines"));
                    y += espacamento;
                    titulo = false;
                }
                else if (y + contador > 269)
                {
                    retorno.Delete(objId);

                    retorno.Delete(objId - 1);
                    retorno.Delete(objId - 2);
                    retorno.Delete(objId - 3);

                    //retorno.Page = 
                    retorno.AddPage();
                    retorno.PageNumber = retorno.PageCount;

                    y = 30;
                    contador = 0;

                    retorno = IncluirTexto(y + contador, x, 5, true, false, false, "E", categoria, retorno.Page, retorno, ref objId);
                    objId = retorno.AddLine(25, Convert.ToDouble((y + contador)) + 5, 185, Convert.ToDouble((y + contador)) + 5);
                    contador += Convert.ToInt32(retorno.GetInfo(objId, "Width"));
                    objId = retorno.AddLine(25, Convert.ToDouble((y + contador)) + 5, 185, Convert.ToDouble((y + contador)) + 5);
                    contador += 7 * Convert.ToInt32(retorno.GetInfo(objId, "Width"));

                    retorno = IncluirHtml(y + contador, x, 4, false, false, false, "J", "<li> " + row["des_resumo_edt"].ToString() + " </li>", retorno.Page, retorno, ref objId);

                    y += 4 * Convert.ToInt32(retorno.GetInfo(objId, "Lines"));
                    y += espacamento;
                }
            }

            while (y + contador < 235)
            {
                System.Data.DataView vwb = new DsAdmin.Banner().ListarPorFiltro(null, Convert.ToInt32(Convert.ToDouble(235 > contador ? 235 - y : y - 235) * 3.8),
                            Convert.ToInt32(235 * 3.8), Convert.ToInt32(vw[0]["cod_tipocliente"]), 0);
                System.Data.DataRowView row = getBanner(vwb, banners);
                if (vwb.Count > 0 && row != null)// && !banners.Contains(Convert.ToInt32(vwb[0]["cod_baner_ban"])))
                {
                    byte[] imagem = new DsAdmin.FTPManager().DownloadFileFromFTP(ftpHost + row["des_arquivo_ban"].ToString(), ftpUid, ftpPwd);

                    double rectx = (Convert.ToDouble(h) / 2) - (((Convert.ToDouble(row["num_largura_ban"]) / 3.8) / 2) - Convert.ToDouble(x));//((Convert.ToDouble(h) / 2) - (((Convert.ToDouble(row["num_largura_ban"]) / 3.8) / 2)) - Convert.ToDouble(x));

                    retorno.Rect.SetRect(Convert.ToDouble(x), (Convert.ToDouble(row["num_altura_ban"]) / 3.8) + Convert.ToDouble(y) + Convert.ToDouble(contador),
                        (Convert.ToDouble(row["num_largura_ban"]) / 3.8), (Convert.ToDouble(row["num_altura_ban"]) / 3.8));

                    retorno = IncluirImagem(Convert.ToDecimal(rectx),
                        Convert.ToDecimal((Convert.ToDouble(row["num_altura_ban"]) / 3.8) + Convert.ToDouble(y) + Convert.ToDouble(contador)), retorno.Page, imagem, retorno, ref objId);
                    banners.Add(Convert.ToInt32(row["cod_baner_ban"]));

                    y += 20 + (Convert.ToDecimal(row["num_altura_ban"]) / (decimal)3.8);
                }
                else
                {
                    break;
                }
            }

            #endregion

            #region Nº Páginas Aux
            double nx = 13;
            double ny = 26;
            double nh = 235;
            //double nw = 155;

            double ncontador = 0;
            #endregion

            #region Pdfs e Propagandas
            foreach (byte[] pdf in arquivos)
            {
                Doc d = new Doc();
                d.Read(pdf);
                d.Units = "Mm";
                if (d.MediaBox.Height > 240)
                {
                    if (ncontador != 0)
                    {
                        while (ny + ncontador < 269)
                        {
                            System.Data.DataView vwa = new DsAdmin.Banner().ListarPorFiltro(null, Convert.ToInt32((nh > ncontador ? nh - ncontador : ncontador - nh) * 3.8),
                                        Convert.ToInt32(235 * 3.8), Convert.ToInt32(vw[0]["cod_tipocliente"]), 0);
                            System.Data.DataRowView row = getBanner(vwa, banners);
                            if (vwa.Count > 0 && row != null)//&& !banners.Exists(element => element == Convert.ToInt32(vwa[0]["cod_baner_ban"])))
                            {
                                byte[] imagem = new DsAdmin.FTPManager().DownloadFileFromFTP(ftpHost + row["des_arquivo_ban"].ToString(), ftpUid, ftpPwd);

                                double rectx = ((nh / 2) - ((Convert.ToDouble(row["num_largura_ban"]) / 3.8) / 2)) - nx;

                                retorno.Rect.SetRect(rectx, (Convert.ToDouble(row["num_altura_ban"]) / 3.8) + ny + ncontador,
                                    (Convert.ToDouble(row["num_largura_ban"]) / 3.8), (Convert.ToDouble(row["num_altura_ban"]) / 3.8));

                                retorno = IncluirImagem(Convert.ToDecimal(rectx),
                                    Convert.ToDecimal((Convert.ToDouble(row["num_altura_ban"]) / 3.8) + ny + ncontador), retorno.Page, imagem, retorno, ref objId);
                                banners.Add(Convert.ToInt32(row["cod_baner_ban"]));

                                ncontador += 20 + (Convert.ToDouble(row["num_altura_ban"]) / 3.8);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    retorno.Append(d);
                    retorno.PageNumber = retorno.PageCount;
                    ncontador = 0;
                }
                else
                {
                    if (ncontador + d.MediaBox.Height < (double)nh)
                    {
                        if (ncontador == 0)
                        {
                            //retorno.Page = 
                            retorno.AddPage();
                            retorno.PageNumber = retorno.PageCount;
                        }

                        retorno.Rect.SetRect(((nh / 2) - (d.MediaBox.Width / 2)) - nx, d.MediaBox.Top + ny + ncontador, d.MediaBox.Width, d.MediaBox.Height);
                        retorno.AddImageDoc(d, 1, null);
                        ncontador += 20 + d.MediaBox.Height;
                    }
                    else if (ncontador + d.MediaBox.Height < (double)nh)
                    {
                        System.Data.DataView vwa = new DsAdmin.Banner().ListarPorFiltro(null, Convert.ToInt32((nh > ncontador ? nh - ncontador : ncontador - nh) * 3.8),
                            Convert.ToInt32(235 * 3.8), Convert.ToInt32(vw[0]["cod_tipocliente"]), 0);
                        System.Data.DataRowView row = getBanner(vwa, banners);
                        if (vwa.Count > 0 && row != null)// && !banners.Contains(Convert.ToInt32(vwa[0]["cod_baner_ban"])))//!banners.Exists(element => element == Convert.ToInt32(vwa[0]["cod_baner_ban"])))
                        {
                            byte[] imagem = new DsAdmin.FTPManager().DownloadFileFromFTP(ftpHost + row["des_arquivo_ban"].ToString(), ftpUid, ftpPwd);

                            double rectx = ((nh / 2) - ((Convert.ToDouble(row["num_largura_ban"]) / 3.8) / 2)) - nx;

                            retorno.Rect.SetRect(rectx, (Convert.ToDouble(row["num_altura_ban"]) / 3.8) + ny + ncontador,
                                (Convert.ToDouble(row["num_largura_ban"]) / 3.8), (Convert.ToDouble(row["num_altura_ban"]) / 3.8));

                            retorno = IncluirImagem(Convert.ToDecimal((Convert.ToDouble(row["num_altura_ban"]) / 3.8) + ny + ncontador),
                                Convert.ToDecimal(rectx), retorno.Page, imagem, retorno, ref objId);
                            banners.Add(Convert.ToInt32(row["cod_baner_ban"]));

                            ncontador += 20 + (Convert.ToDouble(row["num_altura_ban"]) / 3.8);
                        }

                        //retorno.Page = 
                        retorno.AddPage();
                        retorno.PageNumber = retorno.PageCount;

                        ncontador = 0.01;//bug do milênio
                        retorno.Rect.SetRect(((nh / 2) - (d.MediaBox.Width / 2)) - nx, d.MediaBox.Top + ny + ncontador, d.MediaBox.Width, d.MediaBox.Height);
                        retorno.AddImageDoc(d, retorno.PageNumber, null);
                        ncontador += 20 + d.MediaBox.Height;
                    }
                    else
                    {
                        while (ny + ncontador < 235)
                        {
                            System.Data.DataView vwa = new DsAdmin.Banner().ListarPorFiltro(null, Convert.ToInt32((nh > ncontador ? nh - ncontador : ncontador - nh) * 3.8),
                                        Convert.ToInt32(235 * 3.8), Convert.ToInt32(vw[0]["cod_tipocliente"]), 0);
                            System.Data.DataRowView row = getBanner(vwa, banners);
                            if (vwa.Count > 0 && row != null)//&& !banners.Exists(element => element == Convert.ToInt32(vwa[0]["cod_baner_ban"])))
                            {
                                byte[] imagem = new DsAdmin.FTPManager().DownloadFileFromFTP(ftpHost + row["des_arquivo_ban"].ToString(), ftpUid, ftpPwd);

                                double rectx = ((nh / 2) - ((Convert.ToDouble(row["num_largura_ban"]) / 3.8) / 2)) - nx;

                                retorno.Rect.SetRect(rectx, (Convert.ToDouble(row["num_altura_ban"]) / 3.8) + ny + ncontador,
                                    (Convert.ToDouble(row["num_largura_ban"]) / 3.8), (Convert.ToDouble(row["num_altura_ban"]) / 3.8));

                                retorno = IncluirImagem(Convert.ToDecimal(rectx),
                                    Convert.ToDecimal((Convert.ToDouble(row["num_altura_ban"]) / 3.8) + ny + ncontador), retorno.Page, imagem, retorno, ref objId);
                                banners.Add(Convert.ToInt32(row["cod_baner_ban"]));

                                ncontador += 20 + (Convert.ToDouble(row["num_altura_ban"]) / 3.8);
                            }
                        }
                    }
                }
            }
            #endregion

            var ms = new MemoryStream();
            retorno.Save(ms);

            return ms.ToArray();
        }

        private DataRowView getBanner(DataView vw, List<int> banners)
        {
            foreach (DataRowView row in vw)
            {
                if (!banners.Contains(Convert.ToInt32(row["cod_baner_ban"])))
                {
                    return row;
                }
            }

            return null;
        }
    }
}
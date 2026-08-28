using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.IO;


/// <summary>
/// Summary description for uteis
/// </summary>
public class Util
{
    public Util()
    {
        
    }

    ////alerta   
    public static void MessageBox(Page page, string Mensagem, bool AjaxCommand)
    {
        if (AjaxCommand)
           // AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(page, page.GetType(), "", "window.alert('" + Mensagem + "');", true);
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "window.alert('" + Mensagem + "');", true);
        else
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "window.alert('" + Mensagem + "');", true);
    }

    public static void msg(Page page, string Mensagem)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "", "window.alert('" + Mensagem + "');", true);
    }
    //gera senha
    public static string GeraSenha()
    {
        string charPool = "ABCDEFGOPQRSTUVWXY1234567890Z!@";
        StringBuilder rs = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < 7; i++)
        {
            rs.Append(charPool[(int)(random.NextDouble() * charPool.Length)]);
        }

        return rs.ToString();
    }

    //criptografia 
    public static string MD5(string senha)
    {

        System.Security.Cryptography.MD5 ts = System.Security.Cryptography.MD5.Create();

        // Criptografa o valor passado
        byte[] valorCriptografado = ts.ComputeHash(Encoding.Default.GetBytes(senha));

        // Cria um StringBuilder para passarmos os bytes gerados para ele
        StringBuilder strBuilder = new StringBuilder();

        // Converte cada byte em um valor hexadecimal e adiciona ao
        // string builder
        // and format each one as a hexadecimal string.

        for (int i = 0; i < valorCriptografado.Length; i++)
        {

            strBuilder.Append(valorCriptografado[i].ToString("x2"));

        }

        // retorna o criptografado como string
        return (strBuilder.ToString());

    }

    //===========================================================================================

    public static string EnviarEmail(string Mensagem, string Nome, string Email, string LinhaAssunto)// string Nome, string Email, string LinhaAssunto)
    {
        // Nome = nome da Prefeitura 
        // Email  = Email da Prefeitura
        // Mensagem  = corpo da msg 
        //LinhaAssunto = Assunto
        try
        {

            //  MailAddress fromAddress = new MailAddress(Email, Nome);//teste  
            String remetenteEmail = "sistemas@portalimap.org.br";  //email do remetente
            MailMessage mail = new MailMessage();// Ob email


            mail.To.Add(Email);
            //mail.Bcc.Add("ademir@portalimap.org.br");
            
            mail.From = new MailAddress(remetenteEmail, Nome, Encoding.UTF8);//add remetentes
            mail.Subject = LinhaAssunto;//LinhaAssunto;// define assunto 
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = Mensagem; // o corpo do email
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;// coloca o email html verdadeiro 
            mail.Priority = MailPriority.High; //prioridade do email

            SmtpClient client = new SmtpClient(); // adicionando as credenciais do seu email e senha:
            client.Credentials = new NetworkCredential(remetenteEmail, Environment.GetEnvironmentVariable("SIOF_SMTP_PASSWORD"));
            client.Port = 587;//porta utilizada pelo gmail 
            client.Host = "smtp.gmail.com";//"smtp.terra.com.br"; //definindo o provedor que irá disparar o email
            client.EnableSsl = true;//false;// gmail trabalha com server secured layer 
            client.Send(mail);
            return null;

        }

        catch (Exception erro)
        {
            return erro.Message.ToString();
        }
    }

    public static bool ValidarCPF(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;
        if (cpf == cpf.Substring(0, 1).PadRight(11, cpf.ToCharArray(0, 1)[0]))
        {
            return false;
        }
        tempCpf = cpf.Substring(0, 9);
        soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCpf = tempCpf + digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    public static bool ValidaCnpj(string cnpj)
    {
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        int resto;
        string digito;
        string tempCnpj;

        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)
            return false;

        tempCnpj = cnpj.Substring(0, 12);

        soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCnpj = tempCnpj + digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cnpj.EndsWith(digito);
    }
    //public static byte[] imageToByteArray(System.Drawing.Image imageIn)
    //{
    //    MemoryStream ms = new MemoryStream();
    //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
    //    return ms.ToArray();
    //}

}
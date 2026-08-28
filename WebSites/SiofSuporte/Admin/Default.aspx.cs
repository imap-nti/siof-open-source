using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["usu_codigo"] = "11124";
        //Session.Add("des_login_usu", "02643290585");
        //Session.Add("des_nome_usu", "Danilo Nascimento Cunha");
        //Session.Add("des_perfil_per", "Desenvolvedor");
        if (!IsPostBack)
        {
            ViewState["cod_verificador_usu"] = new Random().Next(1, 40);
            userChave.Text = "Chave " + ViewState["cod_verificador_usu"].ToString();
        }
    }

    protected void AcessarLinkButton_Click(object sender, EventArgs e)
    {
        try
        {
            string authServiceAddress = Environment.GetEnvironmentVariable("SIOF_AUTH_WS_ADDRESS");
            SUPAutenticaUsuarioIMAP.AuthServiceClient cliente = new SUPAutenticaUsuarioIMAP.AuthServiceClient("BasicHttpBinding_IAuthService", authServiceAddress);
            System.Data.DataView vw = new System.Data.DataView();
            vw = cliente.LogMeInSiofSuporte(SHA1(userLogin.Text.Replace(".", "").Replace("-", "") + userPassword.Text), null, 7).Tables["si_usuario"].DefaultView;

            if (vw.Table.Rows.Count == 0)
            {
                throw new Exception("Login, Senha ou Chave Incorretos!");
            }

            Int32 usu_codigo = Convert.ToInt32(vw.Table.Rows[0]["usu_codigo"]);

            System.Data.DataView vw1 = cliente.ValidarCartao(usu_codigo, Convert.ToInt32(ViewState["cod_verificador_usu"]), Convert.ToInt32(userChaveTextBox.Text)).Tables["sgu_cartao_acesso"].DefaultView;

            if (vw1.Table.Rows.Count == 0)
            {
                throw new Exception("Erro ao tentar estabelecer comunicação com servidor web. Por favor tente mais tarde.");
            }

            if (vw.Table.Rows.Count > 0 && Convert.ToInt32(vw1.Table.Rows[0]["qtd"]) > 0)
            {
                Session.Clear();
                Session["usu_codigo"] = vw.Table.Rows[0]["usu_codigo"];
                Session.Add("des_login_usu", userLogin.Text);
                Session.Add("des_nome_usu", vw.Table.Rows[0]["usu_nome"].ToString());
                Session.Add("des_perfil_per", vw.Table.Rows[0]["usu_cargo"].ToString());

                Response.Redirect("~/Admin/Principal.aspx");
            }
        }
        catch (Exception ex)
        {
            Msg.MessageBox(Page, ex.Message, true);
        }
    }

    //protected void lnk_EsqueciSenha_Click(object sender, EventArgs e)
    //{
    //    ModalPopupExtender1.Show();
    //}

    //protected void btnRecupera_Click(object sender, EventArgs e)
    //{
    //    DsAdmin.Usuario consul = new DsAdmin.Usuario();
    //    System.Data.DataView result = consul.ConsultarPorCpfEmail(txtCpf.Text.ToString(), txt_email.Text.ToString());

    //    if (result.Table.Rows.Count > 0)
    //    {
    //        //senha aleatária 
    //        string senha = Util.GeraSenha();
    //        // Session["cod_municipio_mun"] 
    //        consul.AlterarSenha(senha, result.Table.Rows[0]["des_nome_usu"].ToString(), Convert.ToInt64(Session["cod_municipio_mun"]), result.Table.Rows[0]["des_login_usu"].ToString());

    //        Msg.MessageBox(Page, "Senha Alterada com Sucesso", true);

    //        //informaçoes do email 
    //        Util.EnviarEmail("Sua senha Foi Alterada: <br>" + " " + senha, "SOS web", result.Table.Rows[0]["des_email_usu"].ToString(), "Alteração de senha");
    //        ModalPopupExtender1.Hide();
    //    }

    //    else
    //    {
    //        Msg.MessageBox(Page, "Os dados Informados Não conferem. Entre em contato", true);
    //    }
    //}

    public static string SHA1(string str)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        try
        {
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            byte[] combined = encoder.GetBytes(str);
            System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
            byte[] bytehash = hash.ComputeHash(combined);
            foreach (byte b in bytehash)
            {
                builder.Append(b.ToString("x2").ToUpper());
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return builder.ToString();
    }
}
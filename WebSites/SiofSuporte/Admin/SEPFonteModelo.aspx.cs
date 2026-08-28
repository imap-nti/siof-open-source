using System;
using Microsoft.VisualBasic;
using System.Web.UI.WebControls;

public partial class admin_SEPFonteModelo : System.Web.UI.Page
{

    protected override void OnLoad(EventArgs e)
    {

        //verifica se a ação está nula, 
        //preenche com "cadastrar" no primeiro momento que a tela abrir,
        //para inicializar a variavel
        if (ViewState["acao"] == null)
        {
            ViewState["acao"] = "cadastrar";
        }
        base.OnLoad(e);
    }

    protected void grvPrincipal_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

        //verifica se clicou em editar ou excluir
        if (e.CommandName == "btnExcluir")
        {
            //exclui o item selecionado
            //e chama o cancelar para limpar tela e carregar o grid
            DsAdmin.FonteModelo rn = new DsAdmin.FonteModelo();
            rn.Excluir(Convert.ToInt32(e.CommandArgument.ToString()));
            CancelarLinkButton_Click(null, null);

        }
        else if (e.CommandName == "btnEditar")
        {

            //carrega os dados do item selecionado para permitir a edição
            DsAdmin.FonteModelo rn = new DsAdmin.FonteModelo();
            System.Data.DataView vw;

            //1) consulta os dados do banco
            vw = rn.Consultar(Convert.ToInt32(e.CommandArgument.ToString()));

            //2) preenche os campos da tela
            des_fonte_modelo_fomTextBox.Text = vw.Table.Rows[0]["des_fonte_modelo_fom"].ToString();
            num_tamanho_minimo_fomTextBox.Text = vw.Table.Rows[0]["num_tamanho_minimo_fom"].ToString();
            //3) coloca o codigo do item selecionado no ViewState, para permitir a alteração no botão confirmar (CadastrarLinkButton_Click)
            ViewState["cod_fonte_modelo_fom"] = e.CommandArgument;
            //4) coloca a ação a ser realizada ao clicar no confirmar (CadastrarLinkButton_Click)
            ViewState["acao"] = "alterar";
            //5) altera o texto do botão cadastrar (CadastrarLinkButton), para confirmar
            CadastrarLinkButton.Text = "Confirmar";
        }
    }

    protected void CadastrarLinkButton_Click(Object sender, System.EventArgs e)
    {

        //cria uma transação para incluir ou alterar os dados dependendo da ação selecionada
        using (DsAdmin.Factory factory = new DsAdmin.Factory())
        {

            DsAdmin.FonteModelo rn = factory.FonteModelo();

            Int32 retorno = 0;

            if (ViewState["acao"].ToString() == "cadastrar")
            {
                //se for incluir, chama o metodo de inclusão e passa os parametros
                rn.Incluir(des_fonte_modelo_fomTextBox.Text, Convert.ToInt32(Information.IsNumeric(num_tamanho_minimo_fomTextBox.Text) ? num_tamanho_minimo_fomTextBox.Text : null), ref retorno);
            }
            else if (ViewState["acao"].ToString() == "alterar")
            {
                //se for alteração , chama o metodo de alteração e passa os parametros
                rn.Alterar(des_fonte_modelo_fomTextBox.Text, Convert.ToInt32(Information.IsNumeric(num_tamanho_minimo_fomTextBox.Text) ? num_tamanho_minimo_fomTextBox.Text : null), Convert.ToInt32(ViewState["cod_fonte_modelo_fom"]));
            }

        } //fecha transação

        //chama o metodo Cancelar para limpar os campos e atualizar o Grid
        CancelarLinkButton_Click(null, null);

    }

    protected void CancelarLinkButton_Click(Object sender, System.EventArgs e)
    {

        //limpa os campos da tela, atualiza o grid, coloca a ação em modo de cadastrar e altera o texto do botão

        //1) limpa os campos
        des_fonte_modelo_fomTextBox.Text = "";
        num_tamanho_minimo_fomTextBox.Text = "";
        //2) coloca a ação cadastrar no ViewState
        ViewState["acao"] = "cadastrar";
        //3) altera o texto do botão cadastrar
        CadastrarLinkButton.Text = "Cadastrar";
        //4) atualiza o Grid
        grvPrincipal.DataBind();
    }

    protected void grvPrincipal_PreRender(object sender, EventArgs e)
    {
        //força o atributo de acessibilidade
        grvPrincipal.UseAccessibleHeader = true;

        //adiciona <thead> e <tbody>
        if (grvPrincipal.HeaderRow != null)
        {
            grvPrincipal.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}


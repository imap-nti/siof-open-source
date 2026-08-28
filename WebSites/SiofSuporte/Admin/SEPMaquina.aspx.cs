using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SEPMaquina : System.Web.UI.Page
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

    protected void grvPrincipal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //verifica se clicou em editar ou excluir
        if (e.CommandName == "btnExcluir")
        {
            //exclui o item selecionado
            //e chama o cancelar para limpar tela e carregar o grid
            DsAdmin.Maquina rn = new DsAdmin.Maquina();
            rn.Excluir(Convert.ToInt32(e.CommandArgument));
            CancelarLinkButton_Click(null, null);

        }
        else if (e.CommandName == "btnEditar")
        {

            //carrega os dados do item selecionado para permitir a edição
            DsAdmin.Maquina rn = new DsAdmin.Maquina();
            System.Data.DataView vw;

            //1) consulta os dados do banco
            vw = rn.Consultar(Convert.ToInt32(e.CommandArgument));

            //2) preenche os campos da tela
            des_macTextBox.Text = vw.Table.Rows[0]["des_mac"].ToString();
            des_descricaoTextBox.Text = vw.Table.Rows[0]["des_descricao"].ToString();

            //3) coloca o codigo do item selecionado no ViewState, para permitir a alteração no botão confirmar (CadastrarLinkButton_Click)
            ViewState["cod_maquina"] = e.CommandArgument;
            //4) coloca a ação a ser realizada ao clicar no confirmar (CadastrarLinkButton_Click)
            ViewState["acao"] = "alterar";
            //5) altera o texto do botão cadastrar (CadastrarLinkButton), para confirmar
            CadastrarLinkButton.Text = "Confirmar";
        }
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

    protected void CancelarLinkButton_Click(object sender, EventArgs e)
    {

        //limpa os campos da tela, atualiza o grid, coloca a ação em modo de cadastrar e altera o texto do botão

        //1) limpa os campos
        des_macTextBox.Text = "";
        des_descricaoTextBox.Text = "";
        //2) coloca a ação cadastrar no ViewState
        ViewState["acao"] = "cadastrar";
        ViewState["cod_maquina"] = null;
        //3) altera o texto do botão cadastrar
        CadastrarLinkButton.Text = "Cadastrar";
        //4) atualiza o Grid
        grvPrincipal.DataBind();
    }

    protected void CadastrarLinkButton_Click(object sender, EventArgs e)
    {
        //cria uma transação para incluir ou alterar os dados dependendo da ação selecionada
        using (DsAdmin.Factory factory = new DsAdmin.Factory())
        {
            DsAdmin.Maquina rn = factory.Maquina();

            if (ViewState["acao"].ToString() == "cadastrar")
            {
                //se for incluir, chama o metodo de inclusão e passa os parametros
                rn.Incluir(des_macTextBox.Text, des_descricaoTextBox.Text);
            }
            else if (ViewState["acao"].ToString() == "alterar")
            {
                //se for alteração , chama o metodo de alteração e passa os parametros
                rn.Alterar(des_macTextBox.Text, des_descricaoTextBox.Text, Convert.ToInt32(ViewState["cod_maquina"]));
            }
            factory.Commit();
        } //fecha transação

        //chama o metodo Cancelar para limpar os campos e atualizar o Grid
        CancelarLinkButton_Click(null, null);
    }
}
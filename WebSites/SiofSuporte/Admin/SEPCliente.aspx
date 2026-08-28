<%@ Page Title="SepCliente" Language="C#" MasterPageFile="~/Admin/MasterPage.master"
    EnableViewState="true" AutoEventWireup="true" CodeFile="SEPCliente.aspx.cs" Inherits="Admin_SEPCliente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>SepCliente</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <fieldset>
                        <legend>Cliente</legend>
                        <div class="flutuaEsq">
                            <asp:Label ID="COD_CLIENTELabel" runat="server" Text="Código" AssociatedControlID="COD_CLIENTETextBox"></asp:Label>
                            <asp:TextBox ID="COD_CLIENTETextBox" Enabled="false" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled"></asp:TextBox><br />
                            <asp:Label ID="COD_TIPOCLIENTELabel" runat="server" Text="Tipo Cliente" AssociatedControlID="COD_TIPOCLIENTEDropDownList"></asp:Label>
                            <asp:DropDownList ID="COD_TIPOCLIENTEDropDownList" runat="server" AppendDataBoundItems="true"
                                DataSourceID="COD_TIPOCLIENTEObjectDataSource" DataTextField="DES_NOME" DataValueField="COD_TIPOCLIENTE"
                                EnableViewState="False" ViewStateMode="Disabled">
                                <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="COD_TIPOCLIENTEObjectDataSource" SelectMethod="Listar"
                                TypeName="DsAdmin.TipoCliente" runat="server"></asp:ObjectDataSource>
                            <br />
                            <asp:Label ID="NOM_SECRETARIOLabel" runat="server" Text="Secretário" AssociatedControlID="NOM_SECRETARIOTextBox"></asp:Label>
                            <asp:TextBox ID="NOM_SECRETARIOTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="DES_AUTORIDADELabel" runat="server" Text="Autoridade" AssociatedControlID="DES_AUTORIDADETextBox"></asp:Label>
                            <asp:TextBox ID="DES_AUTORIDADETextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <br />
                            <asp:Label ID="NOM_EDITORLabel" runat="server" Text="Editor" AssociatedControlID="NOM_EDITORTextBox"></asp:Label>
                            <asp:TextBox ID="NOM_EDITORTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <br />
                            <asp:Label ID="des_url_alternativaLabel" runat="server" Text="Url Alternativa" AssociatedControlID="des_url_alternativaTextBox"></asp:Label>
                            <asp:TextBox ID="des_url_alternativaTextBox" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled"></asp:TextBox>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="TIP_ATIVOLabel" runat="server" Text="Cliente Ativo" AssociatedControlID="TIP_ATIVORadioButtonList"></asp:Label>
                            <asp:RadioButtonList ID="TIP_ATIVORadioButtonList" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled">
                                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            <asp:Label ID="tip_site_ativoLabel" runat="server" Text="Site Ativo" AssociatedControlID="tip_site_ativoRadioButtonList"></asp:Label>
                            <asp:RadioButtonList ID="tip_site_ativoRadioButtonList" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled">
                                <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Não" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="BrasaoLabel" runat="server" Text="Brasão" AssociatedControlID="BrasaoImage"></asp:Label>
                            <%--<img id="BrasaoImage" width="122" height="134" runat="server" alt="Brasão">--%>
                            <asp:Image ID="BrasaoImage" Width="122" Height="134" BorderStyle="Groove" BorderWidth="1"
                                BorderColor="Black" runat="server" /><br />
                            <asp:AsyncFileUpload ID="BrasaoAsyncFileUpload" OnUploadedComplete="BrasaoAsyncFileUpload_UploadedComplete"
                                runat="server" />
                            <%--<asp:LinkButton ID="AlterarImagemBrasãoLinkButton" runat="server" Text="Alterar"
                                CssClass="button_style icon_edit"></asp:LinkButton>--%><%--OnClick="AlterarImagemBrasãoLinkButton_Click"OnClick="RemoverBrasaoLinkButton_Click"--%>
                            <%--<asp:LinkButton ID="RemoverBrasaoLinkButton" runat="server" Text="Remover" CssClass="button_style icon_clean"></asp:LinkButton>--%>
                        </div>
                        <br />
                        <br />
                        <%--<div class="flutuaEsq">
                            <asp:Label ID="Label9" runat="server" Text="Outros" AssociatedControlID="chkOutrosTextBox"></asp:Label>
                            <asp:TextBox ID="TextBox9" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label10" runat="server" Text="Outros" AssociatedControlID="chkOutrosTextBox"></asp:Label>
                            <asp:TextBox ID="TextBox10" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="Label11" runat="server" Text="Outros" AssociatedControlID="chkOutrosTextBox"></asp:Label>
                            <asp:TextBox ID="TextBox11" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label12" runat="server" Text="Outros" AssociatedControlID="chkOutrosTextBox"></asp:Label>
                            <asp:TextBox ID="TextBox12" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                        </div>--%>
                        <div class="flutuaEsq">
                            <fieldset>
                                <legend>Logradouro</legend>
                                <div class="flutuaEsq">
                                    <asp:Label ID="DES_ENDERECOLabel" runat="server" Text="Endereço" AssociatedControlID="DES_ENDERECOTextBox"></asp:Label>
                                    <asp:TextBox ID="DES_ENDERECOTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                    <%--<br />--%>
                                </div>
                                <div class="flutuaEsq">
                                    <asp:Label ID="DES_CEPLabel" runat="server" Text="CEP" AssociatedControlID="DES_CEPTextBox"></asp:Label>
                                    <asp:TextBox ID="DES_CEPTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                </div>
                                <div class="flutuaEsq">
                                    <asp:Label ID="DES_CIDADELabel" runat="server" Text="Cidade" AssociatedControlID="DES_CIDADETextBox"></asp:Label>
                                    <asp:TextBox ID="DES_CIDADETextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                    <%-- <br />--%>
                                </div>
                                <div class="flutuaEsq">
                                    <asp:Label ID="DES_BAIRROLabel" runat="server" Text="Bairro" AssociatedControlID="DES_BAIRROTextBox"></asp:Label>
                                    <asp:TextBox ID="DES_BAIRROTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                    <br />
                                </div>
                                <%--<div class="flutuaEsq">
                                    <asp:Label ID="DES_UFLabel" runat="server" Text="UF" AssociatedControlID="DES_UFTextBox"></asp:Label>
                                    <asp:TextBox ID="DES_UFTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                </div>--%>
                            </fieldset>
                        </div>
                        <div class="flutuaEsq">
                            <fieldset>
                                <legend>Contato</legend>
                                <div class="flutuaEsq">
                                    <asp:Label ID="DES_CONTATOLabel" runat="server" Text="Contato" AssociatedControlID="DES_CONTATOTextBox"></asp:Label>
                                    <asp:TextBox ID="DES_CONTATOTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><br />
                                    <asp:Label ID="DES_TELEFONELabel" runat="server" CssClass="mask_telefone" Text="Telefone"
                                        AssociatedControlID="DES_TELEFONETextBox"></asp:Label>
                                    <asp:TextBox ID="DES_TELEFONETextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                </div>
                                <div class="flutuaEsq">
                                    <asp:Label ID="DES_EMAILLabel" runat="server" Text="Email" AssociatedControlID="DES_EMAILTextBox"></asp:Label>
                                    <asp:TextBox ID="DES_EMAILTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                </div>
                            </fieldset>
                        </div>
                        <div class="flutuaEsq">
                            <fieldset>
                                <legend>Dados da edição</legend>
                                <div class="flutuaEsq">
                                    <asp:Label ID="cod_modelo_edicao_moeLabel" runat="server" Text="Modelo" AssociatedControlID="cod_modelo_edicao_moeDropDownList"></asp:Label>
                                    <asp:DropDownList ID="cod_modelo_edicao_moeDropDownList" DataSourceID="cod_modelo_edicao_moeObjectDataSource"
                                        DataTextField="des_modelo_edicao_moe" DataValueField="cod_modelo_edicao_moe"
                                        runat="server" EnableViewState="False" AppendDataBoundItems="true" ViewStateMode="Disabled">
                                        <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="cod_modelo_edicao_moeObjectDataSource" SelectMethod="Listar"
                                        TypeName="DsAdmin.ModeloEdicao" runat="server"></asp:ObjectDataSource>
                                    <br />
                                    <asp:Label ID="TIP_CAPA_INDICELabel" runat="server" Text="Capa / Índice" AssociatedControlID="TIP_CAPA_INDICERadioButtonList"></asp:Label>
                                    <asp:RadioButtonList ID="TIP_CAPA_INDICERadioButtonList" RepeatDirection="Horizontal"
                                        runat="server" EnableViewState="False" ViewStateMode="Disabled">
                                        <asp:ListItem Text="Capa" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Índice" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="flutuaEsq">
                                    <ul class="buttonBar grad_rd_box">
                                        <li>
                                            <%--<asp:Label ID="tip_assinatura_digitalLabel" runat="server" Text="tip_assinatura_digital"
                                    AssociatedControlID="tip_assinatura_digitalTextBox"></asp:Label>--%>
                                            <asp:CheckBox ID="tip_assinatura_digitalCheckBox" runat="server" Text="Ass. Digital"
                                                EnableViewState="False" ViewStateMode="Disabled"></asp:CheckBox>
                                            <%--<asp:Label ID="tip_carimbo_tempoLabel" runat="server" Text="Outros" AssociatedControlID="tip_carimbo_tempoTextBox"></asp:Label>--%>
                                            <asp:CheckBox ID="tip_carimbo_tempoCheckBox" runat="server" Text="Carimbo" EnableViewState="False"
                                                ViewStateMode="Disabled"></asp:CheckBox>
                                        </li>
                                        <br />
                                        <%--<li class="div_right_grayLight"></li>--%>
                                        <li>
                                            <%--<asp:Label ID="TIP_PAGINA_MULTIPLO_4Label" runat="server" Text="TIP_PAGINA_MULTIPLO_4"
                                AssociatedControlID="TIP_PAGINA_MULTIPLO_4TextBox"></asp:Label>--%>
                                            <%--<asp:CheckBox ID="TIP_PAGINA_MULTIPLO_4CheckBox" Text="Múltip. 4" runat="server"
                                                EnableViewState="False" ViewStateMode="Disabled"></asp:CheckBox>--%>
                                            <%--<asp:Label ID="TIP_INCLUIR_PUBLICIDADELabel" runat="server" Text="TIP_INCLUIR_PUBLICIDADE"
                                AssociatedControlID="TIP_INCLUIR_PUBLICIDADETextBox"></asp:Label>--%>
                                            <%--<asp:CheckBox ID="TIP_INCLUIR_PUBLICIDADECheckBox" Text="Publicidade" runat="server"
                                                EnableViewState="False" ViewStateMode="Disabled"></asp:CheckBox>--%>
                                        </li>
                                    </ul>
                                </div>
                            </fieldset>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="1" ShowMessageBox="true"
                            ShowSummary="false" runat="server" />
                    </fieldset>
                    <asp:LinkButton ID="CadastrarLinkButton" Visible="false" runat="server" CssClass="button_style icon_save shrink_animation"
                        ValidationGroup="1" OnClick="CadastrarLinkButton_Click">Cadastrar</asp:LinkButton>
                    <asp:LinkButton ID="PesquisarLinkButton" runat="server" CssClass="button_style icon_search shrink_animation">Pesquisar</asp:LinkButton>
                    <asp:LinkButton ID="CancelarLinkButton" runat="server" CssClass="button_style icon_cancel shrink_animation"
                        OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="second_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="COD_CLIENTE" DataSourceID="GridViewObjectDataSource"
                        EnableViewState="False" PageSize="30" CellSpacing="-1" GridLines="None" OnPreRender="grvPrincipal_PreRender"
                        OnRowCommand="grvPrincipal_RowCommand" CssClass="stylized">
                        <Columns>
                            <asp:BoundField DataField="des_tipocliente" HeaderText="Tipo Cliente" SortExpression="des_tipocliente">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_CIDADE" HeaderText="Cidade" SortExpression="DES_CIDADE">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_DESCRICAO" HeaderText="Descrição" SortExpression="DES_DESCRICAO">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_EMAIL" HeaderText="Email" SortExpression="DES_EMAIL">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_AUTORIDADE" HeaderText="Autoridade" SortExpression="DES_AUTORIDADE">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="btnEditar"
                                        CssClass="tb_icon_editDoc shrink_animation" ToolTip="Editar" CommandArgument='<%# Eval("COD_CLIENTE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            Nenhum registro foi encontrado!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" DeleteMethod="Excluir"
                        SelectMethod="ListarPorFiltro" TypeName="DsAdmin.Cliente" EnableViewState="False">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DES_CIDADETextBox" PropertyName="Text" Name="DES_CIDADE"
                                Type="String" />
                            <asp:Parameter Name="DES_UF" Type="String" />
                            <asp:Parameter Name="TIP_ATIVO" Type="String" />
                            <asp:Parameter Name="DES_ENDERECO" Type="String" />
                            <asp:Parameter Name="DES_BAIRRO" Type="String" />
                            <asp:Parameter Name="DES_CEP" Type="Int32" />
                            <asp:Parameter Name="DES_TELEFONE" Type="String" />
                            <asp:Parameter Name="DES_EMAIL" Type="String" />
                            <asp:Parameter Name="DES_CONTATO" Type="String" />
                            <asp:Parameter Name="DES_AUTORIDADE" Type="String" />
                            <asp:ControlParameter ControlID="COD_TIPOCLIENTEDropDownList" PropertyName="SelectedValue"
                                Name="COD_TIPOCLIENTE" Type="Int32" />
                            <asp:Parameter Name="NOM_EDITOR" Type="String" />
                            <asp:Parameter Name="NOM_SECRETARIO" Type="String" />
                            <asp:Parameter Name="tip_site_ativo" Type="Int32" />
                            <asp:Parameter Name="des_url_alternativa" Type="String" />
                            <asp:Parameter Name="TIP_CAPA_INDICE" Type="String" />
                            <asp:Parameter Name="TIP_PAGINA_MULTIPLO_4" Type="String" />
                            <asp:Parameter Name="TIP_INCLUIR_PUBLICIDADE" Type="String" />
                            <asp:Parameter Name="cod_modelo_edicao_moe" Type="Int32" />
                            <asp:Parameter Name="tip_assinatura_digital" Type="String" />
                            <asp:Parameter Name="tip_carimbo_tempo" Type="String" />
                            <asp:Parameter Name="COD_CLIENTE" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

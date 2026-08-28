<%@ Page Title="FonteModelo" Language="C#" MasterPageFile="~/Admin/MasterPage.master"
    AutoEventWireup="false" EnableViewState="true" ViewStateEncryptionMode="Never"
    EnableTheming="false" CodeFile="SEPFonteModelo.aspx.cs" Inherits="admin_SEPFonteModelo" %>

<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Sistema de criação de modelos - FonteModelo</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>Modelos de Fonte</legend>
                        <div class="flutuaEsq">
                            <asp:Label ID="des_fonte_modelo_fomLabel" CssClass="required" runat="server" Text="Modelo da Fonte"
                                AssociatedControlID="des_fonte_modelo_fomTextBox"></asp:Label>
                            <asp:TextBox ID="des_fonte_modelo_fomTextBox" runat="server" MaxLength="50" EnableViewState="False"
                                ViewStateMode="Disabled"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server" ControlToValidate="des_fonte_modelo_fomTextBox" ErrorMessage="Informe o modelo da fonte!"
                                    ValidationGroup="1" Display="None"></asp:RequiredFieldValidator><br />
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="num_tamanho_minimo_fomLabel" runat="server" Text="Tamanho Minimo"
                                AssociatedControlID="num_tamanho_minimo_fomTextBox"></asp:Label><asp:TextBox ID="num_tamanho_minimo_fomTextBox"
                                    runat="server" MaxLength="9" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><asp:MaskedEditExtender
                                        ID="num_tamanho_minimo_fomTextBox_MaskedEditExtender" runat="server" Enabled="True"
                                        Mask="999999999" PromptCharacter="" TargetControlID="num_tamanho_minimo_fomTextBox">
                                    </asp:MaskedEditExtender>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="1" ShowMessageBox="true"
                            ShowSummary="false" runat="server" />
                        <br />
                    </fieldset>
                    <asp:LinkButton ID="CadastrarLinkButton" runat="server" CssClass="button_style icon_save"
                        ValidationGroup="1" OnClick="CadastrarLinkButton_Click">Cadastrar</asp:LinkButton>
                    <asp:LinkButton ID="PesquisarLinkButton" runat="server" CssClass="button_style icon_search">Pesquisar</asp:LinkButton>
                    <asp:LinkButton ID="CancelarLinkButton" runat="server" CssClass="button_style icon_cancel"
                        OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="second_content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="cod_fonte_modelo_fom" DataSourceID="GridViewObjectDataSource"
                        EnableViewState="False" PageSize="30" CellSpacing="-1" GridLines="None" OnPreRender="grvPrincipal_PreRender"
                        OnRowCommand="grvPrincipal_RowCommand" CssClass="stylized">
                        <Columns>
                            <asp:BoundField DataField="des_fonte_modelo_fom" HeaderText="Modelo Fonte" SortExpression="des_fonte_modelo_fom">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_tamanho_minimo_fom" HeaderText="Tamanho Minimo" SortExpression="num_tamanho_minimo_fom">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="btnEditar"
                                        CssClass="tb_icon_editDoc" ToolTip="Editar" CommandArgument='<%# Eval("cod_fonte_modelo_fom") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="False" CommandName="btnExcluir"
                                        CssClass="tb_icon_delete" OnClientClick="return confirm('Confirma exclusão?')"
                                        ToolTip="Excluir" CommandArgument='<%# Eval("cod_fonte_modelo_fom") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            Nenhum registro foi encontrado!
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" DeleteMethod="Excluir"
                SelectMethod="ListarPorFiltro" TypeName="DsAdmin.FonteModelo" EnableViewState="False">
                <SelectParameters>
                    <asp:Parameter Name="cod_fonte_modelo_fom" Type="Int32" />
                    <asp:ControlParameter ControlID="des_fonte_modelo_fomTextBox" DefaultValue="" Name="des_fonte_modelo_fom"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="num_tamanho_minimo_fomTextBox" DefaultValue="" Name="num_tamanho_minimo_fom"
                        PropertyName="Text" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>

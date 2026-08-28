<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="~/Admin/SEPMaquina.aspx.cs"
    Inherits="Admin_SEPMaquina" %>

<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function macAdd(val) {
            val = val.replace(/[^\w:]|_/g, '')
            val = val.replace(/(\w{2})([^:])/g, '$1' + ':' + '$2')
            val = val.replace(/:$/, '')
            return val
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>Máquinas</legend>
                        <div class="flutuaEsq">
                            <asp:Label ID="des_descricaoLabel" runat="server" CssClass="required" Text="Descrição" AssociatedControlID="des_descricaoTextBox"></asp:Label>
                            <asp:TextBox ID="des_descricaoTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="des_descricaoTextBox"
                                ErrorMessage="Informe a Descrição!" ID="IMAPRequiredFieldValidator1" runat="server"></imap:IMAPRequiredFieldValidator>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="des_macLabel" runat="server" CssClass="required" Text="Mac" AssociatedControlID="des_macTextBox"></asp:Label>
                            <asp:TextBox ID="des_macTextBox" onkeypress="this.value=macAdd(this.value)" runat="server" EnableViewState="False" MaxLength="17" ViewStateMode="Disabled"></asp:TextBox>
                            <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="des_macTextBox"
                                ErrorMessage="Informe o Mac!" ID="IMAPRequiredFieldValidator2" runat="server"></imap:IMAPRequiredFieldValidator>
                        </div>
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="1" runat="server" />
                    </fieldset>
                    <asp:LinkButton ID="CadastrarLinkButton" runat="server" CssClass="button_style icon_save shrink_animation"
                        ValidationGroup="1" OnClick="CadastrarLinkButton_Click">Cadastrar</asp:LinkButton>
                    <asp:LinkButton ID="CancelarLinkButton" runat="server" CssClass="button_style icon_cancel shrink_animation"
                        OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="second_content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="cod_maquina" DataSourceID="GridViewObjectDataSource"
                        EnableViewState="False" PageSize="20" CellSpacing="-1" GridLines="None" OnPreRender="grvPrincipal_PreRender"
                        OnRowCommand="grvPrincipal_RowCommand" CssClass="stylized">
                        <Columns>
                            <asp:BoundField DataField="des_descricao" HeaderText="Descrição" SortExpression="des_descricao">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="des_mac" HeaderText="Mac" SortExpression="des_mac">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="btnEditar"
                                        CssClass="tb_icon_editDoc shrink_animation" ToolTip="Editar" CommandArgument='<%# Eval("cod_maquina") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="False" CommandName="btnExcluir"
                                        CssClass="tb_icon_delete shrink_animation" OnClientClick="return confirm('Confirma exclusão?')" ToolTip="Excluir"
                                        CommandArgument='<%# Eval("cod_maquina") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            Nenhum registro foi encontrado!
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" SelectMethod="Listar"
                TypeName="DsAdmin.Maquina" EnableViewState="False"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>


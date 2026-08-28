<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPageLogin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="clearfix marginBottom_10">
                    <div class="flutuaEsqLess">
                        <label for="userLogin" class="required">
                            Login</label>
                        <asp:TextBox ID="userLogin" type="text" size="32" CssClass="mask_cpf" runat="server"></asp:TextBox>
                        <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="userLogin"
                            ErrorMessage="Informe seu Usuário!" ID="loginTextBoxIMAPRequiredFieldValidator"
                            runat="server"></imap:IMAPRequiredFieldValidator>
                        <label for="userPassword" class="required">
                            Senha</label>
                        <asp:TextBox ID="userPassword" size="32" runat="server" TextMode="Password"></asp:TextBox>
                        <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="userPassword"
                            ErrorMessage="Informe sua Senha!" ID="senhaTextBoxIMAPRequiredFieldValidator"
                            runat="server"></imap:IMAPRequiredFieldValidator>
                        <asp:Label runat="server" ID="userChave" CssClass="required" AssociatedControlID="userChaveTextBox"></asp:Label>
                        <asp:TextBox ID="userChaveTextBox" type="text" size="32" runat="server"></asp:TextBox>
                        <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="userChaveTextBox"
                            ErrorMessage="Informe sua Chave!" ID="IMAPRequiredFieldValidator3" runat="server"></imap:IMAPRequiredFieldValidator>
                        <asp:FilteredTextBoxExtender ID="userChaveFilteredTextBox" runat="server" TargetControlID="userChaveTextBox"
                            FilterType="Numbers" />
                    </div>
                </div>
                <div class="flutuaEsq">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:LinkButton ID="lnk_EsqueciSenha" runat="server" rel="nofollow" CssClass="legenda infoHelp"
                                ToolTip="Ao clicar em 'Esqueci minha Senha', será enviada uma mensagem para o seu e-mail cadastrado. Siga todas as intruções contidas nesse e-mail.">esqueci minha senha!</asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="1" ShowMessageBox="true"
                    ShowSummary="false" runat="server" />
                <asp:LinkButton ID="AcessarLinkButton" OnClick="AcessarLinkButton_Click" runat="server"
                    ValidationGroup="1" CssClass="gb_darkGreen shrink_animation flutua_last-child button_style">Entrar</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <asp:Label ID="lbModal" runat="server">
    </asp:Label>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        OkControlID="ButtonOK" PopupControlID="Pnl_ModalEsquecisenha" TargetControlID="lbModal" />
    <!-- Modal Esqueci minha senha -->
    <asp:Panel ID="Pnl_ModalEsquecisenha" runat="server" Style="display: none; width: 400px;
        background-color: White; border-width: 2px; border-color: Black; border-style: solid;
        padding: 20px;">
        <div id="Div1" class="stylized">
            <h2>
                Recuperar Senha
            </h2>
            <p>
                Confirme os dados abaixo e receberá a senha no e-mail cadastrado</p>
            <label for="txtCpf">
                CPF:</label>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtCpf" size="36" runat="server"></asp:TextBox>
                    <imap:IMAPRequiredFieldValidator ValidationGroup="2" ControlToValidate="txtCpf" ID="IMAPRequiredFieldValidator1"
                        runat="server"></imap:IMAPRequiredFieldValidator>
                    <label for="txt_email">
                        Email :</label>
                    <asp:TextBox ID="txt_email" size="36" runat="server"></asp:TextBox>
                    <imap:IMAPRequiredFieldValidator ValidationGroup="2" ControlToValidate="txt_email"
                        ID="IMAPRequiredFieldValidator2" runat="server"></imap:IMAPRequiredFieldValidator>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnRecupera" runat="server" Text="Enviar" ValidationGroup="2" CssClass="flutuaEsq" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:LinkButton ID="ButtonOK" runat="server">Voltar</asp:LinkButton>
        </div>
    </asp:Panel>
    <!-- END Modal Esqueci minha senha-->
</asp:Content>

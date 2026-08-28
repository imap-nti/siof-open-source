<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" EnableViewState="true"
    AutoEventWireup="false" CodeFile="TEMPSEPRevisor.aspx.cs" Inherits="Admin_TEMPSEPRevisor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Revisor</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="86400">
    </asp:ToolkitScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <legend>Revisor</legend>
                        <div class="flutuaEsq">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <div class="lightbox">
                                        <div class="preloader_box modal grad_rd_box">
                                            <h2 class="preloader_title">Aguarde um momento por favor</h2>
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:Label ID="ArquivoFileUploadLabel" runat="server" CssClass="required" Text="Arquivo"
                                AssociatedControlID="ArquivoFileUpload"></asp:Label>
                            <asp:AsyncFileUpload ID="ArquivoFileUpload" OnUploadedComplete="ArquivoFileUpload_UploadedComplete"
                                runat="server" EnableViewState="false" />
                            <br />
                            <asp:Button ID="ButtonSelecionar" runat="server" CssClass="shrink_animation icon_send"
                                OnClick="ButtonSelecionar_Click" Text="Carregar Arquivo" />
                            <br />
                            <asp:Label ID="Dados_Cliente" ViewStateMode="Disabled" runat="server"></asp:Label>

                            <asp:Label ID="FilePreview" runat="server"></asp:Label>
                        </div>
                        <br />
                        <div class="flutuaEsq">
                            <br />
                            <asp:Label ID="des_motivo_motLabel" runat="server" CssClass="required" Text="Descrição"
                                AssociatedControlID="des_motivo_motTextBox"></asp:Label>
                            <asp:TextBox ID="des_motivo_motTextBox" runat="server" TextMode="MultiLine" MaxLength="2147483647"
                                EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="des_motivo_motTextBox"
                                ErrorMessage="Informe o Motivo!" ID="des_motivo_motTextBoxIMAPRequiredFieldValidator"
                                runat="server"></imap:IMAPRequiredFieldValidator>
                            <br />
                            <div class="flutuaEsq">
                                <asp:CheckBox ID="tip_assinatura_digitalCheckBox" runat="server" Text="Assinatura Digital" Checked="true" />
                                <asp:CheckBox ID="tip_carimbo_tempoCheckBox" runat="server" Text="Carimbo de Tempo" />
                                <br />
                                <asp:CheckBox ID="LimparAoPublicarCheckBox" runat="server" Text="Limpar ao Publicar" Checked="true" />
                                <asp:CheckBox ID="tip_amostra_gratis_docCheckBox" runat="server" Text="Amostra Grátis" Checked="false" />
                                <br />
                                <asp:CheckBox ID="tip_texto_extraidoCheckBox" runat="server" Text="OCR" Checked="false" />
                            </div>
                        </div>
                        <br />
                        <ul class="buttonBar grad_rd_box">
                            <li>
                                <asp:LinkButton ID="PublicarLinkButton" runat="server" ValidationGroup="1" CssClass="button_style icon_publish shrink_animation"
                                    OnClick="PublicarLinkButton_Click">Publicar</asp:LinkButton>
                                <asp:ConfirmButtonExtender ID="PublicarLinkButtonConfirmButtonExtender" runat="server"
                                    TargetControlID="PublicarLinkButton" ConfirmText="Está certo de que deseja publicar esta edição?" />
                            </li>
                            <li class="div_right_grayLight"></li>
                            <li>
                                <asp:LinkButton ID="CancelarLinkButton" runat="server" CssClass="button_style icon_cancelDoc shrink_animation"
                                    OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton>
                            </li>
                        </ul>
                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="1" runat="server" />
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

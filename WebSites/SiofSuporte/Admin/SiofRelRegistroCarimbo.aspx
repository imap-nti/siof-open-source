<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="SiofRelRegistroCarimbo.aspx.cs" Inherits="Admin_SiofRelRegistroCarimbo" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/reportviewer.css" />
    <title>RelRegistroCarimbo</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <legend>Revisor</legend>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div class="lightbox">
                                    <div class="preloader_box grad_rd_box">
                                        <h2 class="preloader_title">Aguarde um momento por favor</h2>
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <div class="flutuaEsq">
                            <asp:Label ID="dat_inicioLabel" runat="server" CssClass="required" Text="Data Inicial"
                                AssociatedControlID="dat_inicioTextBox"></asp:Label><asp:TextBox ID="dat_inicioTextBox"
                                    runat="server" MaxLength="10" EnableViewState="False" ViewStateMode="Disabled"
                                    onKeyPress="javascript: return false;" onPaste="javascript: return false;"></asp:TextBox><imap:IMAPRequiredFieldValidator
                                        ID="IMAPRequiredFieldValidator1" Display="None" ValidationGroup="1" ErrorMessage="Informe a Data Inicial!"
                                        ControlToValidate="dat_inicioTextBox" runat="server"></imap:IMAPRequiredFieldValidator><asp:CalendarExtender
                                            ID="dat_inicioTextBox_CalendarExtender" runat="server" Enabled="True" TargetControlID="dat_inicioTextBox"
                                            Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="dat_fimLabel" runat="server" CssClass="required" Text="Data Final"
                                AssociatedControlID="dat_fimTextBox"></asp:Label><asp:TextBox ID="dat_fimTextBox"
                                    runat="server" MaxLength="10" EnableViewState="False" ViewStateMode="Disabled"
                                    onKeyPress="javascript: return false;" onPaste="javascript: return false;"></asp:TextBox><imap:IMAPRequiredFieldValidator
                                        ID="IMAPRequiredFieldValidator2" Display="None" ValidationGroup="1" ErrorMessage="Informe a Data Final!"
                                        ControlToValidate="dat_fimTextBox" runat="server"></imap:IMAPRequiredFieldValidator><asp:CalendarExtender
                                            ID="dat_fimTextBox_CalendarExtender" runat="server" Enabled="True" TargetControlID="dat_fimTextBox"
                                            Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                        </div>
                        <br />
                        <div class="flutuaEsq">
                            <ul class="buttonBar grad_rd_box">
                                <li>
                                    <asp:LinkButton ID="GerarRelatorioLinkButton" runat="server" ValidationGroup="1"
                                        CssClass="button_style icon_pdf shrink_animation" OnClick="GerarRelatorioLinkButton_Click">Gerar Relatório</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="1" runat="server" />
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="flutuaEsq">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                            Height="500" Width="650">
                            <localreport reportpath="Admin\RelCarimboTempoUsado.rdlc">
                                
                            </localreport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="DsAdmin.CarimboRegistro"
                            SelectMethod="ConsultarRelatorioCarimboTempo" OnSelecting="ObjectDataSource1_Selecting">
                            <SelectParameters>
                                <asp:Parameter Name="data_inicio" Type="DateTime" />
                                <asp:Parameter Name="data_fim" Type="DateTime" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GerarRelatorioLinkButton" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

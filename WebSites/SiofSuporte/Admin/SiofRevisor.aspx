<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false"
    CodeFile="SiofRevisor.aspx.cs" EnableViewState="true" Inherits="Admin_SiofRevisor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Revisor</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
        </div>
        <div id="second_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <legend>Demandas do Revisor</legend>
                        <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" EnableViewState="False" PageSize="25" CellSpacing="-1"
                            DataSourceID="RevisorObjectDataSource" GridLines="None" OnRowCommand="grvPrincipal_RowCommand"
                            OnPreRender="grvPrincipal_PreRender" CssClass="stylized">
                            <Columns>
                                <asp:BoundField DataField="des_cidade" HeaderText="Cliente" SortExpression="des_cidade">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="des_nome" HeaderText="Cliente" SortExpression="des_nome">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="doc_dt_publicacao" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}"
                                    SortExpression="doc_dt_publicacao">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="doc_comentario" HeaderText="Comentário" SortExpression="doc_comentario">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="uf" HeaderText="UF" SortExpression="uf">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField ShowHeader="False">
                                    <ControlStyle Height="15px" Width="15px" />
                                    <ItemStyle Width="15px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDetalhar" runat="server" CausesValidation="False" CommandName="btnDetalhar"
                                            CssClass="tb_icon_viewDoc" ToolTip="Detalhar" CommandArgument='<%# Eval("doc_codigo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                Nenhuma demanda pendente, Parabéns!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </fieldset>
                    <asp:ObjectDataSource ID="RevisorObjectDataSource" runat="server" SelectMethod="ListarDemandaPorFiltro"
                        TypeName="DsAdmin.Revisor">
                        <SelectParameters>
                            <asp:Parameter Name="txtDtIni" Type="DateTime" />
                            <asp:Parameter Name="txtDtFim" Type="DateTime" />
                            <asp:Parameter Name="cboCidadeFiltro" Type="String" />
                            <asp:Parameter Name="codUsuario" Type="Int32" />
                            <asp:Parameter Name="cboStatus" Type="Int32" />
                            <asp:Parameter Name="cboCidadeCliente" Type="Int32" />
                            <asp:Parameter Name="cboTipo" Type="Int32" />
                            <asp:Parameter Name="txtComprovante" Type="String" />
                            <asp:Parameter Name="cboTipoDocumento" Type="Int32" />
                            <asp:Parameter Name="cboUsuario" Type="Int32" />
                            <asp:Parameter Name="cboOperador" Type="Int32" />
                            <asp:Parameter Name="cboUf" Type="String" />
                            <asp:Parameter Name="NIVELRELPUBLICACOES" Type="Int32" />
                            <asp:Parameter Name="chkOutros" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- Modal Detalhamento de Demandas -->
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="ModalDetalhamentoPlaceholder" runat="server">
                    <div id="Div1" class="lightbox">
                        <div class="screen_editDescSite windowFloat">
                            <fieldset>
                                <legend>Detalhes</legend>
                                <asp:LinkButton ID="FecharModalDetalhamentoLinkButton" runat="server" CssClass="close_window flutuaEsq"
                                    OnClick="FecharModalDetalhamentoLinkButton_Click">Fechar</asp:LinkButton>
                                <div class="flutuaEsq">
                                    <asp:GridView ID="ItensUtilizadosGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" EnableViewState="False" ViewStateMode="Disabled"
                                        DataKeyNames="doc_codigo,and_codigo" PageSize="20" CellSpacing="-1" DataSourceID="ItensUtilizadosObjectDataSource"
                                        GridLines="None" OnPreRender="grvPrincipal_PreRender" CssClass="stylized">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Item" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="and_caminho" HeaderText="Documento(s)" SortExpression="and_caminho">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField ShowHeader="False">
                                                <ControlStyle Height="15px" Width="15px" />
                                                <ItemStyle Width="15px" />
                                                <ItemTemplate>
                                                    <a class="tb_icon_pdf" href="DownloadHandler.ashx?c=<%# Eval("and_codigo") %>"></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="ItensUtilizadosObjectDataSource" runat="server" SelectMethod="ListarArquivoPublicacao"
                                        OnSelecting="DemandaObjectDataSource_Selecting" TypeName="DsAdmin.Arquivo">
                                        <SelectParameters>
                                            <asp:Parameter Name="cod_publicacao_pub" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:Label ID="HyperLinkArquivoFinalLabel" runat="server" Text="Edição Final" AssociatedControlID="HyperLinkArquivoFinal"></asp:Label>
                                    <asp:LinkButton ID="HyperLinkArquivoFinal" ToolTip="Baixar" OnClick="HyperLinkArquivoFinal_Click"
                                        runat="server"></asp:LinkButton>
                                </div>
                                <div class="flutuaEsq">
                                    <asp:Label ID="des_observacao_ediLabel" runat="server" CssClass="required" Text="Descrição"
                                        AssociatedControlID="des_observacao_ediTextBox"></asp:Label>
                                    <asp:TextBox ID="des_observacao_ediTextBox" runat="server" TextMode="MultiLine" MaxLength="2147483647"
                                        EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                    <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="des_observacao_ediTextBox"
                                        ErrorMessage="Informe a descrição da edição!" ID="des_observacao_ediTextBoxIMAPRequiredFieldValidator"
                                        runat="server"></imap:IMAPRequiredFieldValidator>
                                </div>
                                <br />
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="1" runat="server" />
                                <ul class="buttonBar grad_rd_box">
                                    <li>
                                        <asp:LinkButton ID="DevolverLinkButton" runat="server" CssClass="button_style icon_clean shrink_animation"
                                            OnClick="DevolverLinkButton_Click">Devolver</asp:LinkButton>
                                    </li>
                                    <%--<li class="div_right_grayLight"></li>--%>
                                    <%--<li>
                                        <asp:LinkButton ID="AlterarLinkButton" runat="server" CssClass="button_style icon_editDoc shrink_animation"
                                            OnClick="AlterarLinkButton_Click">Alterar</asp:LinkButton>
                                    </li>--%>
                                    <%--<li class="div_right_grayLight"></li>--%>
                                    <li>
                                        <asp:LinkButton ID="GerarEdiçãoLinkButton" runat="server" ValidationGroup="1" CssClass="button_style icon_newBlank shrink_animation"
                                            OnClick="GerEdicaoLinkButton_Click">Gerar Edição</asp:LinkButton>
                                    </li>
                                </ul>
                            </fieldset>
                        </div>
                    </div>
                    <asp:ObjectDataSource ID="ItensPublicacaoObjectDataSource" runat="server" SelectMethod="ListarDemandaPorFiltro"
                        TypeName="DsAdmin.Revisor"></asp:ObjectDataSource>
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- Modal Arquivo Alterado -->
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="ModalArquivoAlteradoPlaceHolder" runat="server">
                    <div id="Div2" class="lightbox">
                        <div class="screen_editDescSite windowFloat">
                            <fieldset>
                                <legend>Alterar Publicação</legend>
                                <asp:LinkButton ID="FecharModalArquivoAlteradoLinkButton" runat="server" CssClass="close_window flutuaEsq"
                                    OnClick="FecharModalArquivoAlteradoLinkButton_Click">Fechar</asp:LinkButton>
                                <div class="flutuaEsq">
                                    <asp:Label ID="ArquivoAlteradoFileUploadLabel" runat="server" CssClass="required"
                                        Text="Arquivo" AssociatedControlID="ArquivoAlteradoFileUpload"></asp:Label>
                                    <asp:AsyncFileUpload ID="ArquivoAlteradoFileUpload" runat="server" EnableViewState="false"
                                        OnUploadedComplete="ArquivoAlteradoFileUpload_UploadedComplete" OnUploadedFileError="ArquivoAlteradoFileUpload_UploadedError" />
                                    <%--<imap:IMAPRequiredFieldValidator ValidationGroup="2" Display="None" ControlToValidate="ArquivoAlteradoFileUpload"
                                        ErrorMessage="Anexe o arquivo final!" ID="IMAPRequiredFieldValidator1" runat="server"></imap:IMAPRequiredFieldValidator>--%><br />
                                    <asp:Label ID="des_motivo_motLabel" runat="server" CssClass="required" Text="Descrição"
                                        AssociatedControlID="des_motivo_motTextBox"></asp:Label>
                                    <asp:TextBox ID="des_motivo_motTextBox" runat="server" TextMode="MultiLine" MaxLength="2147483647"
                                        EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                    <imap:IMAPRequiredFieldValidator ValidationGroup="2" Display="None" ControlToValidate="des_motivo_motTextBox"
                                        ErrorMessage="Informe o Motivo!" ID="des_motivo_motTextBoxIMAPRequiredFieldValidator"
                                        runat="server"></imap:IMAPRequiredFieldValidator>
                                </div>
                            </fieldset>
                            <asp:ValidationSummary ID="ValidationSummary2" ShowMessageBox="true" ShowSummary="false"
                                ValidationGroup="2" runat="server" />
                            <ul class="buttonBar grad_rd_box">
                                <li>
                                    <asp:LinkButton ID="PublicarArquivoAlteradoLinkButton" runat="server" ValidationGroup="2"
                                        CssClass="button_style icon_publish shrink_animation" OnClick="GerEdicaoLinkButton_Click">Publicar</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- Modal Arquivo Rejeitado -->
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder ID="ModalArquivoRejeitadoPlaceHolder" runat="server" Visible="false">
                    <div id="Div3" class="lightbox">
                        <div class="screen_editDescSite windowFloat">
                            <fieldset>
                                <legend>Motivo</legend>
                                <asp:LinkButton ID="FecharModalArquivoRejeitadoLinkButton" runat="server" CssClass="close_window flutuaEsq"
                                    OnClick="FecharModalArquivoRejeitadoLinkButton_Click">Fechar</asp:LinkButton>
                                <div class="flutuaEsq">
                                    <asp:Label ID="des_ArquivoRejeitadoLabel" runat="server" CssClass="required" Text="Descrição"
                                        AssociatedControlID="des_ArquivoRejeitadoTextBox"></asp:Label>
                                    <asp:TextBox ID="des_ArquivoRejeitadoTextBox" runat="server" TextMode="MultiLine"
                                        MaxLength="2147483647" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                    <imap:IMAPRequiredFieldValidator ValidationGroup="3" Display="None" ControlToValidate="des_ArquivoRejeitadoTextBox"
                                        ErrorMessage="Informe o Motivo!" ID="IMAPRequiredFieldValidator3" runat="server"></imap:IMAPRequiredFieldValidator>
                                </div>
                            </fieldset>
                            <asp:ValidationSummary ID="ValidationSummary3" ShowMessageBox="true" ShowSummary="false"
                                ValidationGroup="3" runat="server" />
                            <ul class="buttonBar grad_rd_box">
                                <li>
                                    <asp:LinkButton ID="ArquivoRejeitadoLinkButton" runat="server" ValidationGroup="3"
                                        CssClass="button_style icon_cancelDoc shrink_animation" OnClick="ArquivoRejeitadoLinkButton_Click">Confirmar</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--Modal Gerar Publicação -->
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:PlaceHolder ID="ModalGerarPublicacaoPlaceHolder" runat="server" Visible="true">
                    <div id="Div4" class="lightbox">
                        <div class="screen_editDescSite windowFloat">
                            <fieldset>
                                <legend>Gerar Publicação</legend>
                                <asp:LinkButton ID="FecharModalGerarPublicacaoLinkButton" runat="server" CssClass="close_window flutuaEsq"
                                    OnClick="FecharModalGerarPublicacaoLinkButton_Click">Fechar</asp:LinkButton>
                                <div class="flutuaEsq">
                                    <%--<asp:Label runat="server" Text="Operações em documento" ID="GroupCheckLabel" AssociatedControlID="GroupCheck"></asp:Label>--%>
                                    <ul id="GroupCheck" class="buttonBar vertical grad_rd_box">
                                        <label id="GroupCheckLabel" for="GroupCheck">
                                            Operações em documento
                                        </label>
                                        <br />
                                        <li>
                                            <asp:CheckBox ID="tip_assinatura_digitalCheckBox" Text="Assinatura Digital" runat="server" />
                                        </li>
                                        <%--</div>
                                <div class="flutuaEsq">--%>
                                        <%--<asp:Label runat="server" Text="Carimbo de Tempo" ID="tip_carimbo_tempoLabel" AssociatedControlID="tip_carimbo_tempoCheckBox"></asp:Label>--%>
                                        <li>
                                            <asp:CheckBox ID="tip_carimbo_tempoCheckBox" Text="Carimbo de Tempo" runat="server" />
                                        </li>
                                    </ul>
                                </div>
                                <br />
                                <ul class="buttonBar grad_rd_box">
                                    <li>
                                        <asp:LinkButton ID="PublicarLinkButton" runat="server" CssClass="button_style icon_publish shrink_animation"
                                            OnClick="PublicarLinkButton_Click">Publicar</asp:LinkButton>
                                    </li>
                                    <li class="div_right_grayLight"></li>
                                    <li>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="VisualizarLinkButton" runat="server" CssClass="button_style icon_viewDoc shrink_animation"
                                                    OnClick="VisualizarLinkButton_Click">Visualizar</asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="VisualizarLinkButton" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </li>
                                    <li class="div_right_grayLight"></li>
                                    <li>
                                        <asp:LinkButton ID="CancelarLinkButton" runat="server" CssClass="button_style icon_cancelDoc shrink_animation"
                                            OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton>
                                    </li>
                                </ul>
                            </fieldset>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

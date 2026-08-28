<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="false"
    EnableViewState="true" CodeFile="SiofDiagramador.aspx.cs" Inherits="Admin_SiofDiagramador" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Diagramador</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
        </div>
        <div id="second_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <legend>Demandas do Diagramador</legend>
                        <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" EnableViewState="False" ViewStateMode="Disabled"
                            DataKeyNames="doc_codigo" PageSize="25" CellSpacing="-1" DataSourceID="DiagramadorObjectDataSource"
                            GridLines="None" OnRowCommand="grvPrincipal_RowCommand" OnPreRender="grvPrincipal_PreRender"
                            CssClass="stylized">
                            <Columns>
                                <asp:BoundField DataField="doc_codigo" HeaderText="Documento" SortExpression="doc_codigo"
                                    Visible="true">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
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
                                        <asp:LinkButton ID="btnDetalhar" runat="server" CausesValidation="False" CommandArgument='<%# Eval("doc_codigo") %>'
                                            CommandName="btnDetalhar" CssClass="tb_icon_viewDoc" ToolTip="Detalhar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                Nenhuma demanda pendente, Parabéns!
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </fieldset>
                    <asp:ObjectDataSource ID="DiagramadorObjectDataSource" runat="server" SelectMethod="ListarDemandaPorFiltro"
                        TypeName="DsAdmin.Diagramador">
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
            <!-- Modal Detalhamento de Demandas -->
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="ModalDetalhamentoPlaceholder" Visible="true" runat="server">
                        <div id="Div1" class="lightbox">
                            <div class="screen_editDescSite windowFloat">
                                <fieldset>
                                    <legend>Detalhes</legend>
                                    <asp:LinkButton ID="FecharModalDetalhamentoLinkButton" runat="server" CssClass="close_window flutuaEsq"
                                        OnClick="FecharModalDetalhamentoLinkButton_Click">Fechar</asp:LinkButton>
                                    <asp:Label ID="des_motivo_devolucao_label" runat="server" Text=""></asp:Label>
                                    <div class="flutuaEsq">
                                        <asp:GridView ID="ItensUtilizadosGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" EnableViewState="False" ViewStateMode="Disabled"
                                            DataKeyNames="doc_codigo,and_codigo" PageSize="20" OnRowCommand="ItensUtilizadosGridView_RowCommand"
                                            CellSpacing="-1" DataSourceID="DemandaObjectDataSource" GridLines="None" OnPreRender="grvPrincipal_PreRender"
                                            CssClass="stylized">
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
                                                        <asp:LinkButton ID="btnUpload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("doc_codigo") %>'
                                                            CommandName="btnUpload" CssClass="tb_icon_uploadFile" ToolTip="Contas Públicas" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ControlStyle Height="15px" Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <a class="tb_icon_pdf" href="DownloadHandler.ashx?c=<%# Eval("and_codigo") %>"></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="DemandaObjectDataSource" runat="server" OnSelecting="DemandaObjectDataSource_Selecting"
                                            SelectMethod="ListarArquivoPublicacao" TypeName="DsAdmin.Arquivo">
                                            <SelectParameters>
                                                <asp:Parameter Name="cod_publicacao_pub" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:Label ID="ArquivoFinalFileUploadLabel" runat="server" CssClass="required" Text="Arquivo Final"
                                            AssociatedControlID="ArquivoFinalFileUpload"></asp:Label>
                                        <asp:AsyncFileUpload ID="ArquivoFinalFileUpload" EnableViewState="false" OnUploadedComplete="ArquivoFinalFileUpload_UploadedComplete"
                                            runat="server" />
                                    </div>
                                    <div class="flutuaEsq">
                                        <asp:Label ID="des_observacao_ediLabel" runat="server" CssClass="required" Text="Descrição"
                                            AssociatedControlID="des_observacao_ediTextBox"></asp:Label>
                                        <asp:TextBox ID="des_observacao_ediTextBox" runat="server" TextMode="MultiLine" MaxLength="2147483647"
                                            EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                        <imap:IMAPRequiredFieldValidator ValidationGroup="1" Display="None" ControlToValidate="des_observacao_ediTextBox"
                                            ErrorMessage="Informe a descrição da edição!" ID="des_observacao_ediTextBoxIMAPRequiredFieldValidator"
                                            runat="server">
                                        </imap:IMAPRequiredFieldValidator>
                                    </div>
                                </fieldset>
                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="1" runat="server" />
                                <ul class="buttonBar grad_rd_box">
                                    <li>
                                        <asp:LinkButton ID="EnviarRevisaoLinkButton" runat="server" ValidationGroup="1" CssClass="button_style icon_send shrink_animation"
                                            OnClick="EnviarRevisaoLinkButton_Click">Enviar para Revisão</asp:LinkButton>
                                    </li>
                                    <li class="div_right_grayLight"></li>
                                    <li>
                                        <asp:LinkButton ID="PublicarContasPublicasLinkButton" runat="server" CssClass="button_style icon_publish shrink_animation"
                                            OnClick="PublicarContasPublicasLinkButton_Click">Publicar Contas Publicas</asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Modal Arquivos Contas Públicas -->
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:PlaceHolder ID="ModalArquivosContasPublicasPlaceHolder" runat="server" Visible="true">
                        <div id="Div2" class="lightbox">
                            <div class="screen_editDescSite windowFloat">
                                <fieldset>
                                    <legend>Arquivos Contas Públicas</legend>
                                    <asp:LinkButton ID="FecharModalArquivosContasPublicasLinkButton" runat="server" CssClass="close_window flutuaEsq"
                                        OnClick="FecharModalArquivosContasPublicasLinkButton_Click">Fechar</asp:LinkButton>
                                    <asp:Label ID="ArquivosLabel" Text="Arquivos" runat="server" CssClass="required"
                                        AssociatedControlID="ItemContasPublicasFileUpload"></asp:Label>
                                    <asp:AjaxFileUpload AllowedFileTypes="pdf" OnUploadComplete="ItemContasPublicasFileUpload_UploadedComplete"
                                        OnUploadCompleteAll="ItemContasPublicasFileUpload_UploadCompleteAll" MaximumNumberOfFiles="10"
                                        ID="ItemContasPublicasFileUpload" runat="server"></asp:AjaxFileUpload>
                                </fieldset>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                    <ContentTemplate>
                                        <asp:GridView ID="ArquivosContasPublicasGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" EnableViewState="False" ViewStateMode="Disabled"
                                            DataKeyNames="cod_publicacao" PageSize="10" CellSpacing="-1" DataSourceID="ArquivosContasPublicasObjectDataSource"
                                            GridLines="None" OnPreRender="grvPrincipal_PreRender" OnRowCommand="ArquivosContasPublicasGridview_RowCommand"
                                            CssClass="stylized">
                                            <Columns>
                                                <asp:BoundField DataField="des_nomearquivo" HeaderText="Arquivo" SortExpression="des_nomearquivo">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="des_nomepublicacao" HeaderText="Publicação" SortExpression="des_nomepublicacao">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dat_publicacao" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}"
                                                    SortExpression="dat_publicacao">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <%--<asp:TemplateField ShowHeader="False">
                                                    <ControlStyle Height="15px" Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="btnEditar"
                                                            CssClass="tb_icon_editDoc" ToolTip="Detalhar" CommandArgument='<%# Eval("cod_publicacao") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ControlStyle Height="15px" Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="False" CommandName="btnExcluir"
                                                            CssClass="tb_icon_delete" ToolTip="Excluir" OnClientClick="return confirm('Confirma exclusão?')"
                                                            CommandArgument='<%# Eval("cod_publicacao") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Não foram encontrados registros.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="ArquivosContasPublicasObjectDataSource" OnSelecting="ArquivosContasPublicasObjectDataSource_Selecting"
                                            runat="server" TypeName="DsAdmin.Contaspublicas" SelectMethod="ListarPorFiltro">
                                            <SelectParameters>
                                                <asp:Parameter Name="cod_publicacao" Type="Int32" />
                                                <asp:Parameter Name="cod_tipopublicacao" Type="Int32" />
                                                <asp:Parameter Name="cod_tiposubpublicacao" Type="Int32" />
                                                <asp:Parameter Name="des_nomepublicacao" Type="String" />
                                                <asp:Parameter Name="dat_publicacao" Type="DateTime" />
                                                <asp:Parameter Name="des_nomearquivo" Type="String" />
                                                <asp:Parameter Name="num_ano_referencia" Type="Int32" />
                                                <asp:Parameter Name="COD_CLASSE" Type="Int32" />
                                                <asp:Parameter Name="COD_SUBCLASSE" Type="Int32" />
                                                <asp:Parameter Name="DES_CERTIFICACAO" Type="String" />
                                                <asp:Parameter Name="DAT_CADASTRO" Type="DateTime" />
                                                <asp:Parameter Name="cod_cliente" Type="Int32" />
                                                <asp:Parameter Name="usu_codigo" Type="Int32" />
                                                <asp:Parameter Name="and_codigo" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Modal Contas Públicas -->
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:PlaceHolder ID="ModalContaPublicaPlaceHolder" runat="server" Visible="false">
                        <div id="Div3" class="lightbox">
                            <div class="screen_editDescSite windowFloat">
                                <fieldset>
                                    <legend>Detalhes</legend>
                                    <asp:LinkButton ID="FecharModalContaPublicaLinkButton" runat="server" CssClass="close_window flutuaEsq"
                                        OnClick="FecharModalContaPublicaLinkButton_Click">Fechar</asp:LinkButton>
                                    <%--<div class="flutuaEsq">
                                        <asp:Label ID="des_nomepublicacaoLabel" CssClass="required" Text="Nome Publicação"
                                            AssociatedControlID="des_nomepublicacaoTextBox" runat="server"></asp:Label>
                                        <asp:TextBox ID="des_nomepublicacaoTextBox" runat="server"></asp:TextBox>
                                        <imap:IMAPRequiredFieldValidator ValidationGroup="2" ControlToValidate="des_nomepublicacaoTextBox"
                                            ID="IMAPRequiredFieldValidator3" ErrorMessage="Informe o Nome da Publicação"
                                            runat="server" Display="None"></imap:IMAPRequiredFieldValidator><br />
                                        <asp:Label ID="des_nomearquivoLabel" CssClass="required" Text="Nome Arquivo" AssociatedControlID="des_nomearquivoTextBox"
                                            runat="server"></asp:Label>
                                        <asp:TextBox ID="des_nomearquivoTextBox" runat="server"></asp:TextBox>
                                        <imap:IMAPRequiredFieldValidator ValidationGroup="2" ControlToValidate="des_nomearquivoTextBox"
                                            ID="IMAPRequiredFieldValidator4" ErrorMessage="Informe o Nome do Arquivo" runat="server"
                                            Display="None"></imap:IMAPRequiredFieldValidator><br />
                                    </div>--%>
                                    <div class="flutuaEsq">
                                        <asp:Label ID="des_nomepublicacaoLabel" runat="server" CssClass="required" Text="Nome Publicação"
                                            AssociatedControlID="des_nomepublicacaoTextBox"></asp:Label>
                                        <asp:TextBox ID="des_nomepublicacaoTextBox" runat="server" TextMode="MultiLine" MaxLength="2147483647"
                                            EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="flutuaEsq">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="TipoContasPublicasLabel" runat="server" Text="Tipo" AssociatedControlID="TipoContasPublicasDropDownList"
                                                    CssClass="required"></asp:Label>
                                                <asp:DropDownList ID="TipoContasPublicasDropDownList" AutoPostBack="true" DataTextField="des_tipo_contas_publicas_tcp"
                                                    DataValueField="cod_tipo_contas_publicas_tcp" runat="server" OnSelectedIndexChanged="TipoContasPublicasDropDownList_SelectedIndexChanged"
                                                    AppendDataBoundItems="true" DataSourceID="TipoContasPublicasObjectDataSource">
                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                </asp:DropDownList>
                                                <imap:IMAPRequiredFieldValidator ID="IMAPRequiredFieldValidator2" ValidationGroup="2"
                                                    ControlToValidate="TipoContasPublicasDropDownList" runat="server" InitialValue="0"
                                                    Display="None" ErrorMessage="Informe o Tipo"></imap:IMAPRequiredFieldValidator>
                                                <asp:ObjectDataSource ID="TipoContasPublicasObjectDataSource" runat="server" SelectMethod="Listar"
                                                    TypeName="DsAdmin.TipoContasPublicas"></asp:ObjectDataSource>
                                                <br />
                                                <asp:Label ID="SubtipoContasPublicasLabel" runat="server" Text="Sub-Tipo" AssociatedControlID="SubtipoContasPublicasDropDownList"
                                                    CssClass="required"></asp:Label>
                                                <asp:DropDownList ID="SubtipoContasPublicasDropDownList" EnableViewState="false"
                                                    ViewStateMode="Disabled" DataTextField="des_subtipo_contas_publicas_scp" DataValueField="cod_subtipo_contas_publicas_scp"
                                                    runat="server" AppendDataBoundItems="false">
                                                    <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                                                </asp:DropDownList>
                                                <imap:IMAPRequiredFieldValidator ID="IMAPRequiredFieldValidator1" InitialValue="0"
                                                    ValidationGroup="2" ControlToValidate="SubtipoContasPublicasDropDownList" runat="server"
                                                    Display="None" ErrorMessage="Informe o Sub-Tipo"></imap:IMAPRequiredFieldValidator>
                                                <asp:ObjectDataSource ID="SubtipoContasPublicasObjectDataSource" runat="server" SelectMethod="ListarPorFiltro"
                                                    OnSelecting="SubtipoContasPublicasObjectDataSource_Selecting" TypeName="DsAdmin.SubtipoContasPublicas">
                                                    <SelectParameters>
                                                        <asp:Parameter Name="cod_subtipo_contas_publicas_scp" Type="Int32" />
                                                        <asp:Parameter Name="des_subtipo_contas_publicas_scp" Type="String" />
                                                        <asp:Parameter Name="cod_tipo_contas_publicas_tcp" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                    <%--<div class="flutuaEsq">
                                        <asp:Label ID="num_ano_referenciaLabel" Text="Ano Referência" AssociatedControlID="num_ano_referenciaTextBox"
                                            runat="server"></asp:Label>
                                        <asp:TextBox ID="num_ano_referenciaTextBox" runat="server"></asp:TextBox>
                                        <br />--%>
                                    <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label1" Text="Arquivos" runat="server" CssClass="required" AssociatedControlID="ItemContasPublicasFileUpload"></asp:Label>
                                                <asp:AjaxFileUpload AllowedFileTypes="jpg,jpeg" OnUploadComplete="ItemContasPublicasFileUpload_UploadedComplete"
                                                    OnUploadCompleteAll="ItemContasPublicasFileUpload_UploadCompleteAll" MaximumNumberOfFiles="10"
                                                    ID="AjaxFileUpload1" runat="server"></asp:AjaxFileUpload>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    <%--<asp:AsyncFileUpload ID="ItemContasPublicasFileUpload" OnUploadedComplete="ItemContasPublicasFileUpload_UploadedComplete"
                                        runat="server" />--%>
                                    <%--</div>--%>
                                    <asp:ValidationSummary ID="ValidationSummary2" ShowMessageBox="true" ShowSummary="false"
                                        ValidationGroup="2" runat="server" />
                                    <br />
                                    <%--<ul class="buttonBar grad_rd_box">
                                        <li>--%>
                                    <asp:LinkButton ID="CadastrarLinkButton" runat="server" Visible="false" ValidationGroup="2"
                                        OnClick="CadastrarLinkButton_Click" CssClass="button_style icon_save shrink_animation"
                                        Text="Confirmar"></asp:LinkButton>
                                    <%--</li>
                                        <li class="div_right_grayLight"></li>
                                        <li>--%>
                                    <%--<asp:LinkButton ID="PesquisarLinkButton" runat="server" CssClass="button_style icon_search shrink_animation">Pesquisar</asp:LinkButton>--%>
                                    <%--</li>
                                        <li class="div_right_grayLight"></li>
                                        <li>--%>
                                    <asp:LinkButton ID="CancelarLinkButton" runat="server" OnClick="CancelarLinkButton_Click"
                                        CssClass="button_style icon_cancel shrink_animation">Cancelar</asp:LinkButton>
                                    <%--</li>
                                    </ul>--%>
                                </fieldset>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                    <ContentTemplate>
                                        <asp:GridView ID="ContasPublicasGridview" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" EnableViewState="False" ViewStateMode="Disabled"
                                            DataKeyNames="cod_publicacao" PageSize="10" CellSpacing="-1" DataSourceID="ContasPublicasObjectDataSource"
                                            GridLines="None" OnPreRender="grvPrincipal_PreRender" OnRowCommand="ContasPublicasGridview_RowCommand"
                                            CssClass="stylized">
                                            <Columns>
                                                <asp:BoundField DataField="des_nomearquivo" HeaderText="Arquivo" SortExpression="des_nomearquivo">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="des_nomepublicacao" HeaderText="Publicação" SortExpression="des_nomepublicacao">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="dat_publicacao" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}"
                                                    SortExpression="dat_publicacao">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ControlStyle Height="15px" Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="btnEditar"
                                                            CssClass="tb_icon_editDoc" ToolTip="Detalhar" CommandArgument='<%# Eval("cod_publicacao") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ControlStyle Height="15px" Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="False" CommandName="btnExcluir"
                                                            CssClass="tb_icon_delete" ToolTip="Excluir" OnClientClick="return confirm('Confirma exclusão?')"
                                                            CommandArgument='<%# Eval("cod_publicacao") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                Não foram encontrados registros.
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="ContasPublicasObjectDataSource" OnSelecting="ContasPublicasObjectDataSource_Selecting"
                                            runat="server" TypeName="DsAdmin.Contaspublicas" SelectMethod="ListarPorFiltro">
                                            <SelectParameters>
                                                <asp:Parameter Name="cod_publicacao" Type="Int32" />
                                                <asp:Parameter Name="cod_tipopublicacao" Type="Int32" />
                                                <asp:Parameter Name="cod_tiposubpublicacao" Type="Int32" />
                                                <asp:Parameter Name="des_nomepublicacao" Type="String" />
                                                <asp:Parameter Name="dat_publicacao" Type="DateTime" />
                                                <asp:Parameter Name="des_nomearquivo" Type="String" />
                                                <asp:Parameter Name="num_ano_referencia" Type="Int32" />
                                                <asp:Parameter Name="COD_CLASSE" Type="Int32" />
                                                <asp:Parameter Name="COD_SUBCLASSE" Type="Int32" />
                                                <asp:Parameter Name="DES_CERTIFICACAO" Type="String" />
                                                <asp:Parameter Name="DAT_CADASTRO" Type="DateTime" />
                                                <asp:Parameter Name="cod_cliente" Type="Int32" />
                                                <asp:Parameter Name="usu_codigo" Type="Int32" />
                                                <asp:Parameter Name="and_codigo" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

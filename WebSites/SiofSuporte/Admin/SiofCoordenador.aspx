<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    CodeFile="SiofCoordenador.aspx.cs" Inherits="Admin_SiofCoordenador" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Coordenador</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <fieldset>
                        <legend>Andamento das Demandas</legend>
                        <div class="flutuaEsq">
                            <asp:Label ID="txtDtIniLabel" runat="server" Text="Data Inicio" AssociatedControlID="txtDtIniTextBox"></asp:Label>
                            <asp:TextBox ID="txtDtIniTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><asp:CalendarExtender
                                ID="txtDtIniTextBox_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDtIniTextBox"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <br />
                            <asp:Label ID="txtDtFimLabel" runat="server" Text="Data Fim" AssociatedControlID="txtDtFimTextBox"></asp:Label>
                            <asp:TextBox ID="txtDtFimTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><asp:CalendarExtender
                                ID="txtDtFimTextBox_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDtFimTextBox"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <br />
                            <asp:Label ID="cboTipoLabel" runat="server" Text="Tipo" AssociatedControlID="cboTipoTextBox"></asp:Label>
                            <asp:DropDownList ID="cboTipoTextBox" runat="server" AppendDataBoundItems="true"
                                DataSourceID="cboTipoTextBoxObjectDataSource" DataTextField="DES_NOME" DataValueField="COD_TIPOCLIENTE"
                                EnableViewState="False" ViewStateMode="Disabled">
                                <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="cboTipoTextBoxObjectDataSource" SelectMethod="Listar" TypeName="DsAdmin.TipoCliente"
                                runat="server"></asp:ObjectDataSource>
                            <br />
                            <asp:Label ID="chkOutrosLabel" runat="server" Text="Outros" AssociatedControlID="chkOutrosTextBox"></asp:Label>
                            <asp:TextBox ID="chkOutrosTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="cboCidadeFiltroLabel" runat="server" Text="Cidade" AssociatedControlID="cboCidadeFiltroTextBox"></asp:Label>
                            <asp:TextBox ID="cboCidadeFiltroTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><br />
                            <asp:Label ID="codUsuarioLabel" runat="server" Text="Usuario" AssociatedControlID="codUsuarioTextBox"></asp:Label>
                            <asp:TextBox ID="codUsuarioTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><br />
                            <asp:Label ID="txtComprovanteLabel" runat="server" Text="Comprovante" AssociatedControlID="txtComprovanteTextBox"></asp:Label>
                            <asp:TextBox ID="txtComprovanteTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <asp:Label ID="NIVELRELPUBLICACOESLabel" runat="server" Text="Nivel Publicação" AssociatedControlID="NIVELRELPUBLICACOESTextBox"></asp:Label>
                            <asp:TextBox ID="NIVELRELPUBLICACOESTextBox" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled"></asp:TextBox><br />
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="cboStatusLabel" runat="server" Text="Status" AssociatedControlID="cboStatusTextBox"></asp:Label>
                            <asp:DropDownList ID="cboStatusTextBox" runat="server" DataSourceID="cboStatusTextBoxObjectDataSource"
                                DataTextField="des_status_publicacao_stp" AppendDataBoundItems="true" DataValueField="cod_status_publicacao_stp"
                                EnableViewState="False" ViewStateMode="Disabled">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="cboStatusTextBoxObjectDataSource" runat="server" SelectMethod="Listar"
                                TypeName="DsAdmin.StatusPublicacao"></asp:ObjectDataSource>
                            <br />
                            <asp:Label ID="cboCidadeClienteLabel" runat="server" Text="Cliente" AssociatedControlID="cboCidadeClienteTextBox"></asp:Label>
                            <asp:TextBox ID="cboCidadeClienteTextBox" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled"></asp:TextBox><br />
                            <asp:Label ID="cboTipoDocumentoLabel" runat="server" Text="Tipo Documento" AssociatedControlID="cboTipoDocumentoTextBox"></asp:Label>
                            <asp:TextBox ID="cboTipoDocumentoTextBox" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled"></asp:TextBox>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="cboUsuarioLabel" runat="server" Text="Usuario" AssociatedControlID="cboUsuarioTextBox"></asp:Label>
                            <asp:DropDownList ID="cboUsuarioTextBox" DataTextField="usu_nome" AppendDataBoundItems="true"
                                DataSourceID="cboUsuarioTextBoxObjectDataSource" DataValueField="usu_codigo"
                                runat="server" EnableViewState="False" ViewStateMode="Disabled">
                                <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="cboUsuarioTextBoxObjectDataSource" SelectMethod="ListarUsuarioPorSistema"
                                TypeName="DsAdmin.Usuario" runat="server">
                                <SelectParameters>
                                    <asp:Parameter Name="sis_codigo" DefaultValue="7" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <br />
                            <asp:Label ID="cboOperadorLabel" runat="server" Text="Operador" AssociatedControlID="cboOperadorTextBox"></asp:Label>
                            <asp:TextBox ID="cboOperadorTextBox" runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><br />
                            <asp:Label ID="cboUfLabel" runat="server" Text="UF" AssociatedControlID="cboUfTextBox"></asp:Label>
                            <asp:DropDownList ID="cboUfTextBox" AppendDataBoundItems="true" DataSourceID="cboUfTextBoxObjectDataSource"
                                DataTextField="DES_UF" DataValueField="DES_UF" runat="server" EnableViewState="False"
                                ViewStateMode="Disabled">
                                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="cboUfTextBoxObjectDataSource" SelectMethod="Listar" TypeName="DsAdmin.Uf"
                                runat="server"></asp:ObjectDataSource>
                        </div>
                    </fieldset>
                    <ul class="buttonBar grad_rd_box">
                        <li>
                            <asp:LinkButton ID="EnviarRevisaoLinkButton" runat="server" CssClass="button_style icon_search shrink_animation">Pesquisar</asp:LinkButton>
                        </li>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <ul class="legend_block">
            <li><a href="javascript:void(0)" class="tb_doc shrink_animation icon_legend " title="Docs. após 15:30">
                Docs. após 15:30</a></li>
            <li><a href="javascript:void(0)" class="tb_newDoc shrink_animation icon_legend" title="Lics. após 17:30">
                Lics. após 17:30</a></li>
            <li><a href="javascript:void(0)" class="tb_doc shrink_animation icon_legend " title="Licitações">
                Licitações</a></li>
            <li><a href="javascript:void(0)" class="tb_newDoc shrink_animation icon_legend" title="Documentos">
                Documentos</a></li>
            <li><a href="javascript:void(0)" class="tb_doc shrink_animation icon_legend " title="Outros veículos">
                Outros veículos</a></li>
            <li><a href="javascript:void(0)" class="tb_newDoc shrink_animation icon_legend" title="Erro de data">
                Erro de data</a></li>
            <li><a href="javascript:void(0)" class="tb_newDoc shrink_animation icon_legend" title="Ub. Sas">
                Ub. Sas</a></li>
        </ul>
        <div id="second_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" EnableViewState="False" PageSize="25" CellSpacing="-1"
                        DataSourceID="CoordenadorObjectDataSource" GridLines="None" OnRowCommand="grvPrincipal_RowCommand"
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
                            Nenhum resultado encontrado!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="CoordenadorObjectDataSource" runat="server" SelectMethod="ListarDemandaPorFiltro"
                        TypeName="DsAdmin.Coordenador">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtDtIniTextBox" Name="txtDtIni" PropertyName="Text"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="txtDtFimTextBox" Name="txtDtFim" PropertyName="Text"
                                Type="DateTime" />
                            <asp:ControlParameter ControlID="codUsuarioTextBox" Name="codUsuario" PropertyName="Text"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="chkOutrosTextBox" Name="chkOutros" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="cboCidadeFiltroTextBox" Name="cboCidadeFiltro" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="cboStatusTextBox" Name="cboStatus" PropertyName="Text"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="cboCidadeClienteTextBox" Name="cboCidadeCliente"
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="cboTipoTextBox" Name="cboTipo" PropertyName="Text"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="txtComprovanteTextBox" Name="txtComprovante" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="cboTipoDocumentoTextBox" Name="cboTipoDocumento"
                                PropertyName="Text" Type="Int32" />
                            <asp:ControlParameter ControlID="cboUsuarioTextBox" Name="cboUsuario" PropertyName="Text"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="cboOperadorTextBox" Name="cboOperador" PropertyName="Text"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="cboUfTextBox" Name="cboUf" PropertyName="Text" Type="String" />
                            <asp:ControlParameter ControlID="NIVELRELPUBLICACOESTextBox" Name="NIVELRELPUBLICACOES"
                                PropertyName="Text" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="SiofEditoracao.aspx.cs"
    Inherits="Admin_SiofEditoracao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="imap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Editoração</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="main_content">
                <div id="first_content" class="formStylized">
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
                        </div>
                        <br />
                        <div class="flutuaEsq">
                            <asp:Label runat="server" ID="DES_CIDADELabel" Text="Nome" AssociatedControlID="DES_CIDADETextBox"></asp:Label>
                            <asp:TextBox runat="server" ID="DES_CIDADETextBox" CssClass="grid_4"></asp:TextBox>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="DES_TIPOCLIENTELabel" AssociatedControlID="DES_TIPOCLIENTEDropDownList"
                                runat="server" Text="Tipo"></asp:Label>
                            <asp:DropDownList ID="DES_TIPOCLIENTEDropDownList" runat="server" DataTextField="DES_TIPOCLIENTE"
                                DataValueField="COD_TIPOCLIENTE" AppendDataBoundItems="true" DataSourceID="DES_TIPOCLIENTEDropDownListObjectDataSource">
                                <asp:ListItem Value="0" Text="Selecione"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="DES_TIPOCLIENTEDropDownListObjectDataSource" TypeName="DsAdmin.TipoCliente"
                                SelectMethod="Listar" runat="server"></asp:ObjectDataSource>
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="DES_UFLabel" AssociatedControlID="DES_UFDropDownList" runat="server"
                                Text="UF"></asp:Label>
                            <asp:DropDownList ID="DES_UFDropDownList" runat="server" AppendDataBoundItems="true"
                                DataTextField="DES_UF" DataValueField="DES_UF" DataSourceID="DES_UFDropDownListObjectDataSource">
                                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="DES_UFDropDownListObjectDataSource" TypeName="DsAdmin.Uf"
                                SelectMethod="Listar" runat="server"></asp:ObjectDataSource>
                        </div>
                        <br />
                        <hr />
                        <ul class="buttonBar grad_rd_box">
                            <li>
                                <asp:LinkButton ID="PesquisarLinkButton" runat="server" CssClass="button_style icon_search shrink_animation">Pesquisar</asp:LinkButton>
                            </li>
                            <li class="div_right_grayLight"></li>
                            <li>
                                <asp:LinkButton ID="CancelarLinkButton" runat="server" CssClass="button_style icon_cancelDoc shrink_animation"
                                    OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton>
                            </li>
                            <li class="div_right_grayLight"></li>
                            <li>
                                <asp:LinkButton ID="GerarLinkButton" runat="server" ValidationGroup="1" CssClass="button_style icon_publish shrink_animation"
                                    OnClick="GerarLinkButton_Click">Gerar</asp:LinkButton>
                            </li>
                        </ul>
                    </fieldset>
                </div>
                <div id="second_content" class="formStylized">
                    <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" EnableViewState="False" PageSize="25" CellSpacing="-1"
                        DataSourceID="grvPrincipalObjectDataSource" GridLines="None" OnRowCommand="grvPrincipal_RowCommand"
                        OnPreRender="grvPrincipal_PreRender" CssClass="stylized">
                        <Columns>
                            <asp:BoundField DataField="DES_TIPOCLIENTE" HeaderText="Tipo Cliente" SortExpression="DES_TIPOCLIENTE">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_CIDADE" HeaderText="Cliente" SortExpression="DES_CIDADE">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_DESCRICAO" HeaderText="Descrição" SortExpression="DES_DESCRICAO">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DES_UF" HeaderText="UF" SortExpression="DES_UF">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="true" HeaderText=".:">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSelecionar" runat="server" CausesValidation="False" CommandName="btnSelecionar"
                                        CssClass="tb_icon_regular shrink_animation" CommandArgument='<%# Eval("COD_CLIENTE") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="grvPrincipalObjectDataSource" runat="server" SelectMethod="ListarPorFiltro"
                        TypeName="DsAdmin.Cliente">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DES_CIDADETextBox" Name="DES_CIDADE" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="DES_UFDropDownList" Name="DES_UF" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:Parameter Name="TIP_ATIVO" Type="String" DefaultValue="S" />
                            <asp:Parameter Name="DES_ENDERECO" Type="String" />
                            <asp:Parameter Name="DES_BAIRRO" Type="String" />
                            <asp:Parameter Name="DES_CEP" Type="String" />
                            <asp:Parameter Name="DES_TELEFONE" Type="String" />
                            <asp:Parameter Name="DES_EMAIL" Type="String" />
                            <asp:Parameter Name="DES_CONTATO" Type="String" />
                            <asp:Parameter Name="DES_AUTORIDADE" Type="String" />
                            <asp:ControlParameter ControlID="DES_TIPOCLIENTEDropDownList" Name="COD_TIPOCLIENTE"
                                PropertyName="SelectedValue" Type="Int32" />
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
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GerarLinkButton" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

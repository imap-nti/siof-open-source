<%@ Page Title="ModeloEdicao" Language="C#" MasterPageFile="~/Admin/MasterPage.master"
    AutoEventWireup="false" EnableViewState="true" ViewStateEncryptionMode="Never"
    EnableTheming="false" CodeFile="SEPModeloEdicao.aspx.cs" Inherits="admin_SEPModeloEdicao" %>

<%@ Register Assembly="ComponentesWEB" Namespace="Validation" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Sistema de criação de modelos - ModeloEdicao</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="main_content">
        <div id="first_content" class="formStylized">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>Modelo Edição</legend>
                        <div class="flutuaEsq">
                            <asp:Label ID="des_modelo_edicao_moeLabel" CssClass="required" runat="server" Text="Nome Modelo"
                                AssociatedControlID="des_modelo_edicao_moeTextBox"></asp:Label><asp:TextBox ID="des_modelo_edicao_moeTextBox"
                                    runat="server" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox>
                            <cc1:IMAPRequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="des_modelo_edicao_moeTextBox"
                                ErrorMessage="Informe o Modelo!" ValidationGroup="2" Display="None"></cc1:IMAPRequiredFieldValidator>
                            <asp:Label ID="ArquivoImagemCapaLabel" runat="server" Text="Imagem Capa" CssClass="required"
                                AssociatedControlID="ArquivoImagemCapa"></asp:Label><asp:AsyncFileUpload ID="ArquivoImagemCapa"
                                    runat="server" EnableViewState="False" OnUploadedComplete="ArquivoImagemCapa_UploadedComplete"
                                    ViewStateMode="Disabled" />
                        </div>
                        <div class="flutuaEsq">
                            <asp:Label ID="ArquivoModeloEdicaoLabel" runat="server" CssClass="required" Text="Modelo Edição"
                                AssociatedControlID="ArquivoModeloEdicao"></asp:Label><asp:AsyncFileUpload ID="ArquivoModeloEdicao"
                                    runat="server" EnableViewState="False" OnUploadedComplete="ArquivoModeloEdicao_UploadedComplete"
                                    ViewStateMode="Disabled" />
                        </div>
                    </fieldset>
                    <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="2" ShowMessageBox="true"
                        ShowSummary="false" runat="server" />
                    <asp:LinkButton ID="CadastrarLinkButton" runat="server" CssClass="button_style icon_save shrink_animation"
                        ValidationGroup="2" OnClick="CadastrarLinkButton_Click">Cadastrar</asp:LinkButton>
                    <asp:LinkButton ID="PesquisarLinkButton" runat="server" CssClass="button_style icon_search shrink_animation">Pesquisar</asp:LinkButton></li>
                    <asp:LinkButton ID="CancelarLinkButton" runat="server" CssClass="button_style icon_cancel shrink_animation"
                        OnClick="CancelarLinkButton_Click">Cancelar</asp:LinkButton></li>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="second_content">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grvPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="cod_modelo_edicao_moe" DataSourceID="GridViewObjectDataSource"
                        EnableViewState="False" PageSize="30" CellSpacing="-1" GridLines="None" OnPreRender="grvPrincipal_PreRender"
                        OnRowCommand="grvPrincipal_RowCommand" CssClass="stylized">
                        <Columns>
                            <asp:TemplateField ShowHeader="true" HeaderText="Preview">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <img src="DownloadHandler.ashx?q=<%# Eval("cod_modelo_edicao_moe") %>" alt="Capa de Modelo">
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="dat_criacao_modelo_moe" HeaderText="Data Criação" DataFormatString="{0:dd/MM/yyyy}"
                                SortExpression="dat_criacao_modelo_moe">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="des_modelo_edicao_moe" HeaderText="Descrição Modelo" SortExpression="des_modelo_edicao_moe">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="usu_nome" HeaderText="Usuario Criador" SortExpression="usu_nome">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnIncluirCaderno" runat="server" CausesValidation="False" CommandName="btnIncluirCaderno"
                                        CssClass="tb_icon_releasePaper shrink_animation" ToolTip="Incluir Caderno" CommandArgument='<%# Eval("cod_modelo_edicao_moe") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="btnGerarEdicao" runat="server" CausesValidation="False" CommandName="btnGerarEdicao"
                                                CssClass="tb_icon_pdf shrink_animation" ToolTip="Visualizar Edição" CommandArgument='<%# Eval("cod_modelo_edicao_moe") %>' />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnGerarEdicao" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnIncluirCampos" runat="server" CausesValidation="False" CommandName="btnIncluirCampos"
                                        CssClass="tb_icon_newDoc shrink_animation" ToolTip="Incluir Campos" CommandArgument='<%# Eval("cod_modelo_edicao_moe") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ControlStyle Height="15px" Width="15px" />
                                <ItemStyle Width="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="False" CommandName="btnExcluir"
                                        CssClass="tb_icon_delete shrink_animation" OnClientClick="return confirm('Confirma exclusão?')"
                                        ToolTip="Excluir" CommandArgument='<%# Eval("cod_modelo_edicao_moe") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            Nenhum registro foi encontrado!
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="GridViewObjectDataSource" runat="server" DeleteMethod="Excluir"
                        SelectMethod="ListarPorFiltro" TypeName="DsAdmin.ModeloEdicao" EnableViewState="False">
                        <SelectParameters>
                            <asp:Parameter Name="cod_modelo_edicao_moe" Type="Int32" />
                            <asp:Parameter Name="dat_criacao_modelo_moe" Type="DateTime" />
                            <asp:Parameter Name="cod_usuario_usu" Type="Int32" />
                            <asp:Parameter Name="dat_exclusao_modelo_moe" Type="DateTime" />
                            <asp:ControlParameter ControlID="des_modelo_edicao_moeTextBox" PropertyName="Text"
                                Name="des_modelo_edicao_moe" Type="String" />
                            <asp:Parameter Name="cod_usuario_exclusao_usu" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <!--Modal Campos Modelo -->
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="ModalCamposModeloPlaceHolder" runat="server" Visible="false">
                                <div id="Div4" class="lightbox">
                                    <div class="screen_editDescSite windowFloat">
                                        <div id="Div1" class="formStylized">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <fieldset>
                                                        <legend>Campos do Modelo</legend>
                                                        <asp:LinkButton ID="FecharModalCamposModeloLinkButton" runat="server" CssClass="close_window flutuaEsq shrink_animation"
                                                            OnClick="FecharModalCamposModeloLinkButton_Click">
                                                        </asp:LinkButton>
                                                        <div class="flutuaEsq">
                                                            <asp:Label ID="TipoCampoLabel" runat="server" CssClass="required" Text="Tipo Campo"
                                                                AssociatedControlID="TipoCampoRadioButtonList"></asp:Label><asp:RadioButtonList AutoPostBack="true"
                                                                    OnTextChanged="TipoCampoRadioButtonList_TextChanged" ID="TipoCampoRadioButtonList"
                                                                    runat="server" EnableViewState="False">
                                                                    <asp:ListItem Value="1">Texto</asp:ListItem>
                                                                    <asp:ListItem Value="2">Imagem</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            <cc1:IMAPRequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TipoCampoRadioButtonList"
                                                                ErrorMessage="Informe o Tipo do Campo!" InitialValue="0" ValidationGroup="1"
                                                                Display="None"></cc1:IMAPRequiredFieldValidator>
                                                            <asp:Label ID="des_nome_camLabel" runat="server" CssClass="required" Text="Nome Campo"
                                                                AssociatedControlID="des_nome_camTextBox"></asp:Label>
                                                            <asp:TextBox ID="des_nome_camTextBox" runat="server" MaxLength="15" EnableViewState="False"
                                                                ViewStateMode="Disabled"></asp:TextBox><cc1:IMAPRequiredFieldValidator ID="IMAPRequiredFieldValidator1"
                                                                    runat="server" ControlToValidate="des_nome_camTextBox" ErrorMessage="Informe o Nome do Campo!"
                                                                    ValidationGroup="1" Display="None"></cc1:IMAPRequiredFieldValidator><br />
                                                            <asp:Label ID="des_regra_camLabel" runat="server" CssClass="required" Text="Regra"
                                                                AssociatedControlID="des_regra_camTextBox"></asp:Label>
                                                            <asp:TextBox ID="des_regra_camTextBox" runat="server" MaxLength="600" EnableViewState="False"
                                                                ViewStateMode="Disabled"></asp:TextBox><cc1:IMAPRequiredFieldValidator ID="IMAPRequiredFieldValidator2"
                                                                    runat="server" ControlToValidate="des_regra_camTextBox" ErrorMessage="Informe a Regra!"
                                                                    ValidationGroup="1" Display="None"></cc1:IMAPRequiredFieldValidator><br />
                                                            <asp:Label ID="num_height_camLabel" CssClass="required" runat="server" Text="Altura"
                                                                AssociatedControlID="num_height_camTextBox"></asp:Label><asp:TextBox ID="num_height_camTextBox"
                                                                    runat="server" MaxLength="9" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><%--<asp:MaskedEditExtender
                                                            ID="num_height_camTextBox_MaskedEditExtender" runat="server" Enabled="True" Mask="999999999"
                                                            PromptCharacter="" TargetControlID="num_height_camTextBox">
                                                        </asp:MaskedEditExtender>--%>
                                                            <cc1:IMAPRequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="num_height_camTextBox"
                                                                ErrorMessage="Informe a Altura!" ValidationGroup="1" Display="None"></cc1:IMAPRequiredFieldValidator><br />
                                                        </div>
                                                        <div class="flutuaEsq">
                                                            <asp:Label ID="cod_pagina_modelo_pamLabel" Text="Pagina Modelo" runat="server" AssociatedControlID="cod_pagina_modelo_pamRadioButtonList"
                                                                CssClass="required"></asp:Label>
                                                            <asp:RadioButtonList runat="server" DataTextField="des_pagina_modelo_pam" DataValueField="cod_pagina_modelo_pam"
                                                                ID="cod_pagina_modelo_pamRadioButtonList" DataSourceID="PaginaModeloObjectDataSource">
                                                            </asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ValidationGroup="1" ControlToValidate="cod_pagina_modelo_pamRadioButtonList"
                                                                ID="cod_pagina_modelo_pamRadioButtonListRequiredFieldValidator" Display="None"
                                                                ErrorMessage="Informe o Modelo!" runat="server"></asp:RequiredFieldValidator>
                                                            <asp:ObjectDataSource ID="PaginaModeloObjectDataSource" SelectMethod="Listar" runat="server"
                                                                TypeName="DsAdmin.PaginaModelo"></asp:ObjectDataSource>
                                                            <asp:Label ID="num_width_camLabel" runat="server" Text="Largura" CssClass="required"
                                                                AssociatedControlID="num_width_camTextBox"></asp:Label><asp:TextBox ID="num_width_camTextBox"
                                                                    runat="server" MaxLength="9" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><%--<asp:MaskedEditExtender
                                                            ID="num_width_camTextBox_MaskedEditExtender" runat="server" Enabled="True" Mask="999999999"
                                                            PromptCharacter="" TargetControlID="num_width_camTextBox">
                                                        </asp:MaskedEditExtender>--%>
                                                            <cc1:IMAPRequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="num_width_camTextBox"
                                                                ErrorMessage="Informe a Largura!" ValidationGroup="1" Display="None"></cc1:IMAPRequiredFieldValidator><br />
                                                            <asp:Label ID="num_top_camLabel" runat="server" CssClass="required" Text="Margem Topo"
                                                                AssociatedControlID="num_top_camTextBox"></asp:Label><asp:TextBox ID="num_top_camTextBox"
                                                                    runat="server" MaxLength="9" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><%--<asp:MaskedEditExtender ID="num_top_camTextBox_MaskedEditExtender"
                                                        runat="server" Enabled="True" Mask="999999999" PromptCharacter="" TargetControlID="num_top_camTextBox">
                                                    </asp:MaskedEditExtender>--%>
                                                            <cc1:IMAPRequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="num_top_camTextBox"
                                                                ErrorMessage="Informe o Topo!" ValidationGroup="1" Display="None"></cc1:IMAPRequiredFieldValidator><br />
                                                            <asp:Label ID="num_left_camLabel" runat="server" Text="Margem Esquerda" CssClass="required"
                                                                AssociatedControlID="num_left_camTextBox"></asp:Label><asp:TextBox ID="num_left_camTextBox"
                                                                    runat="server" MaxLength="9" EnableViewState="False" ViewStateMode="Disabled"></asp:TextBox><%--<asp:MaskedEditExtender
                                                            ID="num_left_camTextBox_MaskedEditExtender" runat="server" Enabled="True" Mask="999999999"
                                                            PromptCharacter="" TargetControlID="num_left_camTextBox">
                                                        </asp:MaskedEditExtender>--%>
                                                            <cc1:IMAPRequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="num_left_camTextBox"
                                                                ErrorMessage="Informe a Esquerda!" ValidationGroup="1" Display="None"></cc1:IMAPRequiredFieldValidator><br />
                                                        </div>
                                                        <div class="flutuaEsq">
                                                            <asp:PlaceHolder ID="ImagemPlaceHolder" runat="server" Visible="false">
                                                                <asp:Label ID="arq_imagem_camLabel" runat="server" Text="Imagem" AssociatedControlID="arq_imagem_camAsyncFileUpload"></asp:Label>
                                                                <asp:AsyncFileUpload ID="arq_imagem_camAsyncFileUpload" runat="server" OnUploadedComplete="arq_imagem_camAsyncFileUpload_UploadedComplete"
                                                                    EnableViewState="False" ViewStateMode="Disabled" />
                                                                <br />
                                                            </asp:PlaceHolder>
                                                        </div>
                                                        <%--<br />--%>
                                                        <asp:PlaceHolder ID="TextoPlaceholder" runat="server" Visible="false">
                                                            <div class="flutuaEsq">
                                                                <%--<br />--%>
                                                                <asp:Label ID="cod_fonte_modelo_fomLabel" runat="server" Text="Fonte do Modelo" AssociatedControlID="cod_fonte_modelo_fomDropDownList"></asp:Label><asp:DropDownList
                                                                    ID="cod_fonte_modelo_fomDropDownList" runat="server" DataSourceID="cod_fonte_modelo_fomObjectDataSource"
                                                                    DataTextField="des_fonte_modelo_fom" DataValueField="cod_fonte_modelo_fom" EnableViewState="False"
                                                                    AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">Selecione</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="cod_fonte_modelo_fomObjectDataSource" runat="server" SelectMethod="Listar"
                                                                    TypeName="DsAdmin.FonteModelo"></asp:ObjectDataSource>
                                                                <br />
                                                                <asp:Label ID="num_tamanho_fonte_camLabel" runat="server" Text="Tamanho Fonte" AssociatedControlID="num_tamanho_fonte_camTextBox"></asp:Label><asp:TextBox
                                                                    ID="num_tamanho_fonte_camTextBox" runat="server" MaxLength="9" EnableViewState="False"
                                                                    ViewStateMode="Disabled"></asp:TextBox><%--<asp:MaskedEditExtender ID="num_tamanho_fonte_camTextBox_MaskedEditExtender"
                                                        runat="server" Enabled="True" Mask="999999999" PromptCharacter="" TargetControlID="num_tamanho_fonte_camTextBox">
                                                    </asp:MaskedEditExtender>--%><br />
                                                                <asp:Label ID="tip_alinhamento_fonte_camLabel" runat="server" Text="Alinhamento"
                                                                    AssociatedControlID="tip_alinhamento_fonte_camRadioButtonList"></asp:Label><asp:RadioButtonList
                                                                        ID="tip_alinhamento_fonte_camRadioButtonList" runat="server" EnableViewState="False"
                                                                        ViewStateMode="Disabled">
                                                                        <asp:ListItem Value="E">Alinhar à Esquerda</asp:ListItem>
                                                                        <asp:ListItem Value="D">Alinhar à Direita</asp:ListItem>
                                                                        <asp:ListItem Value="C">Centralizar</asp:ListItem>
                                                                        <asp:ListItem Value="J">Justificar</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                            </div>
                                                            <div class="flutuaEsq">
                                                                <asp:Label ID="tip_negrito_fonte_camLabel" runat="server" Text="Negrito" AssociatedControlID="tip_negrito_fonte_camRadioButtonList"></asp:Label><asp:RadioButtonList
                                                                    ID="tip_negrito_fonte_camRadioButtonList" runat="server" EnableViewState="False"
                                                                    ViewStateMode="Disabled">
                                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <%--<br />--%>
                                                                <asp:Label ID="tip_italico_fonte_camLabel" runat="server" Text="Itálico" AssociatedControlID="tip_italico_fonte_camRadioButtonList"></asp:Label><asp:RadioButtonList
                                                                    ID="tip_italico_fonte_camRadioButtonList" runat="server" EnableViewState="False"
                                                                    ViewStateMode="Disabled">
                                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <%--<br />--%>
                                                                <asp:Label ID="tip_sublinhado_fonte_camLabel" runat="server" Text="Sublinhado" AssociatedControlID="tip_sublinhado_fonte_camRadioButtonList"></asp:Label><asp:RadioButtonList
                                                                    ID="tip_sublinhado_fonte_camRadioButtonList" runat="server" EnableViewState="False"
                                                                    ViewStateMode="Disabled">
                                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </asp:PlaceHolder>
                                                    </fieldset>
                                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="1" ShowMessageBox="true"
                                                        ShowSummary="false" runat="server" />
                                                    <asp:LinkButton ID="CadastrarModalLinkButton" runat="server" CssClass="button_style icon_save shrink_animation"
                                                        ValidationGroup="1" OnClick="CadastrarModalLinkButton_Click">Cadastrar</asp:LinkButton>
                                                    <asp:LinkButton ID="PesquisarModalLinkButton" runat="server" CssClass="button_style icon_search shrink_animation">Pesquisar</asp:LinkButton>
                                                    <asp:LinkButton ID="CancelarModalLinkButton" runat="server" CssClass="button_style icon_cancel shrink_animation"
                                                        OnClick="CancelarModalLinkButton_Click">Cancelar</asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div id="Div2">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grvModalPrincipal" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" DataKeyNames="cod_campo_modelo_cam" DataSourceID="grvModalPrincipalObjectDataSource"
                                                        EnableViewState="False" PageSize="30" CellSpacing="-1" GridLines="None" OnPreRender="grvPrincipal_PreRender"
                                                        OnRowCommand="grvModalPrincipal_RowCommand" CssClass="stylized">
                                                        <Columns>
                                                            <asp:BoundField DataField="des_nome_cam" HeaderText="Nome" SortExpression="des_nome_cam">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="des_regra_cam" HeaderText="Regra" SortExpression="des_regra_cam">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="des_pagina_modelo_pam" HeaderText="Página Modelo" SortExpression="des_pagina_modelo_pam">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ControlStyle Height="15px" Width="15px" />
                                                                <ItemStyle Width="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="btnEditar"
                                                                        CssClass="tb_icon_editDoc shrink_animation" ToolTip="Editar" CommandArgument='<%# Eval("cod_campo_modelo_cam") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ControlStyle Height="15px" Width="15px" />
                                                                <ItemStyle Width="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="False" CommandName="btnExcluir"
                                                                        CssClass="tb_icon_delete shrink_animation" OnClientClick="return confirm('Confirma exclusão?')"
                                                                        ToolTip="Excluir" CommandArgument='<%# Eval("cod_campo_modelo_cam") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            Nenhum registro foi encontrado!
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:ObjectDataSource ID="grvModalPrincipalObjectDataSource" runat="server" DeleteMethod="Excluir"
                                                SelectMethod="ListarPorFiltro" TypeName="DsAdmin.CamposModelo" OnSelecting="grvModalPrincipalObjectDataSource_Selecting"
                                                EnableViewState="False">
                                                <SelectParameters>
                                                    <asp:Parameter Name="cod_campo_modelo_cam" Type="Int32" />
                                                    <asp:ControlParameter ControlID="des_nome_camTextBox" DefaultValue="" Name="des_nome_cam"
                                                        PropertyName="Text" Type="String" />
                                                    <asp:Parameter Name="des_regra_cam" Type="String" />
                                                    <asp:Parameter Name="num_height_cam" Type="Int32" />
                                                    <asp:Parameter Name="num_width_cam" Type="Int32" />
                                                    <asp:Parameter Name="num_top_cam" Type="Int32" />
                                                    <asp:Parameter Name="num_left_cam" Type="Int32" />
                                                    <asp:Parameter Name="cod_modelo_edicao_moe" Type="Int32" />
                                                    <asp:ControlParameter ControlID="cod_fonte_modelo_fomDropDownList" DefaultValue="0"
                                                        Name="cod_fonte_modelo_fom" PropertyName="SelectedValue" Type="Int32" />
                                                    <asp:ControlParameter ControlID="num_tamanho_fonte_camTextBox" DefaultValue="" Name="num_tamanho_fonte_cam"
                                                        PropertyName="Text" Type="Int32" />
                                                    <asp:ControlParameter ControlID="tip_alinhamento_fonte_camRadioButtonList" DefaultValue=""
                                                        Name="tip_alinhamento_fonte_cam" PropertyName="SelectedValue" Type="String" />
                                                    <asp:ControlParameter ControlID="tip_negrito_fonte_camRadioButtonList" DefaultValue=""
                                                        Name="tip_negrito_fonte_cam" PropertyName="SelectedValue" Type="String" />
                                                    <asp:ControlParameter ControlID="tip_sublinhado_fonte_camRadioButtonList" DefaultValue=""
                                                        Name="tip_sublinhado_fonte_cam" PropertyName="SelectedValue" Type="String" />
                                                    <asp:ControlParameter ControlID="tip_italico_fonte_camRadioButtonList" DefaultValue=""
                                                        Name="tip_italico_fonte_cam" PropertyName="SelectedValue" Type="String" />
                                                    <asp:Parameter DefaultValue="" Name="arq_imagem_cam" Type="String" />
                                                    <asp:Parameter Name="cod_pagina_modelo_pam" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </div>
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--Modal Caderno Modelo -->
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:PlaceHolder ID="ModalCadernoModeloPlaceHolderPlaceHolder" runat="server" Visible="false">
                                <div id="Div3" class="lightbox">
                                    <div class="screen_editDescSite windowFloat">
                                        <div id="Div5" class="formStylized">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <fieldset>
                                                        <legend>Caderno do Modelo</legend>
                                                        <asp:LinkButton ID="FecharModalCadernoModeloLinkButton" runat="server" CssClass="close_window flutuaEsq shrink_animation"
                                                            OnClick="FecharModalCadernoModeloLinkButton_Click"></asp:LinkButton>
                                                        <div class="flutuaEsq">
                                                            <asp:Label ID="des_caderno_modelo_camLabel" CssClass="required" runat="server" Text="Caderno Modelo"
                                                                AssociatedControlID="des_caderno_modelo_camTextBox"></asp:Label>
                                                            <asp:TextBox runat="server" ID="des_caderno_modelo_camTextBox"></asp:TextBox><asp:RequiredFieldValidator
                                                                runat="server" ValidationGroup="9" ControlToValidate="des_caderno_modelo_camTextBox"
                                                                Display="None" ErrorMessage="Informe o Caderno Modelo!"></asp:RequiredFieldValidator>
                                                            <br />
                                                        </div>
                                                        <div class="flutuaEsq">
                                                            <asp:Label ID="num_ordem_camLabel" runat="server" Text="Ordem" AssociatedControlID="num_ordem_camTextBox"></asp:Label>
                                                            <asp:TextBox runat="server" ID="num_ordem_camTextBox"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                                runat="server" ValidationGroup="9" ControlToValidate="des_caderno_modelo_camTextBox"
                                                                Display="None" ErrorMessage="Informe o Caderno Modelo!"></asp:RequiredFieldValidator>--%>
                                                            <br />
                                                        </div>
                                                    </fieldset>
                                                    <asp:ValidationSummary ID="ValidationSummary3" ValidationGroup="9" ShowMessageBox="true"
                                                        ShowSummary="false" runat="server" />
                                                    <asp:LinkButton ID="CadastrarCadernoModeloLinkButton" runat="server" CssClass="button_style icon_save shrink_animation"
                                                        ValidationGroup="9" OnClick="CadastrarCadernoModeloLinkButton_Click">Cadastrar</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="button_style icon_search shrink_animation">Pesquisar</asp:LinkButton>
                                                    <asp:LinkButton ID="CancelarCadernoModeloLinkButton" runat="server" CssClass="button_style icon_cancel shrink_animation"
                                                        OnClick="CancelarCadernoModeloLinkButton_Click">Cancelar</asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grvCadernoModelo" runat="server" AllowPaging="True" AllowSorting="True"
                                                        AutoGenerateColumns="False" DataKeyNames="cod_caderno_modelo_cam" DataSourceID="grvCadernoModeloObjectDataSource"
                                                        EnableViewState="False" PageSize="30" CellSpacing="-1" GridLines="None" OnPreRender="grvPrincipal_PreRender"
                                                        OnRowCommand="grvCadernoModelo_RowCommand" CssClass="stylized">
                                                        <Columns>
                                                            <asp:BoundField DataField="des_caderno_modelo_cam" HeaderText="Caderno Modelo" SortExpression="des_caderno_modelo_cam">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="num_ordem_cam" HeaderText="Ordem" SortExpression="num_ordem_cam">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <%--<asp:BoundField DataField="cod_modelo_edicao_moe" HeaderText="Modelo Edicao" SortExpression="cod_modelo_edicao_moe">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>--%>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ControlStyle Height="15px" Width="15px" />
                                                                <ItemStyle Width="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="False" CommandName="btnExcluir"
                                                                        CssClass="tb_icon_delete shrink_animation" OnClientClick="return confirm('Confirma exclusão?')"
                                                                        ToolTip="Excluir" CommandArgument='<%# Eval("cod_caderno_modelo_cam") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            Nenhum registro foi encontrado!
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:ObjectDataSource ID="grvCadernoModeloObjectDataSource" runat="server" DeleteMethod="Excluir"
                                                SelectMethod="ListarPorFiltro" TypeName="DsAdmin.CadernoModelo" OnSelecting="grvCadernoModeloObjectDataSource_Selecting"
                                                EnableViewState="False">
                                                <SelectParameters>
                                                    <asp:ControlParameter Name="des_caderno_modelo_cam" ControlID="des_caderno_modelo_camTextBox"
                                                        PropertyName="Text" Type="String" />
                                                    <asp:Parameter Name="cod_modelo_edicao_moe" Type="Int32" />
                                                    <asp:Parameter Name="cod_caderno_modelo_cam" Type="Int32" />
                                                    <asp:ControlParameter Name="num_ordem_cam" ControlID="num_ordem_camTextBox" PropertyName="Text"
                                                        Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </div>
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

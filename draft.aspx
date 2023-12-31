﻿<%@ Page Title="Magic: The Gathering Online Draft Viewer. | limitedinformation" Language="C#"
    MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="draft.aspx.cs"
    Inherits="draft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta name="description" content="Allows you to upload Magic:The Gathering draft files and to then view, comment on, and rate the player's picks." />
    <meta name="keywords" content="Limited Pointing, limited information, Draft Viewer, draft viewer magic online, magic the gathering draft, MTGO Draft Viewer, Magic The Gathering Online Draft Viewer, Draft Pointing, Rate Draft cards, Rate limited magic cards" />
    <%--Changed keyword "Magic the gathering" with "draft viewer magic online"--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableViewState="False">
    </asp:ScriptManager>
    <table class="infobox" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td class="TL">
                </td>
                <td class="TM">
                    <h2>
                        Upload MTGO draft file</h2>
                </td>
                <td class="TR">
                </td>
            </tr>
            <tr>
                <td class="ML">
                </td>
                <td class="MM">
                    <asp:FileUpload ID="UploadDraft" runat="server" EnableViewState="False" />
                    <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click"
                        EnableViewState="False" /><br />
                    <asp:Label ID="UploadResults" runat="server" Text="By default, MTGO saves the draft files to Documents\Games\Magic The Gathering Online\Drafts"></asp:Label>
                </td>
                <td class="MR">
                </td>
            </tr>
            <tr>
                <td class="BL">
                </td>
                <td class="BM">
                </td>
                <td class="BR">
                </td>
            </tr>
        </tbody>
    </table>
    <div class="clear filter">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="False">
        <ContentTemplate>
            <table class="infobox" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td class="TL">
                        </td>
                        <td class="TM">
                            <h2>
                                Availible Drafts</h2>
                        </td>
                        <td class="TR">
                        </td>
                    </tr>
                    <tr>
                        <td class="ML">
                        </td>
                        <td class="MM">
                            <asp:GridView ID="GridView1" runat="server" EnableViewState="False" AllowSorting="True"
                                ViewStateMode="Disabled" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanged"
                                OnSorting="GridView1_SortCommand" PageSize="10" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID">
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="ID" runat="server" OnClick="GridView1_SortCommand" EnableViewState="False">ID</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="IDDetailViewLink" runat="server" Text='<%# Bind("ID") %>' NavigateUrl='<%# "~/Show.aspx?ID=" + Eval("ID") + "&Pack=1&SubPack=1"%>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Player Name" HeaderText="Player Name" SortExpression="Player Name" />--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="Player" runat="server" OnClick="GridView1_SortCommand" EnableViewState="False">Player Name</asp:LinkButton><br />
                                            <asp:DropDownList ID="DropDownListPlayer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectedIndexChanged"
                                                ViewStateMode="Enabled">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Player Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Set 1" HeaderText="Set 1" ReadOnly="True" SortExpression="Set 1" />--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="Set1" runat="server" OnClick="GridView1_SortCommand" EnableViewState="False">Set 1</asp:LinkButton><br />
                                            <asp:DropDownList ID="DropDownListSet1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectedIndexChanged"
                                                ViewStateMode="Enabled">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Set 1") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Set 2" HeaderText="Set 2" ReadOnly="True" SortExpression="Set 2" />--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="Set2" runat="server" OnClick="GridView1_SortCommand" EnableViewState="False">Set 2</asp:LinkButton><br />
                                            <asp:DropDownList ID="DropDownListSet2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectedIndexChanged"
                                                ViewStateMode="Enabled">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Set 2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Set 3" HeaderText="Set 3" ReadOnly="True" SortExpression="Set 3" />--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="Set3" runat="server" OnClick="GridView1_SortCommand" EnableViewState="False">Set 3</asp:LinkButton><br />
                                            <asp:DropDownList ID="DropDownListSet3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectedIndexChanged"
                                                ViewStateMode="Enabled">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Set 3") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="Draft Date" HeaderText="Draft Date" ReadOnly="true" SortExpression="Draft Date" />--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="DraftDate" runat="server" OnClick="GridView1_SortCommand" EnableViewState="False">Draft Date</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Draft Date") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <p>
                                        No Results returned.</p>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                        <td class="MR">
                        </td>
                    </tr>
                    <tr>
                        <td class="BL">
                        </td>
                        <td class="BM">
                        </td>
                        <td class="BR">
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModifyDate" runat="server">
    <div class="Copyright">
        Last Modified on Sunday, June 26, 2011</div>
</asp:Content>

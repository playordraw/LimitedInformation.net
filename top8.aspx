<%@ Page Title="Top 8 Predictor - A Swiss Rounds Tournament Tool. | limitedinformation"
    Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="top8.aspx.cs"
    Inherits="top8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta name="description" content="Top 8 Predictor, gives an estimate for the top eight of a tournament with modified swiss style rounds, including draws, such as MTG (Magic: The Gathering)." />
    <meta name="keywords" content="Top 8 Predictor, limited information, Top 8 formula, magic the gathering, magic the gathering programs, swiss tournament software, swiss tournament, Top 8 math , swiss rounds calculator, MTG top 8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="infobox" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td class="TL">
                </td>
                <td class="TM">
                <h2>Calculation Inputs</h2>
                </td>
                <td class="TR">
                </td>
            </tr>
            <tr>
                <td class="ML">
                </td>
                <td class="MM">
                    <asp:TextBox ID="Players" runat="server"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text=" Players"></asp:Label>
                    <br />
                    <asp:TextBox ID="Rounds" runat="server"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text=" Rounds"></asp:Label>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                    <asp:CheckBox ID="NotMTGO" runat="server" Text="Include draws" Checked="true" />
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
    <table class="infobox" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td class="TL">
                </td>
                <td class="TM">
                    <h2>
                        Welcome to the Top 8 predictor!
                    </h2>
                </td>
                <td class="TR">
                </td>
            </tr>
            <tr>
                <td class="ML">
                </td>
                <td class="MM" id="resultsbox" runat="server">
                    <p>
                        Enter the number of players and rounds above, and click submit.
                    </p>
                    <p>                        
                        If you think the results are incorrect for some reason, please send me a message at <b>playordraw (at) gmail (dot) com</b> with as much
                        information as possible.
                    </p>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModifyDate" runat="server">
<div class="Copyright">Last Modified on Wednesday, June 15, 2011</div>
</asp:Content>
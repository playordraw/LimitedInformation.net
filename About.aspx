<%@ Page Title="About limited information. | limitedinformation" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table class="infobox" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td class="TL">
                </td>
                <td class="TM">
                    <h2>
                        About my site.
                    </h2>
                </td>
                <td class="TR">
                </td>
            </tr>
            <tr>
                <td class="ML">
                </td>
                <td class="MM">
                <%-- In an actual tournament, there are factors that will make the actual results
                        different with what my program projects.<br />
                        Possible sources of discrepancy are:<br />
                        1. Unintentional draws. While the program takes intentional draws into account, unintentional draws are harder to predict, and
                        often rare.<br />
                        2. The paired-down player losing. The program makes the assumption that the paired-down player will win. While this is obviously
                        more common then a UD, it seemed a better choice then presuming the paired-up player wins.<br />
                        These, and other variables may result in discrepancies between expected results and actual results, but the program should still
                        provide a good guideline as to points required to make the cut to the top eight.--%>
                    <p>
                        The goal of this site is to fill noticeable voids in the community of Magic: the Gathering.
                        Currently offerings include:<br />
                        The Top 8 Predictor. An application to make an educated guess of what the final standing will be at the end of a
                        Swiss style tournament, taking intentional draws into account. The results of the program are intended to be a guideline of
                        what win records will make it into the top eight.<br />
                        The MTGO Draft Viewer. Allows you to upload MTGO draft files to view, rate, and comment on picks.
                    </p>
                    <p>
                        I plan to add new features in the future, so be sure to check back.                        
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
    <table class="infobox" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td class="TL">
                </td>
                <td class="TM">
                    <h2>
                        Contact Information.
                    </h2>
                </td>
                <td class="TR">
                </td>
            </tr>
            <tr>
                <td class="ML">
                </td>
                <td class="MM">
                    <p>
                        If you wish to submit a comment or a bug report, please contact me at <strong>playordraw
                            at gmail</strong>.<br />
                        If you are submitting a bug report, please include as much information as possible
                        so I can try to reproduce it. Thanks.
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
    <div class="Copyright">Last Modified on Monday, June 27, 2011</div>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="Show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/jquery-1.4.1.js" />
        </Scripts>
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        $(function () {
            var width;
            $('.rating').mouseenter(function () {
                width = $('.rating').css('width');
            }).mouseleave(function () {
                $('.rating').css('width', width);
            });
            $('.rating').mousemove(function (e) {
                var x = e.pageX - $(this).offset().left;
                $('.rating').css('width', (x + 1) + 'px');
            });
        });
    </script>
    <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%#"http://wizards.com/global/images/magic/" + Eval("setname") + "/" + ((string)Eval("cardname")).Replace(Convert.ToChar(Convert.ToChar("(")- Convert.ToChar(1)).ToString(),"").Replace(" ","_").Replace(":","").Replace("-","_") + ".jpg"%>' /></ItemTemplate>                     --%>
    <table class="infobox draftbox" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td class="TL">
                </td>
                <td class="TM" style="text-align: center">
                    <h2>
                        Set:
                        <%=Request.QueryString["Pack"]%>
                        Pick:
                        <%=Request.QueryString["SubPack"] %></h2>
                </td>
                <td class="TR">
                </td>
            </tr>
            <tr>
                <td class="ML">
                </td>
                <td class="MM">
                    <div class="showclear">
                        <asp:HyperLink ID="Previous" class="previous" runat="server" Text="&larr;Previous"></asp:HyperLink>
                        <asp:HyperLink ID="Next" class="next" runat="server" Text="Next&rarr;"></asp:HyperLink>
                    </div>
                    <asp:DataList ID="DetailListrptr" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                        ShowFooter="False" OnItemCreated="FormatString" ViewStateMode="disabled">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                            <span ID="RatingLabel" class="RatingLabel" style="position: absolute" runat="server">
                                Rating</span>
                        </ItemTemplate>
                        <SelectedItemTemplate>
                            <asp:Image ID="Image1" runat="server" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div ID="divrating" class="rating" runat="server">
                                        <asp:ImageMap ID="rating" runat="server" ImageUrl="~/images/stars.gif" HotSpotMode="PostBack" onclick="rating_click" OnLoad="RegisterAsync">
                                            <asp:PolygonHotSpot Coordinates="0,22,11,15,7,0,20,9,33,0,29,15,40,22,40,25,25,25,20,39,15,25,0,25" />
                                            <asp:PolygonHotSpot Coordinates="41,22,52,15,48,0,61,9,74,0,70,15,81,22,81,25,66,25,61,39,56,25,41,25" />
                                            <asp:PolygonHotSpot Coordinates="82,22,93,15,89,0,102,9,115,0,111,15,122,22,122,25,107,25,102,39,97,25,82,25" />
                                            <asp:PolygonHotSpot Coordinates="123,22,134,15,130,0,143,9,156,0,152,15,163,22,163,25,148,25,143,39,138,25,123,25" />
                                            <asp:PolygonHotSpot Coordinates="164,22,175,15,171,0,184,9,197,0,193,15,204,22,204,25,189,25,184,39,179,25,164,25" />
                                            <asp:PolygonHotSpot Coordinates="205,22,216,15,212,0,225,9,238,0,234,15,245,22,245,25,230,25,225,39,220,25,205,25" />
                                        </asp:ImageMap></div>
                                </ContentTemplate>                                
                            </asp:UpdatePanel>
                        </SelectedItemTemplate>
                    </asp:DataList>
                    <div class="clear showfoot">
                        <asp:HyperLink ID="Previous2" runat="server" Text="&larr;Previous"></asp:HyperLink>
                        <span>Set:</span>
                        <asp:HyperLink ID="Set1" runat="server" Text="1"></asp:HyperLink>
                        <asp:HyperLink ID="Set2" runat="server" Text="2"></asp:HyperLink>
                        <asp:HyperLink ID="Set3" runat="server" Text="3"></asp:HyperLink>
                        <span>Pick:</span>
                        <asp:HyperLink ID="Pack1" runat="server" Text="1"></asp:HyperLink>
                        <asp:HyperLink ID="Pack2" runat="server" Text="2"></asp:HyperLink>
                        <asp:HyperLink ID="Pack3" runat="server" Text="3"></asp:HyperLink>
                        <asp:HyperLink ID="Pack4" runat="server" Text="4"></asp:HyperLink>
                        <asp:HyperLink ID="Pack5" runat="server" Text="5"></asp:HyperLink>
                        <asp:HyperLink ID="Pack6" runat="server" Text="6"></asp:HyperLink>
                        <asp:HyperLink ID="Pack7" runat="server" Text="7"></asp:HyperLink>
                        <asp:HyperLink ID="Pack8" runat="server" Text="8"></asp:HyperLink>
                        <asp:HyperLink ID="Pack9" runat="server" Text="9"></asp:HyperLink>
                        <asp:HyperLink ID="Pack10" runat="server" Text="10"></asp:HyperLink>
                        <asp:HyperLink ID="Pack11" runat="server" Text="11"></asp:HyperLink>
                        <asp:HyperLink ID="Pack12" runat="server" Text="12"></asp:HyperLink>
                        <asp:HyperLink ID="Pack13" runat="server" Text="13"></asp:HyperLink>
                        <asp:HyperLink ID="Pack14" runat="server" Text="14"></asp:HyperLink>
                        <asp:HyperLink ID="Pack15" runat="server" Text="15"></asp:HyperLink>
                        <asp:HyperLink ID="Next2" runat="server" Text="Next&rarr;"></asp:HyperLink>
                        <div id="disqus_thread">
                        </div>
                        <script type="text/javascript">
                            /* * * CONFIGURATION VARIABLES: EDIT BEFORE PASTING INTO YOUR WEBPAGE * * */
                            var disqus_shortname = 'limitedinformation'; // required: replace example with your forum shortname

                            // The following are highly recommended additional parameters. Remove the slashes in front to use.
                            var disqus_identifier = '<%=GetSha(Request.Url.ToString()) %>';
                            var disqus_url = '<%=Request.Url %>';
                            var disqus_title = 'Draft <%=Request.QueryString["ID"] %> Pack <%=Request.QueryString["Pack"] %> Pick <%=Request.QueryString["SubPack"] %>';

                            //var disqus_developer = 1; ///////REMOVE FOR LIVE
                            /* * * DON'T EDIT BELOW THIS LINE * * */
                            (function () {
                                var dsq = document.createElement('script'); dsq.type = 'text/javascript'; dsq.async = true;
                                dsq.src = 'http://' + disqus_shortname + '.disqus.com/embed.js';
                                (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(dsq);
                            })();
                        </script>
                        <noscript>
                            Please enable JavaScript to view the <a href="http://disqus.com/?ref_noscript">comments
                                powered by Disqus.</a></noscript>
                        <a href="http://disqus.com" class="dsq-brlink">comments powered by <span class="logo-disqus">
                            Disqus</span></a>
                    </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ModifyDate" runat="Server">
    <div class="Copyright">
        Last Modified on Sunday, June 26, 2011</div>
</asp:Content>

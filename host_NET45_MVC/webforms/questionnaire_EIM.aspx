<%@ Page Title="" Language="VB" MasterPageFile="~/site.master" AutoEventWireup="false" CodeFile="questionnaire_EIM.aspx.vb" Inherits="questionnaire_EIM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<META HTTP-EQUIV="Content-Type" CONTENT="text/html; charset=us-ascii">
<meta name=Generator content="Microsoft Word 12 (filtered medium)"><!--[if !mso]><style>v\:* {behavior:url(#default#VML);}
o\:* {behavior:url(#default#VML);}
w\:* {behavior:url(#default#VML);}
.shape {behavior:url(#default#VML);}
</style><![endif]--><style><!--
/* Font Definitions */
@font-face
	{font-family:"Cambria Math";
	panose-1:2 4 5 3 5 4 6 3 2 4;}
@font-face
	{font-family:Calibri;
	panose-1:2 15 5 2 2 2 4 3 2 4;}
@font-face
	{font-family:Tahoma;
	panose-1:2 11 6 4 3 5 4 4 2 4;}
/* Style Definitions */
p.MsoNormal, li.MsoNormal, div.MsoNormal
	{margin:0in;
	margin-bottom:.0001pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";}
a:link, span.MsoHyperlink
	{mso-style-priority:99;
	color:blue;
	text-decoration:underline;}
a:visited, span.MsoHyperlinkFollowed
	{mso-style-priority:99;
	color:purple;
	text-decoration:underline;}
p
	{mso-style-priority:99;
	mso-margin-top-alt:auto;
	margin-right:0in;
	mso-margin-bottom-alt:auto;
	margin-left:0in;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";}
p.MsoAcetate, li.MsoAcetate, div.MsoAcetate
	{mso-style-priority:99;
	mso-style-link:"Balloon Text Char";
	margin:0in;
	margin-bottom:.0001pt;
	font-size:8.0pt;
	font-family:"Tahoma","sans-serif";}
span.EmailStyle17
	{mso-style-type:personal-compose;
	font-family:"Calibri","sans-serif";
	color:windowtext;}
span.BalloonTextChar
	{mso-style-name:"Balloon Text Char";
	mso-style-priority:99;
	mso-style-link:"Balloon Text";
	font-family:"Tahoma","sans-serif";}
.MsoChpDefault
	{mso-style-type:export-only;}
@page WordSection1
	{size:8.5in 11.0in;
	margin:56.7pt 42.5pt 56.7pt 85.05pt;}
div.WordSection1
	{page:WordSection1;}
--></style><!--[if gte mso 9]><xml>
<o:shapedefaults v:ext="edit" spidmax="2050" />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext="edit">
<o:idmap v:ext="edit" data="1" />
</o:shapelayout></xml><![endif]-->

    <link href="StyleSheetForQst.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  
       <div>
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
</asp:ToolkitScriptManager>
    </div>

         <div style="position: fixed;	top: 50%; width: 100%;">
                <table width=100% >
                            <tr>
                                    <td width=100%>
                                            <table align=center>
                                                <tr>
                                                    <td>
                                                      </td>
                                                      <td >
                                                         <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                                             AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10" DynamicLayout="False">
                                                             <ProgressTemplate>
                                                                 <img alt="" src="~/Images/bar.gif"  />
                                                             </ProgressTemplate>
                                                         </asp:UpdateProgress>
                                                      </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                    </td>
                            </tr>
                    </table>    
         </div>

            <div style="text-align: left">
            <asp:LoginName ID="LoginName" runat="server"  FormatString="Welcome, {0} " ForeColor="Black"  Font-Size="10px" />
                    
            <br />
            <asp:LoginStatus ID="LoginStatus" runat="server" ForeColor="Gray" Font-Size="10px" />
            </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    
    <ContentTemplate>

    <div style="width: 687pt; font-family: 'Times New Roman', Times, serif; font-size: 55px;  color: #000000; text-align: center;">
     <asp:Label id="labelTitle1" runat="server" ></asp:Label>
    </div>
    
  <asp:GridView ID="GridViewQstGrid_1" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceQstGrid_1" EnableModelValidation="True" 
    CellPadding="0" CellSpacing="0" style='border-collapse:collapse'>
    <AlternatingRowStyle BackColor="#E9EDF4" />
    <Columns>
      <asp:TemplateField HeaderStyle-Width="151.0pt" ItemStyle-Width="151.0pt" ItemStyle-CssClass="FirstColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt; border:solid white 1.0pt;border-bottom:solid white 3.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Application / Запрос</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="application" runat="server" Text='<%# Eval("Application") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle Width="151pt"></HeaderStyle>

<ItemStyle CssClass="FirstColumnRow" Width="151pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="405.0pt" ItemStyle-Width="405.0pt" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>What it offers / Описание</span></b><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'> </span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="WhatItOffers" runat="server" Text='<%# Eval("WhatItOffers") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left" Width="405pt"></HeaderStyle>

<ItemStyle CssClass="SecondColumnRow" Width="405pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;width:300px;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-GB style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Business View / Оценка</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p><p class=MsoNormal style='margin-top:2.9pt;vertical-align:baseline'><i><span lang=EN-GB style='font-size:12.0pt;font-family:"Arial","sans-serif";color:white'>(Please choose; must have, nice to have, or not needed / <br />Выберите: необходимое приложение,  полезное приложение, бесполезное приложение)</span></i><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 

                  <table style="width: 300px; text-align: center; font-size: 12px;">
                   <tr>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNone" runat="server" CommandName="None" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonMust" runat="server" CommandName="Must" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNice" runat="server" CommandName="Nice" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNot" runat="server" CommandName="Not" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                   </tr>
                   <tr>
                    <td>
                     None
                    </td>
                    <td>
                     Must<br />необходимое
                    </td>
                    <td>
                     Nice<br />полезное
                    </td>
                    <td>
                     Not<br />бесполезное
                    </td>
                   </tr>
                  </table>
                </ItemTemplate> 

      </asp:TemplateField>
    </Columns>
    <RowStyle BackColor="#D0D8E8" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceQstGrid_1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT id, rtrim(Application) as Application, rtrim(WhatItOffers) as WhatItOffers FROM [Table_Questionnaire] WHERE ([GroupNameId] = @GroupNameId)">
    <SelectParameters>
      <asp:Parameter DefaultValue="1" Name="GroupNameId" Type="Int16" />
    </SelectParameters>
  </asp:SqlDataSource>

      <br /> <br />
      <div style="width: 687pt; font-family: 'Times New Roman', Times, serif; font-size: 55px;  color: #000000; text-align: center;">
     <asp:Label id="labelTitle2" runat="server" ></asp:Label>
    </div>

  <asp:GridView ID="GridViewQstGrid_2" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceQstGrid_2" EnableModelValidation="True" 
    CellPadding="0" CellSpacing="0" style='border-collapse:collapse'>
    <AlternatingRowStyle BackColor="#E9EDF4" />
    <Columns>
      <asp:TemplateField HeaderStyle-Width="151.0pt" ItemStyle-Width="151.0pt" ItemStyle-CssClass="FirstColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt; border:solid white 1.0pt;border-bottom:solid white 3.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Application / Запрос</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="application" runat="server" Text='<%# Eval("Application") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle Width="151pt"></HeaderStyle>

<ItemStyle CssClass="FirstColumnRow" Width="151pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="405.0pt" ItemStyle-Width="405.0pt" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>What it offers / Описание</span></b><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'> </span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="WhatItOffers" runat="server" Text='<%# Eval("WhatItOffers") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left" Width="405pt"></HeaderStyle>

<ItemStyle CssClass="SecondColumnRow" Width="405pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;width:300px;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-GB style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Business View / Оценка</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p><p class=MsoNormal style='margin-top:2.9pt;vertical-align:baseline'><i><span lang=EN-GB style='font-size:12.0pt;font-family:"Arial","sans-serif";color:white'>(Please choose; must have, nice to have, or not needed / <br />Выберите: необходимое приложение,  полезное приложение, бесполезное приложение)</span></i><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 

                  <table style="width: 300px; text-align: center; font-size: 12px;">
                   <tr>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNone" runat="server" CommandName="None" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonMust" runat="server" CommandName="Must" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNice" runat="server" CommandName="Nice" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNot" runat="server" CommandName="Not" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                   </tr>
                   <tr>
                    <td>
                     None
                    </td>
                    <td>
                     Must<br />необходимое
                    </td>
                    <td>
                     Nice<br />полезное
                    </td>
                    <td>
                     Not<br />бесполезное
                    </td>
                   </tr>
                  </table>
                </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left" Width="147pt"></HeaderStyle>

<ItemStyle CssClass="SecondColumnRow" Width="147pt"></ItemStyle>
      </asp:TemplateField>
    </Columns>
    <RowStyle BackColor="#D0D8E8" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceQstGrid_2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT id, rtrim(Application) as Application, rtrim(WhatItOffers) as WhatItOffers FROM [Table_Questionnaire] WHERE ([GroupNameId] = @GroupNameId)">
    <SelectParameters>
      <asp:Parameter DefaultValue="2" Name="GroupNameId" Type="Int16" />
    </SelectParameters>
  </asp:SqlDataSource>


      <br /> <br />
      <div style="width: 687pt; font-family: 'Times New Roman', Times, serif; font-size: 55px;  color: #000000; text-align: center;">
     <asp:Label id="labelTitle3" runat="server" ></asp:Label>
    </div>

  <asp:GridView ID="GridViewQstGrid_3" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceQstGrid_3" EnableModelValidation="True" 
    CellPadding="0" CellSpacing="0" style='border-collapse:collapse'>
    <AlternatingRowStyle BackColor="#E9EDF4" />
    <Columns>
      <asp:TemplateField HeaderStyle-Width="151.0pt" ItemStyle-Width="151.0pt" ItemStyle-CssClass="FirstColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt; border:solid white 1.0pt;border-bottom:solid white 3.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Application / Запрос</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="application" runat="server" Text='<%# Eval("Application") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle Width="151pt"></HeaderStyle>

<ItemStyle CssClass="FirstColumnRow" Width="151pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="405.0pt" ItemStyle-Width="405.0pt" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>What it offers / Описание</span></b><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'> </span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="WhatItOffers" runat="server" Text='<%# Eval("WhatItOffers") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left" Width="405pt"></HeaderStyle>

<ItemStyle CssClass="SecondColumnRow" Width="405pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;width:300px;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-GB style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Business View / Оценка</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p><p class=MsoNormal style='margin-top:2.9pt;vertical-align:baseline'><i><span lang=EN-GB style='font-size:12.0pt;font-family:"Arial","sans-serif";color:white'>(Please choose; must have, nice to have, or not needed / <br />Выберите: необходимое приложение,  полезное приложение, бесполезное приложение)</span></i><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 

                  <table style="width: 300px; text-align: center; font-size: 12px;">
                   <tr>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNone" runat="server" CommandName="None" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonMust" runat="server" CommandName="Must" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNice" runat="server" CommandName="Nice" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNot" runat="server" CommandName="Not" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                   </tr>
                   <tr>
                    <td>
                     None
                    </td>
                    <td>
                     Must<br />необходимое
                    </td>
                    <td>
                     Nice<br />полезное
                    </td>
                    <td>
                     Not<br />бесполезное
                    </td>
                   </tr>
                  </table>
                </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left" Width="147pt"></HeaderStyle>

<ItemStyle CssClass="SecondColumnRow" Width="147pt"></ItemStyle>
      </asp:TemplateField>
    </Columns>
    <RowStyle BackColor="#D0D8E8" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceQstGrid_3" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT id, rtrim(Application) as Application, rtrim(WhatItOffers) as WhatItOffers FROM [Table_Questionnaire] WHERE ([GroupNameId] = @GroupNameId)">
    <SelectParameters>
      <asp:Parameter DefaultValue="3" Name="GroupNameId" Type="Int16" />
    </SelectParameters>
  </asp:SqlDataSource>


      <br /> <br />
      <div style="width: 687pt; font-family: 'Times New Roman', Times, serif; font-size: 55px;  color: #000000; text-align: center;">
     <asp:Label id="labelTitle4" runat="server" ></asp:Label>
    </div>

  <asp:GridView ID="GridViewQstGrid_4" runat="server" AutoGenerateColumns="False" 
    DataSourceID="SqlDataSourceQstGrid_4" EnableModelValidation="True" 
    CellPadding="0" CellSpacing="0" style='border-collapse:collapse'>
    <AlternatingRowStyle BackColor="#E9EDF4" />
    <Columns>
      <asp:TemplateField HeaderStyle-Width="151.0pt" ItemStyle-Width="151.0pt" ItemStyle-CssClass="FirstColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt; border:solid white 1.0pt;border-bottom:solid white 3.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Application / Запрос</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="application" runat="server" Text='<%# Eval("Application") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle Width="151pt"></HeaderStyle>

<ItemStyle CssClass="FirstColumnRow" Width="151pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="405.0pt" ItemStyle-Width="405.0pt" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>What it offers / Описание</span></b><b><span lang=EN-IE style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'> </span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 
                  <asp:label id="WhatItOffers" runat="server" Text='<%# Eval("WhatItOffers") %>' ></asp:label>
                </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left" Width="405pt"></HeaderStyle>

<ItemStyle CssClass="SecondColumnRow" Width="405pt"></ItemStyle>
      </asp:TemplateField>
      <asp:TemplateField   HeaderStyle-Width="300px" ItemStyle-Width="300px" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="SecondColumnRow">
                <HeaderTemplate >
                  <div style='height:101.0pt;width:300px;border-top:solid white 1.0pt;border-left:none;border-bottom:solid white 3.0pt;border-right:solid white 1.0pt;background:#4F81BD;padding:.05in .1in .05in .1in;'><p style='margin-top:3.85pt;vertical-align:baseline'><b><span lang=EN-GB style='font-size:16.0pt;font-family:"Arial","sans-serif";color:white'>Business View / Оценка</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p><p class=MsoNormal style='margin-top:2.9pt;vertical-align:baseline'><i><span lang=EN-GB style='font-size:12.0pt;font-family:"Arial","sans-serif";color:white'>(Please choose; must have, nice to have, or not needed / <br />Выберите: необходимое приложение,  полезное приложение, бесполезное приложение)</span></i><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
                </HeaderTemplate>
                <ItemTemplate> 

                  <table style="width: 300px; text-align: center; font-size: 12px;">
                   <tr>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNone" runat="server" CommandName="None" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonMust" runat="server" CommandName="Must" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNice" runat="server" CommandName="Nice" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                    <td style="width: 25%">
                      <asp:ImageButton ID="ImageButtonNot" runat="server" CommandName="Not" 
                      CommandArgument='<%# Eval("Id") %>' />
                    </td>
                   </tr>
                   <tr>
                    <td>
                     None
                    </td>
                    <td>
                     Must<br />необходимое
                    </td>
                    <td>
                     Nice<br />полезное
                    </td>
                    <td>
                     Not<br />бесполезное
                    </td>
                   </tr>
                  </table>
                </ItemTemplate> 

<HeaderStyle HorizontalAlign="Left" Width="147pt"></HeaderStyle>

<ItemStyle CssClass="SecondColumnRow" Width="147pt"></ItemStyle>
      </asp:TemplateField>
    </Columns>
    <RowStyle BackColor="#D0D8E8" />
  </asp:GridView>
  <asp:SqlDataSource ID="SqlDataSourceQstGrid_4" runat="server" 
    ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
    SelectCommand="SELECT id, rtrim(Application) as Application, rtrim(WhatItOffers) as WhatItOffers FROM [Table_Questionnaire] WHERE ([GroupNameId] = @GroupNameId)">
    <SelectParameters>
      <asp:Parameter DefaultValue="4" Name="GroupNameId" Type="Int16" />
    </SelectParameters>
  </asp:SqlDataSource>

  <br />

      <div style="width: 687pt; font-family: 'Times New Roman', Times, serif; font-size: 55px;  color: #000000; text-align: center;">
     <asp:Label id="labelYourBusinessNeedTitle" runat="server" Text="Your business needs / Ваши запросы" ></asp:Label>
    </div>

      <asp:GridView ID="GridViewYourBusinessNeeds" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="Id" DataSourceID="SqlDataSourceYourBusinessNeeds2" 
        EnableModelValidation="True">
        <Columns>
         <asp:TemplateField HeaderText="Description" ItemStyle-Height="65.9pt" ItemStyle-Width="363.0pt" ItemStyle-CssClass="YourBusinessNeedFirstColumnRow" >
          <HeaderTemplate>
          <div style='border-top:2.25pt;border-left:solid black 2.25pt;border-bottom:1.0pt;border-right:1.0pt;border-color:black;border-style:solid; padding-top: .05in; padding-bottom: .05in; height: 80pt;'><p class=MsoNormal style='margin-top:3.35pt;vertical-align:baseline'><b><span style='font-size:14.0pt;font-family:"Arial","sans-serif";color:black'>What are your Top 5 Information Pain Points? <br /> Перечислите 5 наиболее существенных для Вас запросов?</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
          </HeaderTemplate>
          <ItemTemplate>
           <asp:TextBox ID="TextBoxDescription" runat="server" Text='<%# Bind("Description") %>' Height="65.9pt" Width="363.0pt"  
           BorderColor="White" BorderStyle="Solid" BorderWidth="1px" TextMode="MultiLine" CssClass="HideScroll"></asp:TextBox>
           <asp:Label ID="labelWarningDescription" runat="server" Text="!!!" Visible="false" ></asp:Label>
           <asp:Label ID="labelID" runat="server" Text='<%# Bind("Id") %>'  Visible="false" ></asp:Label>
          </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Solution" ItemStyle-Height="65.9pt" ItemStyle-Width="96.0pt" ItemStyle-CssClass="YourBusinessNeedSecondColumnRow" >
          <HeaderTemplate>
          <div style='border-top:solid black 2.25pt;border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt; padding-top: .05in; padding-bottom: .05in;height: 80pt;'><p class=MsoNormal align=center style='margin-top:3.35pt;text-align:center;vertical-align:baseline'><b><span lang=EN-IE style='font-size:14.0pt;font-family:"Arial","sans-serif";color:black'>Solution <br /> Решение</span></b><b><span lang=EN-IE style='font-size:14.0pt;font-family:"Arial","sans-serif";color:black'> </span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
          </HeaderTemplate>
          <ItemTemplate>
           <asp:TextBox ID="TextBoxSolution" runat="server" Text='<%# Bind("Solution") %>' Height="65.9pt" Width="96.0pt" 
           BorderColor="White" BorderStyle="Solid" BorderWidth="1px" TextMode="MultiLine" CssClass="HideScroll"></asp:TextBox>
           <asp:Label ID="labelWarningSolution" runat="server" Text="!!!" Visible="false" ></asp:Label>
          </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Benefit" ItemStyle-Height="65.9pt" ItemStyle-Width="1.5in" ItemStyle-CssClass="YourBusinessNeedThirdColumnRow" >
          <HeaderTemplate>
           <div style='border-top:solid black 2.25pt;border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt; padding-top: .05in; padding-bottom: .05in;height: 80pt;'><p class=MsoNormal align=center style='margin-top:3.35pt;text-align:center;vertical-align:baseline'><b><span lang=EN-GB style='font-size:14.0pt;font-family:"Arial","sans-serif";color:black'>Benefit <br /> Преимущества</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
          </HeaderTemplate>
          <ItemTemplate>
           <asp:TextBox ID="TextBoxBenefit" runat="server" Text='<%# Bind("Benefit") %>' Height="65.9pt" Width="1.5in" 
           BorderColor="White" BorderStyle="Solid" BorderWidth="1px" TextMode="MultiLine" CssClass="HideScroll"></asp:TextBox>
          </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="BenefitInExpence" ItemStyle-Height="65.9pt" ItemStyle-Width="102.0pt" ItemStyle-CssClass="YourBusinessNeedForthColumnRow" >
          <HeaderTemplate>
          <div style='border-top:solid black 2.25pt;border-left:none;border-right:solid black 1.0pt; padding-top: .05in; padding-bottom: .05in;height: 36pt;'><p class=MsoNormal align=center style='margin-top:1.9pt;text-align:center;vertical-align:baseline'><b><span lang=EN-GB style='font-size:8.0pt;font-family:"Arial","sans-serif";color:black'>Benefit in </span></b><b><span lang=EN-GB style='font-size:14.0pt;font-family:"Arial","sans-serif";color:black'>€€€</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
          <div style='border-left:none;border-bottom:solid black 1.0pt;border-right:solid black 1.0pt; padding-top: .05in; padding-bottom: .05in;height: 36pt;'><p class=MsoNormal align=center style='margin-top:1.9pt;text-align:center;vertical-align:baseline'><b><span lang=EN-GB style='font-size:8.0pt;font-family:"Arial","sans-serif";color:black'>Преимущества, измеримые </span></b><b><span lang=EN-GB style='font-size:14.0pt;font-family:"Arial","sans-serif";color:black'>в €€€</span></b><span style='font-size:18.0pt;font-family:"Arial","sans-serif"'><o:p></o:p></span></p></div>
          </HeaderTemplate>
          <ItemTemplate>
           <asp:TextBox ID="TextBoxBenefitInExpence" runat="server" Text='<%# Bind("BenefitInExpence") %>' Height="65.9pt" Width="102.0pt" 
           BorderColor="White" BorderStyle="Solid" BorderWidth="1px" TextMode="MultiLine" CssClass="HideScroll"></asp:TextBox>
          </ItemTemplate>
         </asp:TemplateField>
        </Columns>
      </asp:GridView>

      <div style="text-align: center; width: 920px;">
            <asp:Button ID="ButtonSaveYourBusinessNeeds" runat="server" 
        Text="Save Your Business Needs" BackColor="#FF0066" Font-Size="24px" 
        ForeColor="White" />
      </div>

      <asp:SqlDataSource ID="SqlDataSourceYourBusinessNeeds" runat="server" 
        ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
        DeleteCommand="DELETE FROM [Table_QuestionnaireYourBusinessNeeds] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [Table_QuestionnaireYourBusinessNeeds] ([UserName], [Description], [Solution], [Benefit], [BenefitInExpence]) VALUES (@UserName, @Description, @Solution, @Benefit, @BenefitInExpence)" 
        SelectCommand="SELECT [Id], [UserName], [Description], [Solution], [Benefit], [BenefitInExpence] FROM [Table_QuestionnaireYourBusinessNeeds]" 
        UpdateCommand="UPDATE [Table_QuestionnaireYourBusinessNeeds] SET [UserName] = @UserName, [Description] = @Description, [Solution] = @Solution, [Benefit] = @Benefit, [BenefitInExpence] = @BenefitInExpence WHERE [Id] = @Id">
        <DeleteParameters>
          <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
          <asp:Parameter Name="UserName" Type="String" />
          <asp:Parameter Name="Description" Type="String" />
          <asp:Parameter Name="Solution" Type="String" />
          <asp:Parameter Name="Benefit" Type="String" />
          <asp:Parameter Name="BenefitInExpence" Type="String" />
        </InsertParameters>
        <UpdateParameters>
          <asp:Parameter Name="UserName" Type="String" />
          <asp:Parameter Name="Description" Type="String" />
          <asp:Parameter Name="Solution" Type="String" />
          <asp:Parameter Name="Benefit" Type="String" />
          <asp:Parameter Name="BenefitInExpence" Type="String" />
          <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
      </asp:SqlDataSource>

      <asp:SqlDataSource  ID="SqlDataSourceYourBusinessNeeds2" runat="server" 
      ConnectionString="<%$ ConnectionStrings:MERCURYConnectionString %>" 
      SelectCommand="DECLARE @Adet int
SET @Adet =5-(SELECT COUNT(ID) 
  FROM [SQL2008_794282_mercury].[dbo].[Table_QuestionnaireYourBusinessNeeds]
  where UserName = @UserName)

CREATE TABLE #YourBusinessNeeds
 (Id INT  
 ,UserName NvarChar(256) 
 ,Description nVarChar(max) 
 ,Solution nVarChar(256) 
 ,Benefit nvarChar(256) 
 ,BenefitInExpence nvarChar(256) )

INSERT INTO #YourBusinessNeeds (Id, UserName,Description,Solution,Benefit,BenefitInExpence)
SELECT [Id]
      ,[UserName]
      ,[Description]
      ,[Solution]
      ,[Benefit]
      ,[BenefitInExpence]
  FROM [Table_QuestionnaireYourBusinessNeeds]
  where UserName = @UserName 


WHILE @Adet > 0 BEGIN
INSERT INTO #YourBusinessNeeds (Id, UserName,Description,Solution,Benefit,BenefitInExpence)
(SELECT 0, N'', N'', N'', N'', N'')
SET @Adet = @Adet-1
END

SELECT [Id]
      ,[UserName]
      ,[Description]
      ,[Solution]
      ,[Benefit]
      ,[BenefitInExpence]
FROM #YourBusinessNeeds

DROP TABLE #YourBusinessNeeds" >
        <SelectParameters> 
         <asp:Parameter  Name="UserName" Type = "String"/>
        </SelectParameters>
      </asp:SqlDataSource>
    </ContentTemplate> 
    </asp:UpdatePanel> 
</asp:Content>


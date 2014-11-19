<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportMedicion.aspx.cs" Inherits="Protell.WebReport.ReportMedicion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style/StyleSheet.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; height: 100%;">
        <div style="width: 100%; margin: 0 auto; text-align: center;">
            <table style="margin: 0 auto; text-align: center;">
                <tr>
                    <td>Fecha Inicial:
                    </td>
                    <td>
                        <asp:TextBox ID="cldrFechaIni" runat="server" type="date" name="fechaIni"></asp:TextBox>                   
                    </td>
                </tr>
                <tr>
                    <td>Fecha Final:
                    </td>
                    <td>
                        <asp:TextBox ID="cldrFechaFin" runat="server" type="date" name="fechaFin"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <div style="margin: 0 auto; text-align: center;">
            </div>
            <img src="~/img/unnamed.jpg" runat="server" style="width: 250px; height: 80px; position: absolute; top: 35px; left: 10px;" />
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <div style="margin: 0 auto; text-align: center; position: absolute; width: 99%">
            <asp:UpdatePanel UpdateMode="Conditional" ID="updpReport" runat="server">
                <ContentTemplate>
                    Lumbreras:
                <asp:CheckBox ID="ChckBlumbr" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                    Estaciones:
                <asp:CheckBox ID="ChckBest" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                    Estructuras:
                <asp:CheckBox ID="ChckBestr" runat="server" AutoPostBack="true" OnCheckedChanged="Check_Clicked" />
                    <div id="dvLine"></div>
                    <br />                    
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="false" ZoomMode="PageWidth" Width="100%" AsyncRendering="False" ShowBackButton="True" EnableViewState="True">
                    </rsweb:ReportViewer>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ChckBlumbr" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ChckBest" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ChckBestr" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

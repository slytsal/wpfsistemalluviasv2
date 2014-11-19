<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportEstacion.aspx.cs" Inherits="Protell.WebReport.DefaultReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style/StyleSheet.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0 auto; text-align: center; position: absolute; width: 99%">
            <br />
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
            </asp:ScriptManager>
            <img src="~/img/unnamed.jpg" runat="server" style="width: 250px; height: 65px; position: absolute; top: 8px; left: 10px;" />
            <asp:UpdatePanel UpdateMode="Conditional" ID="updpReports" runat="server">
                <ContentTemplate>
                    <table style="margin: 0 auto; text-align: center;">
                        <tr>
                            <td>Fecha:
                            </td>
                            <td>
                                 <asp:TextBox ID="cldrFecha" runat="server" type="date" name="fecha"></asp:TextBox>                                                           
                            </td>
                            <td>Estaciones:
                            </td>
                            <td>
                                <asp:DropDownList ID="dpdlTipo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="itemSelected">
                                    <asp:ListItem Value="Elige una opcion">Elige una opción</asp:ListItem>
                                    <asp:ListItem Value="Protocolo">Protocolo</asp:ListItem>
                                    <asp:ListItem Value="Todos">Todos</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div id="dvLine"></div>
                    <br />
                    <rsweb:ReportViewer ID="RptVwrEstacion" runat="server" SizeToReportContent="false" ZoomMode="PageWidth" Width="100%" AsyncRendering="False" ShowBackButton="True" EnableViewState="True">
                    </rsweb:ReportViewer>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dpdlTipo" EventName="selectedindexchanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

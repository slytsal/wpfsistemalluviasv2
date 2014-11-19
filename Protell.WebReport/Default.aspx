<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Protell.WebReport.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style/StyleSheet.css" rel="stylesheet">
    <link href="style/StyleSheetbtn.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>  
        <br /><br /><br />             
    <table id="tblDefault">
        <tr>
            <td>
                <img style="width:60px; height:60px;" src="~/img/Report_img.png" runat="server" /> 
            </td>
            <td>
               <%--<asp:HyperLink ID="lnkRpPM" runat="server" NavigateUrl="ReportMedicion.aspx">Reporte de puntos de medición</asp:HyperLink>--%>
                <a href="ReportMedicion.aspx" class="button"> Puntos de medición </a>
            </td>
        </tr>
        <tr>
            <td>
                <img style="width:60px; height:60px;" src="~/img/Report_img.png" runat="server" />
            </td>
            <td>
                <%--<asp:HyperLink ID="lnkRpEst" runat="server" NavigateUrl="ReportEstacion.aspx">Reporte de Estaciones</asp:HyperLink>--%>
                <a href="ReportEstacion.aspx" class="button"> Estaciones </a>
            </td>
        </tr>          
    </table>
    </div>
    </form>
</body>
</html>

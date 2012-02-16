<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudyForm.aspx.cs" Inherits="StudyForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblStdName" runat="server" Text="Study name goes here"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblStdCreator" runat="server" Text="Study Creator Name goes here"></asp:Label>
    <br />
    <asp:Label ID="lblStdDate" runat="server" Text="Study date created goes here"></asp:Label>
    <br />
    <asp:Textbox ID="tbStdDescription" runat="server" Text="Study description goes here" TextMode="MultiLine"></asp:Textbox>

</asp:Content>


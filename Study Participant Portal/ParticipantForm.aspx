<%@ Page Title="Participant" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ParticipantForm.aspx.cs" Inherits="ParticipantForm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Participant Page
    </h2>
    <asp:Panel ID="pnlMain" runat="server" Height="329px">
        <asp:Label ID="lblStudies" runat="server" Text="Please select a study you are interested in participating in"> </asp:Label>
        <br />
        <asp:ListBox ID="lboxStudyList" runat="server" Height="204px" Width="401px"></asp:ListBox>
        <br /> 
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:Button ID="btnView" runat="server" Text="View Study" 
            onclick="btnView_Click" />
    </asp:Panel>
</asp:Content>

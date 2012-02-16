<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ResearcherForm.aspx.cs" Inherits="ResearcherForm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Researcher Page
    </h2>
    <asp:Panel ID="pnlMain" runat="server" Height="329px">
        <asp:Button ID="btnResEdit" runat="server" Text="Edit Profile" Width="100px" />
        <asp:Button ID="btnResLogout" runat="server" onclick="logout_Click" Text="Logout" Width="100px" />
        <br />
        <br />
        <asp:Button ID="btnResCreate" runat="server" Text="Create Study" Width="100px" onclick="btnResCreate_Click" />
        <asp:Button ID="btnResEditStdy" runat="server" Text="Edit Study" Width="100px" onclick="btnResEditStdy_Click" />
        <br />
        <br />
        <asp:Label ID="lblStudyList" runat="server" Text="Your Studies"></asp:Label>
        <br />
        <asp:ListBox ID="lboxStudyList" runat="server" Height="204px" Width="401px"></asp:ListBox>     
    </asp:Panel>
    </asp:Content>
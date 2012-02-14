<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Researcher.aspx.cs" Inherits="Researcher" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Researcher Page
    </h2>
    <asp:Panel ID="PnlMain" runat="server" Height="329px">
        <asp:Button ID="btnResEdit" runat="server" Text="Edit Profile" Width="100px" />
        <asp:Button ID="btnResLogout" runat="server" Margin="-100px" 
            onclick="logout_Click" Text="Logout" Width="100px" />
        <br />
        <br />
        <asp:Button ID="btnResCreate" runat="server" Text="Create Study" 
            Width="100px" onclick="btnResCreate_Click" />
        <asp:Button ID="btnResEditStdy" runat="server" Text="Edit Study" Width="100px" />
        <br />
        <br />
        <asp:Label ID="stdyListLabel" runat="server" Text="Your Studies"></asp:Label>
        <br />
        <asp:ListBox ID="studyList" runat="server" Height="204px" Width="401px"></asp:ListBox>     
    </asp:Panel>
    </asp:Content>
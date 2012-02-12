<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Researcher.aspx.cs" Inherits="Researcher" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Researcher Page
    </h2>
    <asp:Panel ID="PnlMain" runat="server" Height="222px">
        <asp:Button ID="btnResLogout" runat="server" Margin="-100px" onclick="Button2_Click" 
            Text="Logout" Width="100px"  />
        <br />
        <asp:Button ID="btnResEdit" runat="server" Text="Edit Profile" Width="100px" />
        <br />
        <asp:Button ID="btnResCreate" runat="server" Width="100px" Text="Create Study" />
       
        

    </asp:Panel>
    <p>
    
    </p>
</asp:Content>
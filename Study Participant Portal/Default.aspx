<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to the Study Participant Portal! 
    </h2>
    <h2>
        </h2>
    <asp:Panel ID="PnlMain" runat="server">
        <asp:Button ID="btnResearcher" runat="server" Text="Researcher" Width="105px" 
            onclick="btnResearcher_Click"/>
        <br />
        <asp:Button ID="btnParticipant" runat="server" Text="Participant" Width="105px" 
            onclick="btnParticipant_Click" />

        <asp:Panel ID="pnlResearcher" runat="server" Visible = "false">
            <asp:Label ID="lblResLogin" runat="server" Text="Researcher Login"></asp:Label>
            <br />             
            <asp:Label ID="lblResUser" runat="server" Text="User" Width="75px"></asp:Label>
            <asp:TextBox ID="tbResUser" runat="server" Width="173px"></asp:TextBox>
            <br />
            <asp:Label ID="lblResPassword" runat="server" Text="Password" Width="75px"></asp:Label>
            <asp:TextBox ID="tbResPassword" runat="server" Width="173px" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="btnResSubmit" runat="server" Text="Submit" Width="75" 
                onclick="btnResSubmit_Click" />
            <br />
            <br />
            <asp:Label ID="lblResCreateAcc" runat="server" Text="Don't have an account? Create one here: "></asp:Label>       
            <br />
            <asp:Button ID="btnResCreateAcc" runat="server" Text="Create New Account" 
                onclick="btnResCreateAcc_Click" />
            <br />
            <br />
            <asp:Button ID="btnResCancel" runat="server" Text="Cancel" 
                onclick="btnResCancel_Click" />
        </asp:Panel>

        <asp:Panel ID="pnlParticipant" runat="server" Visible = "false">
            <asp:Label ID="lblParLogin" runat="server" Text="Participant Login"></asp:Label>
            <br />             
            <asp:Label ID="lblParUser" runat="server" Text="User" Width="75px"></asp:Label>
            <asp:TextBox ID="tbParUser" runat="server" Width="173px"></asp:TextBox>
            <br />
            <asp:Label ID="lblParPassword" runat="server" Text="Password" Width="75px"></asp:Label>
            <asp:TextBox ID="tbParPassword" runat="server" Width="173px" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="btnParSubmit" runat="server" Text="Submit" Width="75" 
                onclick="btnParSubmit_Click" />
            <br />
            <br />
            <asp:Label ID="lblParCreateAcc" runat="server" Text="Don't have an account? Create one here: "></asp:Label>       
            <br />
            <asp:Button ID="btnParCreateAcc" runat="server" Text="Create New Account" 
                onclick="btnParCreateAcc_Click" />
            <br />
            <br />
            <asp:Button ID="btnParCancel" runat="server" Text="Cancel" 
                onclick="btnParCancel_Click" />
        </asp:Panel>
    </asp:Panel>
    <h2>
        &nbsp;</h2>
    <h2>
        &nbsp;</h2>
    <p>
        To learn more about ASP.NET visit net</a>.
    </p>
</asp:Content>

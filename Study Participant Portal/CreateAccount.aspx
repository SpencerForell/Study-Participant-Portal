<%@ Page Title="Study Participant Portal" Language="C#" MasterPageFile="~/Site.master" 
AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
        <asp:Panel ID="pnlResearcher" runat="server" Visible = "false">
            <asp:Label ID="lblResLogin" runat="server" Text="Researcher Create Account"></asp:Label>
            <br />
            <asp:Label ID="lblResStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />             
            <asp:Label ID="lblResUser" runat="server" Text="User" Width="158px"></asp:Label>
            <asp:TextBox ID="tbResUser" runat="server" Width="173px"></asp:TextBox>
            <br />
            <asp:Label ID="lblResPassword" runat="server" Text="Password" Width="158px"></asp:Label>
            <asp:TextBox ID="tbResPassword" runat="server" Width="158px" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="lblResPasswordConfirm" runat="server" Text="Repeat Password" 
                Width="158px"></asp:Label>
            <asp:TextBox ID="tbResPasswordConfirm" runat="server" TextMode="Password" 
                Width="173px"></asp:TextBox>
            <br />
            <asp:Label ID="lblResEmail" runat="server" Text="Email Address" Width="158"></asp:Label>
            <asp:TextBox ID="tbResEmail" runat="server" Width="125px"></asp:TextBox>
            <br />           
            <br />
            <asp:Button ID="btnResSubmit" runat="server" Text="Submit" 
                onclick="btnResSubmit_Click" /><br />
            <asp:Button ID="btnResCancel" runat="server" Text="Cancel" 
                onclick="btnResCancel_Click"/>
        </asp:Panel>
        <asp:Panel ID="pnlParticipant" runat="server" Visible = "false">
            <asp:Label ID="lblParCreateAccount" runat="server" Text="Participant Create Account"></asp:Label>
            <br />
            <asp:Label ID="lblParStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
            <br />             
            <asp:Label ID="lblParUser" runat="server" Text="User" Width="158px"></asp:Label>
            <asp:TextBox ID="tbParUser" runat="server" Width="173px"></asp:TextBox>
            <br />
            <asp:Label ID="lblParPassword" runat="server" Text="Password" Width="158px"></asp:Label>
            <asp:TextBox ID="tbParPassword" runat="server" Width="158px" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="lblParPasswordConfirm" runat="server" Text="Repeat Password" 
                Width="158px"></asp:Label>
            <asp:TextBox ID="tbParPasswordConfirm" runat="server" TextMode="Password" 
                Width="173px"></asp:TextBox>
            <br />
            <asp:Label ID="lblParEmail" runat="server" Text="Email Address" Width="158"></asp:Label>
            <asp:TextBox ID="tbParEmail" runat="server" Width="125px"></asp:TextBox>
            <br />           
            <br />
            <asp:Button ID="btnParSubmit" runat="server" Text="Submit" 
                onclick="btnParSubmit_Click" /><br />
            <asp:Button ID="btnParCancel" runat="server" Text="Cancel" 
                onclick="btnParCancel_Click"/>
        </asp:Panel>
    </p>
</asp:Content>


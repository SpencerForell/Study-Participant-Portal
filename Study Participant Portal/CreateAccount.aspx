<%@ Page Title="Study Participant Portal" Language="C#" MasterPageFile="~/Site.master" 
AutoEventWireup="true" CodeFile="CreateAccount.aspx.cs" Inherits="CreateAccount" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" Runat="Server">
        <asp:Panel ID="pnlResearcher" runat="server" Visible = "false" GroupingText="Researcher Create Account"> 
            <table>
                <tr>
                    <td colspan=2><asp:Label ID="lblResStatus" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblResUser" runat="server" Text="User Name"></asp:Label></td>
                    <td width=200><asp:TextBox ID="tbResUserName" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblResFirstName" runat="server" Text="First Name"></asp:Label></td>
                    <td><asp:TextBox ID="tbResFirstName" runat="server" Width="100%" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblResLastName" runat="server" Text="Last Name" ></asp:Label></td>
                    <td><asp:TextBox ID="tbResLastName" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblResPassword" runat="server" Text="Password" ></asp:Label></td>
                    <td><asp:TextBox ID="tbResPassword" runat="server" Width="100%" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblResPasswordConfirm" runat="server" Text="Repeat Password" ></asp:Label></td>
                    <td><asp:TextBox ID="tbResPasswordConfirm" runat="server" TextMode="Password" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblResEmail" runat="server" Text="Email Address" ></asp:Label></td>
                    <td><asp:TextBox ID="tbResEmail" runat="server" Width="100%"></asp:TextBox></td>                
                </tr>
                <tr>
                    <td><asp:Button ID="btnResSubmit" runat="server" Text="Submit" onclick="btnResSubmit_Click" /><asp:Button ID="btnResCancel" runat="server" Text="Cancel" onclick="btnResCancel_Click"/></td>
                </tr>
            </table>     
        </asp:Panel>
        <asp:Panel ID="pnlParticipant" runat="server" Visible = "false" GroupingText="Participant Create Account">
            <table>
                <tr >
                    <td colspan=2><asp:Label ID="lblParStatus" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblParUser" runat="server" Text="User Name" ></asp:Label></td>
                    <td width=200><asp:TextBox ID="tbParUserName" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblParFirstName" runat="server" Text="First Name"></asp:Label></td>
                    <td><asp:TextBox ID="tbParFirstName" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblParLastName" runat="server" Text="Last Name"></asp:Label></td>            
                    <td><asp:TextBox ID="tbParLastName" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblParPassword" runat="server" Text="Password" ></asp:Label></td>
                    <td><asp:TextBox ID="tbParPassword" runat="server" Width="100%" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblParPasswordConfirm" runat="server" Text="Repeat Password" ></asp:Label></td>
                    <td><asp:TextBox ID="tbParPasswordConfirm" runat="server" TextMode="Password" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblParEmail" runat="server" Text="Email Address" ></asp:Label></td>
                    <td><asp:TextBox ID="tbParEmail" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnParSubmit" runat="server" Text="Submit" onclick="btnParSubmit_Click" /><asp:Button ID="btnParCancel" runat="server" Text="Cancel" onclick="btnParCancel_Click"/></td>                
                </tr>
            </table>
        </asp:Panel>
</asp:Content>


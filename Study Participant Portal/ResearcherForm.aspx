<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ResearcherForm.aspx.cs" Inherits="ResearcherForm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Researcher Page
    </h2>
    <asp:Panel ID="pnlMain" runat="server" Height="600px">
        <asp:Button ID="btnResEditProfile" runat="server" Text="Edit Profile" Width="100px" onclick="btnResEdit_Click" />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                    <asp:Label ID="lblStudyList" runat="server" Text="Your Studies"></asp:Label><br />
                    <asp:ListBox ID="lboxStudyList" runat="server" Height="204px" Width="401px"></asp:ListBox>
                </td>
                <td>
                    <asp:Button ID="btnResCreate" runat="server" Text="Create Study" Width="100px" onclick="btnResCreate_Click" /><br />
                    <asp:Button ID="btnResEditStdy" runat="server" Text="Edit Study" Width="100px" onclick="btnResEditStdy_Click" /><br />
                    <asp:Button ID="btnResView" runat="server" Text="View Study" Width="100px" onclick="btnResView_Click" /><br />
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    </asp:Content>
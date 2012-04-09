<%@ Page Title="View Study" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="ParticipantStudy.aspx.cs" Inherits="ParticipantStudy" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        View The Study
    </h2>
    <asp:Panel ID="pnlStudy" runat="server">
        <table>
            <tr>
                <td>Name</td>
            </tr>
            <tr>
                <td width="380px"><asp:TextBox ID="tbName" runat="server" Width="100%" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Description</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="tbDescription" TextMode="MultiLine" runat="server" Height="88px" Width="100%" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Button ID="btnShowQuestions" runat="server" Text="Show Questions" onclick="btnShowQuestions_Click"/></td>
            </tr>
        </table>
        <br />
    </asp:Panel>

    <hr />

    <asp:Panel ID="pnlQuals" runat="server">
    </asp:Panel>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit Answers" Visible="false" onclick="btnSubmit_Click" />
    <asp:Label ID="lblError" runat="server" Text="Please Select An Answer For Each Question." Visible="false" ForeColor="Red" />
</asp:Content>

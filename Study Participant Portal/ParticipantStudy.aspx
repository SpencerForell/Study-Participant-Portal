<%@ Page Title="View Study" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
    CodeFile="ParticipantStudy.aspx.cs" Inherits="ParticipantStudy" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent"></asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>
        Study Details
    </h1>
    <asp:Panel ID="pnlStudy" runat="server">
        <br />
        <table>
            <tr>
                <td><asp:Label ID="lblStudyName" runat="server" Text="Study Name:" Font-Bold="true" Font-Size="18px" Width="115px"></asp:Label>
                <asp:Label ID="lblName" runat="server" Font-Size="18px" ></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblResTag" runat="server" Text="Researcher Name:" Font-Bold="true" Font-Size="18px" Width="165px"></asp:Label>
                <asp:Label ID="lblResName" runat="server" Font-Size="18px"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblResEmailTag" runat="server" Text="Researcher Email:" Font-Bold="true" Font-Size="18px" Width="165px"></asp:Label>
                <asp:Label ID="lblResEmail" runat="server" Font-Size="18px"></asp:Label></td>
            </tr>
            <tr>
                <td><br /><asp:Label ID="lblDescriptionTag" runat="server" Text="Short Description:" Font-Bold="true" Font-Size="18px" Width="165px" 
                    Font-Underline="true" ></asp:Label><br /><asp:Label ID="lblDescription" runat="server" Font-Size="18px"></asp:Label></td>
            </tr>
            <tr>
                <td><br /><asp:Button ID="btnShowQuestions" runat="server" Text="Show Questions" onclick="btnShowQuestions_Click"/></td>
            </tr>
        </table>
        <br />
    </asp:Panel>

    <hr />
    <asp:Label ID="lblPreviouslyAnswered" runat="server" Text="You have previously submitted answers for all of the questions." Visible="false" />
    <asp:Panel ID="pnlQuals" runat="server">
    </asp:Panel>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit Answers" Visible="false" onclick="btnSubmit_Click" />
    <asp:Label ID="lblError" runat="server" Text="Please Select An Answer For Each Question." Visible="false" ForeColor="Red" />
</asp:Content>

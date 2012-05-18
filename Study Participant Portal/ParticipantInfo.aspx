<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ParticipantInfo.aspx.cs" Inherits="ParticipantInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Panel ID="Wrapper" runat="server" CssClass="panel">
    <table style="width:100%">
        <tr>
            <td style="width:50%; vertical-align:top;">
                <asp:Panel ID="pnlParticipantInfo" runat="server" GroupingText="Participant Info">
                    <asp:Label ID="lblFullName1" runat="server" Text="Name:" CssClass="label" ></asp:Label>
                    <asp:Label ID="lblFullName2" runat="server" ></asp:Label><br />
                    <asp:Label ID="lblUserName1" runat="server" Text="User Name:" CssClass="label" ></asp:Label>
                    <asp:Label ID="lblUserName2" runat="server" ></asp:Label><br />
                    <asp:Label ID="lblEmail1" runat="server" Text="Email:" CssClass="label" ></asp:Label>
                    <asp:Label ID="lblEmail2" runat="server" ></asp:Label><br /><br />
                    <asp:Button ID="btnShowThisStudy" runat="server" onclick="btnShowThisStudy_Click" Text="Show Questions & Answers for this study" Width="260px" />
                    <br />
                    <asp:Button ID="btnShowAll" runat="server" Text="Show Questions & Answers for all studies" onclick="btnShowAll_Click" Width="260px" /><br />
                </asp:Panel>    
            </td>
            <td >
               <asp:Panel ID="pnlQualifiers" runat="server" GroupingText="Questions/Answers"></asp:Panel>
            </td>
        </tr>
    
    </table>

</asp:Panel>
</asp:Content>


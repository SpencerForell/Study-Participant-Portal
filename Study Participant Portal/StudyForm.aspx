<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudyForm.aspx.cs" Inherits="StudyForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <table>
        <tr>
            <td style="width:50%;">
                <asp:Panel ID="pnlStudyDetails" runat="server" GroupingText="Study Info" Width="100%">
                    <asp:Label ID="lblStdNameHeader" runat="server" Text="Study Name: " CssClass="label" ></asp:Label>
                    <asp:Label ID="lblStdName" runat="server" Text="Study name goes here"></asp:Label><br />
                    <asp:Label ID="lblStdDateHeader" runat="server" Text="Date Created: " CssClass="label" ></asp:Label>
                    <asp:Label ID="lblStdDate" runat="server" Text="Study date created goes here"></asp:Label><br />
                    <asp:Label ID="lblStdDescriptionHeader" runat="server" Text="Description: " CssClass="label" ></asp:Label>
                    <asp:Label ID="lblStdDescription" runat="server" Text="Study description goes here" TextMode="MultiLine" ></asp:Label>
                                    
                    <asp:Panel ID="pnlStdQualifiers"  Width="100%" runat="server" GroupingText="Qualifiers"></asp:Panel>
                </asp:Panel>
                <asp:Button ID="btnFindParticipants" runat="server" Text="Find Participants" onclick="btnFindParticipants_Click" />
            </td>
            <td style="width:49%; vertical-align:top;>               
                   <asp:Panel ID="pnlmatchmakingResults" runat="server"  GroupingText="Participants" Visible="false" Width="100%" ></asp:Panel>
                   <asp:Button ID="btnEmailParticipant" runat="server" Text="Email Participants" Visible="false" onclick="btnEmailParticipant_Click" />
                   <asp:Label ID="lblEmailStatus" runat="server" Visible="false" Text="Emails have been succesfully sent out to the list of participants."></asp:Label>
                
            </td>
        </tr>
    
    </table>

</asp:Content>


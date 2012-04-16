<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StudyForm.aspx.cs" Inherits="StudyForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="pnlStudyDetails" runat="server">
        <asp:Label ID="lblStdName" runat="server" Text="Study name goes here"></asp:Label>
        <br />
        <asp:Label ID="lblStdDate" runat="server" Text="Study date created goes here"></asp:Label>
        <br />
        <asp:Textbox ID="tbStdDescription" runat="server" Text="Study description goes here" TextMode="MultiLine" Enabled="false"></asp:Textbox>

        <asp:Panel ID="pnlStdQualifiers" runat="server">
            <asp:Label ID="lblQualifiers" runat="server" Text="Qualifiers"></asp:Label>
        </asp:Panel>
    </asp:Panel>

    <asp:Button ID="btnFindParticipants" runat="server" Text="Find Participants" 
        onclick="btnFindParticipants_Click" />

    <br />
    <asp:Panel ID="pnlmatchmakingResults" runat="server"></asp:Panel>

</asp:Content>


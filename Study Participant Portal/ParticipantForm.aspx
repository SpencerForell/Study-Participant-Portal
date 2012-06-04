<%@ Page Title="Participant" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ParticipantForm.aspx.cs" Inherits="ParticipantForm" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Panel ID="Wrapper" runat="server" CssClass="panel">
    <h2>
        Participant Page
    </h2>
    <asp:Button ID="btnParEdit" runat="server" Text="Edit Profile" 
            onclick="btnParEdit_Click" />
    <asp:Label ID="lblNoSelection" runat="server" ForeColor="Red" Text="Please select an answer for at least one question and try again." Visible="false" />
    <br />
    <asp:Panel ID="pnlConfirmation" runat="server" Visible="false">
        <asp:Label ID="lblConfirmation" runat="server" Font-Size="14px" Text="Thank you for your contributions. Should you be selected for a study, a researcher will contact you." />
        <br />
        <br />
        <br />
        <asp:Button ID="btnConfirm" runat="server" Text="Back To Studies" onclick="btnConfirm_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlCrossroad" runat="server" Visible="true">
        <asp:Label ID="lblChoice" runat="server" CssClass="label" Text="There are requirement questions that you have not answered! Would you like to answer them now?"></asp:Label>
        <br />
        <asp:Label ID="lblQuestionChoice" runat="server" Text="Yes! Let me answer all the questions now!"></asp:Label>
        <asp:Button ID="btnGoQuestions" runat="server" Text="GO" onclick="btnGoQuestions_Click" />
        <br />
        <asp:Label ID="lblStudyChoice" runat="server" Text="No thanks. Just take me to the list of studies."></asp:Label>
        <asp:Button ID="btnGoStudies" runat="server" Text="GO" onclick="btnGoStudies_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlStudyList" runat="server" Height="329px" Visible="false">     
        <br />
        <br />
        <asp:Label ID="lblStudies" runat="server" Text="Please select a study you are interested in participating in"> </asp:Label>
        <br />
        <asp:ListBox ID="lboxStudyList" runat="server" Height="204px" Width="401px" CssClass="listbox"></asp:ListBox>
        <br /> 
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:Button ID="btnView" runat="server" Text="View Study" onclick="btnView_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlQualList" runat="server" Visible="false" GroupingText="Requirement Questions">    
    </asp:Panel>
    <asp:Button ID="btnSubmitQuestions" runat="server" Enabled="true" onclick="btnSubmitQuestions_Click" Text="Submit" Visible="false" />
    <br />
    <asp:Label ID="lblAnswerError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
</asp:Panel>
</asp:Content>

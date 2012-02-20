<%@ Page Title="Create Your Study" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CreateStudy.aspx.cs" Inherits="CreateStudy" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Create Your Study
    </h2>
    <asp:Panel runat="server">
        <asp:Label ID="lblTitle" runat="server" Text="Provide A Study Title"></asp:Label>
        <br />
        <asp:TextBox ID="tbTitle" runat="server" Width="333px"></asp:TextBox>
        <br />    
        <asp:Label ID="lblDesc" runat="server" Text="Please Describe Your Study."></asp:Label>        
        <br />
        <asp:TextBox ID="tbDescription" TextMode="multiline" runat="server" Height="166px" Width="334px"></asp:TextBox>
        <br />
        (maximum 100 chars please)
        <br />
        <asp:CheckBox ID="cbStdExpired" runat="server" />
        <asp:Label ID="lblExpired" runat="server" Text="This study is expired."></asp:Label>
        <br />
        <asp:Label ID="lblError" runat="server" ForeColor = "Red"></asp:Label>
        <br />
        <asp:Button ID="btnStdQual" runat="server" Text="Select Qualifiers" />
        <br />
        <br />
        <asp:Button ID="btnStdSubmit" runat="server" Text="Submit" 
            onclick="BtnStdSubmit_Click" />
        <asp:Button ID="btnStdCancel" runat="server" Text="Cancel" 
            onclick="BtnStdCancel_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlQuals" runat="server">
    
    </asp:Panel>
    
</asp:Content>

<%@ Page Title="Create Your Study" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CreateStudy.aspx.cs" Inherits="CreateStudy" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Create Your Study
    </h2>
    <asp:Panel>
        <asp:Label ID="titleLable" runat="server" Text="Provide A Study Title"></asp:Label>
        <br />
        <asp:TextBox ID="title" runat="server" Width="333px"></asp:TextBox>
        <br />    
        <asp:Label ID="descLable" runat="server" Text="Please Describe Your Study."></asp:Label>        
        <br />
        <asp:TextBox ID="description" TextMode="multiline" runat="server" Height="166px" Width="334px"></asp:TextBox>
        <br />
        (maximum 100 chars please)
        <br />
        <br />
        <asp:Label ID="errorLable" runat="server" ForeColor = "Red"></asp:Label>
        <br />
        <asp:Button ID="BtnStdQual" runat="server" Text="Select Qualifiers" />
        <br />
        <br />
        <asp:Button ID="BtnStdSubmit" runat="server" Text="Submit" 
            onclick="BtnStdSubmit_Click" />
        <asp:Button ID="BtnStdCancel" runat="server" Text="Cancel" 
            onclick="BtnStdCancel_Click" />
    </asp:Panel>
    <asp:Panel>
    
    </asp:Panel>
    
</asp:Content>

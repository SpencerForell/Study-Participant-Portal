<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ContactInfo.aspx.cs" Inherits="ContactInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Contact info from the creators of the website:
    </h2>
    <asp:Panel runat="server" ID="pnl1" GroupingText="Spencer Forell" Width="220px">
        forells@onid.oregonstate.edu <br />
        Computer Science Major <br />
        Oregon State University <br />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnl2" GroupingText="Max Mueller" Width="220px">
        muellmax@onid.oregonstate.edu <br />
        Computer Science Major <br />
        Oregon State University <br />    
    </asp:Panel>
    <asp:Panel runat="server" ID="pnl3" GroupingText="Dr. Ron Metoyer" Width="220px">
        metoyer@eecs.oregonstate.edu <br />
        Client <br />
        Oregon State University <br />    
    </asp:Panel>
</asp:Content>


<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to the Study Participant Portal! 
    </h2>
    <!-- Panel for getting started-->
    <asp:Panel ID="pnlMain" runat="server">
         To begin, click on the appropriate button below.
        <br /><br />
        <table>
            <tr>
                <th width =100> User Type </th>
                <th> Description </th>
                <th width=150></th>
                <th rowspan=4><asp:Image ID="imgHome" runat="server" Height="150" ImageUrl="~/Images/portalImage.jpg" /></th>
            </tr>
            <tr>
                <td><asp:Button ID="btnResearcher" runat="server" Text="Researcher" Width="100%" onclick="btnResearcher_Click"/></td>
                <td> Researchers can create, edit and view studies</td>
            </tr>
            <tr>
                <td><asp:Button ID="btnParticipant" runat="server" Text="Participant" Width="100%" onclick="btnParticipant_Click"/></td>
                <td>Participants can view and volunteer for studies</td>
            </tr>
            <tr height =100>
                <td></td>
            </tr>
        </table>
    </asp:Panel>
    <br />

    <!-- Panel for study of the week-->
    <asp:Panel ID="pnlWeeklyStudy" runat="server" GroupingText="Study of the week">
        <table border="1" >
            <tr>
                <td><asp:Label ID="lblWeeklyStudyName" runat="server" Width="100px" Text="Name"></asp:Label></td>
                <td><asp:Label ID="lblWeeklyStudyDesc" runat="server" Text="Description of study of the week" ></asp:Label></td>
                <td>Other miscallaneous study of the week things</td>
            </tr>
        </table>
        
    </asp:Panel>

    <!-- Panel for researchers to login -->
    <asp:Panel ID="pnlResearcher" runat="server" Visible = "false" GroupingText="Researcher Login">
        <asp:Label ID="lblResSatus" runat="server" ForeColor="Red" Text=""></asp:Label>
        <br />             
        <asp:Label ID="lblResUser" runat="server" Text="User" Width="75px"></asp:Label>
        <asp:TextBox ID="tbResUser" runat="server" Width="173px"></asp:TextBox>
        <br />
        <asp:Label ID="lblResPassword" runat="server" Text="Password" Width="75px"></asp:Label>
        <asp:TextBox ID="tbResPassword" runat="server" Width="173px" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnResSubmit" runat="server" Text="Submit" Width="75" 
            onclick="btnResSubmit_Click" />
        <br />
        <br />
        <asp:Label ID="lblResCreateAcc" runat="server" Text="Don't have an account? Create one here: "></asp:Label>       
        <br />
        <asp:Button ID="btnResCreateAcc" runat="server" Text="Create New Account" 
            onclick="btnResCreateAcc_Click" />
        <br />
        <br />
        <asp:Button ID="btnResCancel" runat="server" Text="Cancel" 
            onclick="btnResCancel_Click" />
    </asp:Panel>

    <!-- Panel for participants to login -->
    <asp:Panel ID="pnlParticipant" runat="server" Visible = "false" GroupingText="Participant Login">
        <asp:Label ID="lblParUser" runat="server" Text="User" Width="75px"></asp:Label>
        <asp:TextBox ID="tbParUser" runat="server" Width="173px"></asp:TextBox>
        <br />
        <asp:Label ID="lblParPassword" runat="server" Text="Password" Width="75px"></asp:Label>
        <asp:TextBox ID="tbParPassword" runat="server" Width="173px" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnParSubmit" runat="server" Text="Submit" Width="75" 
            onclick="btnParSubmit_Click" />
        <br />
        <br />
        <asp:Label ID="lblParCreateAcc" runat="server" Text="Don't have an account? Create one here: "></asp:Label>       
        <br />
        <asp:Button ID="btnParCreateAcc" runat="server" Text="Create New Account" 
            onclick="btnParCreateAcc_Click" />
        <br />
        <br />
        <asp:Button ID="btnParCancel" runat="server" Text="Cancel" 
            onclick="btnParCancel_Click" />
    </asp:Panel>
</asp:Content>

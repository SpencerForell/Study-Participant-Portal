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
        <table>
            <tr>
                <td colspan=5 style="width:70%">
                <asp:Label runat="server" ID="lblProgramDescription" Width="80%" Text="Our goal is to provide the best experience for researchers and participants to partake in research studies. 
                Researchers can create and modify studies,  while participants can see if they are eligible to take part in studies to receive the incentive offered. Click the appropriate button below
                to get started."></asp:Label>
                </td>
                <td rowspan=6><asp:Image ID="imgHome" runat="server" Height="250" Width="250" ImageUrl="~/Images/case-studies.jpg" /></td>
            </tr>
            <tr>
                <td><br /><br /></td>
            </tr>
            <tr>
                <th width =100> User Type </th>
                <th> Description </th>
                <th width=150></th>
            </tr>
            <tr>
                <td><asp:Button ID="btnResearcher" runat="server" Text="Researcher" Width="100%" onclick="btnResearcher_Click"/></td>
                <td> Researchers can create, edit and view studies.</td>
            </tr>
            <tr>
                <td><asp:Button ID="btnParticipant" runat="server" Text="Participant" Width="100%" onclick="btnParticipant_Click"/></td>
                <td>Participants can view and volunteer for studies.</td>
            </tr>
            <tr height =100>
                <td></td>
            </tr>
        </table>
    </asp:Panel>

    <!-- Panel for study of the week-->
    <asp:Panel ID="pnlWeeklyStudy" runat="server" GroupingText="Most Recent New Study">
        <table border="1" class="null">
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Name: " CssClass="latestStudyHeader"></asp:Label>
                    <asp:Label ID="lblWeeklyStudyName" runat="server" Width="70%" CssClass="latestStudy"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblIncentive" runat="server" Text="Inventive: " CssClass="latestStudyHeader"></asp:Label>
                    <asp:Label ID="lblWeeklyIncentive" runat="server" Width="70%" CssClass="latestStudy"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan=2 style="vertical-align:top">
                    <asp:Label ID="lblDescription" runat="server" Text="Description: " CssClass="latestStudyHeader"></asp:Label>
                    <asp:Label ID="lblWeeklyStudyDesc" Width="80%" runat="server" CssClass="latestStudy"></asp:Label>
                </td>
            </tr>
        </table>
        
    </asp:Panel>

    <!-- Panel for researchers to login -->
    <asp:Panel ID="pnlResearcher" runat="server" Visible = "false" GroupingText="Researcher Login" Width="80%">
        <table>
            <tr>
                <td style="Width:500px">
                
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
                </td>
                <td style="float:right">
                    <asp:Image runat="server" ID="earth" ImageAlign="AbsMiddle" Height="220" ImageUrl="~/Images/earth.png" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <!-- Panel for participants to login -->
    <asp:Panel ID="pnlParticipant" runat="server" Visible = "false" GroupingText="Participant Login" Width="80%">
        <table>
            <tr>
                <td style="width:500px">
                    <asp:Label ID="lblParStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <br />
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
                </td>
                <td style="float:right">
                    <asp:Image runat="server" ID="Image1" ImageAlign="AbsMiddle" Height="220" ImageUrl="~/Images/globe.png" />                
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

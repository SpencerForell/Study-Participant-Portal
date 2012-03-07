<%@ Page Title="Create Your Study" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CreateStudy.aspx.cs" Inherits="CreateStudy" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Create Your Study
    </h2>
    <asp:Panel ID="pnlStudy" runat="server">
        <table>
            <tr>
                <td>Name</td>
            </tr>
            <tr> 
                <td width="380px"><asp:TextBox ID="tbName" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Description (100 chars)</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="tbDescription" TextMode="multiline" runat="server" Height="88px" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblExpired" runat="server" Visible="false" Text="Expired" ></asp:Label></td>
            </tr>
            <tr>
                <td><asp:CheckBox ID="cbStdExpired" runat="server" Visible="false" />
                    <asp:Label ID="lblExpired2" runat="server" Visible="false" Text="Check this if the study has been completed"></asp:Label></td
            </tr>
        </table>  
        <br />
    </asp:Panel>

    <hr />

    <asp:Panel ID="pnlQuals" runat="server">
        <asp:Panel ID="pnlExistingQuals" runat="server" Visible="false" ScrollBars="Auto" >
            <table>
                <tr>
                    <td>Existing Qualifiers</td>
                </tr>
                <tr>
                    <td><asp:ListBox ID="lbQualifiers" runat="server"></asp:ListBox></td>
                </tr>
            </table>
        </asp:Panel>
                
        <h2>
            Please Setup Qualifiers For Your Study.
        </h2>
        <table>
            <tr>
                <td>Qualifier Description</td>
            </tr>
            <tr>
                <td colspan=2 width=200><asp:TextBox ID="tbQualDesc" runat="server" TextMode="MultiLine" Height="100px" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Question to be answered</td>
            </tr>
            <tr>
                <td colspan=2><asp:TextBox ID="tbQuestion" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td><br /></td>
            </tr>
            <tr>
                <td colspan = 2><asp:Label ID="lblErrorAdd" runat="server" ForeColor="Red" Visible="false"></asp:Label></td>
            </tr>
            <tr>
                <td>Possible Answer</td><td>Score</td>
            </tr>
            <tr><td><asp:TextBox ID="tbAnswer" runat="server" Width="100%"></asp:TextBox></td>
                <td><asp:TextBox ID="tbScore" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Click Add to add the current possible answer to the list below</td>
                <td><asp:Button ID="btnAddAnswer" runat="server" Text="Add Answer"  onclick="btnAddAnswer_Click" /></td>
            </tr>
            <tr>
                <td colspan=2><asp:ListBox ID="lbAnswerList" runat="server" Width="100%"></asp:ListBox></td>
            </tr>
            <tr>
                <td colspan=2><asp:Button ID="btnRemove" runat="server" Text="Remove Answer" onclick="btnRemove_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear Answers" onclick="btnClear_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="Edit Answer" onclick="btnEdit_Click" /></td>
            </tr>
        
        </table>
                
        <asp:Label ID="lblQualContinue" runat="server" Text="Please click 'Create Qualifier' if you are ready to submit this qualifier. This will allow you to create other qualifiers as needed."></asp:Label>
        <br />
        <asp:Button ID="btnContinue" runat="server" Text="Create Qualifier" Width="100px" onclick="btnAddQualifier" />
        <br />
        <asp:Label ID="lblErrorCont" runat="server" Text="Please make sure all text fields are completed" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="lblFinished" runat="server" Text="Click 'Finished' if you are done modifying this study."></asp:Label>
        <br />
        <asp:Button ID="btnFinished" runat="server" Text="Finished" Width="100px" onclick="btnFinished_Click" />
        <asp:Button ID="btnStdCancel" runat="server" onclick="BtnStdCancel_Click" Text="Cancel" />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <asp:Label ID="lblErrorFinish" runat="server" Text="Please make sure all text fields are completed" ForeColor="Red" Visible="false"></asp:Label>
    </asp:Panel>
</asp:Content>

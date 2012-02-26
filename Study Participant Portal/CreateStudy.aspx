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
        <asp:Button ID="btnStdQual" runat="server" Text="Select Qualifiers"
            onclick="BtnStdSubmit_Click" /> />
        <br />
        <br />
        <asp:Button ID="btnStdCancel" runat="server" Text="Cancel" 
            onclick="BtnStdCancel_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlQuals" runat="server" Height="564px">
        <h2>
            Please Select Qualifiers For Your Study.
        </h2>
        <br />
        <asp:Label ID="lblQualDesc" runat="server" Text="Please describe a description for this qualifying question."></asp:Label>
        <br />
        <asp:TextBox ID="tbQualDesc" runat="server" TextMode="MultiLine" Height="100px" Width="368"></asp:TextBox>
        <br />
        <asp:Label ID="lblQualQuestion" runat="server" Text="Please phrase your qualifier in the form of a question."></asp:Label>
        <br />
        <asp:TextBox ID="tbQuestion" runat="server" Width="368px"></asp:TextBox>
        <br />
        <asp:Label ID="lblQualAnswer" runat="server" Text="Please provide all answer candatidtes"></asp:Label>
        <asp:Label ID="lblQualRank" runat="server" Text="  Rank of answer"></asp:Label>
        <br />
        <asp:TextBox ID="tbAnswer" runat="server" Width="264px"></asp:TextBox>
        <asp:TextBox ID="tbRank" runat="server" Width="104px"></asp:TextBox>
        <asp:Button ID="btnAddAnswer" runat="server" Text="Add" Width="70px" 
            onclick="btnAddAnswer_Click" />
        <br />
        <asp:Label ID="lblErrorAdd" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:ListBox ID="lbAnswerList" runat="server" Width="368px"></asp:ListBox>
        <br />
        <asp:Button ID="btnRemove" runat="server" Text="Remove Answer" Width="100px" 
            onclick="btnRemove_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear Answers" Width="100px" 
            onclick="btnClear_Click" />
        <asp:Button ID="btnEdit" runat="server" Text="Edit Answer" Width="100px" 
            onclick="btnEdit_Click" />
        <br />
        <br />
        <asp:Label ID="lblQualContinue" runat="server" Text="Please click 'Continue' if you are ready to submit this qualifier and would like to add another one."></asp:Label>
        <br />
        <asp:Button ID="btnContinue" runat="server" Text="Continue" Width="100px" 
            onclick="btnContinue_Click" />
        <br />
        <asp:Label ID="lblErrorCont" runat="server" Text="Please make sure all text fields are completed" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="lblFinished" runat="server" Text="Please click 'Finished' if you are ready to submit this qualifier and do not want to add more."></asp:Label>
        <br />
        <asp:Button ID="btnFinished" runat="server" Text="Finished" Width="100px" 
            onclick="btnFinished_Click" />
        <asp:Label ID="lblErrirFinish" runat="server" Text="Please make sure all text fields are completed" ForeColor="Red"></asp:Label>
    </asp:Panel>
</asp:Content>

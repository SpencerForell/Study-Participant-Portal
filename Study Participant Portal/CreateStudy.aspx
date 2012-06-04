<%@ Page Title="Create Your Study" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CreateStudy.aspx.cs" Inherits="CreateStudy" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Panel ID="Wrapper" runat="server" cssClass="panel">
    <h2>
        Create Your Study
    </h2>
    <asp:Panel ID="pnlStudy" runat="server" >
        <table>
            <tr>
                <td>Name*</td>
            </tr>
            <tr> 
                <td width="380px"><asp:TextBox ID="tbName" runat="server" Width="100%" CssClass="textbox" ToolTip="The name of your study"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Description* (100 chars)</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="tbDescription" TextMode="multiline" runat="server" Height="88px" Width="100%" CssClass="textbox" ToolTip="A brief description of your study that participants will read." ></asp:TextBox></td>
            </tr>
            <tr>
                <td>Incentive* (100 chars)</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="tbIncentive" runat="server" Width="100%" CssClass="textbox" ToolTip="The incentive offered to participant to take part in your study."></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblExpired" runat="server" Visible="false" Text="Expired" ></asp:Label></td>
            </tr>
            <tr>
                <td><asp:CheckBox ID="cbStdExpired" runat="server" Visible="false" />
                    <asp:Label ID="lblExpired2" runat="server" Visible="false" CssClass="label" Text="Check this if the study has been completed"></asp:Label></td>
            </tr>
        </table>  
        <br />
    </asp:Panel>

    <hr />

    <asp:Panel ID="pnlExistingQuals" runat="server" ScrollBars="Auto" >
            <table style="width:400px;">
                <tr>
                    <td>Existing Requirements</td>
                </tr>
                <tr>
                    <td><asp:ListBox ID="lbQualifiers" Height="100px" Width="400px" runat="server" CssClass="textbox"></asp:ListBox></td>
                    <td>
                        <asp:Button ID="btnNewQual" Width="100%" runat="server" Text="New Requirement" onclick="btnNewQual_Click" ToolTip="Create a new requirement to gather additional information or filter participants for your study." />
                        <asp:Button ID="btnEditQual" Width="100%" runat="server" Text="Edit Requirement" onclick="btnEditQual_Click" ToolTip="Edit the requirement question/answers to better suite your needs." /><br />
                        <asp:Button ID="btnDeleteQual" Width="100%" runat="server" Text="Delete Requirement" onclick="btnDeleteQual_Click" ToolTip="Remove the currently selected requirement from the study"/>
                        <asp:Label ID="lblEditQualError" Width="130px" ForeColor="Red" runat="server" Visible="false" Text="Must select a requirement from the box below to edit"></asp:Label></td>
                </tr>
            </table>
        </asp:Panel>

    <asp:Panel ID="pnlQuals" runat="server" Visible="false">
        <table>
            <tr>
                <td style="width:60%;">
                    <asp:Panel ID="pnlNewQuals" runat="server" GroupingText="Requirement Details" >
                        <table>
                            <tr>
                                <td>Requirement Description</td>
                            </tr>
                            <tr>
                                <td colspan=2 style="width:200;"><asp:TextBox ID="tbQualDesc" runat="server" TextMode="MultiLine" Height="100px" Width="100%" CssClass="textbox" ToolTip="Any additional info about the requirement."></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Question to be answered*</td>
                            </tr>
                            <tr>
                                <td colspan=2><asp:TextBox ID="tbQuestion" runat="server" Width="100%" CssClass="textbox" ToolTip="The question the participant will be answering."></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td><br /></td>
                            </tr>
                            <tr>
                                <td colspan = 2><asp:Label ID="lblErrorAdd" runat="server" Text="Please provide an answer, and a score. The score must be between -1-9" ForeColor="Red" Visible="false"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlAnswers" runat="server" GroupingText="Answer Details">
                                        <table>
                                        <tr>
                                            <td>Possible Answer</td><td>Score</td>
                                        </tr>
                            
                                        <tr><td><asp:TextBox ID="tbAnswer" runat="server" Width="100%" CssClass="textbox" ToolTip="A possible answer to the question stated above."></asp:TextBox></td>
                                            <td><asp:TextBox ID="tbScore" runat="server" Width="100%" CssClass="textbox" ToolTip="A high score will rank a participant higher when matched, a negative score will make them ineligible to participate in the Study, a score of 0 will have no affect." ></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Click &#39;Save Answer&#39; to add the current possible answer to the list below</td>
                                            <td><asp:Button ID="btnAddAnswer" runat="server" Text="Save Answer"  onclick="btnAddAnswer_Click" ToolTip="Add possible answer & its score to the list of possible answers below" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan=2><asp:ListBox ID="lbAnswerList" runat="server" Width="100%" CssClass="listbox" ViewStateMode="Enabled" ToolTip="List of all answers and their associated scores [in brackets] to this specific requirement."></asp:ListBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan=2>
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit Answer" onclick="btnEdit_Click" ToolTip="Edit the selected answer" />
                                            <asp:Button ID="btnRemove" runat="server" Text="Remove Answer" onclick="btnRemoveAnswer_Click" ToolTip="Remove the currently selected answer" />
                                            <asp:Button ID="btnClear" runat="server" Text="Clear Answers" onclick="btnClear_Click" ToolTip="Remove all possible answers" />
                                            </td>                  
                                        </tr>
                                        </table>
                                    </asp:Panel>  
                                </td>
                            </tr>      
                        </table>
                        <asp:Label ID="lblQualContinue" runat="server" CssClass="label" Text="Click 'Save Requirement' if you are ready to submit this requirement. You can create additional requirements after saving this one."></asp:Label>
                        <br />
                        <asp:Button ID="btnContinue" runat="server" Text="Save Requirement" Width="120px" onclick="btnSaveQualifier" ToolTip="Save the current requirement. This will save all of the answers associated to the requirement as well." />
                        <asp:Button ID="btnQualCancel" runat="server" Text="Cancel" onclick="btnQualCancel_Click" ToolTip="You will lose all changes to this requirement and go back to viewing the list of current requirements for this study" />
                        <br />
                        <asp:Label ID="lblErrorCont" runat="server" Text="Please make sure all required fields are completed" ForeColor="Red" Visible="false"></asp:Label>
                        <br />
                    </asp:Panel>
                </td>
                <td style="width:40%; vertical-align:top;">
                    <asp:Panel ID="pnlPreExistingQuals" runat="server" GroupingText="Select Pre-Existing Requirements">
                        <table>
                            <tr>
                                <td>Select from the list of pre-defined requirements below.</td>
                            </tr>
                            <tr>
                                <td><asp:ListBox ID="lbPreDefinedQuals" runat="server" Height="435px" Width="357px" CssClass="listbox"></asp:ListBox></td>
                            </tr>
                            <tr>
                                <td><asp:Button ID="btnAddQual" runat="server" Text="Add Requirement" onclick="btnAddQual_Click" ToolTip="Add the pre-defined requirement to your study. Note: You cannot edit any of these requirements, so make sure the answers and scores are what you desire." />
                                    <asp:Button ID="btnRemoveQual" runat="server" Text="Remove From Requirement Details" onclick="btnRemoveQual_Click" ToolTip="Click to remove a selected pre-existing requirement from the requirement details panel."/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel> 
                </td>
            </tr>
        </table>
    </asp:Panel>
        <asp:Label ID="lblFinished" runat="server" CssClass="label" Text="Click 'Finished' to save this study or 'Cancel' to exit without saving."></asp:Label>
        <br />
        <asp:Button ID="btnFinished" runat="server" Text="Finished" Width="120px" onclick="btnFinished_Click" ToolTip="Permanently save this study" />
        <asp:Button ID="btnStdCancel" runat="server" onclick="BtnStdCancel_Click" Text="Cancel" ToolTip="Undo any changes you made since your last save to this study." />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <asp:Label ID="lblErrorFinish" runat="server" Text="Please make sure all text fields are completed" ForeColor="Red" Visible="false"></asp:Label>
</asp:Panel>
</asp:Content>

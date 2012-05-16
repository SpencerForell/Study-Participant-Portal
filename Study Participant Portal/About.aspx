<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

    <asp:Panel runat="server" ID="pnlAbout" Width="70%" GroupingText="Background Information">
        <asp:Label runat="server" ID="lblInfo" Width="100%">
        
            Background: Many professors conduct research at Oregon State University
            and perform various studies to gather information. These studies need 
            specific participants that meet a certain criteria in order to be eligible 
            to get information from them. Each study usually offers some incentive for 
            Participants to want to take part in a study.
            <br /><br />

            Purpose: Our Application is meant to match the best possible matching participants
            with studies that different researchers have created. A researcher can perform a 
            custom made matching algorithm that will find and display a list of the best matches
            and view additional information about the participants, such as answers to all of the
            critera for each study and their contact information. This gives Researchers the ability 
            to quickly find and contact matches for their studies without having to spend time manually
            filtering through several users checking if they meet their criteria.
            <br /><br />
            There are two users of this application: Researcher & Participants.
            <br /><br />
            Researchers create studies, run the matchmaking algorithm, and contact participants they want
            to participant in their studies. 
            <br /><br />
            Participants answers questions for different studies that researchers have created. These answers
            will be used to match them to the studies they qualify for. They can choose to take part in a study
            after being contacted by the researcher.
        </asp:Label>
    </asp:Panel>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" Title="LETTER" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabHistory" runat="server" HideTab="true">
        <dct:Tab ID="HistoryTab" runat="server" HeaderText= "EXAM HISTORY">
            <dct:ExamPanel id="pnlHistory" runat="server" HeaderText="LETTER">

            </dct:ExamPanel>            
        </dct:Tab>
    </dct:tabs> 
</asp:Content>

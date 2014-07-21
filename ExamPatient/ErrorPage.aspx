<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" Title="Error" %>
<%@ Register TagPrefix="dct" Namespace="Exam" Assembly="App_Code" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dct:tabs ID="tabError" runat="server" HideTab="true">
        <dct:Tab ID="ErrorTab" runat="server" HeaderText= "Error">
            <asp:Label ID="resultError" runat="server" Text="" SkinID="skinError" Width="500"></asp:Label>
        </dct:Tab>
    </dct:tabs> 
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JGTZmainpage.aspx.cs" Inherits="JGTZ.JGTZmainpage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <table class="table table-condensed">
            <tr>
                <td style="width: 221px">
                    人员代码：
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td style="width: 267px">
                    人员名称：
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td style="width: 262px">
                    <asp:Button ID="Button2" runat="server" Text="查询" OnClick="Button2_Click" Width="48px" />
                </td>
            </tr>>
            <tr>
                <td style="width: 221px">
                     <div style="overflow:auto;height:100%;">
                          <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" Height="174px" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                         </asp:TreeView>
                    </div>
                </td>
                <td style="width: 267px">
                    <div>
                         <asp:ListBox ID="ListBox1" runat="server" Height="300px" Font-Size="Large" Width="200px"></asp:ListBox>
                    </div>
                </td>
                <td style="width: 262px">
                     <div>
                        &nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交" />
                    </div>
                </td>
            </tr>
        </table>
    </asp:Content>
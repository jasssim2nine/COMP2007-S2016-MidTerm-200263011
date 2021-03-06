﻿<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200263011.TodoList" %>

<%--
    Author Name : Jasim Khan
    student id : 200263011
    date : 13-06-16
    description : TodoList page to display the woks to do.
      --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
             <div class="col-md-offset-2 col-md-8">
                 <h1>Todo List</h1>
                    <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Todo</a>
                   
                 <!-- Paging -->
                 <div>
                  <label for="PageSizeDropDownList"> Records Per Page: </label>
                <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                     AutoPostBack="true" CssClass="btn btn-default btn-sm dropdown-toggle"
                     OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                   <asp:ListItem Text="3" Value="3"></asp:ListItem>
                   <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="All" Value="1000"></asp:ListItem>
               </asp:DropDownList>
                  </div>

    <asp:GridView ID="ToDoListGridView" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false" 
         DataKeyNames="TodoID" OnRowDeleting="ToDoListGridView_RowDeleting" AllowPaging="true" PageSize="3"
         OnPageIndexChanging="ToDoListGridView_PageIndexChanging" AllowSorting="true" OnSorting="ToDoListGridView_Sorting"
         OnRowDataBound="ToDoListGridView_RowDataBound" PagerStyle-CssClass="pagination-ys"
         runat="server">
        <Columns>
            <asp:BoundField  DataField="TodoID" HeaderText="TodoID" Visible="false" />
            <asp:BoundField  DataField="TodoName" HeaderText="To do" Visible="true" SortExpression="TodoName"/>
            <asp:BoundField  DataField="TodoNotes" HeaderText="Notes" Visible="true" SortExpression="TodoNotes"/>
            <asp:BoundField DataField="Completed" HeaderText="Completed" Visible="false"/>
            <asp:TemplateField>
                <ItemTemplate>
            <asp:CheckBox DataField="Completed" HeaderText="Completed" SortExpression="Completed" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'/> Edit"
                  navigateurl="~/TodoDetails.aspx" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server" 
                  DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}"
                               />
            <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'/>Delete"
                              ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
            
        </Columns>    
        </asp:GridView>
      </div>
   </div>
 </div>
</asp:Content>

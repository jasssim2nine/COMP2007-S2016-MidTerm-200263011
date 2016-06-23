using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using statements to connect ef
using COMP2007_S2016_MidTerm_200263011.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/*
* Author : Jasim Khan
* Student# : 200263011
* Description : Department page to display different departments with CRUD functionality.
*/
namespace COMP2007_S2016_MidTerm_200263011
{
    public partial class TodoList : System.Web.UI.Page
    {


        /// <summary>
        /// This method kicks off when page loads.
        /// </summary>
        /// @Param (object) sender
        /// @Param (EventArgs) e
        /// @returns (void)
        protected void Page_Load(object sender, EventArgs e)
        {
                if(!IsPostBack)
            {
                //create a session variable and stored as default
                Session["SortColumn"] = "TodoID";
                Session["SortDirection"] = "ASC";
                this.GetTodoList();
            }
        }


        /// <summary>
        /// This method connects to db and creates a query.
        /// </summary>
        /// @Param () 
        /// @method (GetTodoList) 
        /// @returns (void)

        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetTodoList()
        {
            using (TodoConnection tbc = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                var ToDo = (from allList in tbc.Todos
                                   select allList);
                ToDoListGridView.DataSource = ToDo.AsQueryable().OrderBy(SortString).ToList();
                ToDoListGridView.DataBind();
            }
        }


        /// <summary>
        /// This handler set the no. of records to be displayed
        /// </summary>
        /// @Param (object) sender
        /// @Param (EventArgs) e
        /// @returns (void)
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set the page size
            ToDoListGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);
            //refresh the grid
            this.GetTodoList();
        }

        /// <summary>
        /// This handler deletes the department using EF 
        /// @Param (object) sender
        /// @Param (GridViewDeleteEventArgs) e
        /// @returns (void)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoListGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store the row which is clicked
            int selectedRow = e.RowIndex;

            //get the selected department id using department datakey 
            int TodoID = Convert.ToInt32(ToDoListGridView.DataKeys[selectedRow].Values["TodoID"]);
            // using ef to find the selected department 
            using (TodoConnection tdc = new TodoConnection())
            {
                //create object of department class and store the query
                Todo deletedrecord = (from todorecord in tdc.Todos
                                      where todorecord.TodoID == TodoID
                                      select todorecord).FirstOrDefault();

                //remove the selected todo from the db
                tdc.Todos.Remove(deletedrecord);
                // save my changes back to the database
                tdc.SaveChanges();

                //refresh the grid
                this.GetTodoList();
            }
        }

        /// <summary>
        /// This handler takes care of paging
        /// </summary>
        /// @Param (object) sender
        /// @Param (GridViewPageEventArgs) e
        /// @returns (void)
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoListGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the new page number
            ToDoListGridView.PageIndex = e.NewPageIndex;

            //refresh the grid
            this.GetTodoList();
        }

        /// <summary>
        /// This handler handles sorting
        /// </summary>
        /// @Param (object) sender
        /// @Param (GridViewSotEventArgs) e
        /// @returns (void)
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoListGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            //refresh the grid
            this.GetTodoList();

            //create a toggle for the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        /// <summary>
        /// This method adds the caret to the headers of the table..
        /// </summary>
        /// @Param (object) sender
        /// @Param (GridViewRowEventArgs) e
        /// @Param (void)
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToDoListGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < ToDoListGridView.Columns.Count - 1; index++)
                    {
                        if (ToDoListGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }

        }
    }
}
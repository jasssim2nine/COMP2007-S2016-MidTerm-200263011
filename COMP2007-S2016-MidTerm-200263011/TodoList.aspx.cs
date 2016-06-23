using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                var toDo = (from allList in tbc.Todos
                                   select allList);
                ToDoListGridView.DataSource = toDo.ToList();
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
    }
}
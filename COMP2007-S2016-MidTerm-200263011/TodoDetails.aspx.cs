using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using statements to connect to db.
using COMP2007_S2016_MidTerm_200263011.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/**
 * Author : Jasim Khan
 * student# : 200263011
 * description : todo details page to display a form to edit/save a new todo record.
 */
namespace COMP2007_S2016_MidTerm_200263011
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        /// <summary>
        /// This method kicks off when page loads.
        /// </summary>
        /// @Param (object) sender
        /// @Param (EventArgs) e
        /// @returns (void)
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
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

        protected void GetTodoList()
        {
            //populate the form with existing data
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);
            //connect to db
            using (TodoConnection tdc = new TodoConnection())
            {
                // query the Todo Table using EF and LINQ
                Todo updatedList = (from todo in tdc.Todos
                                        where todo.TodoID == TodoID
                                        select todo).FirstOrDefault();
                // map the todo data...
                if (updatedList!= null)
                {
                    TodoNameTextBox.Text = updatedList.TodoName;
                    TodoNotesTextBox.Text = updatedList.TodoNotes;
                    CompletedCheckBox.Checked = updatedList.Completed.Value;
                }
            }
  
        }

        /// <summary>
        /// This onclick event to cancel and redirect to original page.
        /// </summary>
        /// @Param (object)
        /// @Param (EventArgs) e
        /// @Param returns (void)
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TodoList.aspx");
        }


        /// <summary>
        /// This onclick event to save-update & insert the query. refresh the list and redirects to original page.
        /// </summary>
        /// @Param (object)
        /// @Param (EventArgs) e
        /// @Param returns (void)
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //use EF connect to database server
            using (TodoConnection tdc = new TodoConnection())
            {
                Todo newTodo = new Todo();

                int TodoID = 0;

                if(Request.QueryString.Count > 0)
                {
                    //get the id from the url
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);
                    //get the current department from the EF
                    newTodo = (from todo in tdc.Todos
                               where todo.TodoID == TodoID
                               select todo).FirstOrDefault();
                }
                //add form data to new todo record...
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
                newTodo.Completed = CompletedCheckBox.Checked;
                
                if (TodoID == 0)
                {
                    tdc.Todos.Add(newTodo);
                }
                //save our changes & update & inserts.
                tdc.SaveChanges();
                //redirect back to the updated department page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}
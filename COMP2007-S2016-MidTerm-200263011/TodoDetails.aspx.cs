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

namespace COMP2007_S2016_MidTerm_200263011
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodoList();
            }
        }

        protected void GetTodoList()
        {
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            using (TodoConnection tdc = new TodoConnection())
            {
                Todo updatedList = (from todo in tdc.Todos
                                        where todo.TodoID == TodoID
                                        select todo).FirstOrDefault();

                if(updatedList!= null)
                {
                    TodoNameTextBox.Text = updatedList.TodoName;
                    TodoNotesTextBox.Text = updatedList.TodoNotes;
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

            using (TodoConnection tdc = new TodoConnection())
            {
                Todo newTodo = new Todo();

                int TodoID = 0;

                if(Request.QueryString.Count > 0)
                {
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    newTodo = (from todo in tdc.Todos
                               where todo.TodoID == TodoID
                               select todo).FirstOrDefault();
                }
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;

                if(TodoID == 0)
                {
                    tdc.Todos.Add(newTodo);
                }

                tdc.SaveChanges();
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SarasaviLibraryManagementSystem
{
    public partial class Return : Form
    {
        Functions Con;
        public Return()
        {
            InitializeComponent();
            Con = new Functions();
        }

        private void LoanLbl_Click_1(object sender, EventArgs e)
        {
            Loan Obj = new Loan();
            Obj.Show();
            this.Hide();
        }

        private void ReserveLbl_Click_1(object sender, EventArgs e)
        {
            Reserve Obj = new Reserve();
            Obj.Show();
            this.Hide();
        }

        private void InquiryLbl_Click_1(object sender, EventArgs e)
        {
            Inquiry Obj = new Inquiry();
            Obj.Show();
            this.Hide();
        }

        private void CatalogLbl_Click_1(object sender, EventArgs e)
        {
            Catalog Obj = new Catalog();
            Obj.Show();
            this.Hide();
        }

        private void RegisterLbl_Click_1(object sender, EventArgs e)
        {
            Register Obj = new Register();
            Obj.Show();
            this.Hide();
        }

        private void BookNumberTb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string BookNumber = BookNumberTb.Text;

                if (!string.IsNullOrEmpty(BookNumber))
                {
                    string query = "SELECT * FROM Book WHERE BookNumber = @BookNumber";

                    DataTable dt = Con.GetParameterizedData(query, "@BookNumber", BookNumber);

                    if (dt.Rows.Count > 0)
                    {
                        string BookTitle = dt.Rows[0]["Title"].ToString();
                        TitleTb.Text = BookTitle;
                    }
                    else
                    {
                        TitleTb.Text = "Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserNumberTb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string UserNumber = UserNumberTb.Text;

                if (!string.IsNullOrEmpty(UserNumber))
                {
                    string query = "SELECT * FROM [User] WHERE UserId = @UserNumber";

                    DataTable dt = Con.GetParameterizedData(query, "@UserNumber", UserNumber);

                    if (dt.Rows.Count > 0)
                    {
                        string UserName = dt.Rows[0]["UserName"].ToString();
                        UserNameTb.Text = UserName;
                    }
                    else
                    {
                        UserNameTb.Text = "Not Found";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (BookNumberTb.Text == "" || TitleTb.Text == "" || UserNumberTb.Text == "" || UserNameTb.Text == "")
            {
                MessageBox.Show("Missing Data!");
            }
            else
            {
                try
                {
                    string BookNumber = BookNumberTb.Text;

                    string Query = "DELETE FROM Loan WHERE BookNumber = @BookNumber";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                new SqlParameter("@BookNumber", BookNumber)
                    };

                    Con.SetParameterizedData(Query, parameters);
                    MessageBox.Show("Return Successful!");

                    UserNumberTb.Text = "";
                    UserNameTb.Text = "";


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


    }
}

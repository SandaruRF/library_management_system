using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SarasaviLibraryManagementSystem
{
    public partial class Reserve : Form
    {
        Functions Con;
        public Reserve()
        {
            InitializeComponent();
            Con = new Functions();
        }

        private void RegisterLbl_Click(object sender, EventArgs e)
        {
            Register Obj = new Register();
            Obj.Show();
            this.Hide();
        }

        private void CatalogLbl_Click(object sender, EventArgs e)
        {
            Catalog Obj = new Catalog();
            Obj.Show();
            this.Hide();
        }

        private void InquiryLbl_Click(object sender, EventArgs e)
        {
            Inquiry Obj = new Inquiry();
            Obj.Show();
            this.Hide();
        }

        private void ReturnLbl_Click(object sender, EventArgs e)
        {
            Return Obj = new Return();
            Obj.Show();
            this.Hide();
        }

        private void LoanLbl_Click(object sender, EventArgs e)
        {
            Loan Obj = new Loan();
            Obj.Show();
            this.Hide();
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (SearchTb.Text == "")
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    string Search = SearchTb.Text;

                    string Query = $"Select * from Book " +
                                   $"where Title like '%{Search}%' " +
                                   $"or BookNumber like '%{Search}%'";

                    BookList.DataSource = Con.GetData(Query);

                    //SearchTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void BookList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ReserveStatusTb.Text = BookList.SelectedRows[0].Cells[8].Value.ToString();
            CopyStatusTb.Text = BookList.SelectedRows[0].Cells[6].Value.ToString();

            int TotalCopies = Convert.ToInt32(BookList.SelectedRows[0].Cells[5].Value);
            int LoanedCopies = Convert.ToInt32(BookList.SelectedRows[0].Cells[7].Value);
            int ReservedCopies = Convert.ToInt32(BookList.SelectedRows[0].Cells[8].Value);
            int AvailableCopies = TotalCopies - (LoanedCopies + ReservedCopies);

            if (AvailableCopies > 0)
            {
                //AvailabilityTb.Text = "Available";
            }
            else
            {
                //AvailabilityTb.Text = "Not Available";
            }
        }

        private void ReserveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserNameTb.Text == "" || UserNumberTb.Text == "")
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {

                    MessageBox.Show("Reservation Successfull!");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SarasaviLibraryManagementSystem
{
    public partial class Loan : Form
    {
        Functions Con;
        public Loan()
        {
            InitializeComponent();
            Con = new Functions();
        }

        private void BookNumberTb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string BookNum = BookNumberTb.Text;

                if (!string.IsNullOrEmpty(BookNum))
                {
                    string query = "SELECT * FROM Book WHERE BookNumber = @BookNum";

                    DataTable dt = Con.GetParameterizedData(query, "@BookNum", BookNum);

                    if (dt.Rows.Count > 0)
                    {
                        string BookStatus = dt.Rows[0]["CopyStatus"].ToString();
                        CopyStatusTb.Text = BookStatus;
                    }
                    else
                    {
                        CopyStatusTb.Text = "Not Found";
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
                        NameTb.Text = UserName;

                        string Borrowed = dt.Rows[0]["BorrowedBooks"].ToString();
                        BorrowedTb.Text = Borrowed;

                        string Overdue = dt.Rows[0]["OverdueBooks"].ToString();
                        OverdueTb.Text = Overdue;
                    }
                    else
                    {
                        NameTb.Text = "Not Found";
                        BorrowedTb.Text = "Not Found";
                        OverdueTb.Text = "Not Found";
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
            if (BookNumberTb.Text == "" || CopyStatusTb.Text == "" || UserNumberTb.Text == "" || NameTb.Text == "" || BorrowedTb.Text == "" || OverdueTb.Text == "")
            {
                MessageBox.Show("Missing Data!");
            }else if(Convert.ToInt32(BorrowedTb.Text) > 5)
            {
                MessageBox.Show("Borrowing limit is 5 books!");
            }else if(CopyStatusTb.Text == "Reference")
            {
                MessageBox.Show("Refernece only, Can't borrow!");
            }else if ((ReturnDateTb.Value - TodayDateTb.Value).Days != 14)
            {
                MessageBox.Show("Borrowed period should be two weeks!");
            }
            else
            {
                string BookNumber = BookNumberTb.Text;
                string UserNumber = UserNumberTb.Text;
                string Date = TodayDateTb.Value.ToString("yyyy-MM-dd");
                string ReturnDate = ReturnDateTb.Value.ToString("yyyy-MM-dd");

                string Query = $"INSERT INTO Loan (BookNumber, UserNumber, TodayDate, ReturnDate) " +
                                   $"VALUES ('{BookNumber}', '{UserNumber}', '{Date}', '{ReturnDate}')";

                Con.SetData(Query);
                MessageBox.Show("Loan Successfull!");

                BookNumberTb.Text = "";
                CopyStatusTb.Text = "";
                UserNumberTb.Text = "";
                NameTb.Text = "";
                BorrowedTb.Text = "";
                OverdueTb.Text = "";
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Loan Canceled!");

            BookNumberTb.Text = "";
            CopyStatusTb.Text = "";
            UserNumberTb.Text = "";
            NameTb.Text = "";
            BorrowedTb.Text = "";
            OverdueTb.Text = "";
        }

        private void ReturnLbl_Click_1(object sender, EventArgs e)
        {
            Return Obj = new Return();
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
    }
}

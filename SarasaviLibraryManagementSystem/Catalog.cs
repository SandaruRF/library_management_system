using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SarasaviLibraryManagementSystem
{
    public partial class Catalog : Form
    {
        Functions Con;
        public Catalog()
        {
            InitializeComponent();
            Con = new Functions();
            ShowBooks();
        }

        private void ShowBooks()
        {
            string Query = "Select * from Book";
            BookList.DataSource = Con.GetData(Query);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClassificationTb.Text == "" || BookTitleTb.Text == "" || PublisherTb.Text == "" || CopiesTb.Text == "" || CopyStatusCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    string Classification = ClassificationTb.Text;
                    string BookTitle = BookTitleTb.Text;
                    string Publisher = PublisherTb.Text;
                    int Copies = Convert.ToInt32(CopiesTb.Text);
                    string CopyStatus = CopyStatusCb.SelectedItem.ToString();

                    List<string> bookNumbers = GenerateBookNumbers(Classification, Copies);

                    foreach (string bookNumber in bookNumbers)
                    {
                        string Query = $"INSERT INTO Book (BookNumber, Classification, Title, Publisher, Copies, CopyStatus, LoanedCopies, ReservedCopies) " +
                                       $"VALUES ('{bookNumber}', '{Classification}', '{BookTitle}', '{Publisher}', {Copies}, '{CopyStatus}', {0}, {0})";
                        Con.SetData(Query);
                    }

                    MessageBox.Show("Book(s) Added Successfully!");
                    ShowBooks();
                    ClassificationTb.Text = "";
                    BookTitleTb.Text = "";
                    PublisherTb.Text = "";
                    CopiesTb.Text = "";
                    CopyStatusCb.SelectedIndex = -1;

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private List<string> GenerateBookNumbers(string classification, int copies)
        {
            List<string> bookNumbers = new List<string>();

            string Query = $"SELECT MAX(BookNumber) FROM Book WHERE Classification = '{classification}'";
            DataTable dt = Con.GetData(Query);

            int currentMaxNumber = 0;
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                string maxBookNumber = dt.Rows[0][0].ToString();
                if (int.TryParse(maxBookNumber.Substring(1, 4), out int maxNumber))
                {
                    currentMaxNumber = maxNumber;
                }
            }

            currentMaxNumber++;
            for (int i = 1; i <= copies; i++)
            {
                string bookNumber = $"{classification}{currentMaxNumber:D4}";
                if (copies > 1)
                {
                    bookNumber += i.ToString();
                }
                bookNumbers.Add(bookNumber);
            }

            return bookNumbers;
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            ClassificationTb.Text = "";
            BookTitleTb.Text = "";
            PublisherTb.Text = "";
            CopiesTb.Text = "";
            CopyStatusCb.SelectedIndex = -1;
        }

        private void RegisterLbl_Click(object sender, EventArgs e)
        {
            Register Obj = new Register();
            Obj.Show();
            this.Hide();
        }

        private void InquiryLbl_Click(object sender, EventArgs e)
        {
            Inquiry Obj = new Inquiry();
            Obj.Show();
            this.Hide();
        }

        private void ReserveLbl_Click(object sender, EventArgs e)
        {
            Reserve Obj = new Reserve();
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
    }
}

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
    public partial class Register : Form
    {
        Functions Con;

        public Register()
        {
            InitializeComponent();
            Con = new Functions();
            ShowUsers();
        }

        private void ShowUsers()
        {
            string Query = "Select * from [User]";
            UserList.DataSource = Con.GetData(Query);
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (NameTb.Text == "" || SexCb.SelectedIndex == -1 || NICNoTb.Text == "" || AddressTb.Text == "")
                {
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    string UserName = NameTb.Text;
                    string Sex = SexCb.SelectedItem.ToString();
                    string NICNo = NICNoTb.Text;
                    string Address = AddressTb.Text;

                    string Query = $"INSERT INTO [User] (UserName, Sex, NICNo, Address, BorrowedBooks, OverdueBooks) " +
                                   $"VALUES ('{UserName}', '{Sex}', '{NICNo}', '{Address}', {0}, {0})";
                    Con.SetData(Query);

                    MessageBox.Show("User Added Successfully!");
                    ShowUsers();

                    NameTb.Text = "";
                    SexCb.SelectedIndex = -1;
                    NICNoTb.Text = "";
                    AddressTb.Text = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            NameTb.Text = "";
            SexCb.SelectedIndex = -1;
            NICNoTb.Text = "";
            AddressTb.Text = "";
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SmallBusinessManagementSystem.Model;
using SmallBusinessManagementSystem.Manager;
using System.Net.Mail;

namespace SmallBusinessManagementSystem.UI
{
    public partial class CustomerUI : Form
    {
        public CustomerUI()
        {
            InitializeComponent();
        }

        CustomerManager _customerManager = new CustomerManager();
        public CustomerModel customer;

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveButton.Text == "Save")
            {
                customer = new CustomerModel();
                if (String.IsNullOrEmpty(codeTextBox.Text))
                {
                    MessageBox.Show("Code can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (codeTextBox.Text.Count() != 4)
                {
                    MessageBox.Show("Code Must be 4 length !!", "Code Length Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    codeTextBox.Clear();
                    return;
                }
                customer.Code = codeTextBox.Text;

                if (String.IsNullOrEmpty(nameTextBox.Text))
                {
                    MessageBox.Show("Name can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                customer.Name = nameTextBox.Text;
                if (String.IsNullOrEmpty(addressTextBox.Text))
                {
                    MessageBox.Show("Address can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                customer.Address = addressTextBox.Text;

                if (String.IsNullOrEmpty(emailTextBox.Text))
                {
                    MessageBox.Show("Email can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (IsValid(emailTextBox.Text))
                {
                    customer.Email = emailTextBox.Text;
                }
                else
                {
                    MessageBox.Show("Email is not Valid !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (String.IsNullOrEmpty(contactTextBox.Text))
                {
                    MessageBox.Show("Contact can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (contactTextBox.Text.Count() != 11)
                {
                    MessageBox.Show("Contact Must be 11 length !!", "Contact Length Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    contactTextBox.Clear();
                    return;
                }
                customer.Contact = contactTextBox.Text;

                if (String.IsNullOrEmpty(loyalityPointTextBox.Text))
                {
                    customer.LoyalityPoint = 0;
                }
                else if (loyalityPointTextBox.Text.StartsWith("-"))
                {
                    MessageBox.Show("Loyality Point can not e Negative !!", "Loyality Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loyalityPointTextBox.Clear();
                    return;
                }
                else
                {
                    customer.LoyalityPoint = Convert.ToInt32(loyalityPointTextBox.Text);
                }

                bool isExecute = _customerManager.Save(customer);
                if (isExecute)
                {
                    MessageBox.Show("Saved!!");
                }
                else
                {
                    MessageBox.Show("Not Saved!!");
                }

                codeTextBox.Clear();
                nameTextBox.Clear();
                addressTextBox.Clear();
                emailTextBox.Clear();
                contactTextBox.Clear();
                loyalityPointTextBox.Clear();
                showDataGridView.DataSource = _customerManager.ShowAll();
            }
            else if (saveButton.Text == "Update")
            {
                customer = new CustomerModel();
                customer.Code = codeTextBox.Text;
                if (String.IsNullOrEmpty(nameTextBox.Text))
                {
                    MessageBox.Show("Name can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                customer.Name = nameTextBox.Text;
                if (String.IsNullOrEmpty(addressTextBox.Text))
                {
                    MessageBox.Show("Address can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                customer.Address = addressTextBox.Text;

                if (String.IsNullOrEmpty(emailTextBox.Text))
                {
                    MessageBox.Show("Email can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (IsValid(emailTextBox.Text))
                {
                    customer.Email = emailTextBox.Text;
                }
                else
                {
                    MessageBox.Show("Email is not Valid !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (String.IsNullOrEmpty(contactTextBox.Text))
                {
                    MessageBox.Show("Contact can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (contactTextBox.Text.Count() != 11)
                {
                    MessageBox.Show("Contact Must be 11 length !!", "Contact Length Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    contactTextBox.Clear();
                    return;
                }
                customer.Contact = contactTextBox.Text;

                if (String.IsNullOrEmpty(loyalityPointTextBox.Text))
                {
                    customer.LoyalityPoint = 0;
                }
                else if (loyalityPointTextBox.Text.StartsWith("-"))
                {
                    MessageBox.Show("Loyality Point can not e Negative !!", "Loyality Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    loyalityPointTextBox.Clear();
                    return;
                }
                else
                {
                    customer.LoyalityPoint = Convert.ToInt32(loyalityPointTextBox.Text);
                }

                if (_customerManager.UpdateCustomer(customer))
                {
                    MessageBox.Show("Customer Updated");
                    saveButton.Text = "Save";
                }
                else
                {
                    MessageBox.Show("Customer Not Updated");
                }

                codeTextBox.Clear();
                nameTextBox.Clear();
                addressTextBox.Clear();
                emailTextBox.Clear();
                contactTextBox.Clear();
                loyalityPointTextBox.Clear();
                showDataGridView.DataSource = _customerManager.ShowAll();
            }
        }

        private void showDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                codeTextBox.Text = showDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                nameTextBox.Text = showDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                addressTextBox.Text = showDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                emailTextBox.Text = showDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                contactTextBox.Text = showDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                loyalityPointTextBox.Text = showDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                saveButton.Text = "Update";
            }
        }

        private void showDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            showDataGridView.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
            showDataGridView.Rows[e.RowIndex].Cells[7].Value = "Edit";
        }

        private void CustomerUI_Load(object sender, EventArgs e)
        {
            showDataGridView.DataSource = _customerManager.ShowAll();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            customer = new CustomerModel();
            customer.Name = searchTextBox.Text;
            customer.Email = searchTextBox.Text;
            customer.Contact = searchTextBox.Text;
            DataTable showData = _customerManager.SearchCustomer(customer);
            if (showData.Rows.Count > 0)
            {
                showDataGridView.DataSource = showData;
            }
            else
            {
                MessageBox.Show("No Data Matched!!!");
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            HomeUI newForm = new HomeUI();
            newForm.Show();
            this.Hide();
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}

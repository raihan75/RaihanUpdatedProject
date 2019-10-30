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

namespace SmallBusinessManagementSystem.UI
{
    public partial class CategoryUI : Form
    {
        public CategoryUI()
        {
            InitializeComponent();
        }

        CategoryManager _categoryManager = new CategoryManager();
        public CategoryModel category;

        private void categorySaveButton_Click(object sender, EventArgs e)
        {
            if (categorySaveButton.Text == "Save")
            {
                category = new CategoryModel();
                if (String.IsNullOrEmpty(categoryCodeTextBox.Text))
                {
                    MessageBox.Show("Code can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (categoryCodeTextBox.Text.Count() != 4)
                {
                    MessageBox.Show("Code Must be 4 length !!", "Code Length Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    categoryCodeTextBox.Clear();
                    return;
                }
                category.Code = categoryCodeTextBox.Text;
                if (String.IsNullOrEmpty(categoryNameTextBox.Text))
                {
                    MessageBox.Show("Name can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                category.Name =  categoryNameTextBox.Text;
                if (_categoryManager.IsExists(category))
                {
                    MessageBox.Show("Code or Name already exists !!", "Exist Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool isExecute = _categoryManager.Save(category);
                if (isExecute)
                {
                    MessageBox.Show("Saved!!");
                }
                else
                {
                    MessageBox.Show("Not Saved!!");
                }
                categoryCodeTextBox.Clear();
                categoryNameTextBox.Clear();
                showCategoryDataGridView.DataSource = _categoryManager.ShowAll();
            }
            else if (categorySaveButton.Text == "Update")
            {
                category = new CategoryModel();
                category.Code = categoryCodeTextBox.Text;
                if (String.IsNullOrEmpty(categoryNameTextBox.Text))
                {
                    MessageBox.Show("Name can not be empty !!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                category.Name = categoryNameTextBox.Text;
                if (_categoryManager.UpdateCategory(category))
                {
                    MessageBox.Show("Category Updated");
                }
                else
                {
                    MessageBox.Show("Category Not Updated");                    
                }
                categoryCodeTextBox.Clear();
                categoryNameTextBox.Clear();
                categorySaveButton.Text = "Save";
                showCategoryDataGridView.DataSource = _categoryManager.ShowAll();
            }
        }

        private void showCategoryDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            showCategoryDataGridView.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
            showCategoryDataGridView.Rows[e.RowIndex].Cells[3].Value = "Edit";
        }

        private void CategoryUI_Load(object sender, EventArgs e)
        {
            showCategoryDataGridView.DataSource = _categoryManager.ShowAll();
        }

        private void categorySearchButton_Click(object sender, EventArgs e)
        {
            category = new CategoryModel();
            category.Code = categorySearchTextBox.Text;
            category.Name = categorySearchTextBox.Text;
            DataTable showData = _categoryManager.SearchCategory(category);

            if (showData.Rows.Count > 0)
            {
                showCategoryDataGridView.DataSource = showData;
            }
            else
            {
                MessageBox.Show("No Data Matched!!!");
            }
        }

        private void showCategoryDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                categoryCodeTextBox.Text = showCategoryDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                categoryNameTextBox.Text = showCategoryDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                categorySaveButton.Text = "Update";
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            HomeUI newForm = new HomeUI();
            newForm.Show();
            this.Hide();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace StoreManage
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            DisplayPro();
        }
       
        public void DisplayPro()
        {
            using ( AccessoriesEntitiesProduct _entity = new AccessoriesEntitiesProduct())
            {
                /*List<Product> _productlist = new List<Product>();
                _productlist = _entity.Products.Select(x => new Product
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Provider = x.Provider,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList();
                dataGridView.DataSource = _productlist;*/
                dataGridView.DataSource = _entity.Products.ToList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Product pro = new Product();
            pro.ProductName = txtProName.Text;
            pro.Provider = txtProvider.Text;
            pro.Price = Convert.ToInt32(txtPrice.Text);
            pro.Quantity = Convert.ToInt32(txtQty.Text);
            bool result = SaveProduct(pro);
            ShowStatus(result, "Save");
        }
        public bool SaveProduct(Product pro)
        {
            bool result = false;
            using(AccessoriesEntitiesProduct _entity = new AccessoriesEntitiesProduct())
            {
                _entity.Products.Add(pro);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public Product SetValues(string Name,string Provider, int Price, int Qty)   
        {
            Product pro = new Product();
            pro.ProductName = Name;
            pro.Provider = Provider;
            pro.Price = Price;
            pro.Quantity = Qty;
            return pro;
        }
        public void ShowStatus(bool result, string Action) // validate the Operation Status and Show the Messages To User  
        {
            if (result)
            {
                if (Action.ToUpper() == "SAVE")
                {
                    MessageBox.Show("Saved Successfully!..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Action.ToUpper() == "UPDATE")
                {
                    MessageBox.Show("Updated Successfully!..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deleted Successfully!..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong!. Please try again!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ClearFields();
            DisplayPro();
        }
        public void ClearFields() // Clear the fields after Insert or Update or Delete operation  
        {
            txtProName.Text = "";
            txtProvider.Text = "";
            txtPrice.Text = "";
            txtQty.Value = 0;
        }
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e) //Calling Datagridview cell click to Update and Delete  
        {
            if (dataGridView.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows) // foreach datagridview selected rows values  
                {
                   
                    txtProName.Text = row.Cells[1].Value.ToString();
                    txtProvider.Text = row.Cells[2].Value.ToString();
                    txtPrice.Text = row.Cells[3].Value.ToString();
                    txtQty.Value = Convert.ToInt32(row.Cells[4].Value.ToString());
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product pro = SetValues(txtProName.Text, txtProvider.Text, Convert.ToInt32(txtPrice.Text), Convert.ToInt32(txtQty.Text));
            bool result = UpdateProduct(pro);
            ShowStatus(result, "Update");
        }
        public bool UpdateProduct(Product pro) // UpdateStudentDetails method for update a existing Record  
        {
            bool result = false;
            using (AccessoriesEntitiesProduct _entity = new AccessoriesEntitiesProduct())
            {
                Product _product = new Product();
                _product = _entity.Products.Where(x => x.ProductName == pro.ProductName).Select(x => x).FirstOrDefault();
                _product.ProductName = pro.ProductName;
                _product.Provider = pro.Provider;
                _product.Price = pro.Price;
                _product.Quantity = pro.Quantity;
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool DeleteProduct(Product pro) // UpdateStudentDetails method for update a existing Record  
        {
            bool result = false;
            using (AccessoriesEntitiesProduct _entity = new AccessoriesEntitiesProduct())
            {
                Product _product = new Product();
                _product = _entity.Products.Where(x => x.ProductName == pro.ProductName).Select(x => x).FirstOrDefault();
                _entity.Products.Remove(_product);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Product pro = SetValues(txtProName.Text, txtProvider.Text, Convert.ToInt32(txtPrice.Text), Convert.ToInt32(txtQty.Text));
            bool result = DeleteProduct(pro);
            ShowStatus(result, "Delete");
        }
    }
}

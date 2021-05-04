using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

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
            DisplayCus();
            DisplayStaff();
        }
       
        public void DisplayPro()
        {
            using ( AccessoriesEntities _entity = new AccessoriesEntities())
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
            ClearFields();
            DisplayPro();
        }
        public bool SaveProduct(Product pro)
        {
            bool result = false;
            using(AccessoriesEntities _entity = new AccessoriesEntities())
            {
                _entity.Products.Add(pro);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }
        public Product SetValues(int ID, string Name,string Provider, int Price, int Qty)   
        {
            Product pro = new Product();
            pro.Id = ID;
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
            
        }
        public void ClearFields() // Clear the fields after Insert or Update or Delete operation  
        {
            txtID.Text = "";
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
                    txtID.Text = row.Cells[0].Value.ToString();
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
            Product pro = SetValues(Convert.ToInt32(txtID.Text), txtProName.Text, txtProvider.Text, Convert.ToInt32(txtPrice.Text), Convert.ToInt32(txtQty.Text));
            bool result = UpdateProduct(pro);
            ShowStatus(result, "Update");
            ClearFields();
            DisplayPro();
        }
        public bool UpdateProduct(Product pro) // UpdateStudentDetails method for update a existing Record  
        {
            bool result = false;
            try
            {
                using (AccessoriesEntities _entity = new AccessoriesEntities())
                {

                    Product _product = _entity.Products.Where(x => x.Id == pro.Id).Select(x => x).FirstOrDefault();
                    _product.ProductName = pro.ProductName;
                    _product.Provider = pro.Provider;
                    _product.Price = pro.Price;
                    _product.Quantity = pro.Quantity;
                    _entity.SaveChanges();
                    result = true;
                }
                
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }


            }
            
            return result;
        }
        public bool DeleteProduct(Product pro) // UpdateStudentDetails method for update a existing Record  
        {
            bool result = false;
            using (AccessoriesEntities _entity = new AccessoriesEntities())
            {
                Product _product = new Product();
                _product = _entity.Products.Where(x => x.Id == pro.Id).Select(x => x).FirstOrDefault();
                _entity.Products.Remove(_product);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Product pro = SetValues(Convert.ToInt32(txtID.Text), txtProName.Text, txtProvider.Text, Convert.ToInt32(txtPrice.Text), Convert.ToInt32(txtQty.Text));
            bool result = DeleteProduct(pro);
            ShowStatus(result, "Delete");
            ClearFields();
            DisplayPro();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            DisplayPro();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            DisplayPro();
        }
        public void DisplayCus()
        {
            using (AccessoriesEntities _entityCus = new AccessoriesEntities())
            {

                dataGridViewCus.DataSource = _entityCus.Customers.ToList();
            }
        }
        public void ClearFieldsCus()
        {
            txtCusID.Text = "";
            txtCusname.Text = "";
            txtAddress.Text = "";
            comboGender.Text = "";
            txtPhone.Text = "";
        }
        private void btnAddCus_Click(object sender, EventArgs e)
        {
            Customer cus = new Customer();
            cus.Name = txtCusname.Text;
            cus.Gender = comboGender.SelectedItem.ToString();
            cus.Phone = txtPhone.Text;
            cus.Address = txtAddress.Text;
            bool result = SaveCustomer(cus);
            ShowStatus(result, "Save");
            ClearFieldsCus();
            DisplayCus();
            
        }
        public bool SaveCustomer(Customer cus)
        {
            bool result = false;
            using (AccessoriesEntities _entityCus = new AccessoriesEntities())
            {
                _entityCus.Customers.Add(cus);
                _entityCus.SaveChanges();
                result = true;
            }
            return result;
        }
        public Customer SetValuesCus(int ID, string Name, string Address, string Gender, string Phone)
        {
            Customer cus = new Customer();
            cus.Id = ID;
            cus.Name = Name;
            cus.Address = Address;
            cus.Gender = Gender;
            cus.Phone = Phone;
            return cus;
        }
        private void btnUpdateCus_Click(object sender, EventArgs e)
        {
            Customer cus = SetValuesCus(Convert.ToInt32(txtCusID.Text), txtCusname.Text, txtAddress.Text, comboGender.Text, txtPhone.Text);
            bool result = UpdateCustomer(cus);
            ShowStatus(result, "Update");
            ClearFieldsCus();
            DisplayCus();
        }
        public bool UpdateCustomer(Customer cus)
        {
            bool result = false;
            try
            {

                using (AccessoriesEntities _entityUpdate = new AccessoriesEntities())
                {

                    Customer _customer = _entityUpdate.Customers.Where(x => x.Id == cus.Id).Select(x => x).FirstOrDefault();
                    /*_customer.Id = cus.Id;*/
                    _customer.Name = cus.Name;
                    _customer.Address = cus.Address;
                    _customer.Gender = cus.Gender;
                    _customer.Phone = cus.Phone;
                    _entityUpdate.SaveChanges();
                    result = true;

                }

            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }


            }
            return result;

        }

        private void dataGridViewCus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCus.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCus.SelectedRows)
                {
                    txtCusID.Text = row.Cells[0].Value.ToString();
                    txtCusname.Text = row.Cells[1].Value.ToString();
                    txtAddress.Text = row.Cells[2].Value.ToString();
                    comboGender.Text = row.Cells[3].Value.ToString();
                    txtPhone.Text = row.Cells[4].Value.ToString();

                }
            }
        }

        private void btnDelCus_Click(object sender, EventArgs e)
        {
            Customer cus = SetValuesCus(Convert.ToInt32(txtCusID.Text), txtCusname.Text, txtAddress.Text, comboGender.Text, txtPhone.Text);
            bool result = DeleteCustomer(cus);
            ShowStatus(result, "Delete");
            ClearFieldsCus();
            DisplayCus();
        }
        public bool DeleteCustomer(Customer cus)  
        {
            bool result = false;
            using (AccessoriesEntities _entity = new AccessoriesEntities())
            {
                Customer _customer = new Customer();
                _customer = _entity.Customers.Where(x => x.Id == cus.Id).Select(x => x).FirstOrDefault();
                _entity.Customers.Remove(_customer);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void btnClearCus_Click(object sender, EventArgs e)
        {
            ClearFieldsCus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void DisplayStaff()
        {
            using (AccessoriesEntities _entityStaff = new AccessoriesEntities())
            {

                dataGridViewStaff.DataSource = _entityStaff.Staffs.ToList();
            }
        }
        public void ClearFieldsStaff()
        {
            txtStaffID.Text = "";
            txtStaffName.Text = "";
            txtStaffPhone.Text = "";
            dateTimeStaff.Value = DateTime.Now;
            txtPosition.Text = "";
            txtSalary.Text = "";
        }
        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            Staff sf = new Staff();
            sf.Name = txtStaffName.Text;
            sf.Phone = txtStaffPhone.Text;
            sf.Date_join = dateTimeStaff.Value.Date.ToString();
            sf.Position = txtPosition.Text;
            sf.Salary = Convert.ToInt32(txtSalary.Text);
            bool result = SaveStaff(sf);
            ShowStatus(result, "Save");
            ClearFieldsStaff();
            DisplayStaff();
        }
        public bool SaveStaff(Staff sf)
        {
            bool result = false;
            using (AccessoriesEntities _entityStaff = new AccessoriesEntities())
            {
                _entityStaff.Staffs.Add(sf);
                _entityStaff.SaveChanges();
                result = true;
            }
            return result;
        }
        public Staff SetValuesStaff(int ID, string Name, string Phone, string Date_join, string Position, int Salary)
        {
            Staff sf =new Staff();
            sf.Id = ID;
            sf.Name = Name;
            sf.Phone = Phone;
            sf.Date_join = Date_join;
            sf.Position = Position;
            sf.Salary = Salary;
            return sf;
        }

        private void dataGridViewStaff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewStaff.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewStaff.SelectedRows)
                {
                    txtStaffID.Text = row.Cells[0].Value.ToString();
                    txtStaffName.Text = row.Cells[1].Value.ToString();
                    txtStaffPhone.Text = row.Cells[2].Value.ToString();
                    dateTimeStaff.Value = DateTime.Parse(row.Cells[3].Value.ToString());
                    txtPosition.Text = row.Cells[4].Value.ToString();
                    txtSalary.Text = row.Cells[5].Value.ToString();

                }
            }
        }

        private void btnUpdateStaff_Click(object sender, EventArgs e)
        {
            Staff sf = SetValuesStaff(Convert.ToInt32(txtStaffID.Text), txtStaffName.Text, txtStaffPhone.Text, dateTimeStaff.Value.Date.ToString(), txtPosition.Text, Convert.ToInt32(txtSalary.Text));
            bool result = UpdateStaff(sf);
            ShowStatus(result, "Update");
            ClearFieldsStaff();
            DisplayStaff();
        }
        public bool UpdateStaff(Staff sf)
        {
            bool result = false;
            try
            {

                using (AccessoriesEntities _entityStaff = new AccessoriesEntities())
                {

                    Staff _staff = _entityStaff.Staffs.Where(x => x.Id == sf.Id).Select(x => x).FirstOrDefault();
                    /*_customer.Id = cus.Id;*/
                    _staff.Name = sf.Name;
                    _staff.Phone = sf.Phone;
                    _staff.Date_join = sf.Date_join;
                    _staff.Position = sf.Position;
                    _staff.Salary = sf.Salary;
                    _entityStaff.SaveChanges();
                    result = true;

                }

            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }


            }
            return result;

        }

        private void btnDelStaff_Click(object sender, EventArgs e)
        {
            Staff sf = SetValuesStaff(Convert.ToInt32(txtStaffID.Text), txtStaffName.Text, txtStaffPhone.Text, dateTimeStaff.Value.Date.ToString(), txtPosition.Text, Convert.ToInt32(txtSalary.Text));
            bool result = DeleteStaff(sf);
            ShowStatus(result, "Delete");
            ClearFieldsStaff();
            DisplayStaff();
        }
        public bool DeleteStaff(Staff sf)
        {
            bool result = false;
            using (AccessoriesEntities _entityStaff = new AccessoriesEntities())
            {
                Staff _staff = new Staff();
                _staff = _entityStaff.Staffs.Where(x => x.Id == sf.Id).Select(x => x).FirstOrDefault();
                _entityStaff.Staffs.Remove(_staff);
                _entityStaff.SaveChanges();
                result = true;
            }
            return result;
        }

        private void btnClearStaff_Click(object sender, EventArgs e)
        {
            ClearFieldsStaff();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtSearchOrder_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source = DESKTOP-7K1R4E8; Initial Catalog = Accessories; Integrated Security = true");
            conn.Open();
            /*MessageBox.Show("Connected!");*/
            SqlCommand cmd = new SqlCommand("select * from Product where productName like'%" + txtSearchOrder.Text + "%' or provider like '%" + txtSearchOrder.Text + "%'", conn);
            /*SqlDataReader dataReader;*//*
            cmd.CommandType = CommandType.Text;
            *//*cmd.CommandText = "select * from Users where email=" + txtEmail.Text;*//*
            SqlDataReader dataReader = cmd.ExecuteReader();*/
            SqlDataAdapter sdr = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            dataGridViewOrder.DataSource = dt;
            conn.Close();
        }

        private void dataGridViewOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewOrder.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewOrder.SelectedRows)
                {
                    txtProNameOrder.Text = row.Cells[1].Value.ToString();
                    txtProviderOrder.Text = row.Cells[2].Value.ToString();
                    txtInStock.Text = row.Cells[4].Value.ToString();

                }
            }
        }
    }
}

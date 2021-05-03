using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace StoreManage
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            DisplayCus();
        }
        public void DisplayCus()
        {
            using (AccessoriesEntities _entityCus = new AccessoriesEntities())
            {

                dataGridViewCus.DataSource = _entityCus.Customers.ToList();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Customer cus = new Customer();
            cus.Name = txtName.Text;
            cus.Gender = comboGender.SelectedItem.ToString();
            cus.Phone = txtPhone.Text;
            cus.Address = txtAddress.Text;
            bool result = SaveCustomer(cus);
            ShowStatus(result, "Save");
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
        public Customer SetValues(int ID, string Name, string Address, string Gender, string Phone)
        {
            Customer cus = new Customer();
            cus.Id = ID;
            cus.Name = Name;
            cus.Address = Address;
            cus.Gender = Gender;
            cus.Phone = Phone;
            return cus;
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
            DisplayCus();
        }
        public void ClearFields()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            comboGender.Text = "";
            txtPhone.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridViewCus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCus.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewCus.SelectedRows)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    txtAddress.Text = row.Cells[2].Value.ToString();
                    comboGender.Text = row.Cells[3].Value.ToString();
                    txtPhone.Text = row.Cells[4].Value.ToString();

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            Customer cus = SetValues(Convert.ToInt32(txtID.Text), txtName.Text, txtAddress.Text, comboGender.Text, txtPhone.Text);
            bool result = UpdateCustomer(cus);
            ShowStatus(result, "Update");
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
        public bool DeleteCustomer(Customer cus) // UpdateStudentDetails method for update a existing Record  
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
        private void btnDel_Click(object sender, EventArgs e)
        {
            Customer cus = SetValues(Convert.ToInt32(txtID.Text), txtName.Text, txtAddress.Text, comboGender.Text, txtPhone.Text);
            bool result = DeleteCustomer(cus);
            ShowStatus(result, "Delete");
        }
    }
}
